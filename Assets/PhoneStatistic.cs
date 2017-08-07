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

	public float CurrentPercent;

    public void Init()
    {
        Instance = this;
        CurrentPercent = Settings.StartCharge;
        _energyBar.fillAmount = CurrentPercent / FULL_PERCENT;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (CurrentPercent == 0.0f)
            return;

        switch (Speed)
        {
            case SpeedType.USUAL:
                CurrentPercent -= Settings.UsualSpeed;
                break;
            case SpeedType.TYPE:
                CurrentPercent -= Settings.TypeSpeed;
                break;
            case SpeedType.FORFEIT:
                CurrentPercent -= Settings.ForfeitSpeed;
                break;
        }

        if (CurrentPercent <= 0)
        {
            CurrentPercent = 0.0f;
        }
        GameController.Instance.CheckGameOver();
                
        _energyTxt.text = CurrentPercent.ToString("0.0") + "%";
        _energyBar.fillAmount = CurrentPercent / FULL_PERCENT;
	}
}
