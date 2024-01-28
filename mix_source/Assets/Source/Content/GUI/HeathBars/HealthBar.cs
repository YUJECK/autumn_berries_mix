using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text label;
        
        private UnitHealth connectedHealth;

        public void Connect(Unit unit)
        {
            connectedHealth = unit.UnitHealth;
            label.text = unit.UnitName;

            connectedHealth = unit.UnitHealth;
            
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if(connectedHealth == null)
                return;
            
            healthBar.fillAmount = (float)connectedHealth.CurrentHealth / connectedHealth.MaximumHealth;
        }

        public void Update()
        {
            UpdateHealthBar();
        }
    }
}
