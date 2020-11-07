using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] float horizontalSpeed = 1f;
    [Tooltip("Horizontal distance between two peaks")]
    [Range(0, 10)]
    [SerializeField] float period = 5f;
    [Tooltip("Vertical distance between midpoint and peak")]
    [Range(0, 4)]
    [SerializeField] float amplitude = .5f;

    private float startYPos;

    // Start is called before the first frame update
    void Start()
    {
        startYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(
            transform.position.x - horizontalSpeed * Time.deltaTime,
            startYPos + Mathf.Sin(period * Time.time) * amplitude
        );
        transform.position = movement;
    }
}
