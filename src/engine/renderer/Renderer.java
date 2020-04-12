package engine.renderer;

import engine.core.Colour;
import engine.renderer.buffers.VAO;
import org.lwjgl.opengl.GL;

import static org.lwjgl.opengl.GL33.*;

public class Renderer {
    public static void beginScene() {
    }

    public static void submit(VAO VAO) {
        VAO.bind();
        VAO.draw();
    }

    public static void endScene() {
    }

    public static void createCapabilities() {
        GL.createCapabilities();
    }

    public static void clear(Colour colour) {
        glEnable(GL_DEPTH_TEST);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        glClearColor(colour.r, colour.g, colour.b, colour.a);
    }
}
