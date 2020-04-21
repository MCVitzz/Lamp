using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Scene.Terrains;

namespace Lamp.Scene
{
    public class SceneBase
    {
        public ICamera Camera;
        public FlatTerrain Terrain;
        public Light Sun;

        public void Draw()
        {
            Terrain.Draw(Camera, Sun);
        }

        public void Update(float delta)
        {
            Camera.Move(delta);
        }
    }
}
