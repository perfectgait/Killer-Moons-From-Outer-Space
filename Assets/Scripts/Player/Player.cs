using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float horizontalMovementSpeed = 7.0f;
    [SerializeField] float verticalMovementSpeed = 7.0f;
    [SerializeField] float xRightPadding = 0.5f;
    [SerializeField] float xLeftPadding = 0.5f;
    [SerializeField] float yTopPadding = 0.5f;
    [SerializeField] float yBottomPadding = 0.5f;

    private Coroutine firingCoroutine;
    private BulletEmitter bulletEmitter;
    private Health health;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private bool isFiring = false;
    private bool canFire = true;
    private float canFireCountdown = 0.0f;
    // Offset is used so a human can fire at the same rate as the computer if they chose to rapid press the fire button vs. hold it down
    private float waitTimeBetweenBulletsOffset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        SetupMovementBoundaries();
        bulletEmitter = GetComponent<BulletEmitter>();
        health = GetComponent<Health>();

        AddPowerup(typeof(Minigun));
    }

    // Update is called once per frame
    void Update()
    {
        ApplyPowerups();
        Move();
        UpdateCanFireCountdownTimer();
        Fire();
    }

    private void SetupMovementBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xLeftPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xRightPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yBottomPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yTopPadding;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalMovementSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verticalMovementSpeed;
        float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);
    }

    private void Fire()
    {
        if (!bulletEmitter)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && CanFire())
        {
            firingCoroutine = StartCoroutine(bulletEmitter.Emit());
            isFiring = true;
        }

        // If the player has let go of the fire button, start the countdown timer
        if (Input.GetButtonUp("Fire1") && isFiring)
        {
            canFireCountdown = bulletEmitter.GetWaitTimeBetweenBullets() - waitTimeBetweenBulletsOffset;
        }

        if (Input.GetButtonUp("Fire1") || !CanFire())
        {
            StopCoroutine(firingCoroutine);
            isFiring = false;
        }
    }

    private void ApplyPowerups()
    {
        Powerup[] powerups = GetComponents<Powerup>();

        if (powerups.Length <= 0)
        {
            return;
        }

        foreach (Powerup powerup in powerups)
        {
            powerup.Apply(this);
        }
    }

    private bool CanFire()
    {
        if (!canFire)
        {
            return false;
        }

        if (canFireCountdown > 0)
        {
            return false;
        }

        return true;
    }

    private void UpdateCanFireCountdownTimer()
    {
        canFireCountdown -= Time.deltaTime;
    }

    public float GetHealth()
    {
        if (!health)
        {
            return 0.0f;
        }

        return health.GetHealth();
    }

    public void AddPowerup(System.Type systemType)
    {
        gameObject.AddComponent(systemType);
    }

    public bool IsFiring()
    {
        return isFiring;
    }

    public void SetCanFire(bool canFire)
    {
        this.canFire = canFire;
    }

    public float GetHeatLevel()
    {
        Minigun minigun = gameObject.GetComponent<Minigun>();

        if (minigun)
        {
            return minigun.GetCurrentHeatLevel();
        }

        return 1.0f;
    }
}
