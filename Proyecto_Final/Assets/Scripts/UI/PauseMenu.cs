using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvasPlayer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider sliderSensitivity;
    [SerializeField] private TMP_Text textSensitivity;
    [SerializeField] private AudioMixer mixer;


    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerInput.DeactivateInput();
            canvasPlayer.SetActive(false);
            startMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ReturnToMainMenu()
    {
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OnDisablePauseMenu()
    {
        playerInput.ActivateInput();
        startMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnContinue()
    {
        playerInput.ActivateInput();
        startMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Options()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SubmitSliderSpeedRotation()
    {
        GameManager.Instance.SetSpeedRotation(sliderSensitivity.value);
        textSensitivity.text = sliderSensitivity.value.ToString("F1");
    }

    public void volumeMaster(float volume)
    {
        mixer.SetFloat("volumeMaster", volume);
    }

    public void volumeMusic(float volume)
    {
        mixer.SetFloat("volumeMusic", volume);
    }

    public void volumeSfx(float volume)
    {
        mixer.SetFloat("volumeSFX", volume);
    }
}
