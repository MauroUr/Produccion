using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private List<GameObject> portales;

    [SerializeField] private CubeSpawner cubeSpawner;
    private List<Color> colors;

    private Player player;
    private float planetLife = 30;
    public UnityEvent planetRecovered;
    public UnityEvent planetUnrecovered;
    private bool recoveredSwitch = false;
    [SerializeField] private float _loseLifeDelay;
    private float _lastLifeLosed = 0;

    public static GameController Instance;
    public bool isPaused = true;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private TextMeshProUGUI startCountdown;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        planetRecovered = new UnityEvent();
        planetUnrecovered = new UnityEvent();
    }
    private void Start()
    {
        Time.timeScale = 0;
        isPaused = true;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnDeath.AddListener(GameOver);
        player.OnPixelPicked.AddListener(PixelRetrieved);
        hudManager.onCountdownZero.AddListener(Win);

        if (cubeSpawner != null)
        {
            colors = cubeSpawner.cubeColors;

            for (int i = 0; i < colors.Count && i < portales.Count; i++)
            {
                portales[i].GetComponent<SpriteRenderer>().color = colors[i];
            }
        }
        StartCoroutine(Countdown(3));
        // AudioManager.instance.PlaySound("GameStart");
    }
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            startCountdown.text = count.ToString();
            yield return new WaitForSecondsRealtime(1);
            count--;
        }

        isPaused = false;
        Time.timeScale = 1;
        startScreen.SetActive(false);
    }

    private void Update()
    {
        if (_lastLifeLosed > _loseLifeDelay)
        {
            planetLife--;
            _lastLifeLosed = 0;
        }
        _lastLifeLosed += Time.deltaTime;

        hudManager.UpdateHUD(player.GetPlayerStatus(), planetLife);

        if (planetLife > 80 && !recoveredSwitch)
        {
            planetRecovered.Invoke();
            recoveredSwitch = !recoveredSwitch;
        }

        if (planetLife < 80 && recoveredSwitch)
        {
            planetUnrecovered.Invoke();
            recoveredSwitch = !recoveredSwitch;
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !startScreen.activeSelf)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
                optionsScreen.SetActive(true);
            }
            else if (isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
                optionsScreen.SetActive(false);
            }
        }

    }

    private void PixelRetrieved()
    {
        planetLife += 2;

        if(planetLife > 100)
            planetLife = 100;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    private void Win()
    {
        LevelController.Instance.CheckProgress(SceneManager.GetActiveScene().name[6] - 48);
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

}
