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

    // Start is called before the first frame update
    void Start()
    {
        SetupMovementBoundaries();
        bulletEmitter = GetComponent<BulletEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(bulletEmitter.Emit());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
}
