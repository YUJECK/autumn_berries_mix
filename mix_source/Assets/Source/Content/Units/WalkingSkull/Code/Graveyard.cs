using System;
using System.Collections.Generic;
using autumn_berries_mix.EC;
using autumn_berries_mix.Grid;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Turns;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public class Graveyard : Entity
    {
        [Header("Configuration")]
        [SerializeField] private Skull prefab;
        [SerializeField] private string resurrectSound = "SkullResurrected";
        [SerializeField] private int startAfterTurn = 10;
        [SerializeField] private int rate = 2;
        
        //other services
        private GameplayScene _scene;
        
        //graveyard
        private readonly List<Skull> _skulls = new List<Skull>();
        private Grave[] _graves;

        //indexes
        private int last = -1;
        private int turnCounter;

        public override void LoadedToLevel()
        {
            base.LoadedToLevel();
            
            _graves = GetComponentsInChildren<Grave>();

            _scene = SceneSwitcher.TryGetGameplayScene();
            _scene.TurnController.OnTurnSwitched += OnTurnSwitched;
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
            return turnCounter > startAfterTurn && turnCounter % rate == 0 && _skulls.Count < _graves.Length;
        }

        public void ResurrectNext()
        {
            last++;

            if (_skulls.Count >= _graves.Length)
            {
                SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched -= OnTurnSwitched;
                return;
            }

            if (_graves[last].IsBroken)
            {
                while (last < _graves.Length-1 && _graves[last].IsBroken)
                {
                    last++;
                }
                if (_graves[last].IsBroken)
                {
                    SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched -= OnTurnSwitched;
                    return;
                }
            }
            
            SpawnSkull();
        }

        private void SpawnSkull()
        {
            AudioPlayer.Play(resurrectSound);
            _skulls.Add(SceneSwitcher.CurrentScene.Fabric.Instantiate(prefab, _graves[last].transform.position + Vector3.down, Quaternion.identity, _graves[last].transform));
        }
    }
}