using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2BossEnemiesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnEnemies;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeBetweenSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        InvokeRepeating("SpawnEnemies", 2, 2);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    private void SpawnEnemies()
    {
        if(GetComponent<HealthEnemy>().get_isInPhase2())
        {
            int numberSpawn = Random.Range(0, spawnEnemies.Length);
            Instantiate(enemyPrefab, spawnEnemies[numberSpawn].transform.position, Quaternion.identity, this.transform);
        }
    }
}
