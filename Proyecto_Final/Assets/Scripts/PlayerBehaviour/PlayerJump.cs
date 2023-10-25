using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script que recoge el comportamiento del jugador a la hora de saltar
/// </summary>
public class PlayerJump : MonoBehaviour
{
    /// <summary>
    /// Fuerza de salto
    /// </summary>
    [SerializeField] private float jumpForce;
    /// <summary>
    /// Referencia al layer Grund
    /// </summary>
    [SerializeField] private LayerMask layerGround;
    /// <summary>
    /// Referencia al layer Floor, distincion con la otra, por el nivel de lava. De esa manera, puede saltar en ambas
    /// </summary>
    [SerializeField] private LayerMask layerFloor;
    /// <summary>
    /// Deteccion del suelo
    /// </summary>
    [SerializeField] private Transform floorDetector;
    /// <summary>
    /// Referencia al Rigidbody
    /// </summary>
    private Rigidbody rb;
    /// <summary>
    /// Booleano que indica si puede saltar
    /// </summary>
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //Si puede saltar, y esta tocando con un objeto que este en la layer floor o ground
        if (isJumping && (Physics.OverlapSphere(floorDetector.position, 1f, layerGround).Length > 0 || Physics.OverlapSphere(floorDetector.position, 1f, layerFloor).Length > 0))
            rb.AddForce(Vector3.up * jumpForce);
    }

    /// <summary>
    /// Cuando es pulsado el boton de salto, llamara a este evento
    /// </summary>
    /// <param name="context"></param>
    public void IsJumping(InputAction.CallbackContext context)
    {
        isJumping = context.action.triggered;
    }
}
