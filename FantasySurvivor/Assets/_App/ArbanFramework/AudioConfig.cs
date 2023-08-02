using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArbanFramework
{
    public class AudioConfig
    {
        public AudioClip clip;
        public float volume;

        public AudioConfig(AudioClip clip, float volume)
        {
            this.clip = clip;
            this.volume = volume;
        }
    }
}
