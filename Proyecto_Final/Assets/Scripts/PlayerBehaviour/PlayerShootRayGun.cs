using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShootRayGun : MonoBehaviour
{
    #region Variables de RayGun
    [Header("RayGun Properties")]
    /// <summary>
    /// Objeto que referencia al gameobject vacio donde spawneara el rayo
    /// </summary>
    [SerializeField] private GameObject spawnRay;
    /// <summary>
    /// Variable que guarda el sistema de particulas que se ejecuta cuando colisiona contra una pared de metal
    /// </summary>
    [SerializeField] private GameObject sparkleHitCollisionWithMetalWall;

    [SerializeField] private ParticleSystem shotRayParticles;
    [SerializeField] private int damageRayGun;

    private LineRenderer lineRenderer;
    [SerializeField] private AudioSource laserSound;
    #endregion

    AimGun aimDirection;
    [SerializeField] private TMP_Text textAmmo;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        aimDirection = GetComponent<AimGun>();

        textAmmo.text = "\u221E" + " / " + "\u221E";
    }

    public void OnPlayerShootRayGun(InputAction.CallbackContext context)
    {
        if(context.performed && GameManager.Instance.GetGun() == 0)
        {
            StartCoroutine(shotRayGun());
        }
    }

    private IEnumerator shotRayGun()
    {
        shotRayParticles.Play();

        yield return new WaitForSeconds(.3f);
        laserSound.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, spawnRay.transform.position);

        RaycastHit hit;
        Debug.DrawRay(spawnRay.transform.position, aimDirection.GetRay().direction * 200, Color.red);

        if (Physics.Raycast(spawnRay.transform.position, aimDirection.GetRay().direction, out hit, 200))
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.transform.gameObject.CompareTag("MetalWall"))
            {
                GameObject sparkles = Instantiate(sparkleHitCollisionWithMetalWall, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(sparkles, 1f);
            }
            if (hit.transform.gameObject.CompareTag("Light"))
            {
                hit.transform.gameObject.GetComponent<LightHealth>().Damage(damageRayGun);
            }
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<EnemyIAController>().Destroy();
            }
        }

        yield return new WaitForSeconds(.2f);
        lineRenderer.enabled = false;
    }

    public void UpdateCanvasAmmoInfinite(InputAction.CallbackContext context)
    {
        textAmmo.text = "\u221E" + " / " + "\u221E";
    }
}
