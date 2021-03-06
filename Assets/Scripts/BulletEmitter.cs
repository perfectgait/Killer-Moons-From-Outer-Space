using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletEmitter : MonoBehaviour
{
    // TODO Group with headings
    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("If no origin point is provided, default origin point is this script's game object")]
    [SerializeField] GameObject bulletOriginPoint;
    [Tooltip("Use a sound name from the AudioManager")]
    [SerializeField] string soundEffectName = "Default Laser";

    [Header("Bullet")]
    [SerializeField] float bulletSpeed = 8f;
    [Tooltip("Number of bullets in a wave - set to 1 for a single shot")]
    [SerializeField] int bulletCountPerWave = 10;
    [Tooltip("Number of waves to emit before waiting")]
    [SerializeField] int wavesPerPulse = 3;

    [Header("Arc")]
    [Tooltip("0 is north and it counts clockwise")]
    [Range(0, 360)]
    [SerializeField] int startAngle = 90;
    [Range(0, 360)]
    [SerializeField] int endAngle = 270;
    [Tooltip("+/- angle variance to apply to the bullets")]
    [SerializeField] int angleVariance = 0;

    [Header("Pulse")]
    [Tooltip("Time between each individual bullet or wave of bullets")]
    [SerializeField] float waitTimeBetweenBullets = .25f;
    [Tooltip("Time between each pulse - set to 0 for a continuous stream")]
    [SerializeField] float waitTimeBetweenPulse = 0f;
    [Tooltip("Continue shooting for the lifetime of the game object?")]
    [SerializeField] bool continousEmission = true;

    private Transform bulletOriginTransform;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetBulletOriginTransform()
    {
        bulletOriginTransform = transform;

        if (bulletOriginPoint)
        {
            bulletOriginTransform = bulletOriginPoint.transform;
        }
    }

    public IEnumerator Emit()
    {
        SetBulletOriginTransform();

        do
        {
            for (var i = 1; i <= wavesPerPulse; i++)
            {
                AudioManager.instance.PlaySoundEffect(soundEffectName);

                int randomAngleVariance = Random.Range(-angleVariance, angleVariance);
                float angle = startAngle + randomAngleVariance;
                float angleStep = ((endAngle + randomAngleVariance) - angle) / (bulletCountPerWave - 1);
                for (var j = 1; j <= bulletCountPerWave; j++)
                {
                    // Calculate the angle of the corner in radians.
                    float radians = (-angle + 90) * Mathf.Deg2Rad;

                    Vector3 moveVector = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

                    var projectile = Instantiate(projectilePrefab, bulletOriginTransform.position, bulletOriginTransform.rotation);
                    projectile.GetComponent<Projectile>().SetVelocity(bulletSpeed, moveVector);
                    angle += angleStep;
                }
                yield return new WaitForSeconds(waitTimeBetweenBullets);
            }
            yield return new WaitForSeconds(waitTimeBetweenPulse);
        } while (continousEmission);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        SetBulletOriginTransform();

        // Show the direction and angle of the bullet wave
        float circleRadius = 1.25f;

        Gizmos.DrawWireSphere(bulletOriginTransform.position, circleRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(bulletOriginTransform.position, GetPositionOnCircle(startAngle, circleRadius));
        Gizmos.DrawLine(bulletOriginTransform.position, GetPositionOnCircle(endAngle, circleRadius));
    }

    private Vector2 GetPositionOnCircle(int angle, float radius)
    {
        var centerPosition = new Vector2(bulletOriginTransform.position.x, bulletOriginTransform.position.y);

        // Calculate the angle of the corner in radians.
        float radians = (-angle + 90) * Mathf.Deg2Rad;

        // Get the X and Y coordinates of the corner point.
        return new Vector2(Mathf.Cos(radians) * radius, Mathf.Sin(radians) * radius) + centerPosition;
    }
#endif

    public void SetWaitTimeBetweenBullets(float waitTime)
    {
        waitTimeBetweenBullets = waitTime;
    }

    public void SetBulletSfx(string soundName)
    {
        soundEffectName = soundName;
    }

    public void SetAngleVariance(int variance)
    {
        angleVariance = variance;
    }

    public float GetWaitTimeBetweenBullets()
    {
        return waitTimeBetweenBullets;
    }
}
