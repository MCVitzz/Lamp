﻿using OpenTK;

namespace Lamp.Scene
{
    public class Transform
    {
        public static Vector3 Zero = new Vector3(0, 0, 0);
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public float Scale;

        public Transform() : this(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 1) { }

        public Transform(Vector3 position, Vector3 rotation, float scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public static Matrix4 CalculateTransformationMatrix(Vector3 position)
        {
            return CalculateTransformationMatrix(position, Vector3.Zero, 1);
        }

        public static Matrix4 CalculateTransformationMatrix(Vector3 position, Vector3 rotation, float scale)
        {
            Matrix4 translation = Matrix4.CreateTranslation(position);

            Matrix4 rx = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
            Matrix4 ry = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y));
            Matrix4 rz = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 rot = rx * ry * rz;

            Matrix4 sc = Matrix4.CreateScale(scale);
            return sc * rot * translation;
        }

        public Matrix4 GetMatrix()
        {
            Matrix4 translation = Matrix4.CreateTranslation(Position);

            Matrix4 rx = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X));
            Matrix4 ry = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y));
            Matrix4 rz = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));
            Matrix4 rotation = rx * ry * rz;

            Matrix4 scale = Matrix4.CreateScale(Scale);
            return scale * rotation * translation;
        }
    }

}
