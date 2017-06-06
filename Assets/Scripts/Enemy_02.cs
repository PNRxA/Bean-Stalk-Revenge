using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : Enemy
{
    protected override void Attack()
    {
        // Remove health based on enemy and kill self
        GameManager.health = GameManager.health - 2;
        health = 0;
    }
}
