using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_02 : Tower
{
    public Transform lasorPos;
    private LineRenderer lRenderer;
    // Use this for initialization
    void Awake()
    {
        // Set linerenderer
        lRenderer = gameObject.GetComponent<LineRenderer>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // If there are no targets, disable line renderer 
        if (targets.Count <= 0)
        {
            lRenderer.enabled = false;
            anim.SetBool("T2_attack", false);
        }
        // If the linerenderer is disabled then damage the targets
        if (lRenderer.enabled)
        {
            DamageTargets();
        }
    }

    protected override void Shoot(GameObject targetToShoot)
    {
        // Only attack if placed
        if (placed)
        {
            // Set position of attack line from the current transform position to the target position
            Vector3[] positions = new[] { lasorPos.position, targetToShoot.transform.position };
            // Enable line renderer and set the positions of the line
            lRenderer.enabled = true;
            anim.SetBool("T2_attack", true);
            lRenderer.SetPositions(positions);
        }
    }

    void DamageTargets()
    {
        // If target isn't null then get the health component and damage the enemy based on level
        if (targets[0] != null)
        {
            targets[0].GetComponent<Enemy>().health -= (0.02f * level);
        }
    }
}