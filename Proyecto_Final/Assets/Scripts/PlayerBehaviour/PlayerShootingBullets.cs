using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShootingBullets : MonoBehaviour
{
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
    [SerializeField] private AudioSource bulletSound;
    [SerializeField] private AudioClip rechargeSound;
    [SerializeField] private AudioClip shootBulletSound;
    #endregion

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
    }

    public void OnPlayerShootingBullets(InputAction.CallbackContext context)
    {
        if(context.performed && GameManager.Instance.GetGun() == 1)
        {
            if (Time.time > timeLastShoot + cadency && actualAmmo > 0)
            {
                bulletSound.Play();
                shotBulletParticle.Play();
                bulletPool.Get();
                timeLastShoot = Time.time;
                actualAmmo--;
                textAmmo.text = actualAmmo.ToString() + " / " + maxAmmo.ToString();
            }
            else if (actualAmmo == 0)
            {
                StartCoroutine(Recharge());
            }
        }
    }

    private IEnumerator Recharge()
    {
        bulletSound.clip = rechargeSound;
        bulletSound.Play();
        yield return new WaitForSeconds(.4f);
        bulletSound.clip = shootBulletSound;
        actualAmmo = maxAmmo;
    }

    public void Recharge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Recharge());
        }
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
