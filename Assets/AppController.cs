using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using LudumDare39;

public class AppController : MonoBehaviour 
{
    public static AppController Instance;

    public MessageController MessagesController;

    [SerializeField]
    GameObject _rules;
    bool _rulesIsOpened = true;

    [SerializeField]
    ContactsController _contactsController;

    [SerializeField]
    ChatController _chatController;

    public void Init()
    {
        Instance = this;

        MessagesController = new MessageController();
        MessagesController.Init();
        _contactsController.Init(MessagesController.AllContacts);
        _chatController.Init();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnContactClick(int contactId)
    {
        if (_rulesIsOpened)
        {
            _rulesIsOpened = false;
            _rules.gameObject.SetActive(false);
        }

        _chatController.SetCurrentContact(contactId);
    }
}
