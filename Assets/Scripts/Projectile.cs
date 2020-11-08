using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    private float speed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetVelocity(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    // TODO: Destroy the gameObject when it collides or goes outside the bounds
}
