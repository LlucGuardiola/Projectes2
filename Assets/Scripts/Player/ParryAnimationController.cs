using UnityEngine;

public class ParryAnimationController : MonoBehaviour
{
    private static SpriteRenderer spriteRenderer;
    private static Animator animator;
    private static bool isParrying;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isParrying)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    public static void TriggerParryAnimation()
    {
        isParrying = true;
        animator.SetTrigger("parry");
    }
    public static void EndParryAnimation()
    {
        isParrying = false;
    }
}
