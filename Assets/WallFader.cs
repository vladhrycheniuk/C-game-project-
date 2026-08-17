using UnityEngine;

public class WallFader : MonoBehaviour
{
    [Range(0f, 1f)]
    public float targetAlpha = 0.3f; // Наскільки прозорою буде стіна (0.3 - оптимально)
    public float fadeSpeed = 5f;

    private Material mat;
    private float currentAlpha = 1f;
    private float targetValue = 1f;

    void Start()
    {
        // Отримуємо матеріал. Використовуємо Renderer.material, щоб створити копію для кожної стіни
        if (GetComponent<Renderer>() != null)
        {
            mat = GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        if (mat == null) return;

        // Плавно наближаємося до цілі
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetValue, fadeSpeed * Time.deltaTime);
        
        // Оновлюємо колір матеріалу
        Color color = mat.color;
        color.a = currentAlpha;
        mat.color = color;
    }

    public void FadeOut() => targetValue = targetAlpha;
    public void FadeIn() => targetValue = 1f;
}