package sandbox;

import engine.core.Application;
import engine.core.Colour;
import engine.math.Matrix4;
import engine.math.Vector3;
import engine.models.Quad;
import engine.renderer.Camera;
import engine.renderer.Renderer;
import engine.renderer.buffers.QuadVBO;
import engine.shaders.Shader;

public class Game extends Application {

    public Game() {
        super("Game", 1280, 720);
    }

    Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, 0f),
            new Vector3(0.5f, -0.5f, 0f),
            new Vector3(0, 0.5f, 0f),
    };

    int[] indices = { 0, 1, 2 };

    Quad quad;
    QuadVBO vbo;
    Shader shader;
    Camera camera;

    public void start() {
        showWindow();
        background(new Colour(.7f, .8f, 1));
        quad = new Quad(vertices, indices);
        vbo = new QuadVBO();
        vbo.allocate(quad);
        shader = new Shader("vertexShader", "fragmentShader");
//        shader.addUniform("view");
//        shader.addUniform("projection");
        shader.addUniform("model");
        camera = new Camera(new Vector3(0,0,0));
    }

    public void draw() {

        Renderer.beginScene();

        shader.bind();
//        shader.uploadUniform("view", camera.getViewMatrix());
//        shader.uploadUniform("projection", camera.getProjectionMatrix());
        shader.uploadUniform("model", Matrix4.transformation(quad.getPosition(), quad.getRotation(), quad.getScale()));
        Renderer.submit(vbo);

        Renderer.endScene();
    }

    public void finish() {

    }

    public static void main(String[] args) {
        Game game = new Game();
        game.run();
    }
}
