using UnityEngine;

public class Door : ItemBase
{
    [SerializeField] private Sprite doorClosed;
    [SerializeField] private Sprite doorOpened;
    [SerializeField] private GameObject collision;

    private SpriteRenderer spriteRenderer;

    private bool isOpen;

    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOpen = false;
    }

    private void OnGrab()
    {
        if (CanGrab()) { Grab(); }
    }

    public override void Grab()
    {
        if (!isOpen)
        {
            spriteRenderer.sprite = doorOpened;
            isOpen = true;
            collision.gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = doorClosed;
            isOpen = false;
            collision.gameObject.SetActive(true);
        }
    }
}
