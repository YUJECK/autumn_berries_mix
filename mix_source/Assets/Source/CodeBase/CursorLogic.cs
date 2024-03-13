using autumn_berries_mix.Grid.Inputs;
using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase
{
    public static class CursorLogic
    {
        private static Texture2D CursorIdle;
        private static Texture2D CursorPressed;
        
        public static void Enable()
        {
            CursorIdle = Resources.Load<Texture2D>("Cursor/Cursor");
            CursorPressed = Resources.Load<Texture2D>("Cursor/CursorOnClick");
            
            InputsHandler.OnNodeSelected += ClickDown;
            InputsHandler.OnMouseLeftClickUp += ClickUp;
        }

        private static void ClickUp()
        {
            Cursor.SetCursor(CursorIdle, Vector2.zero, CursorMode.Auto);
        }

        private static void ClickDown()
        {
            Cursor.SetCursor(CursorPressed, Vector2.zero, CursorMode.Auto);
        }
    }
}