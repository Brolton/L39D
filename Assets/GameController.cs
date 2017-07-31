using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        PhoneStatistic _phoneStatistic;
        [SerializeField]
        AppController _appController;


    	// Use this for initialization
    	void Start ()
        {
            Settings.Init();
            _phoneStatistic.Init();
            _appController.Init();
    	}
    	
    	// Update is called once per frame
    	void Update ()
        {
    		
    	}
    }
}