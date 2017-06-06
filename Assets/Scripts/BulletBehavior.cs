using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public Transform targetEnemy;
    public Tower_01 towerShotFrom;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If there's no enemy then kill self
        if (!targetEnemy || targetEnemy == null)
        {
            Destroy(gameObject);
        }
    }

    // If hitting enemy then damage and kill self
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Enemy target = col.gameObject.GetComponent<Enemy>();
            target.health--;
            Destroy(gameObject);
        }
    }
}
