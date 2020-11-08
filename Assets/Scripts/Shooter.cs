using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject weapon;

    public void Fire()
    {
        Instantiate(projectile, weapon.transform.position, transform.rotation);
    }
}
