using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image _nitro;
    [SerializeField] private Image _shipLife;
    [SerializeField] private Image _planetLife;
    [SerializeField] private GameObject countdown;
    private float countdownNumber = 30;
    private Animator _tpAnimator;
    public UnityEvent onCountdownZero;

    private float _pLifeAmount = 30;

    private void Awake()
    {
        onCountdownZero = new UnityEvent();
    }
    private void Start()
    {
        _planetLife.fillAmount = _pLifeAmount;
        _tpAnimator = GetComponentInChildren<Animator>();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnTPUsed.AddListener(ResetTP);
        player.OnPixelPicked.AddListener(RecoverPlanetLife);

        GameController.Instance.planetRecovered.AddListener(StartCountdown);
        GameController.Instance.planetUnrecovered.AddListener(StopCountdown);
    }

    private void Update()
    {
        if (countdown.activeSelf)
        {
            countdown.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(countdownNumber).ToString();
            countdownNumber -= Time.deltaTime;
        }
        if(!countdown.activeSelf && countdownNumber < 30)
            countdownNumber = 30;

        if(countdownNumber <= 0)
            onCountdownZero.Invoke();
    }
    public void UpdateHUD(float[] status, float planetLife)
    {
        _pLifeAmount = planetLife;
        _nitro.fillAmount = status[0] / 100;
        _shipLife.fillAmount = status[1] / 100;
        _planetLife.fillAmount = _pLifeAmount / 100;

        TextMeshProUGUI[] texts = this.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = "%" + status[1].ToString();
        texts[1].text = "%" + _pLifeAmount.ToString();
    }

    private void StartCountdown()
    {
        countdown.SetActive(true);
    }

    private void StopCountdown()
    {
        countdown.SetActive(false);
    }
    public void ResetTP()
    {
        _tpAnimator.SetTrigger("useTP");
    }
    private void RecoverPlanetLife()
    {
        _pLifeAmount += 2;

        if(_pLifeAmount > 100)
            _pLifeAmount = 100;
    }
}
