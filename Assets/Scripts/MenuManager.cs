using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject DirectionalLight;
    [SerializeField] GameObject LevelSelectionPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject VolumeSlider;
    [SerializeField] GameObject VolumePerecentageText;
    [SerializeField] GameObject NightLightToggle;
    [SerializeField] GameObject DayLightToggle;
    [SerializeField] GameObject SelectMusicDropdown;
    [SerializeField] List<AudioClip> GameMusics;
    private GameSettings gameSettings;
    private AudioSource audioSource;

    // Panels currentPanel = null;
    // private List<Panels> panelHistory = new List<Panels>();
    private void Awake()
    {
        gameSettings = GameSettings.GetInstance();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetupPanels();
        LoadSettings();
    }


    public void SetupPanels()
    {
        LevelSelectionPanel.SetActive(true); // Show Levels Panel
        SettingsPanel.SetActive(false); // Hide Setting Panel
    }

    //load and apply saved settings
    private void LoadSettings()
    {
        ApplyVolumeChange(gameSettings.audioVolume);
        ApplyLightChange(gameSettings.gameLight);
        ApplyMusicChange(gameSettings.gameMusicIndex);
    }

    //SelectLevelBtn_onClick is called when the SelectLevel Button in the MainButtonsPanel clicked
    public void SelectLevelBtn_onClick()
    {
        LevelSelectionPanel.SetActive(true); // Show Levels Panel
        SettingsPanel.SetActive(false); // Hide Settings Panel
    }

    //SettingsBtn_onClick is called when the Settings Button in the MainButtonsPanel clicked
    public void SettingsBtn_onClick()
    {
        SettingsPanel.SetActive(true); // Show Settings Panel
        LevelSelectionPanel.SetActive(false); // Hide Levels Panel
    }

    //Load a scene by scene index 
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    //VolumeSlider_OnValueChanged is called when the VolumeSlider in the Settings panel is changed
    public void VolumeSlider_OnValueChanged(float value)
    {
        ApplyVolumeChange(value);
    }
    //Apply Audio Volume value to the settings and settings UI
    private void ApplyVolumeChange(float value)
    {
        Text text = VolumePerecentageText.GetComponent<Text>();
        text.text = Mathf.RoundToInt(value * 100) + "%";
        gameSettings.audioVolume = value;
        VolumeSlider.GetComponent<Slider>().value = value;
        audioSource.volume = value;
    }

    //NightLightToggle_OnValueChanged is called when the player clicked on NightLightToggle
    public void NightLightToggle_OnValueChanged(Boolean value)
    {
        ApplyLightChange(value == true ? GameSettings.GameLight.NightLight : GameSettings.GameLight.DayLight);
    }

    //DayLightToggle_OnValueChanged is called when the player clicked on DayLightToggle
    public void DayLightToggle_OnValueChanged(Boolean value)
    {
        ApplyLightChange(value == true ? GameSettings.GameLight.DayLight : GameSettings.GameLight.NightLight);
    }

    //Apply Light Change to the settings
    private void ApplyLightChange(GameSettings.GameLight gameLight)
    {
        gameSettings.gameLight = gameLight;

        if (gameSettings.gameLight == GameSettings.GameLight.DayLight)
        {
            NightLightToggle.GetComponent<Toggle>().isOn = false;
            DayLightToggle.GetComponent<Toggle>().isOn = true;

            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            DirectionalLight.transform.rotation = rotation;
        }
        else
        {
            NightLightToggle.GetComponent<Toggle>().isOn = true;
            DayLightToggle.GetComponent<Toggle>().isOn = false;
            Quaternion rotation = Quaternion.Euler(-90, 0, 0);
            DirectionalLight.transform.rotation = rotation;
        }
    }

    //SelectMusicDropdown_OnValueChanged is called when player clicked on MusicSelectionDropdown menu
    public void SelectMusicDropdown_OnValueChanged(int value)
    {
        ApplyMusicChange(value);
    }

    //Apply Music Change to the settings
    private void ApplyMusicChange(int musicIndex)
    {   
        gameSettings.gameMusicIndex = musicIndex;
        List<string> options = new List<string>();
        foreach(AudioClip audioClip in GameMusics)
        {
            options.Add(audioClip.name);
        }

        SelectMusicDropdown.GetComponent<Dropdown>().options.Clear();
        SelectMusicDropdown.GetComponent<Dropdown>().AddOptions(options);
        SelectMusicDropdown.GetComponent<Dropdown>().value = musicIndex;
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.PlayOneShot(GameMusics[musicIndex],gameSettings.audioVolume);
    }


    //private void SetupPanels()
    //{
    //    Panels[] panels = GetComponentsInChildren<Panels>();

    //    foreach (Panels panel in panels)
    //    {
    //        panel.Setup(this);
    //    }

    //    currentPanel.Show();

    //}

    ////Should go to previous, still incomplete
    //private void Update()
    //{
       
    //}

    //public void GoToPrevious()
    //{
    //    if (panelHistory.Count == 0)
    //    {
    //        //Add menus that you don't want to include in back button, still incomplete
    //        return;
    //    }

    //    int lastIndex = panelHistory.Count - 1;
    //    SetCurrent(panelHistory[lastIndex]);
    //    panelHistory.RemoveAt(lastIndex);
    //}

    //public void SetCurrentWithHistory(Panels newPanel)
    //{
    //    panelHistory.Add(currentPanel);
    //    SetCurrent(newPanel);
    //}

    //private void SetCurrent(Panels newPanel)
    //{
    //    currentPanel.Hide();
    //    currentPanel = newPanel;
    //    currentPanel.Show();
    //}

}
