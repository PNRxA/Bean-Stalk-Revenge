using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_02 : Tower
{
    private LineRenderer lRenderer;
    // Use this for initialization
    void Start()
    {
        lRenderer = gameObject.GetComponent<LineRenderer>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (targets.Count <= 0)
        {
            lRenderer.enabled = false;
        }
        if (lRenderer.enabled)
        {
            DamageTargets();
        }
    }

    protected override void Shoot(GameObject targetToShoot)
    {
        if (placed)
        {
            Vector3[] positions = new[] { transform.position, targetToShoot.transform.position };

            lRenderer.enabled = true;
            lRenderer.SetPositions(positions);
        }
    }

    void DamageTargets()
    {
        if (targets[0] != null)
        {
            targets[0].GetComponent<Enemy>().health -= (0.02f * level);
        }
    }
}