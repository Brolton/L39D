using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        ChatController _chatController;

    	// Use this for initialization
    	void Start ()
        {
            XmlDocument doc= new XmlDocument();
            doc.Load( Application.dataPath + "/dialogs_with_ids.xml" );
//            XmlDocument _docQuests = XmlUtils.OpenXMLDocument(SF2Paths.GameData, "dialogs_with_ids.xml");
            MessageController.ParseXml(doc);
            _chatController.Init();
    	}
    	
    	// Update is called once per frame
    	void Update ()
        {
    		
    	}
    }
}