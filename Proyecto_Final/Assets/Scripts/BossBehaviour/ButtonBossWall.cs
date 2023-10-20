using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBossWall : MonoBehaviour
{
    [SerializeField] private GameObject platforms;
    [SerializeField] private HealthEnemy enemyScript;

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
        if (other.gameObject.CompareTag("Hook"))
        {
            enemyScript.setToPhase2();
            transform.parent.gameObject.SetActive(false);
        }
    }
}
