using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;
    [SerializeField] float movementSpeed = 1f;

    float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += movementSpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }
}
