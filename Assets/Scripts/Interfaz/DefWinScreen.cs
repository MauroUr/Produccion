using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefWinScreen : MonoBehaviour
{
    [SerializeField] OptionsMenu options;
    [SerializeField] Image star1;
    [SerializeField] Image star2;
    [SerializeField] Image star3;
    public void Restart()
    {
        options.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        options.gameObject.SetActive(true);
        options.BackToMenu();
    }

    public void LevelSelection()
    {
        options.SaveData();
        SceneManager.LoadScene("LevelSelection");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void CheckScore(float score)
    {
        if (score >= 40)
            star1.color = Color.yellow;
        if(score >= 60)
            star2.color = Color.yellow;
        if(score >= 80)
            star3.color = Color.yellow;
    }
}
