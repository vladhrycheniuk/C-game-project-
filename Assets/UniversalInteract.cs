using UnityEngine;
using TMPro;
using System.Collections;

public enum ActionType { Light, Book }

public class UniversalInteract : MonoBehaviour
{
    public ActionType type;
    public GameObject interactionUI; // Напис "Натисніть E"

    [Header("Налаштування Книжки")]
    public GameObject bookPanel; 
    public TextMeshProUGUI leftPageTextUI;  
    public TextMeshProUGUI rightPageTextUI; 

    [TextArea(10, 20)] public string leftPageContent;  
    [TextArea(10, 20)] public string rightPageContent;
    
    [Range(0.1f, 2f)] public float appearanceSpeed = 0.5f; // Швидкість появи тексту

    [Header("Налаштування Світла")]
    public Light lampLight;
    public AudioSource switchSound;

    private bool isPlayerNearby = false;
    private Coroutine appearanceCoroutine;

    void Update()
    {
        // 1. ЛОГІКА ЗАКРИТТЯ КНИЖКИ
        if (type == ActionType.Book && bookPanel != null && bookPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseBook();
            }
            return; 
        }

        // 2. ЛОГІКА ВЗАЄМОДІЇ
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (type == ActionType.Light && lampLight != null)
            {
                ToggleLight();
            }
            else if (type == ActionType.Book && bookPanel != null)
            {
                OpenBook();
            }
        }
    }

    void ToggleLight()
    {
        lampLight.enabled = !lampLight.enabled;
        if (switchSound != null) switchSound.Play();
    }

    void OpenBook()
    {
        bookPanel.SetActive(true);
        if (interactionUI != null) interactionUI.SetActive(false);

        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Запуск магічної появи
        if (appearanceCoroutine != null) StopCoroutine(appearanceCoroutine);
        appearanceCoroutine = StartCoroutine(ShowTextWithEffect());
    }

    IEnumerator ShowTextWithEffect()
    {
        // Встановлюємо текст і робимо його прозорим
        leftPageTextUI.text = leftPageContent;
        rightPageTextUI.text = rightPageContent;
        
        leftPageTextUI.alpha = 0f;
        rightPageTextUI.alpha = 0f;

        float currentAlpha = 0f;

        // Поступово проявляємо текст
        // Використовуємо Time.unscaledDeltaTime, бо Time.timeScale = 0
        while (currentAlpha < 1f)
        {
            currentAlpha += Time.unscaledDeltaTime * appearanceSpeed;
            leftPageTextUI.alpha = currentAlpha;
            rightPageTextUI.alpha = currentAlpha;
            yield return null; 
        }

        leftPageTextUI.alpha = 1f;
        rightPageTextUI.alpha = 1f;
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        
        if (isPlayerNearby && interactionUI != null) interactionUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionUI != null && (bookPanel == null || !bookPanel.activeSelf)) 
            {
                interactionUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionUI != null) interactionUI.SetActive(false);
        }
    }
}