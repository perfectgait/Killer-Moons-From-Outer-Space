using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] protected float speed;

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
}
