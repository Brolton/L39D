using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public enum AnswerLength
    {
        Short,
        Long
    }

    public enum AnswerColor
    {
        Red,
        Green
    }
    
    public class Answer
    {
        public AnswerColor color;      // Color="Red" 
        public AnswerLength length;    // Length="Short" 
        public int points;             // Points="10"
        public string text;            // Text="Pizza?0_o" 
        public int nextQuestionId;     // NextId="2a"

        public void Parse(XmlNode node)
        {
//            color = node.Attributes["Color"].AsString("");
//            length = node.Attributes["Length"].AsString("");
            points = node.Attributes["Points"].AsInt(0);
            text = node.Attributes["Text"].AsString("");
            nextQuestionId = node.Attributes["NextId"].AsInt(-1);
        }
    }

    public class Question
    {
        public int id;                                  // Id="1"
        public string text;                             // Text="Luv, pizza's ready^^" 
        public List<Answer> answers = new List<Answer>();

        public void Parse(XmlNode node)
        {
            id = node.Attributes["Id"].AsInt(0);
            text = node.Attributes["Text"].AsString("");
            foreach (XmlNode answerNode in node.ChildNodes)
            {
                Answer newAnswer = new Answer();
                newAnswer.Parse(answerNode);
                answers.Add(newAnswer);
            }
        }
    }
    
    public class MessageController
    {
        public List<ContactData> AllContacts = new List<ContactData>();

        public void Init()
        {
//            XmlDocument doc= new XmlDocument();
//            doc.Load( Application.dataPath + "/dialogs_with_ids.xml" );
//            //            XmlDocument _docQuests = XmlUtils.OpenXMLDocument(SF2Paths.GameData, "dialogs_with_ids.xml");
//            ParseXml(doc);

            XmlDocument doc= new XmlDocument();
            doc.Load( Application.dataPath + "/gamedata/contacts.xml" );
            ParseContacts(doc);
        }

        void ParseContacts(XmlDocument doc)
        {
            var nodeRoot = doc["Contacts"];
            foreach (XmlNode contactNode in nodeRoot.ChildNodes)
            {
                ContactData newContact = new ContactData();
                newContact.Parse(contactNode);
                AllContacts.Add(newContact);
            }
        }

        public List<Question> GetMessagesByContact(int contactId)
        {
            return AllContacts[contactId].AllQuestions;
        }
    }
}

