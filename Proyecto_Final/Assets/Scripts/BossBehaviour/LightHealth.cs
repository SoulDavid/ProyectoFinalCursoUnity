using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float actualHealth;
    [SerializeField] private ParticleSystem damageParticleSystem;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int _damage)
    {
        if(isActive)
        {
            actualHealth -= _damage;
            damageParticleSystem.Play();

            if (actualHealth <= 0)
            {
                transform.parent.GetComponent<HealthEnemy>().Damage(10);
                transform.parent.GetComponent<RandomLight>().RandomLights();
                actualHealth = maxHealth;
            }
        }
    }

    public void SetIsActive(bool _isActive)
    {
        isActive = _isActive;
    }
}
