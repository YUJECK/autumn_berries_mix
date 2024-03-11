using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Source.CodeBase.Helpers;
using UnityEditor.VersionControl;

namespace autumn_berries_mix.Units
{
    public class ChainsawAttack : PlayerAbility
    {
        private readonly ChainsawAttackData _typedData;
        private readonly PrefabTileOverlayData _rangeData;

        private readonly List<GridTile> _attackRange = new();
        
        public ChainsawAttack(Unit owner, ChainsawAttackData data) : base(owner, data)
        {
            _typedData = data;
            _rangeData = new PrefabTileOverlayData(CommonOverlaysProvider.TileToAttackPrefab, "AttackRange");
        }

        public override void OnAbilitySelected()
        {
            base.OnAbilitySelected();

            //создаем оверлей
            CreateOverlay();
        }

        private void CreateOverlay()
        {
            foreach (var tile in Owner.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y))
            {
                if (tile.Empty || tile.TileStuff is Unit)
                {
                    tile.Overlay.PushPrefabOverlay(_rangeData);
                    _attackRange.Add(tile);
                }
            }
        }

        public override void OnAbilityDeselected()
        {
            base.OnAbilityDeselected();
            
            //убираем оверлей и чистим зону атаки
            foreach (var tile in _attackRange)
            {
                tile.Overlay.RemovePrefabOverlay(_rangeData);
            }
            
            _attackRange.Clear();
        }

        public override void OnUnitPointed(Unit unit, bool withClick)
        {
            //если находится в зоне атаки, то атакуем
            if (_attackRange.Contains(Owner.Grid.Get(unit.Position2Int)))
            {
                if (withClick)
                {
                    Owner.Master.Get<EntityFlipper>().FlipTo(unit.transform);
                    Owner.Master.Get<ChainyAnimator>().PlayChainsawAttack();
                    unit.UnitHealth.Hit(_typedData.Damage);                  
                   
                    Owner.OnUsedAbility(this);
                }
            }    
        }
    }
}