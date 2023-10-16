using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookController : MonoBehaviour
{
    [SerializeField] private string[] tagsToCheck;
    [SerializeField] private float speed, returnSpeed, range, stopRange;

    private Transform caster, collidedWidth;
    [SerializeField] private LineRenderer line;
    private bool hasCollided;
    
    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetChild(1).GetComponent<LineRenderer>();
    }

    public void Init(Transform _caster)
    {
        caster = _caster;
    }

    // Update is called once per frame
    void Update()
    {
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);

            if (hasCollided)
            {
                transform.LookAt(caster);

                float dist = Vector3.Distance(transform.position, caster.position);

                if (dist < stopRange)
                    Destroy(gameObject);
            }
            else
            {
                float dist = Vector3.Distance(transform.position, caster.position);

                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (collidedWidth) collidedWidth.transform.position = transform.position;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.gameObject);
        }
    }

    private void Collision(GameObject col)
    {
        speed = returnSpeed;
        hasCollided = true;

        if(col)
        {
            transform.position = col.transform.position;
            collidedWidth = col.transform;
        }
    }
}
