using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private List<GameObject> portales;
    private Queue<Color> allColors = new Queue<Color>();

    private Player player;
    private float planetLife = 30;
    public static GameController Instance;

    [SerializeField] private GameObject volumeObject;
    private Slider volumeSlider;

    //private float timeSinceStarted = 0f;
    //private bool isInLevel2 = false;
    /// <summary>
    /// [SerializeField] GameObject cubeSpawner;
    /// </summary>

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        Time.timeScale = 1;
        allColors.Enqueue(Color.red);
        allColors.Enqueue(Color.cyan);
        allColors.Enqueue(Color.yellow);
        allColors.Enqueue(Color.magenta);
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnDeath.AddListener(GameOver);
        player.OnPixelPicked.AddListener(PixelRetrieved);
        volumeSlider = volumeObject.GetComponent<Slider>();
    }

    private void Update()
    {
        hudManager.UpdateHUD(player.GetPlayerStatus());

        if (planetLife >= 100)
            this.Win();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            optionsScreen.SetActive(true);
        }

        //timeSinceStarted += Time.deltaTime;
        /*

        if (!isInLevel2 && timeSinceStarted > 20f && timeSinceStarted < 40f)
        {
            player.level2 = true;
            isInLevel2 = true;

            foreach (GameObject p in portales)
            {
                p.SetActive(true);
                p.GetComponent<SpriteRenderer>().color = allColors.Dequeue();
            }
        }
        else if (timeSinceStarted > 40f && player.level2)
        {
            player.level2 = false;
            foreach (GameObject p in portales)
                p.SetActive(false);

            GameObject[] cubesInGame = GameObject.FindGameObjectsWithTag("Cube");
            foreach (GameObject c in cubesInGame)
            {
                Level3Cube script = c.GetComponent<Level3Cube>();
                script.enabled = true;
                script.StartMoving();
            }
            cubeSpawner.GetComponent<CubeSpawner>().isInLevel3 = true;
        }*/
    }

    public void PixelRetrieved()
    {
        planetLife += 2;
        hudManager.RecoverPlanetLife(2);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    private void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }
    public void BackButton()
    {
        optionsScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

}
