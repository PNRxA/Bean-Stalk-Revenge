using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : MonoBehaviour
{
    public GameObject aim;
    public bool shootingStalk = false;
    public GameObject bean;
    public float speed = 5f;

    private Vector3 beanTarget;
    private GameObject activeBean;
    private bool beanInMotion = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveBean();
        if (beanTarget != null && activeBean != null)
        {
            if (activeBean.transform.position == beanTarget)
            {
                Destroy(activeBean);
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
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 target = aim.transform.position;
                beanTarget = aim.transform.position;
                shootingStalk = false;
                beanInMotion = true;
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
            Vector3 aimPos = -hit.point;
            //aimPos.x -= 10;
            //aimPos.z -= 8;
            aimPos.y = 0.6f;

            aim.transform.position = aimPos;
        }
    }

    void ShootBean()
    {
        activeBean = Instantiate(bean, transform.position, transform.rotation);
    }

    void MoveBean()
    {
        float step = speed * Time.deltaTime;
        if (activeBean != null)
        {
            activeBean.transform.position = Vector3.MoveTowards(activeBean.transform.position, beanTarget, step);
        }
    }
}
