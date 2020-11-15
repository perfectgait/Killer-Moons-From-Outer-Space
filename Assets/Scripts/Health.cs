using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";

    IFrames iframes;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        iframes = GetComponent<IFrames>();
        audioManager = AudioManager.instance;
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
        else
        {
            audioManager.PlaySoundEffect("Damage Hit");
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Kill()
    {
        Destroy(gameObject);
        Explode();
    }

    private void Explode()
    {
        if (explosionPrefab)
        {
            audioManager.PlaySoundEffect(explosionSfxName);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 1f);
        }
    }
}
