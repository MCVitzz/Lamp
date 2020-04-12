package engine.core;

import engine.renderer.Renderer;

public abstract class Application {

    private String name;
    public final Input input;
    private Colour background;
    private int height;
    private int width;
    protected float delta;

    public static Window window;

    protected Application(String appName, int width, int height) {
        this.name = appName;
        this.width = width;
        this.height = height;
        window = new Window(this.name, this.width, this.height);
        input = new Input(window.getId());
    }

    public void run() {
        start();
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
        delta = Timing.getInstance().getDelta();
        Renderer.clear(background);
    }

    private void postDraw() {
        input.update();
        window.update();
        Timing.getInstance().update();
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
