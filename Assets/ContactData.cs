using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LudumDare39
{
    public class ContactData
    {
//        <Contact Name="Mom" Pic="mom.png">
//            <Dialog File="dialogs_mom_granpasbirthday.xml"/>
//        </Contact>

        public string Name = "";
        public string AvatarImg = "";
        string dialogFileName = "";

        public List<Question> AllQuestions = new List<Question>();

        public void Parse (XmlNode node)
        {
            Name = node.Attributes["Name"].AsString("");
            AvatarImg = node.Attributes["Pic"].AsString("");

            dialogFileName = node["Dialog"].Attributes["File"].AsString("");

            XmlDocument doc= new XmlDocument();
            doc.Load( Application.dataPath + "/gamedata/" + dialogFileName );
            ParseAllQuestions(doc);
        }

        public void ParseAllQuestions(XmlDocument doc)
        {
            //            var nodeRoot = doc["Root"];
            var nodeDialog = doc["Dialog"];
            foreach (XmlNode questionNode in nodeDialog.ChildNodes)
            {
                Question newQuestion = new Question();
                newQuestion.Parse(questionNode);
                AllQuestions.Add(newQuestion);
            }
        }
    }
}

