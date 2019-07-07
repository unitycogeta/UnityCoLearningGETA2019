using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;


    int[] animationSwitch;
    Animation[] myAnimations;
    int animationCount;
    string lastAnimationState;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animationSwitch = new int[animationCount];
        lastAnimationState = "Idle";
    }

    private int AnimationCount(Animator myAnimator)
    {
        int myAnimationCount = 0;
        foreach (AnimationClip ac in myAnimator.runtimeAnimatorController.animationClips)
        {
            myAnimationCount++;
        }
        return myAnimationCount;
    }

    
    public void PlayAnimation(string myAnimationState)
    {
        //if (myAnimationState != lastAnimationState)
        //{
            animator.SetBool(lastAnimationState, false);
            animator.SetBool(myAnimationState, true);
            lastAnimationState = myAnimationState;
        //}
    }
}
