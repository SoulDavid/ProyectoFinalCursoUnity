using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float actualHealth;
    [SerializeField] private Transform phase2Position;
    [SerializeField] private GameObject spawnEnemies;
    private bool isInPhase2;

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;
        isInPhase2 = false;
        spawnEnemies.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float _damage)
    {
        actualHealth -= _damage;

        if(actualHealth <= 80/*maxHealth/2*/ && !isInPhase2)
        {
            transform.position = Vector3.MoveTowards(transform.position, phase2Position.position, 2 * Time.deltaTime);
            spawnEnemies.SetActive(true);
            GetComponent<Phase2BossEnemiesSpawn>().enabled = true;
        }
        //Comprobar que la vida del enemigo es menor a 0, y saltar la muerte del personaje
    }

    public float getActualHealth()
    {
        return actualHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
