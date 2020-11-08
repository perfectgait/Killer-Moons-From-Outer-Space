using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalMovementSpeed = 7.0f;
    [SerializeField] float verticalMovementSpeed = 7.0f;
    [SerializeField] float xPadding = 0.5f;
    [SerializeField] float yPadding = 0.5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetupMovementBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetupMovementBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalMovementSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verticalMovementSpeed;
        float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);
    }
}
