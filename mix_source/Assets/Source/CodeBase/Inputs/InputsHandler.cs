using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace autumn_berries_mix.Grid.Inputs
{
    public static class InputsHandler
    {
        private static readonly InputsMap Inputs;

        public static event Action OnNodeSelected;

        public static bool TileChosen
            => Inputs.Gameplay.SelectNode.WasPerformedThisFrame() && !EventSystem.current.IsPointerOverGameObject();

        static InputsHandler()
        {
            Inputs = new InputsMap();
            Inputs.Enable();

            Inputs.Gameplay.SelectNode.performed += PerformCallback;
        }

        private static void PerformCallback(InputAction.CallbackContext context)
            => OnNodeSelected?.Invoke();
    }
}