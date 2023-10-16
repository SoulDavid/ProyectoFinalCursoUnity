using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int damageBullet;

    private IObjectPool<BulletController> bulletPool;
    private PlayerShot casterRayDirection;

    public void SetPool(IObjectPool<BulletController> _bulletPool)
    {
        bulletPool = _bulletPool;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speedBullet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Light"))
        {
            collision.gameObject.GetComponent<LightHealth>().Damage(damageBullet);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        bulletPool.Release(this);
    }

    //private void OnBecameInvisible()
    //{
    //    bulletPool.Release(this);
    //}
}
