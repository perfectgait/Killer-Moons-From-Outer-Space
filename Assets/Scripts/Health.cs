using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;

    IFrames iframes;

    // Start is called before the first frame update
    void Start()
    {
        iframes = GetComponent<IFrames>();
    }

    public void Damage(float amount)
    {
        if (iframes && iframes.IsInvulnerable())
        {
            return;
        }

        health -= amount;

        // Ensure health doesn't drop below zero
        health = Mathf.Max(0, health);

        if (iframes)
        {
            iframes.Damage();
        }

        if (health <= 0)
        {
            Kill();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
