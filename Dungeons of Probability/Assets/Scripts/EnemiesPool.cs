﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [System.Serializable]
    public class EnemiesList
    {
        public string enemyName;
        public Sprite enemySprite;
    }
    
    public EnemiesList[] Enemies;
}