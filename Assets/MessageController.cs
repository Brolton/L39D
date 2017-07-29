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
        public static List<Question> AllQuestions = new List<Question>();

        public static void ParseXml(XmlDocument doc)
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

