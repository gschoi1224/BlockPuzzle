using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public int now = -1;
        public int next = -1;
        public int nextnext = -1;
        public int level = 1;
        public int score = 0;
        public int hold = -1;
        public int highRecore = 0;
        public int[,] saveMap = new int[20,10];
        public OptionData optionData = new OptionData();
    }

    [System.Serializable]
    public class OptionData
    {
        public float v_bgm = 1f;
        public float v_eff = 1f;
    }
}