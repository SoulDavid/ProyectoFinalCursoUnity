using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private bool FightStarts;
    [SerializeField] private Image imageTimer;
    private float currentTimer;
    [SerializeField] private float maxTimer;

    // Start is called before the first frame update
    void Start()
    {
        FightStarts = false;
        currentTimer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(FightStarts)
        {
            currentTimer -= Time.deltaTime;
            imageTimer.fillAmount = currentTimer / maxTimer;
        }
    }
}
