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
    [SerializeField] private List<CircleCollider2D> levels;
    public static LevelController Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);

        if (GameObject.Find("8-Bit Odyssey") == null)
            AudioManager.instance.PlayLoop("8-Bit Odyssey");
        progress = SaveSystem.LoadProgress();

        if (progress > 0 && progress < 4 && levels.Count > 0)
            UnlockLevels();
    }

    private void UnlockLevels()
    {
        for (int i = 0; i <= progress; i++)
            levels[i].enabled = true;
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
        Destroy(GameObject.Find("8-Bit Odyssey"));
        if(AudioManager.instance != null)
            AudioManager.instance.StopLoop();
        while (!operation.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
