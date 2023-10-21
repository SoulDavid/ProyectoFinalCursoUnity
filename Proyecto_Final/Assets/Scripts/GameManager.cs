using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] private int gunNumber;
    [SerializeField] private float speed = 5f;
    [Range(0,1)] [SerializeField] private float speedRotation = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGun()
    {
        return gunNumber;
    }

    public void SetGun(int _number)
    {
        gunNumber = _number;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeedRotation(float _speedRotation)
    {
        speedRotation = _speedRotation;
    }

    public float GetSpeedRotation()
    {
        return speedRotation;
    }
}
