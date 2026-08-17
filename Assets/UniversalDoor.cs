using UnityEngine;

public class UniversalDoor : MonoBehaviour
{
    public Transform doorPivot; 
    public float openAngle = 90f; 
    public float speed = 5f;
    
    private bool isOpen = false;
    private bool isPlayerNearby = false;
    
    private Quaternion startRotation; // Початковий стан
    private Quaternion targetQuat;    // Куди хочемо повернути

    void Start()
    {
        if (doorPivot != null)
        {
            // Запам'ятовуємо, як двері стоять спочатку
            startRotation = doorPivot.localRotation;
            targetQuat = startRotation;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                // Визначаємо сторону (від гравця)
                float side = IsPlayerInFront() ? 1f : -1f;
                
                // Створюємо поворот ВІДНОСНО початкового стану
                // Ми множимо на Quaternion.Euler, щоб додати поворот до поточного
                targetQuat = startRotation * Quaternion.Euler(0, openAngle * side, 0);
            }
            else
            {
                targetQuat = startRotation; // Повертаємо в початковий стан
            }
        }

        // Плавно крутимо
        if (doorPivot != null)
        {
            doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetQuat, Time.deltaTime * speed);
        }
    }

    // Перевірка, з якого боку гравець (універсальна через локальні координати)
    private bool IsPlayerInFront()
    {
        Vector3 directionToPlayer = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        return directionToPlayer.z > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = false;
    }
}