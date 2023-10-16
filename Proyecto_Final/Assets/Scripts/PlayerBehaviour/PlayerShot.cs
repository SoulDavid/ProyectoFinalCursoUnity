using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;
using TMPro;

public class PlayerShot : MonoBehaviour
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
    #endregion

    #region Variables de BulletGun
    [Header("Bullet Gun Properties")]
    /// <summary>
    /// Objeto que referencia al gameobject vacio donde spawneara la bala
    /// </summary>
    [SerializeField] private GameObject spawnBulletGun;

    private float timeLastShoot;
    [SerializeField] private float cadency;

    [SerializeField] private BulletController bullet;
    [SerializeField] private int maxAmmo = 5;
    private IObjectPool<BulletController> bulletPool;
    private int actualAmmo;
    [SerializeField] private ParticleSystem shotBulletParticle;
    #endregion

    [Header("Hook Properties")]
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private GameObject hookSpawn;

    AimGun aimDirection;

    [SerializeField] private TMP_Text textAmmo;

    private void Awake()
    {
        bulletPool = new ObjectPool<BulletController>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnDestroyBullet,
            maxSize: maxAmmo
            );
    }

    // Start is called before the first frame update
    void Start() 
    {
        aimDirection = GetComponent<AimGun>();
        actualAmmo = maxAmmo;

        textAmmo.text = "\u221E" + " / " + "\u221E";
    }

    // Update is called once per frame
    void Update() { }

    private void FixedUpdate() { }

    public void OnPlayerShotRayGun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            switch(GameManager.Instance.GetGun())
            {
                case 0:
                    StartCoroutine(shotRayGun());
                    
                    break;

                case 1:
                    if (Time.time > timeLastShoot + cadency && actualAmmo > 0)
                    {
                        shotBulletParticle.Play();
                        bulletPool.Get();
                        timeLastShoot = Time.time;
                        actualAmmo--;
                        textAmmo.text = actualAmmo.ToString() + " / " + maxAmmo.ToString();
                    }
                    else if(actualAmmo == 0)
                    {
                        Recharge();
                    }

                    break;

                case 2:
                    var hook = Instantiate(hookPrefab, hookSpawn.transform.position, hookSpawn.transform.rotation);
                    hook.GetComponent<HookController>().Init(hookSpawn.transform);
                    Debug.Log("Se ha creado");
                    break;
            }
        }
    }

    private IEnumerator shotRayGun()
    {
        shotRayParticles.Play();
        yield return new WaitForSeconds(.3f);
        RaycastHit hit;
        Debug.DrawRay(spawnRay.transform.position, aimDirection.GetRay().direction * 200, Color.red);

        if (Physics.Raycast(spawnRay.transform.position, aimDirection.GetRay().direction, out hit, 200))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.gameObject.CompareTag("MetalWall"))
            {
                GameObject sparkles = Instantiate(sparkleHitCollisionWithMetalWall, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(sparkles, 0.4f);
            }
            if(hit.transform.gameObject.CompareTag("Light"))
            {
                hit.transform.gameObject.GetComponent<LightHealth>().Damage(damageRayGun);
            }
            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void Recharge()
    {
        actualAmmo = maxAmmo;
    }

    public void Recharge(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Recharge();
        }
    }

    public void UpdateCanvasAmmoRayGun(InputAction.CallbackContext context)
    {    
        textAmmo.text = "\u221E" + " / " + "\u221E";
    }

    public void UpdateCanvasAmmoBulletGun(InputAction.CallbackContext context)
    {
        textAmmo.text = actualAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    #region BulletPool Functions
    private BulletController CreateBullet()
    {
        BulletController _bullet = Instantiate(bullet, spawnBulletGun.transform.position, transform.rotation);
        _bullet.SetPool(bulletPool);
        return _bullet;
    }

    private void OnGet(BulletController _bullet)
    {
        _bullet.gameObject.SetActive(true);
        _bullet.transform.position = spawnBulletGun.transform.position;
        _bullet.transform.rotation = spawnBulletGun.transform.rotation;
    }

    private void OnRelease(BulletController _bullet)
    {
        _bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletController _bullet)
    {
        Destroy(_bullet.gameObject);
    }
    #endregion
}
