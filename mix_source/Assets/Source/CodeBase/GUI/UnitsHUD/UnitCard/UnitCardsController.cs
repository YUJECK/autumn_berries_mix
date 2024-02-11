using System.Collections.Generic;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.PrefabTags.CodeBase.GUI.UnitsHUD.UnitCard;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Turns;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class UnitCardsController : MonoBehaviour, ITurnAddicted
    {
        [SerializeField] private Transform playerHealthBarsContainer;
        [SerializeField] private Transform enemyHealthBarsContainer;
        
        private GameplayScene _currentScene;

        private List<UnitCard> playerCards;
        private List<UnitCard> enemyCards;
        
        private readonly Dictionary<Unit, UnitCard> unitToCard = new();

        private UnitCard lastSelected;
        
        private void Start()
        {
            _currentScene = SceneSwitcher.TryGetGameplayScene();

            _currentScene.OnConfiguringFinished += Initialize;
        }

        private void Initialize()
        {
            LoadCards();
            ConnectCardsToUnits();
            
            _currentScene.TurnController.RegisterAddiction(this);
            _currentScene.OnConfiguringFinished -= Initialize;
            SignalManager.SubscribeOnSignal<UnitSelectedSignal>(OnUnitSelected);
        }

        private void Select(Unit unit)
        {
            if (lastSelected != null)
            {
                lastSelected.Deselect();
            }
            if (unit == null)
            {
                return;
            }
            
            unitToCard[unit].Select();
            lastSelected = unitToCard[unit];
        }


        private void ConnectCardsToUnits()
        {
            Connect(_currentScene.PlayerUnitsPull, playerCards);
            Connect(_currentScene.EnemyUnitsPull, enemyCards);
        }

        private void LoadCards()
        {
            playerCards = new List<UnitCard>(playerHealthBarsContainer.GetComponentsInChildren<UnitCard>());
            enemyCards = new List<UnitCard>(enemyHealthBarsContainer.GetComponentsInChildren<UnitCard>());
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

        private void OnUnitSelected(UnitSelectedSignal signal)
        {
            Select(signal.Unit);
        }

        public void OnPlayerTurn(PlayerTurn turn)
        {
            Select(null);
        }

        public void OnEnemyTurn(EnemyTurn turn)
        {
            Select(turn.CurrentEnemy);
        }
    }
}