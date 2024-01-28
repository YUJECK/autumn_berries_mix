using System;
using System.Collections.Generic;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Source.CodeBase.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase.GUI
{
    public sealed class HealthBarsController : MonoBehaviour
    {
        [SerializeField] private Transform playerHealthBarsContainer;
        [SerializeField] private Transform enemyHealthBarsContainer;
        
        private GameplayScene _currentScene;

        private List<HealthBar> playerHealthBars;
        private List<HealthBar> enemyHealthBars;
        
        private void Start()
        {
            _currentScene = SceneSwitcher.TryGetGameplayScene();

            _currentScene.OnConfiguringFinished += ConfigureBars;
        }

        private void ConfigureBars()
        {
            LoadBars();
            ConnectBarsToUnits();
            
            _currentScene.OnConfiguringFinished -= ConfigureBars;
        }

        private void ConnectBarsToUnits()
        {
            Connect(_currentScene.PlayerUnitsPull, playerHealthBars);
            Connect(_currentScene.EnemyUnitsPull, enemyHealthBars);
        }

        private void LoadBars()
        {
            playerHealthBars = new List<HealthBar>(playerHealthBarsContainer.GetComponentsInChildren<HealthBar>());
            enemyHealthBars = new List<HealthBar>(enemyHealthBarsContainer.GetComponentsInChildren<HealthBar>());
        }
        
        private void Connect(Unit[] units, List<HealthBar> bars)
        {
            if (bars.Count < units.Length)
            {
                Debug.LogError("TOO MUCH UNITS");
                return;
            }

            for (int i = 0; i < bars.Count; i++)
            {
                if (i < units.Length)
                {
                    bars[i].Connect(units[i]);    
                }
                else
                {
                    bars[i].gameObject.SetActive(false);                    
                }
            }
        }
    }
}