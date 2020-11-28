using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringIndicator : MonoBehaviour
{
    [SerializeField] string firingIndicatorAnimationBool;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartIndicator()
    {
        if (!animator)
        {
            return;
        }

        animator.SetBool(firingIndicatorAnimationBool, true);
    }

    public void StopIndicator()
    {
        if (!animator)
        {
            return;
        }

        animator.SetBool(firingIndicatorAnimationBool, false);
    }

    public bool IsIndicating()
    {
        if (!animator)
        {
            return false;
        }

        return animator.GetBool(firingIndicatorAnimationBool);
    }
}
