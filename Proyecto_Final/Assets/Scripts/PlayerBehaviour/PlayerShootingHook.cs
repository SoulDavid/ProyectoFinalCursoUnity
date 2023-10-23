using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerShootingHook : MonoBehaviour
{
    [Header("Hook Properties")]
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private GameObject hookSpawn;

    AimGun aimDirection;
    private bool isShooting = false;

    [SerializeField] private AudioSource hookSound;

    // Start is called before the first frame update
    void Start()
    {
        aimDirection = GetComponent<AimGun>();
    }

    public void OnPlayerShootHook(InputAction.CallbackContext context)
    {
        if(context.performed && GameManager.Instance.GetGun() == 2)
        {
            if(!isShooting)
            {
                hookSound.Play();
                var hook = Instantiate(hookPrefab, hookSpawn.transform.position, hookSpawn.transform.rotation);
                hook.GetComponent<HookController>().Init(hookSpawn.transform, this);
                isShooting = true;
            }
        }
    }

    public void ResetShooting()
    {
        isShooting = false;
    }
}
