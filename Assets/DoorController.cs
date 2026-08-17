using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float smooth = 2f;

    private Quaternion targetRotation;
    private Quaternion closedRotation;
    private Transform playerTransform; // Сюди ми знайдемо гравця автоматично

    void Start()
    {
        closedRotation = transform.rotation;
        
        // Автоматично шукаємо об'єкт з тегом Player, щоб не було помилок
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // Плавно повертаємо двері
        Quaternion target = isOpen ? targetRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    public void ToggleDoor()
    {
        // Перестраховка: якщо раптом гравця не знайшли при старті, шукаємо ще раз
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        isOpen = !isOpen;

        if (isOpen && playerTransform != null)
        {
            // Рахуємо сторону відносно гравця
            Vector3 relativePos = transform.InverseTransformPoint(playerTransform.position);
            float angle = (relativePos.z < 0) ? openAngle : -openAngle;
            
            targetRotation = Quaternion.Euler(0, angle, 0) * closedRotation;
        }
        
        Debug.Log(isOpen ? "Двері відчиняються" : "Двері зачиняються");
    }
}