using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyAttack shooter;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<EnemyAttack>();

        if (shooter)
        {
            StartCoroutine(shooter.Fire());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
