using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [Header("Папки, які треба розмити (можна кілька)")]
    public GameObject[] foldersToFade; 

    // OnTriggerStay працює надійніше за Enter, бо перевіряє стан щосекунди
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetFadersState(true); // true = розмити (FadeOut)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetFadersState(false); // false = повернути (FadeIn)
        }
    }

    private void SetFadersState(bool shouldFade)
    {
        foreach (GameObject folder in foldersToFade)
        {
            if (folder == null) continue;

            WallFader[] faders = folder.GetComponentsInChildren<WallFader>(true);
            foreach (WallFader fader in faders)
            {
                if (shouldFade)
                    fader.FadeOut();
                else
                    fader.FadeIn();
            }
        }
    }
}