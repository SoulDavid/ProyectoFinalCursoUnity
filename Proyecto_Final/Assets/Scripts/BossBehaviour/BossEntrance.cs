using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    [SerializeField] private GameObject doorController;
    [SerializeField] private GameObject bossObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            doorController.GetComponent<PlatformBehaviour>().SetIsActiveToMove();
            bossObject.SetActive(true);
            bossObject.GetComponent<HealthEnemy>().StartEnemy();
        }
    }
}
