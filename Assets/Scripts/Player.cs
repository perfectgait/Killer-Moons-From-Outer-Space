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
    [SerializeField] float projectileFiringPeriod = 1.0f;

    private Coroutine firingCoroutine;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private BulletEmitter bulletEmitter;
    private Health health;
    private AudioManager audioManager;
    private bool isFiring = false;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        SetupMovementBoundaries();
        bulletEmitter = GetComponent<BulletEmitter>();
        health = GetComponent<Health>();
        audioManager = AudioManager.instance;

        //AddPowerup(typeof(Minigun));
    }

    // Update is called once per frame
    void Update()
    {
        ApplyPowerups();
        Move();
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

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            firingCoroutine = StartCoroutine(bulletEmitter.Emit());
            isFiring = true;
        }

        if (Input.GetButtonUp("Fire1") || !canFire)
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
}
