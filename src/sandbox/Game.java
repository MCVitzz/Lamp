package sandbox;

import engine.core.Application;
import engine.core.Colour;
import engine.math.Vector3;
import engine.models.Quad;
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
    QuadVBO quadVBO;
    Shader shader;

    public void start() {
        showWindow();
        background(new Colour(.7f, .8f, 1));
        quad = new Quad(vertices, indices);
        quadVBO = new QuadVBO();
        quadVBO.allocate(quad);
        shader = new Shader("vertexShader", "fragmentShader");
    }

    public void draw() {
        shader.bind();
        quadVBO.draw();
        shader.unbind();
    }

    public void finish() {

    }

    public static void main(String[] args) {
        Game game = new Game();
        game.run();
    }
}
