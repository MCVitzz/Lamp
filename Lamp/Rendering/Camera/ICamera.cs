using OpenTK;

namespace Lamp.Rendering.Camera
{
    public interface ICamera
    {
        void Move(float delta);
        Vector3 GetPosition();
        Matrix4 GetViewMatrix();
        Matrix4 GetProjectionMatrix();
        Matrix4 GetViewProjectionMatrix();

        void OnWindowResize(int width, int height);
    }
}
