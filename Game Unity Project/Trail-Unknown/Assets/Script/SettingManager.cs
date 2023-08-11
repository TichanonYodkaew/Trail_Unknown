using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public Button saveButton;

    public AudioMixer audioMixer;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    float soundVolume;
    float musicVolume;

    void Start()
    {
        LoadSettings();
    }

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnSoundVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        saveButton.onClick.AddListener(delegate { OnSaveButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullscreenToggle()
    {
       gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnSoundVolumeChange()
    {

        soundVolume = soundVolumeSlider.value;
        soundVolume = gameSettings.soundVolume = soundVolumeSlider.value;
        audioMixer.SetFloat("soundvolume", soundVolume);
    }

    public void OnMusicVolumeChange()
    {

        musicVolume = musicVolumeSlider.value;
        musicVolume = gameSettings.musicVolume = musicVolumeSlider.value;
        audioMixer.SetFloat("musicvolume", musicVolume);
    }

    public void OnSaveButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/gamesettings.json";
        if (File.Exists(path))
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(path));
        }
        else
        {
            // Set default values for the game settings
            gameSettings.fullscreen = true;
            gameSettings.resolutionIndex = Screen.currentResolution.height;
            gameSettings.soundVolume = 0f;
            gameSettings.musicVolume = 0f;
        }
        soundVolumeSlider.value = gameSettings.soundVolume;
        musicVolumeSlider.value = gameSettings.musicVolume;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();

        audioMixer.SetFloat("musicvolume", gameSettings.musicVolume);
        audioMixer.SetFloat("soundvolume", gameSettings.soundVolume);
    }

}
