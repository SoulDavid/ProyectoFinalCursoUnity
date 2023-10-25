using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// Velocidad de la bala
    /// </summary>
    [SerializeField] private float speedBullet;
    /// <summary>
    /// Referencia al rigidbody
    /// </summary>
    [SerializeField] private Rigidbody rb;
    /// <summary>
    /// Daño que hace la bala
    /// </summary>
    [SerializeField] private int damageBullet;

    /// <summary>
    /// Pool de objetos de la bala
    /// </summary>
    private IObjectPool<BulletController> bulletPool;

    /// <summary>
    /// Recoge la pool de la balas, y guarda esa referencia
    /// </summary>
    /// <param name="_bulletPool"></param>
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
        //Se mueve con una velocidad hacia delante
        rb.velocity = transform.forward * speedBullet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si colisiona con objeto con tag Light
        if(collision.gameObject.CompareTag("Light"))
        {
            //Le hace ese daño
            collision.gameObject.GetComponent<LightHealth>().Damage(damageBullet);
        }
        //Si es enemigo
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Lo destruye
            collision.gameObject.GetComponent<EnemyIAController>().Destroy();
        }

        //Dice que ese objeto esta fuera de la bullet pool activamente, por lo que se desactiva el objeto
        bulletPool.Release(this);
    }
}
