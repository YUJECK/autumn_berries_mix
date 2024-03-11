using System;
using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Source.CodeBase.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    public class ChainAttack : PlayerAbility
    {
        private readonly ChainAttackData _typedData;

        private ChainyAnimator _animator;
        
        private PrefabTileOverlayData _overlayData; //оверлей для клеток
        private readonly List<GridTile> _availableArea = new(); //текущий набор тайлов, которые игрок может выбрать для атаки
        private readonly Dictionary<Vector2Int, List<GridTile>> _lines = new(); //все линии для атаки
        
        public ChainAttack(Unit owner, ChainAttackData data) : base(owner, data)
        {
            _typedData = data;
            _animator = Owner.Master.Get<ChainyAnimator>();
            
            CreateOverlayData();
        }

        //создание образца для оверлеев
        private void CreateOverlayData()
        {
            _overlayData = new PrefabTileOverlayData(CommonOverlaysProvider.TileToAttackPrefab, "AttackRange");
        }

        public override void OnAbilitySelected()
        {
            base.OnAbilitySelected();
            
            //создание оверлея
            foreach (var direction in _typedData.directions)
            {
                List<GridTile> line = new List<GridTile>();
                
                //Проверяем всю линию
                for (int i = 1; i <= _typedData.range; i++)
                {
                    var tile = Owner.Grid.Get(Owner.Position2Int + direction * i);
                    
                    if (tile != null && (tile.Empty || tile.TileStuff is Unit))
                    {
                        line.Add(tile);    
                    }
                }

                //Если линия не закончена, то для атаки она недоступна
                if (line.Count != _typedData.range) continue;
                
                //Добавляем линию в зону атаки
                foreach (var tile in line)
                {
                    tile.Overlay.PushPrefabOverlay(_overlayData);
                    _availableArea.Add(tile);
                }
                
                _lines.Add(direction, line);
            }
        }

        public override void OnAbilityDeselected()
        {
            base.OnAbilityDeselected();
            
            //убираем оверлей
            ClearOverlay();
            
            _lines.Clear();
            _availableArea.Clear();
        }

        private void ClearOverlay()
        {
            foreach (var tile in _availableArea)
            {
                tile.Overlay.RemovePrefabOverlay(_overlayData);
            }
        }

        public override void OnEnemyUnitPointed(EnemyUnit entity, bool withClick)
        {
            base.OnEntityPointed(entity, withClick);

            //если находится в зоне атаки, то атакуем
            if (withClick && _availableArea.Contains(Owner.Grid.Get(entity.Position2Int)))
            {
                Attack(entity);
            }
        }

        private Vector2Int ParseToDirection(EnemyUnit target)
        {
            return new Vector2Int
                (Mathf.Clamp(Direction().x, -1, 1), Mathf.Clamp(Direction().y, -1, 1));

            Vector2Int Direction()
            {
                return (target.Position2Int - Owner.Position2Int);
            }
        }
        
        private async void Attack(EnemyUnit entity)
        {
            var line = _lines[ParseToDirection(entity)];

            ClearOverlay();
            Owner.Master.Get<EntityFlipper>().FlipTo(entity.transform);
            
            _animator.PlayChainAttack();
            
            foreach (var tile in line)
            {
                tile.Overlay.PushPrefabOverlay(_overlayData);
                
                if (!tile.Empty && tile.TileStuff is EnemyUnit enemyUnit)
                {
                    enemyUnit.UnitHealth.Hit(2);
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                
                tile.Overlay.RemovePrefabOverlay(_overlayData);
            }
            
            Owner.OnUsedAbility(this);
        }
    }
}
