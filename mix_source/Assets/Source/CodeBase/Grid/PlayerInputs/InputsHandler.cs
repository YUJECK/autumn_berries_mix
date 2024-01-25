using System;
using UnityEngine.InputSystem;

namespace autumn_berries_mix.Grid.Inputs
{
    public static class InputsHandler
    {
        private static readonly InputsMap Inputs;

        public static event Action OnNodeSelected;

        public static bool NodeSelected
            => Inputs.Gameplay.SelectNode.IsPressed();

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