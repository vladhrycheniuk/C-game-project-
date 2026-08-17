using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Сюди перетягнемо гравця (Player)
    public Vector3 offset = new Vector3(-10, 10, -10); // Відступ камери від гравця
    public float smoothSpeed = 5f; // Наскільки плавно камера слідує

    void LateUpdate()
    {
        if (target != null)
        {
            // Вираховуємо, де має бути камера
            Vector3 desiredPosition = target.position + offset;
            
            // Плавно переміщуємо камеру до цієї точки
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Камера завжди дивиться на гравця
            transform.LookAt(target.position);
        }
    }
}