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

    private void Update()
    {
        timeBetweenSpawn += Time.deltaTime;

        if (timeBetweenSpawn >= 2f)
        {
            int numberSpawn = Random.Range(0, spawnEnemies.Length);
            Instantiate(enemyPrefab, spawnEnemies[numberSpawn].transform.position, Quaternion.identity);
            timeBetweenSpawn = 0;
        }
    }

    //Se activaara cuando el jugador de al boton y se suba de nuevo la pantalla
    public void StartSpawn()
    {
        //while(true)
        //{
        //    timeBetweenSpawn += Time.deltaTime;

        //    //if(timeBetweenSpawn >= 2f)
        //    //{
        //    //    int numberSpawn = Random.Range(0, spawnEnemies.Length);
        //    //    Instantiate(enemyPrefab, spawnEnemies[numberSpawn].transform.position, Quaternion.identity);
        //    //    timeBetweenSpawn = 0;
        //    //}
        //}
    }
}
