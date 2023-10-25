using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwapGun : MonoBehaviour
{
    [Header("Frames de las armas")]
    [SerializeField] private Image gunFrame1;
    [SerializeField] private Image gunFrame2;
    [SerializeField] private Image gunFrame3;

    [Header("Objetos de las Armas")]
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;
    [SerializeField] private GameObject gun3;

    [Header("Referencias del canvas")]
    [SerializeField] private GameObject map;
    [SerializeField] private bool hideMap;
    [SerializeField] private bool hideGuns;
    [SerializeField] private GameObject gunsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetGun(0);
        gunFrame1.color = Color.yellow;
        gunFrame2.color = Color.black;
        gunFrame3.color = Color.black;
        gun1.SetActive(true);
        gun2.SetActive(false);
        gun3.SetActive(false);
        gunsCanvas.SetActive(false);
        map.SetActive(false);
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
            gunFrame3.color = Color.black;
            gun1.SetActive(true);
            gun2.SetActive(false);
            gun3.SetActive(false);
        }
    }

    public void ChangeToGun2(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            GameManager.Instance.SetGun(1);
            gunFrame2.color = Color.yellow;
            gunFrame1.color = Color.black;
            gunFrame3.color = Color.black;
            gun2.SetActive(true);
            gun1.SetActive(false);
            gun3.SetActive(false);
        }
    }

    public void ChangeToGun3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.SetGun(2);
            gunFrame3.color = Color.yellow;
            gunFrame2.color = Color.black;
            gunFrame1.color = Color.black;
            gun3.SetActive(true);
            gun2.SetActive(false);
            gun1.SetActive(false);
        }
    }

    public void HideOrShowGuns(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!hideGuns) hideGuns = true;
            else hideGuns = false;

            gunsCanvas.SetActive(hideGuns);
        }
    }

    public void UseObject(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!hideMap) hideMap = true;
            else hideMap = false;

            map.SetActive(hideMap);
        }
    }
}
