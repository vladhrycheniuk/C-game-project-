using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName = "Предмет";
    [TextArea] // Це зробить поле для тексту в інспекторі більшим і зручнішим
    public string interactionMessage = "Ви взаємодієте з цим.";
    
    public bool destroyOnInteract = false; // Чи має предмет зникати? (для кота поставимо false)
    
    private AudioSource audioSource;

    void Start()
    {
        // Шукаємо компонент AudioSource на цьому ж об'єкті
        audioSource = GetComponent<AudioSource>();
    }

    public void OnInteract()
    {
        Debug.Log(itemName + ": " + interactionMessage);
        
        // Якщо на об'єкті є звук — граємо його
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Якщо галочка стоїть — предмет зникне, якщо ні — залишиться
        if (destroyOnInteract)
        {
            gameObject.SetActive(false);
        }

        // Шукаємо скрипт дверей на самому об'єкті АБО на його "батьку" (петлі)
        DoorController door = GetComponentInParent<DoorController>();
    
        if (door != null)
        {
            door.ToggleDoor();
        }

        Debug.Log(interactionMessage);
        }
}