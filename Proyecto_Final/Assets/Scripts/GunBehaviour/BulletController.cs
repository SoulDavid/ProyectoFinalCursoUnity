using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private Rigidbody rb;

    private IObjectPool<BulletController> bulletPool;
    private PlayerShot casterRayDirection;

    public void SetPool(IObjectPool<BulletController> _bulletPool)//, PlayerShot direction)
    {
        bulletPool = _bulletPool;
        //casterRayDirection = direction;
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

    private void OnBecameInvisible()
    {
        bulletPool.Release(this);
    }
}
