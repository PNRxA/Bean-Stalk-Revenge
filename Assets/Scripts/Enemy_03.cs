using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03 : Enemy
{
    protected override void Attack()
    {
        // Remove health based on enemy and kill self
        GameManager.health = GameManager.health - 3;
        health = 0;
    }
}
