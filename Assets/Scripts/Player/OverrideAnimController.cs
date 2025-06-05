using UnityEngine;

public class OverrideAnimController : MonoBehaviour
{
    private GameObject player;
    public AnimationClip newIdleClip;
    public AnimationClip newRunClip;
    public AnimationClip PlayerAttack1;
    public AnimationClip PlayerAttack2;
    public AnimationClip newOnAirClip;
    private Animator animator;
    AnimatorOverrideController overrideController;

    private bool initialAnimsChanged;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
        initialAnimsChanged = false;
    }

    private void Update()
    {
        if (initialAnimsChanged) return;

        if (PlayerInventory.PlayerHasSword)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            overrideController["PlayerIdle"] = newIdleClip;
            overrideController["PlayerRun"] = newRunClip;
            overrideController["OnAir"] = newOnAirClip;

            animator.runtimeAnimatorController = overrideController;
            initialAnimsChanged = true; 
        }
    }

    public void SwichAttackAnim(float idx)
    {
        switch (idx)
        {
            case 1:
                overrideController["PlayerAttack"] = PlayerAttack1;
                break;
            default:
                overrideController["PlayerAttack"] = PlayerAttack2;
                break;
        }
    }
}
