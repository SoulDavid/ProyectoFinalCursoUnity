using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para controlar la pared del boss que aparece cuando vamos a pasar de la fase 1 a la fase 2
/// </summary>
public class ButtonBossWall : MonoBehaviour
{
    /// <summary>
    /// Objeto que recoge la pared
    /// </summary>
    [SerializeField] private GameObject platforms;
    /// <summary>
    /// Referencia al script que recoge la vida del enemigo
    /// </summary>
    [SerializeField] private HealthEnemy enemyScript;

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
        //Si el boton colisiona el hook
        if (other.gameObject.CompareTag("Hook"))
        {
            //Pasamos a la fase 2
            enemyScript.setToPhase2();
            //Desactivamos la pared
            transform.parent.gameObject.SetActive(false);
        }
    }
}
