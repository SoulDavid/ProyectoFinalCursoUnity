using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotationCamera : MonoBehaviour
{
    private Vector2 inputRotation;

    [SerializeField] private float speed;
    [SerializeField] private float limitRotationX;
    [SerializeField] private float limitRotationY;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * inputRotation.x);

        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0f;

        if (rot.x > 20 && rot.x < 180) rot.x = 20;
        if (rot.x < 340 && rot.x > 180) rot.x = 340;

        transform.rotation = Quaternion.Euler(rot);
    }

    public void GetRotationCamera(InputAction.CallbackContext context)
    {
        inputRotation = context.ReadValue<Vector2>();
        Debug.Log(inputRotation);
    }
}
