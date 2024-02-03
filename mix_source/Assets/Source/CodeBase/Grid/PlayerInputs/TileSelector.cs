using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.Inputs;
using autumn_berries_mix.Scenes;
using autumn_berry_mixВ;
using Source.Content;
using UnityEngine;
using Zenject;

namespace autumn_berries_mix
{
    public class TileSelector : ITickable
    {
        private GridTile _currentCell;
        private IOnTileSelected _currentCallback;
        
        private Scene currentScene;
        private Vector2 mousePosition;
        
        private readonly GameGrid _grid;
        private readonly GameplayResources _resources;

        private readonly List<SelectedTileProcessor> _processors = new();
        private bool disabled;

        public TileSelector(GameGrid grid, GameplayResources resources, params SelectedTileProcessor[] processors)
        {
            _grid = grid;
            _resources = resources;

            foreach (var processor in processors)
            {
                PushProcessor(processor);
            }
            
            Disable();
        }

        public void PushProcessor(SelectedTileProcessor processor)
        {
            if(processor == null)
                return;
            
            Resolver.Instance().InjectTileProcessor(processor);
            _processors.Add(processor);
        }
        
        public void RemoveProcessor(SelectedTileProcessor processor)
        {
            if(processor == null)
                return;
            
            _processors.Remove(processor);
        }

        public void Enable()
        {
            currentScene = SceneSwitcher.CurrentScene;
            disabled = false;
        }   

        public void Disable()
        {
            disabled = true;
        }

        public void Tick()
        {
            Select();

            if (_currentCell != null && InputsHandler.TileChosen)
            {
                for (int i = 0; i < _processors.Count; i++)
                {
                    _processors[i].ProcessSelectedTile(_currentCell);
                }
            }
        }

        private void Select()
        {
            if (disabled)
                return;
            
            Vector2 nextPosition = GetMousePosition();

            if (HasMouseMoved(nextPosition))
            {
                var nextSelected = GetPointedCell(nextPosition);
                
                if(nextSelected == null)
                    return;
                
                if (IsPointingToNewCell(nextSelected))
                {
                    UnpointCurrentCell();
                    
                    _currentCell = nextSelected;

                    PointCurrentCell();
                
                    ProcessCurrentCell();
                }
            }

            mousePosition = nextPosition;
        }

        private Vector3 GetMousePosition()
        {
            return currentScene.GetCamera().ScreenToWorldPoint(Input.mousePosition);
        }

        private void ProcessCurrentCell()
        {
            if (_currentCell != null)
            {
                for (int i = 0; i < _processors.Count; i++)
                {
                    _processors[i].ProcessPointedTile(_currentCell);
                }
            }
        }

        private void PointCurrentCell()
        {
            _currentCell.OnPointed();
            
            if (_currentCell.TileStuff != null &&
                _currentCell.TileStuff.TryGetComponent(out IOnTileSelected callback))
            {
                callback.OnPointed();
                _currentCallback = callback;
            }
        }

        private void UnpointCurrentCell()
        {
            if (_currentCell != null)
            {
                _currentCell.OnUnpointed();
                _currentCell = null;
            }

            if (_currentCallback != null)
            {
                _currentCallback.OnUnpointed();
                _currentCallback = null;
            }
        }

        private bool IsPointingToNewCell(GridTile nextSelected)
        {
            return nextSelected != _currentCell;
        }

        private GridTile GetPointedCell(Vector2 nextPosition)
        {
            return _grid.Get(Mathf.RoundToInt(nextPosition.x), Mathf.RoundToInt(nextPosition.y));
        }

        private bool HasMouseMoved(Vector2 nextPosition)
        {
            return nextPosition != mousePosition;
        }
    }
}