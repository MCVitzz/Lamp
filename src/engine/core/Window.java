package engine.core;

import org.lwjgl.glfw.GLFWVidMode;

import static org.lwjgl.glfw.GLFW.*;
import static org.lwjgl.opengl.GL45.*;
import static org.lwjgl.opengl.GL.createCapabilities;


public class Window {
    private long id;
    public final int height;
    public final int width;

    Window(String name, int width, int height) {
        this.height = height;
        this.width = width;
        if (!glfwInit()) {
            Log.error("Error: Couldn't initialize GLFW.");
            System.exit(-1);
        }
        glfwWindowHint(GLFW_RESIZABLE, GLFW_FALSE);
        id = glfwCreateWindow(width, height, name, 0, 0);
        if (id == 0) {
            Log.error("Window couldn't be created.");
            System.exit(-1);
        }

        glfwHideWindow(id);
    }

    void show() {
        glfwShowWindow(id);
        glfwMakeContextCurrent(id);
        createCapabilities();

        GLFWVidMode vidMode = glfwGetVideoMode(glfwGetPrimaryMonitor());
        if (vidMode != null) {
            glfwSetWindowPos(id,
                    (vidMode.width() - width) / 2,
                    (vidMode.height() - height) / 2
            );
        } else {
            Log.error("Couldn't detect a primary monitor.");
            System.exit(-1);
        }
    }

    public void update() {
        glfwSwapBuffers(id);
        glfwPollEvents();
    }

    long getId() {
        return id;
    }

    void frameRate(int rate) {
        glfwSwapInterval(rate);
    }

    void close() {
        glfwDestroyWindow(id);
    }

    boolean isCloseRequested() {
        return glfwWindowShouldClose(id);
    }
}
