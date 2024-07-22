using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public PlayerController ply;
    public Transform player;
    public GameObject missile;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ply.powerType == PlayerController.PowerType.MissilePower)
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            for (int i = 0; i < enemies.Length; i++)
            {
                Spawn(enemies[i]);
            }

        }

    }

    IEnumerator destroyMissile(GameObject go)
    {
        yield return new WaitForSeconds(2);
        Destroy(go);
    }

    public void Spawn(EnemyController enemy)
    {
        Vector3 offSet = (enemy.transform.position - player.position).normalized;
        GameObject g = Instantiate(missile, player.position + offSet , Quaternion.identity);
        g.GetComponent<Missile>().enemy = enemy.transform;
        StartCoroutine(destroyMissile(g));
    }
}
