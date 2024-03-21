using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using autumn_berries_mix.EC;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    public class SiberianHammer : PlayerAbility 
    {
        private readonly SiberianAnimator _animator;
        private readonly EntityFlipper _flipper;
        private readonly PrefabTileOverlayData _attackAreaOverlay;
        
        private readonly Vector2Int[] _directions = { 
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1) };

        private readonly List<GridTile> availableArea = new List<GridTile>();

        public SiberianHammer(Unit owner, AbilityData data) : base(owner, data)
        {
            _animator = owner.Master.Get<SiberianAnimator>();
            _flipper = owner.Master.Get<EntityFlipper>();
            
            _attackAreaOverlay = new PrefabTileOverlayData(CommonOverlaysProvider.TileToAttackPrefab, "HammerAttackArea");
        }

        //callbacks
        public override void OnAbilitySelected()
        {
            base.OnAbilitySelected();
            
            CreateAreaAndOverlay();
        }

        public override void OnAbilityDeselected()
        {
            base.OnAbilityDeselected();
            
            ClearAreaAndOverlay();
        }

        public override void OnEnemyUnitPointed(EnemyUnit enemyUnit, bool withClick)
        {
            base.OnEnemyUnitPointed(enemyUnit, withClick);

            if (withClick && availableArea.Contains(Owner.Grid.Get(enemyUnit.Position2Int)))
            {
                Attack(enemyUnit);
            }
        }

        public override void OnEntityPointed(Entity entity, bool withClick)
        {
            base.OnEntityPointed(entity, withClick);
            
            if (withClick && entity is IBreakable breakable && !breakable.IsBroken && availableArea.Contains(Owner.Grid.Get(entity.Position2Int)))
            {
                _flipper.FlipTo(entity.transform);
                Attack(breakable);
            }
        }
        
        //logic

        private async void Attack(IBreakable breakable)
        {
            ClearOverlay();
            _animator.PlayHammerAttack();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            
            breakable.Break();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            
            Owner.OnUsedAbility(this);
        }

        private async void Attack(EnemyUnit enemyUnit)
        {
            ClearOverlay();
            
            _flipper.FlipTo(enemyUnit.transform);
            _animator.PlayHammerAttack();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            
            enemyUnit.UnitHealth.Hit(2);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            
            Owner.OnUsedAbility(this);
        }

        //helpers
        private void ClearAreaAndOverlay()
        {
            ClearOverlay();
            ClearArea();
        }
        private void CreateAreaAndOverlay()
        {
            ClearOverlay();
            ClearArea();
            CreateNewAvailableArea();
            DrawOverlay();
        }
        
        //area
        private void CreateNewAvailableArea()
        {
            foreach (var direction in _directions)
            {
                var tile = Owner.Grid.Get(Owner.Position2Int + direction);
                
                if(tile != null)
                    availableArea.Add(tile);
            }
        }

        private void ClearArea()
        {
            availableArea.Clear();
        }

        //overlay
        private void DrawOverlay()
        {
            foreach (var tile in availableArea)
            {
                tile.Overlay.PushPrefabOverlay(_attackAreaOverlay);
            }
        }

        private void ClearOverlay()
        {
            foreach (var tile in availableArea)
            {
                tile.Overlay.RemovePrefabOverlay(_attackAreaOverlay);
            }
        }
    }
}