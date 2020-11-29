using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : DamageTaker
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";

    private Health health;
    IFrames iframes;
    AudioManager audioManager;
    SpriteFlasher spriteFlasher;
    LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        iframes = GetComponent<IFrames>();
        audioManager = AudioManager.instance;
        spriteFlasher = GetComponent<SpriteFlasher>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public override void TakeDamage(float damage)
    {
        if (iframes && iframes.IsInvulnerable())
        {
            return;
        }

        if (health)
        {
            health.Damage(damage);
        }

        if (health.GetHealth() > 0)
        {
            Damage();

            if (iframes)
            {
                iframes.Damage();
            }
        }
        else
        {
            Kill();
        }
    }

    public override void Kill()
    {
        StartCoroutine(KillPlayer())
;
    }

    private void Damage()
    {
        if (audioManager)
        {
            audioManager.PlaySoundEffect("Damage Hit");
        }

        if (spriteFlasher)
        {
            spriteFlasher.Flash();
        }
    }

    private IEnumerator KillPlayer()
    {
        // I couldn't simply call Destroy on the gameobject, because apparently that also cancels
        // any coroutines running on that gameobject
        // So instead, I simply disable or destroy components on the Player
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Canvas>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Player>().SetCanFire(false);
        GetComponent<Player>().StopFiring();
        Destroy(GetComponent<BulletEmitter>());
        Explode();

        // Playing game over music before the scene loads so that the ambient sounds can play first
        if (audioManager)
        {
            audioManager.PlayMusic("Game Over");
        }

        yield return new WaitForSeconds(3.9f);

        levelLoader.LoadLoseScreen();
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
