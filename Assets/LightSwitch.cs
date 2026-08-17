using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light lampLight;
    public AudioSource switchSound; // Сюди перетягни AudioSource
    private bool isPlayerNearby = false;

    void Start()
    {
        // Робимо лампу вимкненою при запуску гри
        if (lampLight != null) lampLight.enabled = false;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        lampLight.enabled = !lampLight.enabled;
        
        // Граємо звук, якщо він призначений
        if (switchSound != null)
        {
            switchSound.Play();
        }
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