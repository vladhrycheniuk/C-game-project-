using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Папка зі стінами ЦІЄЇ кімнати")]
    public GameObject myRoomGroup; 

    [Header("Папка зі стінами СУСІДНЬОЇ кімнати")]
    public GameObject otherRoomGroup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Розмиваємо стіни цієї кімнати
            FadeGroup(myRoomGroup, true); 

            // Проявляємо стіни іншої кімнати
            FadeGroup(otherRoomGroup, false);
        }
    }

    // Маленька функція, яка шукає WallFader у всій папці
    void FadeGroup(GameObject group, bool fadeOut)
    {
        if (group == null) return;

        // Шукаємо всі скрипти WallFader на всіх об'єктах всередині папки
        WallFader[] faders = group.GetComponentsInChildren<WallFader>();

        foreach (WallFader fader in faders)
        {
            if (fadeOut)
                fader.FadeOut(); // Заміни на назву свого методу для розмиття
            else
                fader.FadeIn();  // Заміни на назву свого методу для появи
        }
    }
}