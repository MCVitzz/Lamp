using Lamp.Core;
using Lamp.Resources;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Serilog;
using System.Collections.Generic;

namespace Lamp.Shaders
{
    public class Shader
    {
        public int Program { get; private set; }
        private readonly Dictionary<string, int> uniforms;

        public Shader(string vertexPath, string fragmentPath)
        {
            CreateShaders(vertexPath, fragmentPath);
            uniforms = new Dictionary<string, int>();
        }

        public void AddUniform(string name)
        {
            int location = GL.GetUniformLocation(Program, name);
            uniforms.Add(name, location);
        }

        public void UpdateUniform(string name, Colour colour)
        {
            GL.Uniform4(uniforms[name], colour.ToVector4());
        }
        public void UpdateUniform(string name, Vector3 vector)
        {
            GL.Uniform3(uniforms[name], vector.X, vector.Y, vector.Z);
        }

        public void UpdateUniform(string name, Matrix4 matrix)
        {
            GL.UniformMatrix4(uniforms[name], false, ref matrix);
        }

        public void Bind()
        {
            GL.UseProgram(Program);
        }

        public void Destroy()
        {
            GL.DeleteProgram(Program);
        }

        private void CreateShaders(string vertexPath, string fragmentPath)
        {

            string vSrc = AssetManager.Read(vertexPath);
            string fSrc = AssetManager.Read(fragmentPath);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, vSrc);
            GL.ShaderSource(fragmentShader, fSrc);

            CompileShader(vertexShader);
            CompileShader(fragmentShader);

            LinkShader(vertexShader, fragmentShader);
        }

        private void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            string infoLog = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
            GL.ClearColor(0.8f, 0.5f, 0.2f, 0.0f);
                Log.Error(infoLog);
            }
        }

        private void LinkShader(int vertexShader, int fragmentShader)
        {
            Program = GL.CreateProgram();
            GL.AttachShader(Program, vertexShader);
            GL.AttachShader(Program, fragmentShader);
            GL.LinkProgram(Program);
            string infoLog = GL.GetProgramInfoLog(Program);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                Log.Error(infoLog);
            }
            else
            {
                Log.Information("Shader created and linked.");
            }

            GL.DetachShader(Program, vertexShader);
            GL.DetachShader(Program, fragmentShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }
    }
}
