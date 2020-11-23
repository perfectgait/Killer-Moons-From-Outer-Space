using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float maximumAngleOfRotation;
    [SerializeField] float minimumAngleOfRotation;
    [SerializeField] float angleChangePerFrame;

    private float currentAngle;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        currentAngle = gameObject.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle += angleChangePerFrame * direction;

        if (currentAngle > maximumAngleOfRotation)
        {
            currentAngle = maximumAngleOfRotation;
            direction *= -1;
        }
        else if (currentAngle < minimumAngleOfRotation)
        {
            currentAngle = minimumAngleOfRotation;
            direction *= -1;
        }

        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, currentAngle); //currentAngle;
    }
}
