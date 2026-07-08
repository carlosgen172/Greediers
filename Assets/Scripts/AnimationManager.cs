using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;
    string currentAnimation;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeAnimation(string animationName, Animator animatorJugador)
    {
        if (currentAnimation == animationName) return;

        currentAnimation = animationName;
        animatorJugador.Play(animationName, 0, 0f);
    }
}
