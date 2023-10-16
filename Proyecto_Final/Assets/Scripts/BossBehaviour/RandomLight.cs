using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLight : MonoBehaviour
{
    [SerializeField] private List<GameObject> lightsMaterials;

    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    private HealthEnemy enemyHealth;
    private int actualNumber = -1;
    int new_random_number = -1;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<HealthEnemy>();
        RandomLights();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetLights()
    {
        foreach (GameObject meshLightMaterial in lightsMaterials)
        {
            meshLightMaterial.GetComponent<MeshRenderer>().material = redMaterial;
            meshLightMaterial.GetComponent<LightHealth>().SetIsActive(false);
        }
    }

    public void RandomLights()
    {
        if(enemyHealth.getActualHealth() > enemyHealth.getMaxHealth() / 2)
        {
            ResetLights();

            new_random_number = Random.Range(0, lightsMaterials.Count);

            //Para que no vuelva a ser la misma luz
            while (new_random_number == actualNumber)
            {
                new_random_number = Random.Range(0, lightsMaterials.Count);
            }

            lightsMaterials[new_random_number].GetComponent<MeshRenderer>().material = greenMaterial;
            lightsMaterials[new_random_number].GetComponent<LightHealth>().SetIsActive(true);

            actualNumber = new_random_number;
        }
        else
        {
            ResetLights();
        }
    }
}
