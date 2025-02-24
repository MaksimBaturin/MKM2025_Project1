using UnityEngine;

public class MainThrusterAnimator : MonoBehaviour
{
    private Animator animator;
    private ThrustController thrustController;
    void Start()
    {
        animator = GetComponent<Animator>();
        thrustController = GameObject.FindAnyObjectByType<ThrustController>();
    }
    void Update()
    {
        animator.SetFloat("Thrust", thrustController.CurrentThrust);
    }
}
