using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pequeña IA del controlador
/// </summary>
public class EnemyIAController : MonoBehaviour
{
    /// <summary>
    /// Recoge el transform del player
    /// </summary>
    Transform player;

    /// <summary>
    /// Velocidad a la que se mueven los personajes
    /// </summary>
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        //Encuentra el objeto en la escena del player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Para que se mueva hacia el player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        //Si la vida del boss es menor a 0 que se destruyan todos o si se ha reseteado, de esta manera no quedan enemigos en escena
        if (transform.parent.GetComponent<HealthEnemy>().getActualHealth() <= 0 ||
            !transform.parent.GetComponent<HealthEnemy>().get_isInPhase2()) Destroy(this.gameObject);
    }

    //Se destruye cuando recibe daño
    public void Destroy()
    {
        //Le hace 5 puntos de daño al enemigo
        transform.parent.GetComponent<HealthEnemy>().Damage(5);
        //Se destruye
        Destroy(this.gameObject);
    }
}
