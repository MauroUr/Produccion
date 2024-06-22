using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle toggle;

    private OptionsData data;

    private void Start()
    {
        data = SaveSystem.LoadOptions();

        if (data != null)
            SetAllParameters();
        else
        {
            data = new OptionsData();
            data.volume = volumeSlider.value;
            data.isFullScreen = toggle.isOn;
            data.resolutionHeight = Screen.currentResolution.height;
            data.resolutionWidth = Screen.currentResolution.width;
        }

        resolutions = Screen.resolutions;

        if (resDropdown != null)
        {
            resDropdown.ClearOptions();

            int currentResIndex = 0;
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (i > 0 && resolutions[i].width == resolutions[i - 1].width && resolutions[i].height == resolutions[i - 1].height)
                    continue;
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResIndex = i;
            }

            resDropdown.AddOptions(options);
            resDropdown.value = currentResIndex;
            resDropdown.RefreshShownValue();
        }

        this.gameObject.SetActive(false);
    }
    public void RestartButton()
    {
        SaveSystem.SaveData(data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void NextButton()
    {
        SaveSystem.SaveData(data);
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        SaveSystem.SaveData(data);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
    public void BackButton()
    {
        Time.timeScale = 1.0f;
        GameController.Instance.isPaused = false;
        this.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        SaveSystem.SaveData(data);
        Application.Quit();
    }

    public void BackToMenu()
    {
        SaveSystem.SaveData(data);
        SceneManager.LoadScene("MainMenu");
    }

    private void SetAllParameters()
    {
        volumeSlider.value = data.volume;
        AudioListener.volume = data.volume;
        Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, data.isFullScreen);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        data.volume = volumeSlider.value;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        data.resolutionWidth = resolution.width;
        data.resolutionHeight = resolution.height;
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        data.isFullScreen = isFullScreen;
    }

    public void SaveData()
    {
        SaveSystem.SaveData(data);

    }
}