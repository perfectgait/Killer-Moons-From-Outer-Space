using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : DamageTaker
{
    [SerializeField] int numberOfFlashes = 2;
    [SerializeField] float durationBetweenFlashes = 0.05f;
    [SerializeField] GameObject objectToFlash;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";

    private SpriteRenderer spriteRenderer;
    private IEnumerator flashingCoroutine;
    private Health health;
    IFrames iframes;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = objectToFlash.GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        iframes = GetComponent<IFrames>();
        audioManager = AudioManager.instance;
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
            StartCoroutine(Kill());
        }
    }

    private void Damage()
    {
        audioManager.PlaySoundEffect("Damage Hit");

        if (spriteRenderer)
        {
            if (flashingCoroutine != null)
            {
                StopCoroutine(flashingCoroutine);
            }

            flashingCoroutine = Flash();
            StartCoroutine(flashingCoroutine);
        }
    }

    private IEnumerator Flash()
    {
        bool makeSpriteWhite = true;

        // Iterate twice the length of times - that way numberOfFlashes is "how many times is it turned on", not "how many times does it flip".
        for (int i = 0; i < numberOfFlashes * 2; i++)
        {
            spriteRenderer.material.SetFloat("_FlashAmount", makeSpriteWhite ? 1f : 0f);
            makeSpriteWhite = !makeSpriteWhite;
            yield return new WaitForSeconds(durationBetweenFlashes);
        }
    }

    private IEnumerator Kill()
    {
        // I couldn't simply call Destroy because apparently that also cancels
        // any coroutines running on that gameobject
        // So instead, I simply disable the sprite renderer for the player
        spriteRenderer.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BulletEmitter>().enabled = false;
        Explode();

        // Playing game over music before the scene loads so that the ambient sounds can play first
        audioManager.PlayMusic("Game Over");
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
