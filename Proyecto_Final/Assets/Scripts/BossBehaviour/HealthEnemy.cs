using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script que contiene la vida del enemigo
/// </summary>
public class HealthEnemy : MonoBehaviour
{
    /// <summary>
    /// La vida maxima del boss
    /// </summary>
    [SerializeField] private float maxHealth;
    /// <summary>
    /// Vida actual del boss
    /// </summary>
    [SerializeField] private float actualHealth;
    /// <summary>
    /// La posición a la que se mueve cuando toca la fase 2 del boss
    /// </summary>
    [SerializeField] private Transform phase2Position;
    /// <summary>
    /// Recoge el gameobject que tiene todos los spawns del enemigo para activarlos
    /// </summary>
    [SerializeField] private GameObject spawnEnemies;
    /// <summary>
    /// Recoge el script de las luces para que se inicie la primera fase
    /// </summary>
    [SerializeField] private RandomLight randomLightScript;
    /// <summary>
    /// Recoge el objeto de la pared que divide la fase 1 de la fase 2
    /// </summary>
    [SerializeField] private GameObject wall;
    /// <summary>
    /// Recoge el Animator
    /// </summary>
    [SerializeField] Animator anim;
    /// <summary>
    /// La posicion a resetear el boss
    /// </summary>
    private Vector3 start_position;
    /// <summary>
    /// Booleano que indica que esta en la fase 2
    /// </summary>
    private bool isInPhase2;

    [SerializeField] private GameObject[] dialogues;

    // Start is called before the first frame update
    void Awake()
    {
        //Guarda la posicion inicial
        start_position = transform.position;
        //Inicializacion de variables
        actualHealth = maxHealth;
        isInPhase2 = false;
        spawnEnemies.SetActive(false);
        randomLightScript = GetComponent<RandomLight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Funcion cuando le hacen daño
    /// </summary>
    /// <param name="_damage"></param>
    public void Damage(float _damage)
    {
        //Vida actual reducida por el daño que entra
        actualHealth -= _damage;

        //Si la vida es igual a la mitad de la vida maxima y no esta en la fase 2
        if(actualHealth == maxHealth / 2 && !isInPhase2)
        {
            //Se echa para atras
            transform.position = phase2Position.position;
            //Se activa la pared
            wall.SetActive(true);
            //Booleano para que no se repita continuamente esta iteraccion
            isInPhase2 = true;
        }

        //Si la vida es menor o igual a 0
        if(actualHealth <= 0)
        {
            StartCoroutine(EndGame());
        }
    }

    /// <summary>
    /// Inicio del enemigo
    /// </summary>
    public void StartEnemy()
    {
        //Se activa el metodo de hacer aleatorias las luces
        StartCoroutine(StartEnemyCoroutine());
    }

    private IEnumerator StartEnemyCoroutine()
    {
        dialogues[0].SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogues[0].SetActive(false);
        dialogues[1].SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogues[1].SetActive(false);
        dialogues[2].SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogues[2].SetActive(false);
        MusicManager.Instance.SwapMusic(2);
        randomLightScript.RandomLights();
    }

    /// <summary>
    /// Segunda fase del enemigo
    /// </summary>
    public void setToPhase2()
    {
        //Activa los spawns
        spawnEnemies.SetActive(true);
        //Activa el script de la fase 2
        GetComponent<Phase2BossEnemiesSpawn>().enabled = true;
    }

    /// <summary>
    /// Metodo para resetear el enemigo
    /// </summary>
    public void Restart()
    {
        actualHealth = maxHealth;
        isInPhase2 = false;
        spawnEnemies.SetActive(false);
        transform.position = start_position;
        GetComponent<Phase2BossEnemiesSpawn>().enabled = false;
    }

    /// <summary>
    /// Metodo para la muerte del boss
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndGame()
    {
        //Deja de spawnear enemigos desactivando el script
        GetComponent<Phase2BossEnemiesSpawn>().enabled = false;
        //Ejecuta la animacion de muerte
        anim.SetBool("death", true);
        yield return new WaitForSeconds(3f);
        //Se ponen los creditos
        SceneManager.LoadScene("Credits");
    }

    #region Get Y Setters
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
    #endregion
}
