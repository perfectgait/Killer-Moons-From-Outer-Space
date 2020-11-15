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
    SpriteRenderer spriteRenderer;

    IEnumerator flashingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        iframes = GetComponent<IFrames>();
        audioManager = AudioManager.instance;
        // GetComponentInChildren finds the first component in the parent or child
        // so this works for both the player's body and the enemies
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            HandleDamageEffects();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void HandleDamageEffects()
    {
        audioManager.PlaySoundEffect("Damage Hit");
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
        }
        flashingCoroutine = Flash(2);
        StartCoroutine(flashingCoroutine);
    }

    private IEnumerator Flash(int numTimes)
    {
        bool makeSpriteWhite = true;

        // Iterate twice the length of times - that way numTimes is "how many times is it turned on", not "how many times does it flip".
        for (int i = 0; i < numTimes * 2; i++)
        {
            GetComponentInChildren<SpriteRenderer>().material.SetFloat("_FlashAmount", makeSpriteWhite ? 1f : 0f);
            makeSpriteWhite = !makeSpriteWhite;
            yield return new WaitForSeconds(0.05f);
        }
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
