using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class BulletPoolController : MonoBehaviour
{
    [SerializeField] private BulletController bulletScript;
    [SerializeField] private int maxBulletInAmmo = 8;
    private IObjectPool<BulletController> bulletPool;

    [SerializeField] private GameObject spawnBullet;
    [SerializeField] private Aim rayAim;

    private void Awake()
    {
        bulletPool = new ObjectPool<BulletController>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnDestroyBullet,
            maxSize: maxBulletInAmmo
            );
    }

    private BulletController CreateBullet()
    {
        BulletController bullet = Instantiate(bulletScript, spawnBullet.transform.position, rayAim.GetRayDirection());
        bullet.SetPool(bulletPool, rayAim);
        return bullet;
    }

    private void OnGet(BulletController bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = spawnBullet.transform.position;
        bullet.transform.LookAt(rayAim.GetRay().GetPoint(0));
    }

    private void OnRelease(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletController bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Shot(InputAction.CallbackContext context)
    {
        bulletPool.Get();
    }
}
