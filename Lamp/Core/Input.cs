using OpenTK;
using OpenTK.Input;

namespace Lamp.Core
{
    public sealed class Input
    {
        public static Input Instance { get; private set; } = new Input();

        private KeyboardState Keyboard;
        private Vector2 CursorPostion;
        private Vector2 OldCursorPosition;
        public Vector2 Delta { get; private set; }
        public bool LeftButton { get; private set; }
        public bool MiddleButton { get; private set; }
        public bool RightButton { get; private set; }
        public float ScrollOffset { get; private set; }
        private float Scroll, OldScroll;

        private Input()
        {
            CursorPostion = Vector2.Zero;
            OldCursorPosition = Vector2.Zero;
        }

        public void Update(int width, int height, KeyboardState keyboard, MouseState mouse)
        {
            Keyboard = keyboard;
            OldCursorPosition = new Vector2(CursorPostion.X, CursorPostion.Y);
            CursorPostion = new Vector2(mouse.X, mouse.Y);
            Delta = CursorPostion - OldCursorPosition;
            LeftButton = mouse.LeftButton == ButtonState.Pressed;
            MiddleButton = mouse.MiddleButton == ButtonState.Pressed;
            RightButton = mouse.RightButton == ButtonState.Pressed;
            OldScroll = Scroll;
            Scroll = mouse.Scroll.Y;
            ScrollOffset = Scroll - OldScroll;
        }

        public bool IsKeyPressed(Key c)
        {
            return Keyboard.IsKeyDown(c);
        }
    }
}
