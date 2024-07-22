using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PowerType
    {
        NoPower,
        JumpPower,
        MissilePower,
        CrashPower
    }

    public int crashTime;
    public int jumpTime;
    public int missileTime;

    public PowerType powerType = PowerType.NoPower;
    public float speed;
    public GameObject focalPoint;
    public GameObject indicator;
    public GameObject missileIndicator;
    public GameObject jumpIndicator;
    private bool isOnGround;
    public float jumpAmount = 10;

    private Rigidbody rb;

    

    public float powerForce;
    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        indicator.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isOnGround && powerType == PowerType.JumpPower)
        {
            Debug.Log("jumped");
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, transform.position.y + jumpAmount, transform.position.z);
            rb.velocity = Vector3.zero;
            isOnGround = false;
            
        }

        else if (Input.GetKeyDown(KeyCode.F) && !isOnGround)
        {
            Debug.Log("down");
            transform.position = new Vector3(transform.position.x, transform.position.y - jumpAmount, transform.position.z);
            isOnGround = true;
            rb.useGravity = true;
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Rigidbody>().AddForce((-transform.position + enemies[i].transform.position).normalized * 1000 , ForceMode.Impulse);
            }
        }

        if (isOnGround)
        {
            float input = Input.GetAxis("Vertical");
            rb.AddForce(focalPoint.transform.forward * speed  * input);
        }

        
        

        
        

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && powerType == PowerType.CrashPower)
        {
            Vector3 direction = other.gameObject.transform.position - transform.position;
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(direction.normalized * powerForce , ForceMode.Impulse);
            
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    IEnumerator PowerUpCountDown(int time , GameObject indicator)
    {
        yield return new WaitForSeconds(time);
        indicator.SetActive(false);
        powerType = PowerType.NoPower;
    }
    

    private void setPower(PowerType pw,Collider other , GameObject indicator)
    {
        powerType = pw;
        indicator.SetActive(true);
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            setPower(PowerType.CrashPower , other , indicator);
            StartCoroutine(PowerUpCountDown(crashTime, indicator));
        }
        else if (other.CompareTag("JumpPow"))
        {
            setPower(PowerType.JumpPower , other  , jumpIndicator);
            StartCoroutine(PowerUpCountDown(jumpTime , jumpIndicator));
        }
        else if (other.CompareTag("MissPow"))
        {
            setPower(PowerType.MissilePower , other  , missileIndicator );
            StartCoroutine(PowerUpCountDown(missileTime, missileIndicator));
        }
    }
}
