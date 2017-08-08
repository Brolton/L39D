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
        AudioClip newMessage;

        List<Pair<AudioClip, float>> keyboardType = new List<Pair<AudioClip, float>>();

        System.Random rand = new System.Random();

        public void Init ()
        {
            Instance = this;
            
            failChar = (AudioClip)Resources.Load(Settings.FailChar.First);
            sendSuccess = (AudioClip)Resources.Load(Settings.MessageSuccess.First);
            sendTimeout = (AudioClip)Resources.Load(Settings.MessageTimeout.First);
            contactClick = (AudioClip)Resources.Load(Settings.ContactSelect.First);
            newMessage = (AudioClip)Resources.Load(Settings.NewMessage.First);

            foreach (Pair<string, float> keyTypePath in Settings.Keyboard)
            {
                Pair<AudioClip, float> keyboardTypeSound = new Pair<AudioClip, float>((AudioClip)Resources.Load(keyTypePath.First), keyTypePath.Second);
                keyboardType.Add(keyboardTypeSound);
            }
        }

        public void PlayKeyboard()
        {
            Pair<AudioClip, float> keyboardTypeSound = keyboardType[rand.Next(keyboardType.Count)];
            audioSource.PlayOneShot (keyboardTypeSound.First, keyboardTypeSound.Second);
        }

        public void PlayFailChar()
        {
            audioSource.PlayOneShot (failChar, Settings.FailChar.Second);
        }

        public void PlaySendTimeout()
        {
            audioSource.PlayOneShot (sendTimeout, Settings.MessageTimeout.Second);
        }

        public void PlaySendSuccess()
        {
            audioSource.PlayOneShot (sendSuccess, Settings.MessageSuccess.Second);
        }

        public void PlayContactClick()
        {
            audioSource.PlayOneShot (contactClick, Settings.ContactSelect.Second);
        }

        public void PlayNewMsg()
        {
            audioSource.PlayOneShot (newMessage, Settings.NewMessage.Second);
        }
    }
}

