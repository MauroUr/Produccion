using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject loadingScreen;

    private bool inOptionsMenu = false;

    public void PlayButton()
    {
        StartCoroutine(LoadSceneAsynchronously("LevelSelection"));
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(inOptionsMenu);
        inOptionsMenu = !inOptionsMenu;
        optionsMenu.SetActive(inOptionsMenu);
    }

    public void ExitButton()
    {
        optionsMenu.SetActive(true);
        optionsMenu.GetComponent<OptionsMenu>().QuitGame();
    }

    public void ActivateMenu()
    {
        mainMenu.SetActive(true);
    }

    private IEnumerator LoadSceneAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        background.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
