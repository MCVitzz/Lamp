using OpenTK.Graphics.OpenGL4;
using Serilog;
using System;
using System.Collections.Generic;
using static Lamp.Core.Window;

namespace Lamp.Core
{
    public abstract class Application
    {
        public readonly WindowConfig windowConfig;
        public int Width { get; private set; }
        public int Height { get; private set; }

        private List<Action<int, int>> WindowResizeCallbacks;

        public Application(int width, int height, string name)
        {
            WindowResizeCallbacks = new List<Action<int, int>>();
            Width = width;
            Height = height;
            AppConfig config = new AppConfig(Prepare, Draw, OnResize, Destroy);
            windowConfig = new WindowConfig(width, height, name, config);
            SetupLogger();
            Log.Information("Application Initialized.");
        }

        public void Run()
        {
            using (Window window = new Window(windowConfig))
            {
                window.Run();
            }
        }

        public void AddWindowResizeListener(Action<int, int> action)
        {
            WindowResizeCallbacks.Add(action);
        }

        private void Prepare()
        {
            Start();
        }

        private void Draw(float delta)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Update(delta);
        }

        private void Destroy()
        {
            Exit();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        private void OnResize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            Height = height;
            Width = width;
            foreach(Action<int, int> action in WindowResizeCallbacks)
            {
                action(Width, Height);
            }
        }

        public abstract void Start();

        public abstract void Update(float delta);

        public abstract void Exit();

        private void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss:fff} {Level}] : {Message:l}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
