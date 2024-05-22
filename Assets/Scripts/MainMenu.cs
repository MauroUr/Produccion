using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject volumeObject;
    private Slider volumeSlider;

    private bool inOptionsMenu = false;

    private void Start()
    {
        volumeSlider = volumeObject.GetComponent<Slider>();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OptionsButton()
    {
        title.SetActive(inOptionsMenu);
        mainMenu.SetActive(inOptionsMenu);
        inOptionsMenu = !inOptionsMenu;
        optionsMenu.SetActive(inOptionsMenu);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
