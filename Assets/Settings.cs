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
        }
    }
}

