using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchAnimateProjectile : Projectile
{
    [SerializeField] string onTriggerEnter2DBoolName;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        speed = 0;

        if (animator && onTriggerEnter2DBoolName != null)
        {
            animator.SetBool(onTriggerEnter2DBoolName, true);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
