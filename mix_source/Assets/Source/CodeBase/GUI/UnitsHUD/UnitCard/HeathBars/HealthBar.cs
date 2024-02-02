using autumn_berries_mix.Units;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        
        private UnitHealth connectedHealth;

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
            
            healthBar.fillAmount = (float)connectedHealth.CurrentHealth / connectedHealth.MaximumHealth;
        }

        public void Update()
        {
            UpdateHealthBar();
        }
    }
}
