﻿using Lamp.Models;
using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Scene.Terrains;
using OpenTK;
using System;
using System.Collections.Generic;

namespace Lamp.Scene
{
    public class SceneBase
    {
        public ICamera Camera;
        public FlatTerrain Terrain;
        public Light Sun;
        public List<GameObject> GameObjects { get; private set; }

        public SceneBase()
        {
            GameObjects = new List<GameObject>();
        }

        public void Draw()
        {
            Terrain.Draw(Camera, Sun);
        }

        public void Update(float delta)
        {
            Camera.Move(delta);
        }

        public void GenerateObjs(int count, float min, float max, ModelData data)
        {
            Random random = new Random();
            VAO vao = new VAO(data, Cube.Layout);
            for (int i = 0; i < count; i++)
            {
                Cube obj = new Cube(vao);
                obj.Transform.Position = new Vector3(GetFloat(random, min, max), 0, GetFloat(random, min, max));
                obj.Transform.Scale = .1f;
                GameObjects.Add(obj);
            }
        }

        private float GetFloat(Random random, float min, float max)
        {
            float val = (float)random.NextDouble();
            return Map(val, 0, 1, min, max);
        }

        private float Map(float value, float istart, float istop, float ostart, float ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }
    }
}
