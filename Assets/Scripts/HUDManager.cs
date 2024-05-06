using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image _nitro;
    [SerializeField] private Image _shipLife;
    [SerializeField] private Image _planetLife;
    private Animator _tpAnimator;

    private float _pLifeAmount = 30;
    [SerializeField] private float _loseLifeDelay;
    private float _lastLifeLosed = 0;

    private void Start()
    {
        _planetLife.fillAmount = _pLifeAmount;
        _tpAnimator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (_lastLifeLosed > _loseLifeDelay)
            _pLifeAmount -= 1/100;

        _lastLifeLosed += Time.deltaTime;
    }
    public void UpdateHUD(float nitro, float shipLife)
    {
        _nitro.fillAmount = nitro / 100;
        _shipLife.fillAmount = shipLife / 100;
        _planetLife.fillAmount = _pLifeAmount / 100;

        if (_pLifeAmount >= 100)
            Time.timeScale = 0;
    }

    public void ResetTP()
    {
        _tpAnimator.SetTrigger("useTP");
    }
    public void RecoverPlanetLife(float planetLife)
    {
        _pLifeAmount += planetLife;
    }
}
