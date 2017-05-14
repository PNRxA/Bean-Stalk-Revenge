using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public Transform targetEnemy;
    public Tower_01 towerShotFrom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //towerShotFrom.targets.Remove(col.gameObject);
            Destroy(col.gameObject);
            //towerShotFrom.bullets.Remove(this);
            Destroy(gameObject);
        }
    }
}
