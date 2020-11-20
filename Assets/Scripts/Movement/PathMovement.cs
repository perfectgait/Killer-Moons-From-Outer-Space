using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;
    [Range(0f, 1f)]
    [SerializeField] public float movementSpeed = 1f;

    public float distanceTravelled;
    public bool finishedMoving = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!finishedMoving)
        {
            distanceTravelled += 1f * movementSpeed * Time.deltaTime;
            distanceTravelled = Mathf.Clamp(distanceTravelled, 0f, 1f);
            transform.position = pathCreator.path.GetPointAtTime(distanceTravelled, EndOfPathInstruction.Stop);
            finishedMoving = distanceTravelled == 1f;
        }
    }

    // TODO: Slow movement along the way
}
