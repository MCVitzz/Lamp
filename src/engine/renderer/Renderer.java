package engine.renderer;

import engine.core.Colour;
import engine.renderer.buffers.VBO;

import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.opengl.GL11.glClearColor;

public class Renderer {

    public static void beginScene() {

    }

    public static void submit(VBO vbo) {
        vbo.bind();
        vbo.draw();
    }

    public static void endScene() {
    }

    public static void clear(Colour colour) {
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        glClearColor(colour.r, colour.g, colour.b, colour.a);
    }
}
