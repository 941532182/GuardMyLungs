using UnityEngine;
using Util;

namespace Controller
{
    public static class AudioController
    {

        static AudioController()
        {
            BGM = GameObject.Find("Audio/BGM").GetComponent<AudioSource>();
            BGS = GameObject.Find("Audio/BGS").GetComponent<AudioSource>();
            var se_children = GameObject.Find("Audio/SE").transform.GetChildren();
            se = new AudioSource[se_children.Length];
            for (int i = 0; i < se.Length; i++)
            {
                se[i] = se_children[i].GetComponent<AudioSource>();
            }
        }

        public static AudioSource BGM { get; }
        public static AudioSource BGS { get; }
        private static AudioSource[] se;

        public static void PlayBGM(string bgmName)
        {
            BGM.clip = Resources.Load<AudioClip>($"Audio/BGM/{bgmName}");
            BGM.Play();
        }

        public static void PlayBGS(string bgsName)
        {
            BGS.clip = Resources.Load<AudioClip>($"Audio/BGS/{bgsName}");
            BGS.Play();
        }

        public static void PlaySE(string seName)
        {
            foreach (AudioSource src in se)
            {
                if (!src.isPlaying)
                {
                    src.clip = Resources.Load<AudioClip>($"Audio/SE/{seName}");
                    src.Play();
                    break;
                }
            }
        }

    }
}
