using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float actualHealth;
    [SerializeField] private Transform phase2Position;
    [SerializeField] private GameObject spawnEnemies;
    [SerializeField] private RandomLight randomLightScript;
    [SerializeField] private GameObject wall;
    [SerializeField] Animator anim;
    private Vector3 start_position;
    private bool isInPhase2;

    // Start is called before the first frame update
    void Awake()
    {
        start_position = transform.position;
        actualHealth = maxHealth;
        isInPhase2 = false;
        spawnEnemies.SetActive(false);
        randomLightScript = GetComponent<RandomLight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float _damage)
    {
        actualHealth -= _damage;

        if(actualHealth == maxHealth / 2 && !isInPhase2)
        {
            transform.position = phase2Position.position;
            wall.SetActive(true);
            isInPhase2 = true;
        }

        if(actualHealth <= 0)
        {
            GetComponent<Phase2BossEnemiesSpawn>().enabled = false;
            anim.SetBool("death", true);
        }
        //Comprobar que la vida del enemigo es menor a 0, y saltar la muerte del personaje
    }

    public void StartEnemy()
    {
       randomLightScript.RandomLights();
    }

    public void setToPhase2()
    {
        spawnEnemies.SetActive(true);
        GetComponent<Phase2BossEnemiesSpawn>().enabled = true;
    }

    public float getActualHealth()
    {
        return actualHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public bool get_isInPhase2()
    {
        return isInPhase2;
    }

    public void Restart()
    {
        actualHealth = maxHealth;
        isInPhase2 = false;
        spawnEnemies.SetActive(false);
        transform.position = start_position;
        GetComponent<Phase2BossEnemiesSpawn>().enabled = false;
    }
}
