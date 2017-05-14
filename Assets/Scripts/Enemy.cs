using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float health = 100f;
    public float damage = 10f;
    public float moveSpeed = 3.5f;
    public Transform target;

    protected NavMeshAgent agent;
    protected bool isAttacking = false;
    protected bool isAgentActive = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SeekToTarget();
        if (CheckDeath())
        {
            OnDeath();
        }
    }
    
    protected virtual void FixedUpdate()
    {
        // If at the target attack it
        if (IsAtTarget())
        {
            Attack();
        }
    }

    protected virtual void Attack() { }
    protected virtual void OnDeath() { }

    // Use this for initialization
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    // Seek to Target
    protected void SeekToTarget()
    {
        if (target != null)
        {
            if (isAgentActive)
            {
                agent.Resume();
                agent.SetDestination(target.position);
            }
            else
            {
                agent.Stop();
            }
        }
    }

    // If dead
    protected bool CheckDeath()
    {
        return health <= 0;
    }
    
    // Check if agent is at the target
    protected bool IsAtTarget()
    {
        if (agent.hasPath)
        {
            return (agent.remainingDistance <= agent.stoppingDistance);
        }
        else
        {
            return false;
        }
    }
}
