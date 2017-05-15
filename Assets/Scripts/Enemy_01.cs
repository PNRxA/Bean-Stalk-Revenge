using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : Enemy
{
    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}
