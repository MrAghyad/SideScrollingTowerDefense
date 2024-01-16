using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    [SerializeField]
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationState(int state)
    {
        animator.SetInteger("anim_state", state);
    }
}
