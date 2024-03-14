using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Setting
{
        public float volume;
        public string language;

        public Setting(float volume, string language)
        {
            this.volume = volume;
            this.language = language;
        }
    }
