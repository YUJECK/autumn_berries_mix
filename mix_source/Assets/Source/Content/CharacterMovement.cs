using System;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.Inputs;
using UnityEngine;

namespace Source.Content
{
    public sealed class CharacterMovement 
    {
        private readonly RottenBerriesLevelScene scene;
        private readonly MovementArrow[] arrows = new MovementArrow[8];

        private Sprite borderSprite;
        private readonly GameplayResources _resources;

        private GridTile _currentSelected;

        public Vector3 ChainyPosition => new Vector3(0,0,0);
        
        public CharacterMovement(RottenBerriesLevelScene scene, GameplayResources resources)
        {
            _resources = resources;
            
            this.scene = scene;

            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i] = GameObject.Instantiate(_resources.MovementArrowPrefab);
                arrows[i].ProcessDirection((Directions)i);
            }
            
            InputsHandler.OnNodeSelected += NodeSelected;
        }

        private void NodeSelected()
        {
            if (_currentSelected != null 
                && _currentSelected.transform.position.x - ChainyPosition.x <= 1 &&  _currentSelected.transform.position.x - ChainyPosition.x >= -1
                && _currentSelected.transform.position.y - ChainyPosition.y <= 1 &&  _currentSelected.transform.position.y - ChainyPosition.y >= -1
                && _currentSelected.Empty && _currentSelected.Walkable)
            {
                scene.GameGrid.SwapEntities((int)ChainyPosition.x, (int)ChainyPosition.y, (int)_currentSelected.transform.position.x, (int)_currentSelected.transform.position.y);
            }
        }

        public void Tick()
        {
            DrawArrows();  
        }
        

        private void DrawArrows()
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                Vector3 directionVector = DirectionToVector((Directions)i);
                Vector3 truePosition = ChainyPosition + directionVector;

                if (scene.GameGrid.Get((int)truePosition.x, (int)truePosition.y) == null)
                {
                    arrows[i].gameObject.SetActive(false);
                    continue;
                }
                
                if (scene.GameGrid.Get((int)truePosition.x, (int)truePosition.y).Walkable && scene.GameGrid.Get((int)truePosition.x, (int)truePosition.y).Empty)
                {
                    arrows[i].transform.position = truePosition;
                    arrows[i].gameObject.SetActive(true);
                }
                else
                {
                    arrows[i].gameObject.SetActive(false);
                }
            }
        }

        private Vector3 DirectionToVector(Directions direction)
        {
            switch (direction)
            {
                case Directions.Left:
                    return new Vector3(-1, 0);

                case Directions.Right:
                    return new Vector3(1, 0);
                
                case Directions.Up:
                    return new Vector3(0, 1);
                
                case Directions.Down:
                    return new Vector3(0, -1);
                
                case Directions.UpRight:
                    return new Vector3(1, 1);
                
                case Directions.UpLeft:
                    return new Vector3(-1, 1);
                
                case Directions.DownRight:
                    return new Vector3(1, -1);
                
                case Directions.DownLeft:
                    return new Vector3(-1, -1);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public enum Directions
        {
            Left = 0,
            Right = 1,
            Up = 2,
            UpRight = 3,
            UpLeft = 4,
            Down = 5,
            DownRight = 6,
            DownLeft = 7,
        }
    }
}