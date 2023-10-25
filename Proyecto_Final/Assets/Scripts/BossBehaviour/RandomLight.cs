using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que pone aleatorias las luces
/// </summary>
public class RandomLight : MonoBehaviour
{
    /// <summary>
    /// Lista para recoger las luces
    /// </summary>
    [SerializeField] private List<GameObject> lightsMaterials;

    /// <summary>
    /// Material verde y rojo para las luces 
    /// </summary>
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    /// <summary>
    /// Referencia al script del enemigo
    /// </summary>
    [SerializeField] private HealthEnemy enemyHealth;

    /// <summary>
    /// Numero actual de la luz activa
    /// </summary>
    private int actualNumber = -1;
    /// <summary>
    /// Nuevo numero que representa la siguiente luz que estara activa
    /// </summary>
    int new_random_number = -1;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<HealthEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Metodo para resetear las luces
    /// </summary>
    private void ResetLights()
    {
        //Por cada luz 
        foreach (GameObject meshLightMaterial in lightsMaterials)
        {
            //Las desactivamos todas
            meshLightMaterial.GetComponent<MeshRenderer>().material = redMaterial;
            meshLightMaterial.GetComponent<LightHealth>().SetIsActive(false);
        }
    }

    /// <summary>
    /// Metodo para Randomizar las luces
    /// </summary>
    public void RandomLights()
    {
        //Si la vida del enemigo es mayor que la mitad de la vida
        if(enemyHealth.getActualHealth() > enemyHealth.getMaxHealth() / 2)
        {
            //Reseteamos todas las luces
            ResetLights();

            //Nuevo numero random para la siguiente luz
            new_random_number = Random.Range(0, lightsMaterials.Count);

            //Para que no vuelva a ser la misma luz
            while (new_random_number == actualNumber)
            {
                new_random_number = Random.Range(0, lightsMaterials.Count);
            }

            //Se pone la luz seleccionada activa y el material verde
            lightsMaterials[new_random_number].GetComponent<MeshRenderer>().material = greenMaterial;
            lightsMaterials[new_random_number].GetComponent<LightHealth>().SetIsActive(true);

            //Se setea al actual number
            actualNumber = new_random_number;
        }
        else
        {
            //Si no se quedan desactivadas todas
            ResetLights();
        }
    }
}
