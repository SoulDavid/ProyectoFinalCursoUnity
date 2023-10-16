using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputMovement;
    private float inputRotation;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask layerGround;
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
        transform.Rotate(Vector3.up * inputRotation);
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
    }

    public void GetRotationPlayer(InputAction.CallbackContext context)
    {
        inputRotation = context.ReadValue<float>();
    }

    public void GetDashInteraction(InputAction.CallbackContext context)
    {
        if(context.performed && Physics.OverlapSphere(floorDetector.position, 0.6f, layerGround).Length > 0)
        {
            speed *= 2;
        }
        else if(context.canceled && Physics.OverlapSphere(floorDetector.position, 0.6f, layerGround).Length > 0)
        {
            speed /= 2;
        }
    }
}
