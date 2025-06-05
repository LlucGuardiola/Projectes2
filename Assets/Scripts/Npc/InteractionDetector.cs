using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{

    public IInteractable interactableInRange = null;
    public GameObject Interactionicon;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Interactionicon.SetActive(false);
    }


    public void OnInteract (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.Interact();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract ())
        {
            Debug.Log("SI");
            interactableInRange = interactable;
            Interactionicon.SetActive (true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            Interactionicon.SetActive(false);
        }
    }
}
