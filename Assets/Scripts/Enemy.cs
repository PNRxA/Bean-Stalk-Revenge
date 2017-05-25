using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float health = 2f;
    public float damage = 10f;
    public float moveSpeed = 3.5f;
    public Transform target;
    public int value = 10;

    public BeanBehavior beanBehavior;

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

        // Need to get beanBehavior before destroyed
        beanBehavior = FindObjectOfType<BeanBehavior>();
    }

    protected virtual void Attack() { }
    protected virtual void OnDeath()
    {
        // Increate currency by value of enemy
        GameManager.Money += value;
        // Find if last enemy remaining
        GameObject[] enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");

        // Checking for beanbehaviour killing the last few enemies
        if (beanBehavior != null)
        {
            if (enemiesRemaining.Length == beanBehavior.enemiesToKill.Count)
            {
                EndWave();
            }
        }

        // If last enemy remaining, end the wave and begin coutdown
        if (enemiesRemaining.Length == 1)
        {
            EndWave();
        }
        // Commit sudoku
        Destroy(gameObject);
    }

    void EndWave()
    {
        GameManager.inWave = false;
        WaveSpawner.countdown = 20;
    }

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
