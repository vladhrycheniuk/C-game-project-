using UnityEngine;

public class RoomWallsGroup : MonoBehaviour
{
    private WallFader[] childFaders;

    void Start()
    {
        // Знаходимо всі скрипти WallFader, які є на стінах всередині цієї папки
        childFaders = GetComponentsInChildren<WallFader>();
    }

    public void FadeOutAll()
    {
        foreach (var fader in childFaders)
        {
            fader.FadeOut(); // Викликаємо твій метод розмиття
        }
    }

    public void FadeInAll()
    {
        foreach (var fader in childFaders)
        {
            fader.FadeIn(); // Викликаємо твій метод появи
        }
    }
}