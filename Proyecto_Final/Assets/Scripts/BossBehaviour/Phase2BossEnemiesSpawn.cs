using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que recoge la fase 2 del boss
/// </summary>
public class Phase2BossEnemiesSpawn : MonoBehaviour
{
    /// <summary>
    /// Recoge todos los spawns de los enemigos
    /// </summary>
    [SerializeField] private GameObject[] spawnEnemies;
    /// <summary>
    /// Prefab del enemigo
    /// </summary>
    [SerializeField] private GameObject enemyPrefab;
    /// <summary>
    /// Tiempo entre spawn
    /// </summary>
    [SerializeField] private float timeBetweenSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        //Si esta activado el script, que repita el metodo de spawn
        InvokeRepeating("SpawnEnemies", timeBetweenSpawn, timeBetweenSpawn);
    }

    private void OnDisable()
    {
        //Si esta desactivado, se cancela el invoke
        CancelInvoke();
    }

    /// <summary>
    /// Metodo para spawnear enemigos
    /// </summary>
    private void SpawnEnemies()
    {
        //Si el boss esta en la fase 2
        if(GetComponent<HealthEnemy>().get_isInPhase2())
        {
            //Spawnea un enemigo en una posicion aleatoria
            int numberSpawn = Random.Range(0, spawnEnemies.Length);
            Instantiate(enemyPrefab, spawnEnemies[numberSpawn].transform.position, Quaternion.identity, this.transform);
        }
    }
}
