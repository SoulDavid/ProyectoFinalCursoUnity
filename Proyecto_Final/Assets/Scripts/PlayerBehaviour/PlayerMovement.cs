using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Movimiento del jugador
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Recoge el input del player
    /// </summary>
    private Vector2 inputMovement;
    /// <summary>
    /// Recoge el input de la rotacion
    /// </summary>
    private float inputRotation;
    /// <summary>
    /// Velocidad de movimiento
    /// </summary>
    [SerializeField] private float speed;
    /// <summary>
    /// Velocidad de rotacion
    /// </summary>
    [SerializeField] private float speedRotation;
    /// <summary>
    /// Referencia al layerGround para el dash
    /// </summary>
    [SerializeField] private LayerMask layerGround;

    [SerializeField] private LayerMask layerFloor;
    /// <summary>
    /// Detector del suelo
    /// </summary>
    [SerializeField] private Transform floorDetector;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * inputRotation * speedRotation);
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * inputMovement.y + 
            transform.right * inputMovement.x;

        rb.velocity = (direction.normalized * speed) + (transform.up * rb.velocity.y);
    }

    public void GetMovementPlayer(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();

        if(inputMovement != Vector2.zero)
        {
            //Recoge la velocidad de giro guardada en el gameManager
            speed = GameManager.Instance.GetSpeed();
        }
    }

    public void GetRotationPlayer(InputAction.CallbackContext context)
    {
        inputRotation = context.ReadValue<float>();

        speedRotation = GameManager.Instance.GetSpeedRotation();
    }

    public void GetDashInteraction(InputAction.CallbackContext context)
    {
        if(context.performed && (Physics.OverlapSphere(floorDetector.position, 0.6f, layerGround).Length > 0 || Physics.OverlapSphere(floorDetector.position, 0.6f, layerFloor).Length > 0))
        {
            speed *= 2;
        }
        else if(context.canceled && (Physics.OverlapSphere(floorDetector.position, 0.6f, layerGround).Length > 0 || Physics.OverlapSphere(floorDetector.position, 0.6f, layerFloor).Length > 0))
        {
            speed /= 2;
        }
    }
}
