using Lamp.Scene;

namespace Lamp.Rendering
{
    public static class Renderer
    {
        private static SceneBase Scene;

        public static void BeginScene(SceneBase scene)
        {
            Scene = scene;
        }

        public static void Submit(GameObject gameObject)
        {
            gameObject.Draw(Scene.Camera, Scene.Sun);
        }

        public static void EndScene()
        {
            Scene = null;
        }
    }
}