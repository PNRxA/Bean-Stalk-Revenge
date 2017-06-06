using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_01 : Tower
{
    public List<BulletBehavior> bullets;
    public float speed = 50f;

    private float instantiationTimer = 1.5f;
    private float instantiationTimerUpdate = 1.5f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        moveBullets();
    }

    protected override void Shoot(GameObject targetToShoot)
    {
        if (placed)
        {
            instantiationTimer -= Time.deltaTime; // Timer
            if (instantiationTimer <= 0) //Check if power is needed && if timer == 0
            {
                // Create bullet
                BulletBehavior bulletPrefab = Instantiate(bullet, transform.position, transform.rotation);
                bulletPrefab.targetEnemy = targetToShoot.transform;
                bullets.Add(bulletPrefab);

                instantiationTimer = instantiationTimerUpdate - (level * .3f);
            }
        }

    }

    // Make bullets travel towards enemy
    void moveBullets()
    {
        float step = speed * Time.deltaTime;
        for (int i = 0; i < bullets.Count; i++)
        {
            // If the bullet doesn't exist remove it from the list of alive bullets
            if (bullets[i] == null)
            {
                bullets.Remove(bullets[i]);
            }
            // If the bullet exists move it towards the target
            else
            {
                // Double check if target enemy doesn't exist as errors were happening (maybe can remove first check now) TODO Bullets get stuck if two towers attack at same time and enemy dies while other bullet is traveling 
                if (bullets[i].targetEnemy)
                {
                    bullets[i].transform.position = Vector3.MoveTowards(bullets[i].transform.position, bullets[i].targetEnemy.position, step);
                }
                else
                {
                    Destroy(bullets[i].gameObject);
                }
            }
        }
    }
}