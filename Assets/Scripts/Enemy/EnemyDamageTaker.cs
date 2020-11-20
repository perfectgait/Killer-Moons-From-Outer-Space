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
