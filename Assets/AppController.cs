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


    int frameCounter = 0;

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
	void Update ()
    {
        frameCounter++;
        if (frameCounter % 300 == 0)
        {
            SendNextMsgForContact(1);
        }
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

    public void SendNextMsgForContact(int contactId)
    {
        bool needIndicate = MessagesController.AllContacts[contactId].SendNextQuestion();
        if (needIndicate)
        {
            if (GetCurrentContactID() != contactId)
            {
                _contactsController.TurnOnIndicatorForContact(contactId);
            }
            else
            {
                _chatController.SetCurrentContact(contactId);
            }
        }
    }

    public int GetCurrentContactID()
    {
        return _contactsController.CurrentContactId;
    }
}
