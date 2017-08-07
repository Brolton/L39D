using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare39
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;

        [SerializeField]
        AudioSource audioSource;

        AudioClip failChar;
        AudioClip sendSuccess;
        AudioClip sendTimeout;
        AudioClip contactClick;

        List<AudioClip> keyboardType = new List<AudioClip>();

        System.Random rand = new System.Random();

        public void Init ()
        {
            Instance = this;
            
            failChar = (AudioClip)Resources.Load(Settings.FailChar);
            sendSuccess = (AudioClip)Resources.Load(Settings.MessageSuccess);
            sendTimeout = (AudioClip)Resources.Load(Settings.MessageTimeout);
            contactClick = (AudioClip)Resources.Load(Settings.ContactSelect);

            foreach (string keyTypePath in Settings.Keyboard)
            {
                keyboardType.Add((AudioClip)Resources.Load(keyTypePath));
            }
        }

        public void PlayKeyboard()
        {
            audioSource.PlayOneShot (keyboardType[rand.Next(keyboardType.Count)]);
        }

        public void PlayFailChar()
        {
            audioSource.PlayOneShot (failChar);
        }

        public void PlaySendTimeout()
        {
            audioSource.PlayOneShot (sendTimeout);
        }

        public void PlaySendSuccess()
        {
            audioSource.PlayOneShot (sendSuccess);
        }

        public void PlayContactClick()
        {
            audioSource.PlayOneShot (contactClick);
        }
    }
}

