using System;
using UnityEngine;

namespace Brolton.Utils
{
    public static class Log
    {
        public static void Write(string message)
        {
            Debug.Log(message);
        }

        public static void Write(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }

        public static void Error(string message)
        {
            Debug.LogError(message);
        }

        public static void Error(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }

        public static void Warning(string message, params object[] args)
        {
            Debug.LogWarningFormat(message, args);
        }

        public static void File(string message)
        {
            Debug.Log(message);
        }

        public static void File(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }
    }
}