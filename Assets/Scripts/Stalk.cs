using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    public GameObject aim;
    public bool shootingStalk = false;
    public GameObject bean;
    public float speed = 20f;
    public ParticleSystem explosion;
    public bool exploded = false;

    private Vector3 beanTarget;
    private GameObject activeBean;
    private bool beanInMotion = false;

    // Update is called once per frame
    void Update()
    {
        MoveBean();
        // If there is a target and an active bean then spawn an explosion and destroy if it's reached the target
        if (beanTarget != null && activeBean != null)
        {
            if (activeBean.transform.position == beanTarget)
            {
                activeBean.GetComponent<Renderer>().enabled = false;
                Destroy(activeBean, .25f);
                if (!exploded)
                {
                    ParticleSystem explode = Instantiate(explosion, beanTarget, Quaternion.Euler(-90, 0, 0));
                    Destroy(explode.gameObject, 1f);
                    exploded = true;
                }
                beanInMotion = false;
            }
        }
        if (!beanInMotion)
        {
            // If holding click, set as shooting
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
            // If unclick, set as not shooting and shoot bean
            if (Input.GetMouseButtonUp(0) && shootingStalk)
            {
                Vector3 target = aim.transform.position;
                beanTarget = aim.transform.position;
                shootingStalk = false;
                beanInMotion = true;
                exploded = false;
                ShootBean();
            }
        }
        // If shooting, control reticule and rotation of stalk
        if (shootingStalk)
        {
            aim.GetComponent<Renderer>().enabled = true;
            Controller();
        }
        // Otherwise don't
        else
        {
            aim.GetComponent<Renderer>().enabled = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 3);
        }
    }

    void Controller()
    {
        // Rotate the stalk to look cool
        transform.Rotate(new Vector3(Input.GetAxis("Mouse X") * 4, 0, Input.GetAxis("Mouse Y") * 4));

        // Control reticule position
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // Set the aimpos to the oposite of the cursor
            Vector3 aimPos = -hit.point;
            aimPos.y = 0.6f;

            aim.transform.position = aimPos;
        }
    }

    // Shoot bean
    void ShootBean()
    {
        // Set active bean to the new instantiation
        activeBean = Instantiate(bean, transform.position, transform.rotation);
    }

    // Move bean
    void MoveBean()
    {
        // Speed of bean
        float step = speed * Time.deltaTime;
        // If there is an active bean move it to the target at defined speed
        if (activeBean != null)
        {
            activeBean.transform.position = Vector3.MoveTowards(activeBean.transform.position, beanTarget, step);
        }
    }
}
