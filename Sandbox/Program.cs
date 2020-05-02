using Lamp.Core;
using Lamp.GUI;
using Lamp.GUI.Components;
using Lamp.GUI.Constraints;
using Lamp.Models;
using Lamp.Rendering;
using Lamp.Rendering.Camera;
using Lamp.Resources;
using Lamp.Scene;
using Lamp.Scene.Lights;
using Lamp.Scene.Terrains;
using Lamp.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Sandbox
{
    public class Program : Application
    {

        //readonly float[] vertices = {
        //    // front	            // front colors
        //    -1.0f, -1.0f,  1.0f,      -1.0f, -1.0f,    0f, 0f, 0f,
        //     1.0f, -1.0f,  1.0f,       1.0f, -1.0f,    0f, 0f, 0f,
        //     1.0f,  1.0f,  1.0f,       1.0f,  1.0f,    0f, 0f, 0f,
        //    -1.0f,  1.0f,  1.0f,      -1.0f,  1.0f,    0f, 0f, 0f,
        //    //// back	                // back colors
        //    //-1.0f, -1.0f, -1.0f,       0.0f, 0.0f,    0f, 0f, 0f,
        //    // 1.0f, -1.0f, -1.0f,       1.0f, 1.0f,    0f, 0f, 0f,
        //    // 1.0f,  1.0f, -1.0f,       1.0f, 1.0f,    0f, 0f, 0f,
        //    //-1.0f,  1.0f, -1.0f,       0.0f, 1.0f,    0f, 0f, 0f,
        //};

        //  readonly ushort[] indices = { 
        //// front
        //0, 1, 2,
        //      2, 3, 0,
        ////// right
        ////1, 5, 6,
        ////      6, 2, 1,
        ////// back
        ////7, 6, 5,
        ////      5, 4, 7,
        ////// left
        ////4, 0, 3,
        ////      3, 7, 4,
        ////// bottom
        ////4, 5, 1,
        ////      1, 0, 4,
        ////// top
        ////3, 2, 6,
        ////      6, 7, 3,
        //  };

        SceneBase scene;

        public Program(int width, int height, string name) : base(width, height, name) { }

        public override void Start()
        {
            GUIManager.Init();
            Container container = new Container()
            {
                Layout = new ComponentLayout
                {
                    X = new PixelConstraint(20),
                    Y = new RelativeConstraint(0.05f),
                    W = new RelativeConstraint(0.2f),
                    H = new RelativeConstraint(0.9f)
                }
            };
            GUIManager.AddComponent(container);
            var btn = new Button("Test")
            {
                BackgroundColour = new Colour(0.4f, 0.2f, 0.6f),
                Layout = new ComponentLayout()
                {
                    X = new RelativeConstraint(0),
                    Y = new CenterConstraint(),
                    W = new RelativeConstraint(0.5f),
                    H = new PixelConstraint(30),
                },
            };
            btn.UpdateLayout();
            container.AddChild(btn);
            btn.Setup();
            scene = new SceneBase
            {
                Camera = new OrbitalCamera(Vector3.Zero, new Vector3(0, 900, 0), Width / (float)Height),
                Sun = new Sun(new Colour(1, 1, 1))
            };
            AddWindowResizeListener((int width, int height) => scene.Camera.OnWindowResize(width, height));
            AddWindowResizeListener((int width, int height) => GUIManager.OnResize(width, height));
            scene.Terrain = new FlatTerrain(16, new Colour(0.31f, 0.56f, 0.37f));
            scene.GenerateObjs(35, -16, 16, AssetManager.LoadOBJ("Resources/Models/Tree.obj"));
        }

        public override void Update(float delta)
        {
            scene.Update(delta);
            Renderer.BeginScene(scene);
            Renderer.EndScene();
            GUIManager.DrawComponents();
        }

        public override void Exit()
        {
        }

        public static void Main(string[] args)
        {
            if (args.Length != 0)
                Console.WriteLine("Proragm Launched with args: " + args);
            Program program = new Program(600, 600, "Lamp");
            program.Run();
        }
    }
}