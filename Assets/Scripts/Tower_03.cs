using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower_03 : Tower
{
    private ParticleSystem particles;
    private ParticleSystem.EmissionModule em;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        em = particles.emission;
        em.enabled = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (targets.Count <= 0)
        {
            em.enabled = false;
        }
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<NavMeshAgent>().speed = col.gameObject.GetComponent<Enemy>().moveSpeed;
        }
    }

    // Use this for initialization
    protected override void Shoot(GameObject targetToShoot)
    {
        if (placed)
        {
            em.enabled = true;
            for (int i = 0; i < targets.Count; i++)
            {
                NavMeshAgent currentAgent = targets[i].GetComponent<NavMeshAgent>();
                Enemy currentEnemy = targets[i].GetComponent<Enemy>();
                if (currentAgent.speed > currentEnemy.moveSpeed / level)
                {
                    currentAgent.speed = currentEnemy.moveSpeed / level;
                }
                currentEnemy.health -= (.001f * level);
            }
        }
    }
}
