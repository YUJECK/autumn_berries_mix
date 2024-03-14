using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Units.Abilities.Roll
{
    public class ChainyRoll : PlayerAbility
    {
        private RollData _typedData;
        private readonly Dictionary<Vector2Int, List<GridTile>> _lines = new();
        private readonly List<GridTile> _availableArea = new();
        private bool _currentlyMoving = false;
        private readonly ChainyAnimator _animator;
        private readonly EntityFlipper _flipper;

        public ChainyRoll(Unit owner, RollData data) : base(owner, data)
        {
            _typedData = data;
            
            _animator = owner.Master.Get<ChainyAnimator>();
            _flipper = owner.Master.Get<EntityFlipper>();
        }

        public override void OnAbilitySelected()
        {
            base.OnAbilitySelected();

            RegenerateLinesAndOverlay();
        }

        private void RegenerateLinesAndOverlay()
        {
            ClearLinesAndOverlay();
            
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
                if (line.Count != _typedData.range || !line[line.Count-1].Empty) continue;
                
                //Добавляем линию в зону атаки
                foreach (var tile in line)
                {
                    float angle = Mathf.Atan2(tile.Position2Int.y - Owner.Position2Int.y, tile.Position2Int.x - Owner.Position2Int.x) 
                        * Mathf.Rad2Deg - 90;
                    
                    var directionOverlayData = new PrefabTileOverlayData(_typedData.overlay, "RollDirections", angle);
                    tile.Overlay.PushPrefabOverlay(directionOverlayData);
                    
                    _availableArea.Add(tile);
                }
                
                _lines.Add(direction, line);
            }
        }

        public override void OnAbilityDeselected()
        {
            base.OnAbilityDeselected();
            
            //убираем оверлей и чистим линии
            ClearLinesAndOverlay();
        }

        private void ClearLinesAndOverlay()
        {
            ClearOverlay();
            
            _lines.Clear();
            _availableArea.Clear();
        }

        private void ClearOverlay()
        {
            foreach (var tile in _availableArea)
            {
                tile.Overlay.RemovePrefabOverlay("RollDirections");
            }
        }

        public override void OnTilePointed(GridTile tile, bool withClick)
        {
            base.OnTilePointed(tile, withClick);

            if (!withClick)
            {
                return;
            }

            if (_availableArea.Contains(tile) && !_currentlyMoving)
            {
                Move(tile);
            }
        }

        private async void Move(GridTile tile)
        {
            ClearOverlay();
            _currentlyMoving = true;
            
            _animator.PlayRoll();
            
            var startPosition = Owner.Position2Int;
            var line = _lines[Direction.GetDirection(Owner.Position2Int, tile.Position2Int)];

            _flipper.FlipTo(tile.transform);
            
            foreach (var target in line)
            {
                while (Owner.Position3 != target.transform.position)
                {
                    Owner.transform.position = Vector3.MoveTowards(Owner.Position3, target.transform.position, 5 * Time.deltaTime);
                    await UniTask.WaitForFixedUpdate();
                }
            }

            Owner.Grid.ReplaceEntity(Owner, startPosition, line[line.Count-1].Position2Int);
            
            ClearLinesAndOverlay();
            RegenerateLinesAndOverlay();
            
            _animator.StopRoll();
            _currentlyMoving = false;
            
            Owner.OnUsedAbility(this);
        }
    }
}