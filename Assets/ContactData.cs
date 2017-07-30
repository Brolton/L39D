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

        List<Question> AllQuestions = new List<Question>();

        public List<Question> SendedQuestions = new List<Question>();

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

        public bool SendNextQuestion()
        {
            if (SendedQuestions.Count == 0)
            {
                SendedQuestions.Add(AllQuestions[0]);
                return true;
            }

            Answer lastAnswer = SendedQuestions[SendedQuestions.Count - 1].GetAnswer();
            if (lastAnswer == null)
            {
                return false;
            }

            int nextQuestionId = lastAnswer.nextQuestionId;
            if (nextQuestionId < 0)
            {
                return false;
            }

            Question newQuestion = GetQuestionById(nextQuestionId);
            SendedQuestions.Add(newQuestion);
            return true;
        }

        Question GetQuestionById(int questionId)
        {
            for (int i = 0; i < AllQuestions.Count; i++)
            {
                if (AllQuestions[i].id == questionId)
                {
                    return AllQuestions[i];
                }
            }
            return null;
        }
    }
}

