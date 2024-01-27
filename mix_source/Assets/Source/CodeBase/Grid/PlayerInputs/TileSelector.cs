using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.Inputs;
using autumn_berries_mix.Scenes;
using Source.Content;
using UnityEngine;
using Zenject;

namespace autumn_berries_mix
{
    public class TileSelector : ITickable
    {
        private GridTile _currentSelected;
        private IOnTileSelected _lastCallback;
        
        private Scene currentScene;
        private Vector2 mousePosition;
        
        private readonly GameGrid _grid;
        private readonly GameplayResources _resources;

        private readonly List<SelectedTileProcessor> _processors = new List<SelectedTileProcessor>();
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

            if (_currentSelected != null && InputsHandler.TileChosen)
            {
                for (int i = 0; i < _processors.Count; i++)
                {
                    _processors[i].ProcessSelectedTile(_currentSelected);
                }
            }
        }

        private void Select()
        {
            if (disabled)
                return;
            
            Vector2 nextPosition = currentScene.GetCamera().ScreenToWorldPoint(Input.mousePosition);

            if (nextPosition != mousePosition)
            {
                var nextSelected = _grid.Get(Mathf.RoundToInt(nextPosition.x), Mathf.RoundToInt(nextPosition.y));
                
                if(nextSelected == null)
                    return;
                
                if (nextSelected != _currentSelected)
                {
                    _currentSelected = nextSelected;
                    
                    if (_lastCallback != null)
                    {
                        _lastCallback.OnDeselected();
                        _lastCallback = null;
                    }

                    if (_currentSelected.TileStuff != null &&
                        _currentSelected.TileStuff.TryGetComponent(out IOnTileSelected callback))
                    {
                        callback.OnSelected();
                        _lastCallback = callback;
                    }
                
                    if (_currentSelected != null)
                    {
                        for (int i = 0; i < _processors.Count; i++)
                        {
                            _processors[i].ProcessPointedTile(_currentSelected);
                        }
                    }
                }
            }

            mousePosition = nextPosition;
        }
    }
}