using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public float speed;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce(direction.normalized * speed *rb.mass /25f);
    }
}
