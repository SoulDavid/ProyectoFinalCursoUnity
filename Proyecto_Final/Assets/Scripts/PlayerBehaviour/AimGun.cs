using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//https://forum.unity.com/threads/moving-the-mouse-cursor-with-the-gamepad-joystick.900563/#:~:text=Just%20make%20sure%20to%20setup,used%20to%20click%20on%20buttons.
public class AimGun : MonoBehaviour
{
    /// <summary>
    /// Objeto que referencia al gameobject vacio donde spawneara el rayo
    /// </summary>
    public GameObject spawnRay;

    #region Variables que intervienen a la hora de apuntar
    #region Variables que almacenan el cache del raton
    /// <summary>
    /// Vector que recoge cuando el jugador ha tocado el joystick derecho
    /// </summary>
    Vector2 controllerInput;
    /// <summary>
    /// Posición para precisar el lugar donde se encuentra el raton
    /// </summary>
    Vector2 warpPosition;
    /// <summary>
    /// Variable que guarda la posicion del raton
    /// </summary>
    Vector2 mousePosition;
    #endregion

    [Tooltip("Contrarresta la tendencia del cursor a moverse más fácilmente en algunas direcciones")]
    [SerializeField] private Vector2 bias = new Vector2(0f, -1f);

    [Tooltip("Números más altos para un mayor movimiento del mouse al presionar el joystick. \n" +
         "Advertencia: el movimiento en diagonal pierde movimiento con poca sensibilidad al igual que pasa con el movimiento en 8 direcciones (<1000)")]
    [SerializeField] private Vector2 sensitivity = new Vector2(1000f, 1000f);

    /// <summary>
    /// Variable que almacena el siguiente frame
    /// </summary>
    private Vector2 overflow;

    /// <summary>
    /// Variable que guarda el rayo de Debug
    /// </summary>
    private Ray ray;

    [SerializeField] private GameObject[] guns;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Guardar la posición del mouse
        mousePosition = Mouse.current.position.ReadValue();
        //Valor para precisar la posición ideal del raton
        warpPosition = mousePosition + bias + sensitivity * Time.deltaTime * controllerInput;
        //Mantener el cursor en la pantalla de juego (de esta manera no se sale de los limites)
        overflow = new Vector2(warpPosition.x % 1, warpPosition.y % 1);
        //Asignamos la posición al raton para que se mueva
        Mouse.current.WarpCursorPosition(warpPosition);

        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        guns[GameManager.Instance.GetGun()].transform.LookAt(ray.GetPoint(10));

        Debug.DrawRay(spawnRay.transform.position, ray.direction * 200, Color.green);
    }

    public void GetInputControllerAim(InputAction.CallbackContext context)
    {
        controllerInput = context.ReadValue<Vector2>();
    }

    public Ray GetRay()
    {
        return ray;
    }
}
