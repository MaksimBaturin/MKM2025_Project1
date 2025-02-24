using UnityEngine;

public class LeftThrustAnimator : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isThrust", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isThrust", true);
        }
    }
}
