using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaker : DamageTaker
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";

    private Health health;
    AudioManager audioManager;
    SpriteFlasher spriteFlasher;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        audioManager = AudioManager.instance;
        spriteFlasher = GetComponent<SpriteFlasher>();
    }

    public override void TakeDamage(float damage)
    {
        if (health)
        {
            health.Damage(damage);
        }

        if (health.GetHealth() > 0)
        {
            Damage();
        }
        else
        {
            Kill();
        }
    }

    private void Damage()
    {
        audioManager.PlaySoundEffect("Damage Hit");
        spriteFlasher.Flash();
    }

    private void Kill()
    {
        // Hack: Set delay on destroy so that the Update method in HealthDisplay has a chance to register
        // the final health change before the gameObject is destroyed. Health should probably be updating HealthDisplay
        // but this is fine for now because it is late.
        Destroy(gameObject, .01f);
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
