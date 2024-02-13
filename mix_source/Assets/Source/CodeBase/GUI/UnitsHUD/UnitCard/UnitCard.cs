using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI.UnitsHUD.UnitCard
{
    public class UnitCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text unitLabel;
        
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite currentSprite;
        [SerializeField] private Sprite deadSprite;
        
        [SerializeField] private Image image;

        private Unit connectedUnit;
        
        private HealthBar _healthBar;

        private void Start()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            Deselect();
        }

        public void Connect(Unit unit)
        {
            connectedUnit = unit;
            
            _healthBar.Connect(unit.UnitHealth);
            ApplyToLabel(unit.UnitName);
        }

        private void ApplyToLabel(string unitName)
        {
            unitLabel.text = unitName;
        }

        public void Select()
        {
            image.sprite = currentSprite;
        }
        public void Deselect()
        {
            image.sprite = defaultSprite;
        }
    }
}