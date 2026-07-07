using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    string currentAnimation;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }
 
    void Update()
    {
        
    }

    public void ChangeAnimation(string animationName)
    {
        if (currentAnimation == animationName) return;

        currentAnimation = animationName;
        animator.Play(currentAnimation);
    }
}
