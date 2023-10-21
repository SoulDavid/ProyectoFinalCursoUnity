using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    /// <summary>
    /// Imagen del logo para ocultarlo
    /// </summary>
    [Tooltip("Imagen del logo ")]
    public Image logoImage;

    /// <summary>
    /// Texto desarrollado por...
    /// </summary>
    [Tooltip("Texto inicial")]
    public Text InitalText;

    /// <summary>
    /// Texto donde se va a poner la información
    /// </summary>
    [Tooltip("Cuadro de texto de los créditosk")]
    public Text teams;

    // public int titleSize;

    /// <summary>
    /// Tamaño del texto
    /// </summary>
    [Tooltip("Tamaño del texto")]
    public int TextSize;

    /// <summary>
    /// Tiempo para comenzar
    /// </summary>
    [Tooltip("Tiempo para empezar los créditos")]
    public float startTime;

    /// <summary>
    /// tiempo de espera entre textos
    /// </summary>
    [Tooltip("Tiempo entre textos")]
    public float textTime;

    /// <summary>
    /// textos que se van a mostrar
    /// </summary>
    [Tooltip("Textos a mostrar")]
    [TextArea(15, 20)]
    public string[] texts;

    /// <summary>
    /// indice del array de textos
    /// </summary>
    int index = 0;

    void Start()
    {
        //desactivamos el texto inicial
        Invoke("DisableText", startTime);

        //Ajustes del texto
        teams.text = texts[index];

        teams.fontSize = TextSize;
    }

    /// <summary>
    /// Corrutina para cambiar el texto del cuadro de texto
    /// </summary>
    IEnumerator ShowTexts(GameObject objectToShow, float time)
    {
        while (true)
        {
            //mostramos el gameobject
            if (!objectToShow.activeSelf)
                objectToShow.SetActive(true);

            //mostramos el siguiente texto si no es null
            if (index + 1 < texts.Length)
            {
                index++;
                yield return new WaitForSeconds(time);
                teams.text = texts[index];
            }
            //Si termina carga el menú
            else
            {
                yield return new WaitForSeconds(time);
                SceneManager.LoadScene("Menu");
            }

        }

    }

    /// <summary>
    /// Método para desactivar el texto y comenzar la corrutina
    /// </summary>
    void DisableText()
    {
        logoImage.gameObject.SetActive(false);
        InitalText.gameObject.SetActive(false);

        StartCoroutine(ShowTexts(teams.gameObject, textTime));

    }
}
