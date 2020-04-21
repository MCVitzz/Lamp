using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;

namespace Lamp.Core
{
    public class Window : GameWindow
    {

        private readonly Action load;
        private readonly Action<float> update;
        private readonly Action unload;
        private readonly Action<int, int> resize;
        private bool Active = true;

        public Window(WindowConfig info) : base(info.width, info.height, GraphicsMode.Default, info.title)
        {
            load = info.config.OnLoad;
            update = info.config.OnUpdate;
            unload = info.config.OnUnload;
            resize = info.config.OnResize;
        }

        protected override void OnLoad(EventArgs e)
        {
            load();
            GL.ClearColor(0.9f, 0.9f, 0.9f, 1f);
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            resize(Width, Height);
            base.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            update((float)e.Time);
            Context.SwapBuffers();
            if (Active)
                Input.Instance.Update(Width, Height, Keyboard.GetState(), Mouse.GetState());
            base.OnRenderFrame(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            unload();
            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            Active = !Active;
            base.OnFocusedChanged(e);
        }

        public struct WindowConfig
        {
            public int width, height;
            public string title;
            public AppConfig config;

            public WindowConfig(int width, int height, string title, AppConfig config)
            {
                this.width = width;
                this.height = height;
                this.title = title;
                this.config = config;
            }
        }

        public struct AppConfig
        {
            public Action OnLoad;
            public Action<float> OnUpdate;
            public Action<int, int> OnResize;
            public Action OnUnload;

            public AppConfig(Action onLoad, Action<float> onUpdate, Action<int, int> onResize, Action onUnload)
            {
                OnLoad = onLoad;
                OnUnload = onUnload;
                OnUpdate = onUpdate;
                OnResize = onResize;
            }
        }
    }
}
