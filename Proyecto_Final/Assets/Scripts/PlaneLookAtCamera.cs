using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLookAtCamera : MonoBehaviour
{
    private Camera cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraObject.transform.position);
        transform.forward = -transform.forward;
    }
}
