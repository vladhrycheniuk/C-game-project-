using UnityEngine;
using System.Collections;

public class MirrorScript : MonoBehaviour
{
    [Header("Налаштування камер")]
    public GameObject mirrorCamera; 
    public GameObject mainCamera;   

    [Header("Ефекти")]
    public Animator fadeAnimator;   
    
    [Header("Гравець")]
    public PlayerMovement movementScript; 
    
    private bool isLooking = false;
    private bool canInteract = false;

    void Update()
    {
        // Якщо ми ВЖЕ дивимось в дзеркало
        if (isLooking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(SwitchMirror());
            }
            return; 
        }

        // Якщо ми НЕ дивимось і ми в зоні
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SwitchMirror());
        }
    }

    IEnumerator SwitchMirror()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("StartFade");
        }

        yield return new WaitForSeconds(0.5f);

        isLooking = !isLooking;
        
        if (mirrorCamera != null) mirrorCamera.SetActive(isLooking);
        if (mainCamera != null) mainCamera.SetActive(!isLooking);
        
        if (movementScript != null) 
        {
            movementScript.enabled = !isLooking;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canInteract = false;
        }
    }
}