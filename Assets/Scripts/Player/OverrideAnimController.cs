using UnityEngine;

public class OverrideAnimController : MonoBehaviour
{
    private GameObject player;
    public AnimationClip newIdleClip;
    public AnimationClip newRunClip;
    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerInventory.PlayerHasSword)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            overrideController["PlayerIdle"] = newIdleClip;
            overrideController["PlayerRun"] = newRunClip;

            animator.runtimeAnimatorController = overrideController;
            Debug.Log("Player");
            Destroy(gameObject);
        }
    }
}
