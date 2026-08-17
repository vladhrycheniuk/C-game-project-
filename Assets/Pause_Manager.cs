using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Сюди перетягнеш свою панель паузи
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Кнопка P
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Зупиняємо світ
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Запускаємо світ
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}