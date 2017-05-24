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

    protected override void Update()
    {
        base.Update();
        if (targets.Count <= 0)
        {
            lRenderer.enabled = false;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (lRenderer.enabled)
        {
            DamageTargets();
        }
    }

    protected override void Shoot(GameObject targetToShoot)
    {
        Vector3[] positions = new[] { transform.position, targets[0].transform.position };
        lRenderer.enabled = true;
        lRenderer.SetPositions(positions);
    }

    void DamageTargets()
    {
        targets[0].GetComponent<Enemy>().health -= 0.02f;
    }

}
