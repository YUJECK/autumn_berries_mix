using System;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Turns;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase.Gameplay
{
    public sealed class TurnGUI : MonoBehaviour, ITurnAddicted
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private GameObject[] turnCover;


        private void Start()
        {
            SceneSwitcher.TryGetGameplayScene().OnConfiguringFinished += OnOnConfiguringFinished;
        }

        private void OnOnConfiguringFinished()
        {
            SceneSwitcher.TryGetGameplayScene().TurnController.RegisterAddiction(this);
        }

        public void OnPlayerTurn(PlayerTurn turn)
        {
            EnableCover("Your Turn");
        }

        private async void EnableCover(string label)
        {
            this.label.text = label;
            
            foreach (var element in turnCover)
            {
                element.SetActive(true);                
            }

            await UniTask.Delay(500);
            
            foreach (var element in turnCover)
            {
                element.SetActive(false);                
            }
        }

        public void OnEnemyTurn(EnemyTurn turn)
        {
            EnableCover("Enemy Turn");
        }
    }
}