using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    public virtual void TakeDamage(float health) { }
    public virtual void Kill() { }
}
