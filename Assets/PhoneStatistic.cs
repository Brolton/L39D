using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LudumDare39;

public class PhoneStatistic : MonoBehaviour 
{
    public static PhoneStatistic Instance;

    const float FULL_PERCENT = 100.0f;

    public enum SpeedType
    {
        USUAL,
        TYPE,
        FORFEIT
    }
    public SpeedType Speed = SpeedType.USUAL;

    [SerializeField]
    Image _energyBar;
    [SerializeField]
    UnityEngine.UI.Text _energyTxt;

    float _currentPercent;

    [SerializeField]
    GameObject _theEnd;

    public void Init()
    {
        Instance = this;
        _currentPercent = Settings.StartCharge;
        _energyBar.fillAmount = _currentPercent / FULL_PERCENT;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_currentPercent == 0.0f)
            return;

        switch (Speed)
        {
            case SpeedType.USUAL:
                _currentPercent -= Settings.UsualSpeed;
                break;
            case SpeedType.TYPE:
                _currentPercent -= Settings.TypeSpeed;
                break;
            case SpeedType.FORFEIT:
                _currentPercent -= Settings.ForfeitSpeed;
                break;
        }

        if (_currentPercent <= 0)
        {
            _currentPercent = 0.0f;
            _theEnd.gameObject.SetActive(true);
        }
                
        _energyTxt.text = _currentPercent.ToString("0.0") + "%";
        _energyBar.fillAmount = _currentPercent / FULL_PERCENT;
	}
}
