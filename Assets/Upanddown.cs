using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upanddown : MonoBehaviour
{
    bool isAir;
    float y;
    Rigidbody rb;
    private void Start()
    {
        rb=GetComponent<Rigidbody>();
        isAir = true;
        y =GetComponent<Transform>().position.y;
    }
    private void Update()
    {
        if(isAir)
        {
            rb.AddForce(0, -50*Time.deltaTime, 0);
        }
        else { rb.AddForce(0, 50* Time.deltaTime, 0); }
        if (transform.position.y >= y-0.5f)
        {
            isAir = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isAir = false;
    }
}
