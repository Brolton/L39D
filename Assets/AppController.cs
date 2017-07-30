using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using LudumDare39;

public class AppController : MonoBehaviour 
{
    [SerializeField]
    GameObject _rules;
    bool _rulesIsOpened = true;

    [SerializeField]
    ContactsController _contactsController;

    [SerializeField]
    ChatController _chatController;

    public void Init()
    {
        _contactsController.Init();

        XmlDocument doc= new XmlDocument();
        doc.Load( Application.dataPath + "/dialogs_with_ids.xml" );
        //            XmlDocument _docQuests = XmlUtils.OpenXMLDocument(SF2Paths.GameData, "dialogs_with_ids.xml");
        MessageController.ParseXml(doc);
        _chatController.Init();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnContactClick()
    {
        if (_rulesIsOpened)
        {
            _rulesIsOpened = false;
            _rules.gameObject.SetActive(false);
        }
    }
}
