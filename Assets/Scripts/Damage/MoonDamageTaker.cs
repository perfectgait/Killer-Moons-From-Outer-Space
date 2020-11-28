using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonDamageTaker : DamageTaker
{
    [SerializeField] int score = 0;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";
    [SerializeField] GameObject[] explosionPoints;
    [SerializeField] float waitTimeBetweenExplosions;

    private Health health;
    private AudioManager audioManager;
    private MoonBoss moon;
    private bool isDying = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        audioManager = AudioManager.instance;
        moon = GetComponent<MoonBoss>();
    }

    public bool IsDying()
    {
        return isDying;
    }

    public override void TakeDamage(float damage)
    {
        if (isDying)
        {
            return;
        }

        if (health)
        {
            health.Damage(damage);
        }

        if (health.GetHealth() <= 0)
        {
            isDying = true;
            Kill();
        }
    }

    public override void Kill()
    {
        GameScore.instance.IncrementBy(score);

        moon.StopAttacking();
        StartCoroutine(Explode());
        KillCannons();
    }

    private void KillCannons()
    {
        if (!moon)
        {
            return;
        }

        MoonCannon[] moonCannons = moon.GetMoonCannons();

        foreach (MoonCannon moonCannon in moonCannons)
        {
            if (moonCannon)
            {
                DamageTaker moonCannonDamageTaker = moonCannon.GetComponent<DamageTaker>();

                if (moonCannonDamageTaker)
                {
                    moonCannonDamageTaker.Kill();
                }
            }
        }
    }

    private IEnumerator Explode()
    {
        if (explosionPrefab)
        {
            foreach (GameObject explosionPoint in explosionPoints)
            {
                if (explosionPoint)
                {
                    audioManager.PlaySoundEffect(explosionSfxName);
                    GameObject explosion = Instantiate(explosionPrefab, explosionPoint.transform.position, explosionPoint.transform.rotation);
                    Destroy(explosion, 1f);
                    yield return new WaitForSeconds(waitTimeBetweenExplosions);
                }
            }

            Destroy(gameObject, .01f);
        }
    }
}
