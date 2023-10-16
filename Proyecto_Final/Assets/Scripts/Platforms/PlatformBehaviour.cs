using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private bool isActiveToMove;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        isActiveToMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveToMove)
        {
            transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, transform.GetChild(1).position, speed * Time.deltaTime);
        }
    }

    public void SetIsActiveToMove()
    {
        isActiveToMove = true;
    }
}
