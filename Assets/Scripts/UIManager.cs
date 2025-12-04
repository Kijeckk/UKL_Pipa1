using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    AudioManagment audioManager;
    //public Animator animator;
    //public GameObject Play;
    public GameObject Settings;
    public GameObject SettingsPanel;
    //public GameObject Exit;
    public GameObject Close;

    //private Button PlayButton;
    private Button SettingButton;
    private Button ExitButton;
    private Button CloseButton;


    private void Awake()
    {
        //PlayButton = Play.GetComponent<Button>();
        SettingButton = Settings.GetComponent<Button>();
        //ExitButton = Exit.GetComponent<Button>();
        CloseButton = Close.GetComponent<Button>();

        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagment>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagment>();    


        SettingsPanel.gameObject.SetActive(false);
    }
    private void Start()
    {
        //PlayButton.onClick.AddListener(() =>
        //{
        //    audioManager.PlaySFX(audioManager.buttonClick);
        //});

        SettingButton.onClick.AddListener(() =>
        {
            audioManager.PlaySFX(audioManager.buttonClick);
            SettingsPanel.gameObject.SetActive(true);
            //animator.SetTrigger(SETTINGOPENING_ANIMATION);

        });

        CloseButton.onClick.AddListener(() =>
        {
            audioManager.PlaySFX(audioManager.buttonClick);
        });

        //ExitButton.onClick.AddListener(() =>
        //{
        //    audioManager.PlaySFX(audioManager.buttonClick);
        //    Application.Quit();
        //});
    }
}
