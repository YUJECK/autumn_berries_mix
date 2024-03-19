using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    public class SiberianPush : PlayerAbility
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

        public SiberianPush(Unit owner, AbilityData data) : base(owner, data)
        {
            _animator = owner.Master.Get<SiberianAnimator>();
            _flipper = owner.Master.Get<EntityFlipper>();
            
            _attackAreaOverlay = new PrefabTileOverlayData(CommonOverlaysProvider.TileToAttackPrefab, "HammerAttackArea");
        }

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

        //main logic
        private async void Attack(EnemyUnit enemyUnit)
        {
            Vector2Int direction = enemyUnit.Position2Int - Owner.Position2Int;
            Vector2Int startPosition = enemyUnit.Position2Int;
            
            GridTile toTile = Owner.Grid.Get(enemyUnit.Position2Int + direction);
            
            _flipper.FlipTo(enemyUnit.transform); //flip unit at walk direction
            
            if (toTile.Empty && toTile.Walkable)
            {
                _animator.PlayPush();

                await UniTask.Delay(500);
                
                enemyUnit.UnitHealth.Hit(1);

                while (enemyUnit.transform.position != new Vector3(toTile.Position2Int.x, toTile.Position2Int.y, 0))
                {
                    enemyUnit.transform.position = Vector3.MoveTowards(enemyUnit.Position3,
                        new Vector3(toTile.Position2Int.x, toTile.Position2Int.y, 0), 6 * Time.deltaTime);

                    await UniTask.WaitForFixedUpdate();
                }

                Owner.Grid.ReplaceEntity(enemyUnit, startPosition, enemyUnit.Position2Int); //update unit position in grid
                Owner.OnUsedAbility(this);
            }
            else
            {
                _animator.PlayPush();

                await UniTask.Delay(500);

                Vector3 releasePosition = enemyUnit.transform.position + new Vector3(direction.x / 2, direction.y / 2);
                Vector3 newStartPosition = enemyUnit.transform.position;
                
                while (enemyUnit.transform.position != releasePosition)
                {
                    enemyUnit.transform.position = Vector3.MoveTowards(enemyUnit.Position3,releasePosition, 4 * Time.deltaTime);

                    await UniTask.WaitForFixedUpdate();
                }
                
                while (enemyUnit.transform.position != newStartPosition)
                {
                    enemyUnit.transform.position = Vector3.MoveTowards(enemyUnit.Position3,newStartPosition, 4 * Time.deltaTime);

                    await UniTask.WaitForFixedUpdate();
                }
                
                enemyUnit.UnitHealth.Hit(enemyUnit.UnitHealth.CurrentHealth);
                
                Owner.OnUsedAbility(this);
            }
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
                
                if(tile != null && tile.Walkable && tile.Empty || tile.TileStuff is Unit)
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