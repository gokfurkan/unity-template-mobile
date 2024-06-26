﻿using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Template.Scripts.SO
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public LoadOptions loadOptions;
        public GamePlayOptions gamePlayOptions;
        public EconomyOptions economyOptions;
        public UIOptions uiOptions;
        public LogOptions logOptions;
        public ConnectionOptions connectionOptions;
    }

    [Serializable]
    public class LoadOptions
    {
        public float loadDuration;
    }

    [Serializable]
    public class GamePlayOptions
    {
        public bool vSyncEnabled;
        public int targetFPS;
    }

    [Serializable]
    public class EconomyOptions
    {
        public bool useEconomyAnim; 
        [ShowIf(nameof(useEconomyAnim))]
        public float economyAnimDuration;

        [Space(10)] 
        public int winIncome;
        public int loseIncome;
        
        [Space(10)] 
        public int endSpawnIncomeAmount;
    }

    [Serializable]
    public class UIOptions
    {
        public float winPanelDelay;
        public float losePanelDelay;
        public float endContinueDelay;
        
        [Space(10)]
        public string levelText;

        [Space(10)]
        public bool hasEndPanelLevel;
        public string levelCompletedText;
        public string levelFailedText;

        [Space(10)] 
        public TMP_FontAsset  textFont;
    }

    [Serializable]
    public class LogOptions
    {
        public LogLevel logLevelToSave = LogLevel.All;
    }

    [Serializable]
    public class ConnectionOptions
    {
        public float checkInternetPerTime;
        public float startCheckTime;
    }
}