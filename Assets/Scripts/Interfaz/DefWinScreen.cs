using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefWinScreen : MonoBehaviour
{
    [SerializeField] OptionsMenu options;
    
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
}
