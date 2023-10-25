using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    /// <summary>
    /// Objeto que recoge el controlador para que se abra la puerta
    /// </summary>
    [SerializeField] private GameObject doorController;
    /// <summary>
    /// Objeto que recoge el boss, y de esa manera lo cargamos
    /// </summary>
    [SerializeField] private GameObject bossObject;

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
        //Si hace colision con el jugador
        if(other.gameObject.CompareTag("Player"))
        {
            //Activa el metodo para que se mueva
            doorController.GetComponent<PlatformBehaviour>().SetIsActiveToMove();
            //Carga el personaje
            bossObject.SetActive(true);
            //Empieza el boss a actuar
            bossObject.GetComponent<HealthEnemy>().StartEnemy();
        }
    }
}
