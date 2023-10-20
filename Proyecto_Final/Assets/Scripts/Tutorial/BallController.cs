using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 start_position;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        start_position = transform.position;
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = start_position;
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("LavaFloor"))
        {
            transform.position = start_position;
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
