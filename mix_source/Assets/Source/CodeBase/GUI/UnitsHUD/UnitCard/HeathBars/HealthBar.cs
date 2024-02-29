using System;
using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(Image))]
    public class HealthBar : MonoBehaviour
    {
        //view data
        private Image _healthBar;
        private TMP_Text _text;
        
        //model data
        private UnitHealth connectedHealth;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _healthBar = GetComponent<Image>();
        }

        public void Connect(UnitHealth unitHealth)
        {
            connectedHealth = unitHealth;
            connectedHealth = unitHealth;
            
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if(connectedHealth == null)
                return;
            
            _healthBar.fillAmount = (float)connectedHealth.CurrentHealth / connectedHealth.MaximumHealth;
            _text.text = $"{connectedHealth.CurrentHealth}/{connectedHealth.MaximumHealth}";
        }

        public void Update()
        {
            UpdateHealthBar();
        }
    }
}
