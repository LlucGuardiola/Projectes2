using UnityEngine;

public class OverrideAnimController : MonoBehaviour
{
    private GameObject player;
    public AnimationClip newIdleClip;
    public AnimationClip newRunClip;
    public AnimationClip newOnAirClip;
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
            overrideController["OnAir"] = newOnAirClip;

            animator.runtimeAnimatorController = overrideController;
            Destroy(gameObject);
        }
    }
}
