using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Script que recoge el comportamiento de Hook
/// </summary>
public class HookController : MonoBehaviour
{
    /// <summary>
    /// Tags a comprobar cuando colisiona
    /// </summary>
    [SerializeField] private string[] tagsToCheck;
    /// <summary>
    /// Velocidad, velocidad de retorno, rango y rango para parar 
    /// </summary>
    [SerializeField] private float speed, returnSpeed, range, stopRange;

    /// <summary>
    /// Referencias al caster, y con lo que ha colisionado
    /// </summary>
    private Transform caster, collidedWidth;

    /// <summary>
    /// Referencia al script del disparo de gancho del personaje
    /// </summary>
    private PlayerShootingHook scriptPlayerHook;
    /// <summary>
    /// Referencia al LineRenderer
    /// </summary>
    [SerializeField] private LineRenderer line;
    /// <summary>
    /// Tiempo limite que esta el gancho en pantalla
    /// </summary>
    [SerializeField] private float timeLimit = 3f;
    /// <summary>
    /// Tiempo que lleva en patalla
    /// </summary>
    private float actualTime;
    /// <summary>
    /// Si ha colisionado
    /// </summary>
    private bool hasCollided;
    
    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetChild(1).GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Inicializacion del hook que se llamara nada mas crearse
    /// </summary>
    /// <param name="_caster"></param>
    /// <param name="_scriptPlayerHook"></param>
    public void Init(Transform _caster, PlayerShootingHook _scriptPlayerHook)
    {
        caster = _caster;
        scriptPlayerHook = _scriptPlayerHook;
        actualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Recoge el tiempo que esta por pantalla
        actualTime += Time.deltaTime;

        //Si tiene caster
        if (caster)
        {
            //Creacion del line renderer
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);

            //Si ha colisionado
            if (hasCollided)
            {
                //Mirara hacia el caster para que retorne
                transform.LookAt(caster);

                //Cogemos la distancia
                float dist = Vector3.Distance(transform.position, caster.position);

                //Si la distancia es menor que el rango en el que se tiene que parar
                if (dist < stopRange)
                {
                    //Hara que el jugador pueda volver a lanzar el gancho
                    scriptPlayerHook.ResetShooting();
                    //Destruye el gancho
                    Destroy(gameObject);
                }
            }
            else
            {
                //Calcula la distancia
                float dist = Vector3.Distance(transform.position, caster.position);

                //Si la distancia es mayor que el rango o el tiempo ha superado su limite
                if (dist > range || actualTime >= timeLimit)
                {
                    //Collision igual a null para que retorne con una velocidad de retorno
                    Collision(null);
                }
            }

            //Siempre se movera para adelante
            transform.Translate(Vector3.forward * speed * Time.deltaTime);


            if (collidedWidth) collidedWidth.transform.position = transform.position;
        }
        else
        {
            //Para que el jugador pueda volver a lanzar el hook
            scriptPlayerHook.ResetShooting();
            //Destruye el objeto
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.gameObject);
        }
    }

    /// <summary>
    /// FUncion para cuando colisiona con algo
    /// </summary>
    /// <param name="col"></param>
    private void Collision(GameObject col)
    {
        speed = returnSpeed;
        hasCollided = true;

        if(col)
        {
            transform.position = col.transform.position;
            collidedWidth = col.transform;
        }
    }
}
