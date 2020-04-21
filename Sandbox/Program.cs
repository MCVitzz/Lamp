using Lamp.Core;
using Lamp.GUI;
using Lamp.Models;
using Lamp.Rendering;
using Lamp.Rendering.Camera;
using Lamp.Resources;
using Lamp.Scene;
using Lamp.Scene.Lights;
using Lamp.Scene.Terrains;
using Lamp.Utilities;
using OpenTK;
using System;

namespace Sandbox
{
    public class Program : Application
    {

        static readonly int Width = 800;
        static readonly int Height = 800;

        readonly float[] vertices = {
            // front	            // front colors
            -1.0f, -1.0f,  1.0f,      -1.0f, -1.0f,    0f, 0f, 0f,
             1.0f, -1.0f,  1.0f,       1.0f, -1.0f,    0f, 0f, 0f,
             1.0f,  1.0f,  1.0f,       1.0f,  1.0f,    0f, 0f, 0f,
            -1.0f,  1.0f,  1.0f,      -1.0f,  1.0f,    0f, 0f, 0f,
            //// back	                // back colors
            //-1.0f, -1.0f, -1.0f,       0.0f, 0.0f,    0f, 0f, 0f,
            // 1.0f, -1.0f, -1.0f,       1.0f, 1.0f,    0f, 0f, 0f,
            // 1.0f,  1.0f, -1.0f,       1.0f, 1.0f,    0f, 0f, 0f,
            //-1.0f,  1.0f, -1.0f,       0.0f, 1.0f,    0f, 0f, 0f,
        };

        readonly ushort[] indices = { 
		    // front
		    0, 1, 2,
            2, 3, 0,
		    //// right
		    //1, 5, 6,
      //      6, 2, 1,
		    //// back
		    //7, 6, 5,
      //      5, 4, 7,
		    //// left
		    //4, 0, 3,
      //      3, 7, 4,
		    //// bottom
		    //4, 5, 1,
      //      1, 0, 4,
		    //// top
		    //3, 2, 6,
      //      6, 7, 3,
        };

        SceneBase scene;
        GameObject obj;
        GameObject obj1;
        Texture texture;
        Button button;

        public Program(int width, int height, string name) : base(width, height, name) { }

        public override void Start()
        {
            button = new Button();
            RawModelData rmd = Loader.LoadOBJ("Resources/Models/Tree.obj");
            obj = new Cube();
            texture = new Texture("Resources/Textures/tree.png");
            scene = new SceneBase();
            scene.Camera = new OrbitalCamera(Vector3.Zero, new Vector3(0, 900, 0), Width / (float)Height);
            scene.Sun = new Sun(new Colour(1, 1, 1));
            AddWindowResizeListener((int width, int height) => scene.Camera.OnWindowResize(width, height));
            scene.Terrain = new FlatTerrain(16, new Colour(0.31f, 0.56f, 0.37f));
            scene.GenerateObjs(35, -16, 16, AssetManager.LoadOBJ("Resources/Models/Tree.obj"));
        }

        public override void Update(float delta)
        {
            scene.Update(delta);
            Renderer.BeginScene(scene);
            Renderer.EndScene();
        }

        public override void Exit()
        {
        }

        class Obj
        {
            public int a;
            public int b;
        }

        static void Main(string[] args)
        {
            if (args.Length != 0)
                Console.WriteLine("Proragm Launched with args: " + args);
            Program program = new Program(Width, Height, "Lamp");
            program.Run();
        }
    }
}