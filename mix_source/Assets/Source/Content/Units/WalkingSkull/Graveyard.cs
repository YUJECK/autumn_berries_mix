using System;
using System.Collections.Generic;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Turns;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public class Graveyard : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private Skull prefab;
        [SerializeField] private string resurrectSound = "SkullResurrected";
        [SerializeField] private int startAfterTurn = 10;
        [SerializeField] private int rate = 2;
        
        private readonly List<Skull> _skulls = new List<Skull>();
        private Grave[] _graves;

        private int last = -1;
        private int turnCounter;

        private void Start()
        {
            _graves = GetComponentsInChildren<Grave>();

            SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched += OnTurnSwitched;
        }

        private void OnDisable()
        {
            SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched -= OnTurnSwitched;
        }

        private void OnTurnSwitched(Turn turn)
        {
            if (turn is EnemyTurn)
            {
                turnCounter++;
            
                if(CanResurrect())
                    ResurrectNext();                
            }
        }

        private bool CanResurrect()
        {
            return turnCounter > startAfterTurn && turnCounter % rate == 0;
        }

        public void ResurrectNext()
        {
            last++;

            if (_graves[last].IsBroken)
            {
                while (_graves[last].IsBroken && last < _graves.Length)
                {
                    last++;
                }
            }
            
            if (_skulls.Count < _graves.Length)
            {
                AudioPlayer.Play(resurrectSound);
                _skulls.Add(SceneSwitcher.CurrentScene.Fabric.Instantiate(prefab, _graves[last].transform.position-Vector3.zero, Quaternion.identity, _graves[last].transform));
            }
            else
            {
                SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched -= OnTurnSwitched;
            }
        }
    }
}