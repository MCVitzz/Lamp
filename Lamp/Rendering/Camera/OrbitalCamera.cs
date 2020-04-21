using Lamp.Core;
using Lamp.Utilities;
using OpenTK;
using System;

namespace Lamp.Rendering.Camera
{
    public class OrbitalCamera : ICamera
    {
        private const float Fov = 60;
        private const float NearPlane = 0.1f;
        private const float FarPlane = 1000;
        private const float Sensitivity = 0.4f;
        private const float ScrollSensivity = 1f;

        private float AspectRatio;

        private Vector3 Position;
        private Vector3 Target;

        private Matrix4 ViewMatrix;
        private Matrix4 ProjectionMatrix;

        private readonly SmoothFloat verticalAngle;
        private readonly SmoothFloat horizontalAngle;
        private readonly SmoothFloat distance;

        public OrbitalCamera(Vector3 target, Vector3 position, float aspectRatio)
        {
            Position = position;
            Target = target;
            AspectRatio = aspectRatio;
            verticalAngle = new SmoothFloat(0, 10);
            horizontalAngle = new SmoothFloat(0, 10);
            distance = new SmoothFloat(2, 10);
            CalculateProjectionMatrix();
            CalculateViewMatrix();
        }

        public void Move(float delta)
        {
            if (Input.Instance.MiddleButton)
            {
                verticalAngle.Target -= Input.Instance.Delta.Y * Sensitivity / 4;
                horizontalAngle.Target += Input.Instance.Delta.X * Sensitivity / 4;
                Clamp(verticalAngle, -89, 89);
            }
            if (distance.Get() > 0)
            {
                distance.Target -= Input.Instance.ScrollOffset * ScrollSensivity;
            }
            else
            {
                distance.Target = 0.1f;
            }

            float horizontalDistance = (float)(distance.Get() * Math.Cos(MathHelper.DegreesToRadians(verticalAngle.Get())));
            float verticalDistance = (float)(distance.Get() * Math.Sin(MathHelper.DegreesToRadians(verticalAngle.Get())));

            float xOffset = (float)(horizontalDistance * Math.Sin(MathHelper.DegreesToRadians(-horizontalAngle.Get())));
            float zOffset = (float)(horizontalDistance * Math.Cos(MathHelper.DegreesToRadians(-horizontalAngle.Get())));

            Position = new Vector3(Target.X + xOffset, Target.Y - verticalDistance, Target.Z + zOffset);

            CalculateViewMatrix();
            verticalAngle.Update(delta);
            horizontalAngle.Update(delta);
            distance.Update(delta);
        }

        public Vector3 GetPosition()
        {
            return Position;
        }

        private void CalculateViewMatrix()
        {
            ViewMatrix = Matrix4.LookAt(Position, Target, Vector3.UnitY);
        }

        public Matrix4 GetViewMatrix()
        {
            return ViewMatrix;
        }

        private void CalculateProjectionMatrix()
        {
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), AspectRatio, NearPlane, FarPlane);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return ProjectionMatrix;
        }

        public Matrix4 GetViewProjectionMatrix()
        {
            throw new System.NotImplementedException();
        }

        public void Clamp(SmoothFloat value, float min, float max)
        {
            if (value.Target < min)
            {
                value.Target = min;
            }
            else if (value.Target > max)
            {
                value.Target = max;
            }
        }

        public void OnWindowResize(int width, int height)
        {
            AspectRatio = (float) width / height;
            CalculateProjectionMatrix();
        }
    }
}