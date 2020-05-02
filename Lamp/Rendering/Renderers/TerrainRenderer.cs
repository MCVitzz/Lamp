using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Scene.Terrains;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.Rendering.Renderers
{
    public class TerrainRenderer : BaseRenderer<FlatTerrain>
    {
        public TerrainRenderer() : base(new Shader("Resources/Shaders/Terrain/Vertex.glsl", "Resources/Shaders/Terrain/Fragment.glsl"))
        {
            Shader.AddUniform("modelMatrix");
            Shader.AddUniform("viewMatrix");
            Shader.AddUniform("projectionMatrix");
            Shader.AddUniform("terrainColour");
            Shader.AddUniform("lightColour");
            Shader.AddUniform("lightPosition");
        }

        public override void Render(List<FlatTerrain> terrains, ICamera camera, Light sun)
        {
            foreach (FlatTerrain terrain in terrains)
            {
                Shader.Bind();
                Shader.UpdateUniform("modelMatrix", terrain.Transform.GetMatrix());
                Shader.UpdateUniform("viewMatrix", camera.GetViewMatrix());
                Shader.UpdateUniform("projectionMatrix", camera.GetProjectionMatrix());
                Shader.UpdateUniform("terrainColour", terrain.Colour);
                Shader.UpdateUniform("lightColour", sun.Colour.ToVector3());
                Shader.UpdateUniform("lightPosition", sun.Position);
                terrain.Draw();
            }
        }
    }
}
