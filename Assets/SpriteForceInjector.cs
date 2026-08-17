using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // Магія! Це спрацює навіть БЕЗ запуску гри
public class SpriteForceInjector : MonoBehaviour
{
    public Image targetImage; // Сюди перетягни BookPanel
    public Sprite spriteToInject; // Сюди перетягни картинку книжки

    public bool injectNow = false; // Кнопка для силової ін'єкції

    void Update()
    {
        if (injectNow)
        {
            if (targetImage != null && spriteToInject != null)
            {
                targetImage.sprite = spriteToInject;
                // Налаштовуємо картинку, щоб вона була білою і непрозорою
                targetImage.color = Color.white;
                Debug.Log("СИЛОВА ІН'ЄКЦІЯ СПРАЙТУ ВИКОНАНА!");
            }
            else
            {
                Debug.LogError("ПОМИЛКА: Немає панелі або спрайту!");
            }
            injectNow = false; // Вимикаємо кнопку
        }
    }
}