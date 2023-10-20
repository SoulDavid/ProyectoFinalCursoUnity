using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPlayer;
    private Rigidbody rb;
    private GameObject ball;

    // Start is called before the first frame update
    void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody>();
        //ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = spawnPlayer.position;
            collision.gameObject.transform.parent.GetComponent<HealthEnemy>().Restart();
            rb.velocity = Vector3.zero;
        }

        if(collision.gameObject.CompareTag("LavaFloor") || collision.gameObject.CompareTag("Ball"))
        {
            transform.position = spawnPlayer.position;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawn"))
        {
            spawnPlayer.position = other.transform.position + new Vector3(0, 1, 0);
        }

        if (other.gameObject.CompareTag("SpawnBall"))
        {
            ball.SetActive(true);
        }
    }
}
