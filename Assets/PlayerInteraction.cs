using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // На якій відстані можна взаємодіяти

    void Update()
    {
        // Якщо натиснули клавішу E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

        void Interact()
    {
        // Створюємо сферу навколо гравця і знаходимо всі колайдери всередині
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
    
        foreach (var hitCollider in hitColliders)
        {
        // Шукаємо скрипт взаємодії на об'єктах навколо
            InteractableObject obj = hitCollider.GetComponent<InteractableObject>();

            if (obj != null)
            {
                obj.OnInteract();
                break; // Зупиняємося після першого знайденого предмета
            }
        }
    }
}