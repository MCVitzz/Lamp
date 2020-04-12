package engine.core;

import org.lwjgl.glfw.GLFW;

public class Timing {

    private double delta;
    private double lastFrameTime;

    private static Timing instance;

    public static Timing getInstance() {
        if (instance == null) {
            instance = new Timing();
        }
        return instance;
    }

    private Timing() {
        this.delta = 0;
        this.lastFrameTime = getCurrentTime();
        this.lastFrameTime = 0;
    }

    public void update() {
        double currentFrameTime = getCurrentTime();
        delta = (currentFrameTime - lastFrameTime) / 1000;
        lastFrameTime = currentFrameTime;
    }

    public float getDelta() {
        return (float) delta;
    }

    private long getCurrentTime() {
        return (long) (GLFW.glfwGetTime() * 1000);
    }

}
