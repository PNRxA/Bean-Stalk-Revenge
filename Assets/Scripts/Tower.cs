using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Animator anim;
    public List<GameObject> targets;
    public BulletBehavior bullet;
    public bool placed = false;
    public int level = 1;
    public float speedR = 10f;

    protected void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (placed)
        {
            Attack();
        }
    }

    // Add enemy to list to attack
    protected void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            targets.Add(col.gameObject);
        }
    }

    // Remove enemy from list to attack
    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            targets.Remove(col.gameObject);
        }
    }

    protected void Attack()
    {
        // If there are targets, attack the first one
        if (targets.Count >= 1 && targets[0] != null)
        {
            Shoot(targets[0]);

            Vector3 targetDir = targets[0].transform.position;
            targetDir.y = transform.position.y;
            transform.LookAt(targetDir);
        }
        // If there are targets but it has been destroyed remove it from the list
        else if (targets.Count >= 1 && targets[0] == null)
        {
            targets.Remove(targets[0]);
        }
    }

    protected virtual void Shoot(GameObject targetToShoot) { }
}