using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        AppController _appController;


    	// Use this for initialization
    	void Start ()
        {
            _appController.Init();
    	}
    	
    	// Update is called once per frame
    	void Update ()
        {
    		
    	}
    }
}