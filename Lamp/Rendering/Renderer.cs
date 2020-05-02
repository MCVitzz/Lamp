using Lamp.Rendering.Renderers;
using Lamp.Scene;
using Lamp.Scene.Terrains;
using System.Collections.Generic;

namespace Lamp.Rendering
{
    public static class Renderer
    {
        private static SceneBase Scene;
        private static List<GameObject> ToDraw;
        private static readonly GameObjectRenderer GameObjectRenderer = new GameObjectRenderer();
        private static readonly TerrainRenderer TerrainRenderer = new TerrainRenderer();

        public static void BeginScene(SceneBase scene)
        {
            Scene = scene;
            ToDraw = new List<GameObject>();
            ToDraw.AddRange(scene.GameObjects);
        }

        public static void Submit(GameObject gameObject)
        {
            ToDraw.Add(gameObject);
        }

        public static void EndScene()
        {
            TerrainRenderer.Render(new List<FlatTerrain>(new FlatTerrain[] { Scene.Terrain }), Scene.Camera, Scene.Sun);
            GameObjectRenderer.Render(ToDraw, Scene.Camera, Scene.Sun);
            Scene = null;
            ToDraw = null;
        }
    }
}