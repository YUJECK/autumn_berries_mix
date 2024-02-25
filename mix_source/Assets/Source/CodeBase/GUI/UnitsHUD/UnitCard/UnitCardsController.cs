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
            _currentScene.OnConfiguringFinished -= Initialize;
            
            
            LoadCards();
            ConnectCardsToUnits();
            
            _currentScene.TurnController.RegisterAddiction(this);
            
            SignalManager.SubscribeOnSignal<UnitSelectedSignal>(OnUnitSelected);

            SignalManager.SubscribeOnSignal<UnitSpawned>(OnUnitSpawned);
            SignalManager.SubscribeOnSignal<UnitDead>(OnUnitDestroyed);
        }

        private void OnUnitSpawned(UnitSpawned data)
        {
            var card = GetEmptyFor(data.Unit);
            
            card.gameObject.SetActive(true);
            
            card.Connect(data.Unit);
            unitToCard.Add(data.Unit, card);
        }

        private void OnUnitDestroyed(UnitDead data)
        {
            unitToCard[data.Unit].Die();
        }

        private UnitCard GetEmptyFor(Unit unit)
        {
            if (unit is PlayerUnit)
            {
                foreach (var card in playerCards)
                {
                    if (!card.AlreadyConnected)
                        return card;
                }
            }
            
            if (unit is EnemyUnit)
            {
                foreach (var card in enemyCards)
                {
                    if (card.AlreadyConnected)
                        return card;
                }
            }
            

            return null;
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
            Connect(_currentScene.Units.PlayerUnitsPull, playerCards);
            Connect(_currentScene.Units.EnemyUnitsPull, enemyCards);
        }

        private void LoadCards()
        {
            playerCards = new List<UnitCard>(playerHealthBarsContainer.GetComponentsInChildren<UnitCard>());
            enemyCards = new List<UnitCard>(enemyHealthBarsContainer.GetComponentsInChildren<UnitCard>());
        }

        private void Connect(Unit[] units, List<UnitCard> cards)
        {
            if (cards.Count < units.Length)
            {
                Debug.LogError("TOO MUCH UNITS");
                return;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (i < units.Length)
                {
                    cards[i].Connect(units[i]);    
                    unitToCard.Add(units[i], cards[i]);
                }
                else
                {
                    cards[i].gameObject.SetActive(false);                    
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