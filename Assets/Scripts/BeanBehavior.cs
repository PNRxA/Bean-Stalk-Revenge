using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBehavior : MonoBehaviour
{

    private float rotateX;
    private float rotateY;
    private float rotateZ;



    // Use this for initialization
    void Start()
    {
        // Randomise the rotation of bean
        rotateX = Random.Range(-2f, 2f);
        rotateY = Random.Range(-2f, 2f);
        rotateZ = Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateX, rotateY, rotateZ);
    }
}
