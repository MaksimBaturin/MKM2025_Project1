using UnityEngine;

public class RightThrustAnimator : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isThrust", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isThrust", true);
        }
    }
}
