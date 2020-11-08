using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 8f;
    [SerializeField] int bulletCountPerLoop = 3;
    [SerializeField] float waitTimeBetweenBullets = .25f;
    [SerializeField] bool loopBulletCount = true;
    [SerializeField] float waitTimeBetweenLoop = 2f;

    // TODO: Should pulse/loop
    // TODO: Wait time between pulse/loop

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(FireBullets());
    }

    // Update is called once per frame
    void Update()
    {


    }

    private IEnumerator FireBullets()
    {
        do
        {
            for (var i = 1; i <= bulletCountPerLoop; i++)
            {
                // TODO: Eventually use an object pool for the projectiles instead of Instantiating each one
                var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.GetComponent<Projectile>().SetVelocity(speed, Vector2.left);
                yield return new WaitForSeconds(waitTimeBetweenBullets);
            }
            yield return new WaitForSeconds(waitTimeBetweenLoop);
        } while (loopBulletCount);
    }
}
