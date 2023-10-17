using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private LayerMask layerFloor;
    [SerializeField] private Transform floorDetector;
    private Rigidbody rb;
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
        if (isJumping && (Physics.OverlapSphere(floorDetector.position, 1f, layerGround).Length > 0 || Physics.OverlapSphere(floorDetector.position, 1f, layerFloor).Length > 0))
            rb.AddForce(Vector3.up * jumpForce);
    }

    public void IsJumping(InputAction.CallbackContext context)
    {
        isJumping = context.action.triggered;
    }
}
