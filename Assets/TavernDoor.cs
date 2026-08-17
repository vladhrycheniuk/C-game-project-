using UnityEngine;

public class TavernDoor : MonoBehaviour
{
    public float openAngle = 90f; 
    public float smooth = 3f;
    
    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private int contactCount = 0;

    void Start()
    {
        closedRotation = transform.localRotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        // Плавно крутимо до цілі
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cat"))
        {
            contactCount++;
            
            // Обчислюємо напрямок: чи перед дверима об'єкт, чи за ними
            Vector3 direction = other.transform.position - transform.position;
            float dot = Vector3.Dot(transform.forward, direction);

            // Якщо dot > 0 — об'єкт попереду, відкриваємо назад (-90)
            // Якщо dot < 0 — об'єкт позаду, відкриваємо вперед (90)
            float angle = dot > 0 ? -openAngle : openAngle;
            
            targetRotation = closedRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Cat"))
        {
            contactCount--;
            if (contactCount <= 0)
            {
                contactCount = 0;
                targetRotation = closedRotation; // Закриваємо
            }
        }
    }
}