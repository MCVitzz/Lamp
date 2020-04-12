package sandbox;

import engine.core.Application;
import engine.core.Colour;
import engine.math.Vector3;
import engine.models.Quad;
import engine.renderer.Renderer;
import engine.renderer.buffers.BufferElement;
import engine.renderer.buffers.BufferLayout;
import engine.renderer.buffers.VAO;
import engine.renderer.buffers.VBO;
import engine.renderer.camera.PerspectiveCamera;
import engine.shaders.Shader;
import engine.shaders.ShaderDataType;
import engine.utils.Factory;

public class Game extends Application {

    public Game() {
        super("Game", 1280, 720);
    }

//    Vector3[] vertices = {
//            new Vector3(-0.5f, -0.5f, 0f),
//            new Vector3(0.5f, -0.5f, 0f),
//            new Vector3(0, 0.5f, 0f),
//    };
//
//    int[] indices = { 0, 1, 2 };

    float[] vertices = {
            -0.5f, 0.5f, 0,
            -0.5f, -0.5f, 0,
            0.5f, -0.5f, 0,
            0.5f, 0.5f, 0,

            -0.5f, 0.5f, 1,
            -0.5f, -0.5f, 1,
            0.5f, -0.5f, 1,
            0.5f, 0.5f, 1,

            0.5f, 0.5f, 0,
            0.5f, -0.5f, 0,
            0.5f, -0.5f, 1,
            0.5f, 0.5f, 1,

            -0.5f, 0.5f, 0,
            -0.5f, -0.5f, 0,
            -0.5f, -0.5f, 1,
            -0.5f, 0.5f, 1,

            -0.5f, 0.5f, 1,
            -0.5f, 0.5f, 0,
            0.5f, 0.5f, 0,
            0.5f, 0.5f, 1,

            -0.5f, -0.5f, 1,
            -0.5f, -0.5f, 0,
            0.5f, -0.5f, 0,
            0.5f, -0.5f, 1

    };

    int[] indices = {
            0, 1, 3,
            3, 1, 2,
            4, 5, 7,
            7, 5, 6,
            8, 9, 11,
            11, 9, 10,
            12, 13, 15,
            15, 13, 14,
            16, 17, 19,
            19, 17, 18,
            20, 21, 23,
            23, 21, 22

    };

    Quad quad;
    VAO vao;
    Shader shader;
    PerspectiveCamera camera;

    public void start() {
        showWindow();
        background(new Colour(.7f, .8f, .9f));
        shader = new Shader("vertexShader", "fragmentShader");
        quad = new Quad(toVec3Arr(vertices), indices);
        vao = new VAO();

        BufferLayout layout = new BufferLayout(new BufferElement[]{
                new BufferElement("position", ShaderDataType.VEC3),
        });
        vao.bind();
        vao.setIBO(indices);
        VBO vbo = Factory.createVBO(quad, layout);
        vao.addVBO(vbo);
        vao.bindIbo();

        shader.addUniform("view");
        shader.addUniform("projection");
        shader.addUniform("model");
        camera = new PerspectiveCamera(input);
        quad.getPosition().z = -1;
    }

    public void draw() {
        Renderer.beginScene();
        shader.bind();
        camera.move(delta);
        shader.uploadUniform("view", camera.getViewMatrix());
        shader.uploadUniform("projection", camera.getProjectionMatrix());
        shader.uploadUniform("model", quad.getModelMatrix());
        Renderer.submit(vao);

        Renderer.endScene();
    }

    public void finish() {

    }

    public static void main(String[] args) {
        Game game = new Game();
        game.run();
    }

    public Vector3[] toVec3Arr(float[] data) {
        Vector3[] vs = new Vector3[data.length / 3];

        for (int i = 0; i < data.length; i += 3) {
            float x = data[i];
            float y = data[i + 1];
            float z = data[i + 2];
            Vector3 v = new Vector3(x, y, z);
            vs[i / 3] = v;
        }
        return vs;
    }
}
