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

        for (int i = 0; i < MessagesController.AllContacts.Count; i++)
        {
            SetTimerForContact(i);
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

    public void SetTimerForContact(int contactId)
    {
        float minTime = Settings.MinTimeForNewMsg;
        float maxTime = Settings.MaxTimeForNewMsg;
        float waitSeconds = Random.Range (minTime, maxTime);
        StartCoroutine(WaitAndSendMessageForContact(contactId, waitSeconds));
    }

    IEnumerator WaitAndSendMessageForContact(int contactId, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SendNextMsgForContact(contactId);
    }
}
