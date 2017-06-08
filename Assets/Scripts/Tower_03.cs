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
        // Set particlesystem 
        particles = GetComponentInChildren<ParticleSystem>();
        // Stop particle emission at spawn
        em = particles.emission;
        em.enabled = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // If there are no targets than disable particle emission
        if (targets.Count <= 0)
        {
            em.enabled = false;
            anim.SetBool("T3_attack", false);
        }
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        // If an enemy leaves radius then set speed back to normal
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<NavMeshAgent>().speed = col.gameObject.GetComponent<Enemy>().moveSpeed;
        }
    }

    // Use this for initialization
    protected override void Shoot(GameObject targetToShoot)
    {
        // If the tower is placed then allow it to attack
        if (placed)
        {
            // Enable particle emissions
            em.enabled = true;
            anim.SetBool("T3_attack", true);
            // For each target, slow and damage
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i])
                {
                    // Get navmeshagent and enemy script
                    NavMeshAgent currentAgent = targets[i].GetComponent<NavMeshAgent>();
                    Enemy currentEnemy = targets[i].GetComponent<Enemy>();
                    // Only reduce speed if it is more than when it would be reduced to (to prevent overriding stronger tower)
                    if (currentAgent.speed > currentEnemy.moveSpeed / level)
                    {
                        // Reduce the agent speed based on level
                        currentAgent.speed = currentEnemy.moveSpeed / level;
                    }
                    // Slowly remove all enemies health within radius
                    currentEnemy.health -= (.001f * level);
                }
            }
        }
    }
}
