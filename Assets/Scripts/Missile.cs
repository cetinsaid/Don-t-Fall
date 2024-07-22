using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform enemy;
    public float speed;
    public Rigidbody rb;
    public float xBorder;
    public float negativeXBorder;
    public float zBorder;
    public float negativeZBorder;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 20;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x > xBorder || transform.position.x < negativeXBorder ||
            transform.position.z > zBorder || transform.position.z < negativeZBorder)
        {
            Destroy(gameObject);
        }

        if (enemy != null)
        {
            rb.velocity = ((enemy.position - transform.position).normalized * speed);
            transform.LookAt(enemy);
        }
        
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.gameObject.transform.position - transform.position).normalized * 1000 , ForceMode.Impulse);
        }
    }
}
