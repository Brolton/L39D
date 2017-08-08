using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Brolton.Utils;

namespace LudumDare39
{
    public class Settings
    {
        public static float MinTimeForNewMsg = 0.0f;
        public static float MaxTimeForNewMsg = 0.0f;

        public static float StartCharge = 0.0f;

        public static float UsualSpeed = 0.0f;
        public static float TypeSpeed = 0.0f;
        public static float ForfeitSpeed = 0.0f;

        public static float StartTimeForOneSymbol = 0.0f;
        public static float DeltaTimeForOneSymbol = 0.0f;
        public static float MinTimeForOneSymbol = 0.0f;

        public static string MainTheme = "Sound_effects/Homestuck_-_Fuchsia_ruler";

        public static Pair<string, float> FailChar = new Pair<string, float>("", 0);
        public static Pair<string, float> MessageSuccess = new Pair<string, float>("", 0);
        public static Pair<string, float> MessageTimeout = new Pair<string, float>("", 0);
        public static Pair<string, float> ContactSelect = new Pair<string, float>("", 0);
        public static Pair<string, float> NewMessage = new Pair<string, float>("", 0);
        public static List<Pair<string, float>> Keyboard = new List<Pair<string, float>>();


        public static void Init()
        {
            XmlDocument doc= new XmlDocument();
			doc.Load( Application.streamingAssetsPath + "/gamedata/settings.xml" );
            XmlNode node = doc["Settings"];

            MinTimeForNewMsg = node["MinTimeForNewMsg"].Attributes["Value"].AsFloat();
            MaxTimeForNewMsg = node["MaxTimeForNewMsg"].Attributes["Value"].AsFloat();

            StartCharge = node["StartCharge"].Attributes["Value"].AsFloat();

            UsualSpeed = node["UsualSpeed"].Attributes["Value"].AsFloat();
            TypeSpeed = node["TypeSpeed"].Attributes["Value"].AsFloat();
            ForfeitSpeed = node["ForfeitSpeed"].Attributes["Value"].AsFloat();

            StartTimeForOneSymbol = node["StartTimeForOneSymbol"].Attributes["Value"].AsFloat();
            DeltaTimeForOneSymbol = node["DeltaTimeForOneSymbol"].Attributes["Value"].AsFloat();
            MinTimeForOneSymbol = node["MinTimeForOneSymbol"].Attributes["Value"].AsFloat();

            FailChar.First = node["Sounds"]["FailChar"].Attributes["Path"].AsString("");
            FailChar.Second = node["Sounds"]["FailChar"].Attributes["Volume"].AsFloat();

            MessageSuccess.First = node["Sounds"]["MessageSuccess"].Attributes["Path"].AsString("");
            MessageSuccess.Second = node["Sounds"]["MessageSuccess"].Attributes["Volume"].AsFloat();

            MessageTimeout.First = node["Sounds"]["MessageTimeout"].Attributes["Path"].AsString("");
            MessageTimeout.Second = node["Sounds"]["MessageTimeout"].Attributes["Volume"].AsFloat();

            ContactSelect.First = node["Sounds"]["ContactSelect"].Attributes["Path"].AsString("");
            ContactSelect.Second = node["Sounds"]["ContactSelect"].Attributes["Volume"].AsFloat();

            NewMessage.First = node["Sounds"]["NewMessage"].Attributes["Path"].AsString("");
            NewMessage.Second = node["Sounds"]["NewMessage"].Attributes["Volume"].AsFloat();

            foreach(XmlNode keyNode in node["Sounds"]["Keyboard"].ChildNodes)
            {
                Pair<string, float> newKeyPair = new Pair<string, float>("", 0);
                newKeyPair.First = keyNode.Attributes["Path"].AsString("");
                newKeyPair.Second = keyNode.Attributes["Volume"].AsFloat();
                Keyboard.Add(newKeyPair);

            }
        }
    }
}

