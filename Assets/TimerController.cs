using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    Image _redFront;

    private Tween _tween = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void KillTween()
    {
        if (_tween != null)
        {
            _tween.Kill();
            _tween = null;
        }
    }

    public void StartTimer(float duration)
    {
        KillTween();
        _redFront.fillAmount = 0.0f;
        _tween = DOTween.To(() => GetCurrentPersent(), (newValue) => SetContentPosition(newValue), 1.0f, duration);
        _tween.OnComplete(OnComplete);
    }

    float GetCurrentPersent()
    {
        return _redFront.fillAmount;
    }

    void SetContentPosition(float newValue)
    {
        _redFront.fillAmount = newValue;
    }

    void OnComplete()
    {
        
    }
}
