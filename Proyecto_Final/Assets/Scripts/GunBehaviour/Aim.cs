using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//https://forum.unity.com/threads/moving-the-mouse-cursor-with-the-gamepad-joystick.900563/#:~:text=Just%20make%20sure%20to%20setup,used%20to%20click%20on%20buttons.

public class Aim : MonoBehaviour
{
    public GameObject spawnRay;
    [SerializeField] private GameObject gunsight;
    Vector2 controllerInput;
    Vector2 warpPosition;
    Vector2 mousePosition;

    [SerializeField] private Vector2 bias = new Vector2(0f, -1f);
    [Tooltip("Higher numbers for more mouse movement on joystick press." +
         "Warning: diagonal movement lost at lower sensitivity (<1000)")]

    [SerializeField] private Vector2 sensitivity = new Vector2(1500f, 1500f);

    private Vector2 overflow;

    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       // if (controllerInput.magnitude < 0.1f) return;
       //Guardar la posición del mouse
        mousePosition = Mouse.current.position.ReadValue();
        //Valor para precisar la posición ideal del raton
        warpPosition = mousePosition + bias + sensitivity * Time.deltaTime * controllerInput;
        //Mantener el cursor en la pantalla de juego (de esta manera no se sale de los limites)
        overflow = new Vector2(warpPosition.x % 1, warpPosition.y % 1);
        //Asignamos la posición al raton para que se mueva
        Mouse.current.WarpCursorPosition(warpPosition);

        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        transform.LookAt(ray.GetPoint(10));

        Debug.DrawRay(spawnRay.transform.position, ray.direction * 200, Color.green);
    }

    public Quaternion GetRayDirection()
    {
        return new Quaternion(ray.direction.x, ray.direction.y, ray.direction.z, 1);
    }

    public Ray GetRay()
    {
        return ray;
    }

    private void FixedUpdate()
    {

    }

    public void GetInputControllerAim(InputAction.CallbackContext context)
    {
        controllerInput = context.ReadValue<Vector2>();
    }
}
