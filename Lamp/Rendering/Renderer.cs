using Lamp.Scene;
using System.Collections.Generic;

namespace Lamp.Rendering
{
    public static class Renderer
    {
        private static SceneBase Scene;
        private static List<GameObject> ToDraw;

        public static void BeginScene(SceneBase scene)
        {
            Scene = scene;
            ToDraw = new List<GameObject>();
            ToDraw.AddRange(scene.GameObjects);
            ToDraw.Add(scene.Terrain);
        }

        public static void Submit(GameObject gameObject)
        {
            gameObject.Draw(Scene.Camera, Scene.Sun);
        }

        public static void EndScene()
        {
            foreach (GameObject obj in ToDraw)
            {
                obj.Draw(Scene.Camera, Scene.Sun);
            }
            Scene = null;
            ToDraw = null;
        }
    }
}