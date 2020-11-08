using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // TODO Group with headings
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 8f;
    [SerializeField] int bulletCountPerLoop = 3;
    [SerializeField] int bulletsPerCluster = 10;
    [SerializeField] int startAngle = 90;
    [SerializeField] int endAngle = 270;
    [SerializeField] float waitTimeBetweenBullets = .25f;
    [SerializeField] bool loopBulletCount = true;
    [SerializeField] float waitTimeBetweenLoop = 2f;


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
                float angleStep = (endAngle - startAngle) / bulletsPerCluster;
                float angle = startAngle;
                for (var j = 0; j <= bulletsPerCluster; j++)
                {
                    float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 moveVector = new Vector2(dirX, dirY);
                    Vector2 bulVector = (moveVector - transform.position).normalized;

                    var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    projectile.GetComponent<Projectile>().SetVelocity(speed, bulVector);
                    angle += angleStep;
                }
                yield return new WaitForSeconds(waitTimeBetweenBullets);
            }
            yield return new WaitForSeconds(waitTimeBetweenLoop);
        } while (loopBulletCount);
    }
}
