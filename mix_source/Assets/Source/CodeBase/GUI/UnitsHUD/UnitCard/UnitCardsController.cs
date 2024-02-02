using System.Collections.Generic;
using autumn_berries_mix.PrefabTags.CodeBase.GUI.UnitsHUD.UnitCard;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class UnitCardsController : MonoBehaviour
    {
        [SerializeField] private Transform playerHealthBarsContainer;
        [SerializeField] private Transform enemyHealthBarsContainer;
        
        private GameplayScene _currentScene;

        private List<UnitCard> playerHealthBars;
        private List<UnitCard> enemyHealthBars;
        
        private readonly Dictionary<Unit, UnitCard> unitToCard = new();

        private UnitCard lastSelected;
        
        private void Start()
        {
            _currentScene = SceneSwitcher.TryGetGameplayScene();

            _currentScene.OnConfiguringFinished += ConfigureBars;
        }

        private void OnUnitSelected(PlayerUnit unit)
        {
            if (unit == null && lastSelected != null)
            {
                lastSelected.Deselect();
                return;
            }
            
            if(_currentScene.SelectedPlayerUnit != null)
                unitToCard[_currentScene.SelectedPlayerUnit].Deselect();
            
            unitToCard[unit].Select();
            lastSelected = unitToCard[unit];
        }

        private void ConfigureBars()
        {
            _currentScene.Callbacks.OnPlayerUnitSelected += OnUnitSelected;
            
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
            playerHealthBars = new List<UnitCard>(playerHealthBarsContainer.GetComponentsInChildren<UnitCard>());
            enemyHealthBars = new List<UnitCard>(enemyHealthBarsContainer.GetComponentsInChildren<UnitCard>());
        }
        
        private void Connect(Unit[] units, List<UnitCard> bars)
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
                    unitToCard.Add(units[i], bars[i]);
                }
                else
                {
                    bars[i].gameObject.SetActive(false);                    
                }
            }
        }
    }
}