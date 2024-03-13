using System;
using autumn_berries_mix.Scenes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
    
namespace autumn_berries_mix.Grid.Inputs
{
    public static class InputsHandler
    {
        private static readonly InputsMap Inputs;

        public static event Action OnNodeSelected;
        public static event Action OnMouseLeftClickDown;
        public static event Action OnMouseLeftClickUp;

        public static float CameraZoomEdit => Input.mouseScrollDelta.y;

        public static Vector2 CameraMovement => Inputs.Gameplay.CameraMove.ReadValue<Vector2>();

        public static bool TileChosen
            => Inputs.Gameplay.SelectNode.WasPerformedThisFrame() && !EventSystem.current.IsPointerOverGameObject();

        static InputsHandler()
        {
            Inputs = new InputsMap();

            Inputs.Gameplay.SelectNode.performed += NodeSelected;

            Inputs.Global.MouseClick.performed += MouseLeftClickDown;
            Inputs.Global.MouseClick.canceled += MouseLeftClickUp;
            
            Inputs.Enable();
        }

        private static void MouseLeftClickUp(InputAction.CallbackContext obj)
        {
            OnMouseLeftClickUp?.Invoke();
        }

        private static void MouseLeftClickDown(InputAction.CallbackContext obj)
        {
            OnMouseLeftClickDown?.Invoke();
        }

        public static Vector3 GetMousePosition
            => SceneSwitcher.CurrentScene.GetCamera().ScreenToWorldPoint(Input.mousePosition);

        private static void NodeSelected(InputAction.CallbackContext context) 
            => OnNodeSelected?.Invoke();
    }
}