using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIAController : MonoBehaviour
{
    Transform player;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
