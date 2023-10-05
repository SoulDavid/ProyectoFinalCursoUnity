using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwapGun : MonoBehaviour
{
    [SerializeField] private Image gunFrame1;
    [SerializeField] private Image gunFrame2;

    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;

    // Start is called before the first frame update
    void Start()
    {
        gunFrame1.color = Color.yellow;
        gunFrame2.color = Color.black;
        gun1.SetActive(true);
        gun2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToGun1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.SetGun(0);
            gunFrame1.color = Color.yellow;
            gunFrame2.color = Color.black;
            gun1.SetActive(true);
            gun2.SetActive(false);
        }
    }

    public void ChangeToGun2(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            GameManager.Instance.SetGun(1);
            gunFrame2.color = Color.yellow;
            gunFrame1.color = Color.black;
            gun2.SetActive(true);
            gun1.SetActive(false);
        }
    }
}
