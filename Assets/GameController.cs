using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField]
        PhoneStatistic _phoneStatistic;
        [SerializeField]
        AppController _appController;

        [SerializeField]
        GameOverPanel _theEnd;

    	// Use this for initialization
    	void Start ()
        {
            Instance = this;
            Settings.Init();
            _phoneStatistic.Init();
            _appController.Init();
    	}
    	
    	// Update is called once per frame
    	void Update ()
        {
    		
    	}

        void OpenGameOverPanel()
        {
            _theEnd.UpdateCounters();
            _theEnd.gameObject.SetActive(true);
        }

        public bool CheckGameOver()
        {
            bool isGameOver = false;
            if (_phoneStatistic.CurrentPercent == 0.0f)
            {
                isGameOver = true;
            }
            else if (_appController.MessagesController.IsAllDialogsComplete())
            {
                isGameOver = true;
            }

            if (isGameOver)
            {
                OpenGameOverPanel();
            }

            return isGameOver;
        }
    }
}