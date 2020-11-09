using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmitter : MonoBehaviour
{
    // TODO Group with headings
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject bulletOriginPoint;
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Emit()
    {
        var bulletOriginTransform = transform;

        if (bulletOriginPoint)
        {
            bulletOriginTransform = bulletOriginPoint.transform;
        }

        do
        {
            for (var i = 1; i <= bulletCountPerLoop; i++)
            {
                float angleStep = (endAngle - startAngle) / bulletsPerCluster;
                float angle = startAngle;
                for (var j = 0; j <= bulletsPerCluster; j++)
                {
                    float dirX = bulletOriginTransform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = bulletOriginTransform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 moveVector = new Vector2(dirX, dirY);
                    Vector2 bulVector = (moveVector - bulletOriginTransform.position).normalized;

                    var projectile = Instantiate(projectilePrefab, bulletOriginTransform.position, bulletOriginTransform.rotation);
                    projectile.GetComponent<Projectile>().SetVelocity(speed, bulVector);
                    angle += angleStep;
                }
                yield return new WaitForSeconds(waitTimeBetweenBullets);
            }
            yield return new WaitForSeconds(waitTimeBetweenLoop);
        } while (loopBulletCount);
    }
}
