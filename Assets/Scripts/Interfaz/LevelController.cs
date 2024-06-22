using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelController : MonoBehaviour
{
    private int progress = 0;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject loadingScreen;
    //[SerializeField] private List<Button> buttons;
    public static LevelController Instance;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        progress = SaveSystem.LoadProgress();

        //if (progress > 0 && progress < 4)
            //UnlockLevels();
    }

    private void UnlockLevels()
    {
//for (int i = 0; i < progress; i++)
  //          buttons[i].interactable = true;

    }

    public void StartLevel(int level)
    {
        StartCoroutine(LoadSceneAsynchronously("Level " + level.ToString()));
    }

    public void Back()
    {
        StartCoroutine(LoadSceneAsynchronously("MainMenu"));
        Time.timeScale = 1.0f;
    }

    public void CheckProgress(int level)
    {
        progress = SaveSystem.LoadProgress();

        if (progress < level)
            progress = level;

        SaveSystem.SaveProgress(level);
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
