using System;
using UnityEngine;

namespace Source.Content
{
    public class MovementArrow : MonoBehaviour
    {
        public void ProcessDirection(CharacterMovement.Directions direction)
        {
            switch (direction)
            {
                case CharacterMovement.Directions.Left:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                
                case CharacterMovement.Directions.Right:
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                
                case CharacterMovement.Directions.Up:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                
                case CharacterMovement.Directions.Down:
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                
                case CharacterMovement.Directions.UpRight:
                    transform.rotation = Quaternion.Euler(0, 0, -45);
                    break;
                    
                case CharacterMovement.Directions.UpLeft:
                    transform.rotation = Quaternion.Euler(0, 0, 45);
                    break;
                
                case CharacterMovement.Directions.DownRight:
                    transform.rotation = Quaternion.Euler(0, 0, 225);
                    break;
                
                case CharacterMovement.Directions.DownLeft:
                    transform.rotation = Quaternion.Euler(0, 0, -225);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}