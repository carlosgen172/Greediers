using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    string currentAnimation;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
 
    void Update()
    {
        //Metodo de prueba para la funcion ChangeAnimation, ésto será usado con funciones como isJumping, isRuning...
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeAnimation("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeAnimation("Run");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeAnimation("Idle");
        }
    }

    public void ChangeAnimation(string animationName)
    {
        if (currentAnimation == animationName) return;

        currentAnimation = animationName;
        animator.Play(currentAnimation);
    }
}
