using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        var state = animator.GetCurrentAnimatorStateInfo(layerIndex: 0);
        animator.Play(state.fullPathHash, layer: 0, normalizedTime: Random.Range(0f, 1f));
    }
}
