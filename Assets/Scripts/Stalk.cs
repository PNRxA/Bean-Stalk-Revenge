using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    public bool shootingStalk = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    shootingStalk = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            shootingStalk = false;
        }
        if (shootingStalk)
        {
            Controller();
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 3);
        }
    }
    
    void Controller()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Mouse X") * 4, 0, Input.GetAxis("Mouse Y") * 4));
    }
}
