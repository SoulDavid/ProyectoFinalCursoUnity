using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que recoge la vida de las luces de la fase 1
/// </summary>
public class LightHealth : MonoBehaviour
{
    /// <summary>
    /// Recoge la vida maxima
    /// </summary>
    [SerializeField] private float maxHealth;
    /// <summary>
    /// Recoge la vida actual
    /// </summary>
    [SerializeField] private float actualHealth;
    /// <summary>
    /// Recoge el sistema de particulas para cuando le hagan daño
    /// </summary>
    [SerializeField] private ParticleSystem damageParticleSystem;
    /// <summary>
    /// Booleano que indica si esta activa esa luz
    /// </summary>
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //Reseteo de la vida
        actualHealth = maxHealth;
    }

    /// <summary>
    /// Funcion cuando le hacen daño
    /// </summary>
    /// <param name="_damage"></param>
    public void Damage(int _damage)
    {
        //Para recibir daño tiene que estar activa
        if(isActive)
        {
            //Reduce la vida 
            actualHealth -= _damage;
            //Se activa el sistema de particulas
            damageParticleSystem.Play();

            //Si la vida actual es menor a 0
            if (actualHealth <= 0)
            {
                //Hace daño al boss
                transform.parent.GetComponent<HealthEnemy>().Damage(10);
                //Se vuelve a ejecutar un random 
                transform.parent.GetComponent<RandomLight>().RandomLights();
                //Resetea la vida
                actualHealth = maxHealth;
            }
        }
    }

    /// <summary>
    /// Poner activa la luz
    /// </summary>
    /// <param name="_isActive"></param>
    public void SetIsActive(bool _isActive)
    {
        isActive = _isActive;
    }
}
