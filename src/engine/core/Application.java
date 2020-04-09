package engine.core;

import engine.renderer.Renderer;

import static org.lwjgl.opengl.GL11.*;

public abstract class Application {

    private String name;
    public final Input input;
    private Colour background;
    private int height;
    private int width;

    public static Window window;

    public Application(String appName, int width, int height) {
        this.name = appName;
        this.width = width;
        this.height = height;
        window = new Window(this.name, this.width, this.height);
        input = new Input(window.getId());
    }

    public void run() {
        start();
        Log.info("Application started successfully");
        Log.info("Application running on OpenGL " + glGetString(GL_VERSION));
        Log.info("Application using Graphics card " + glGetString(GL_RENDERER));
        while (!window.isCloseRequested()) {
            preDraw();
            draw();
            postDraw();
        }
        finish();
        pFinish();
    }

    public abstract void start();

    public abstract void draw();

    public abstract void finish();

    private void preDraw() {
        Renderer.clear(background);
    }

    private void postDraw() {
        window.update();
    }

    private void pFinish() {
        window.close();
    }

    public void showWindow() {
        window.show();
    }

    public void background(Colour colour) {
        this.background = colour;
    }

    public int height() {
        return height;
    }

    public int width() {
        return width;
    }

    public String toString() {
        return this.name;
    }
}
