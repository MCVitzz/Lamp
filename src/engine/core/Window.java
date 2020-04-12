package engine.core;

import engine.renderer.Renderer;
import org.lwjgl.glfw.GLFWVidMode;
import org.lwjgl.opengl.GLUtil;

import static org.lwjgl.glfw.Callbacks.glfwFreeCallbacks;
import static org.lwjgl.glfw.GLFW.*;


public class Window {
    private long id;
    public final int height;
    public final int width;

    public Window(String name, int width, int height) {
        this.height = height;
        this.width = width;
        if (!glfwInit()) {
            Log.error("Error: Couldn't initialize GLFW.");
            System.exit(-1);
        }
        glfwWindowHint(GLFW_RESIZABLE, GLFW_FALSE);
        glfwWindowHint(GLFW_OPENGL_DEBUG_CONTEXT, GLFW_TRUE);
        id = glfwCreateWindow(width, height, name, 0, 0);
        if (id == 0) {
            Log.error("Window couldn't be created.");
            System.exit(-1);
        }

        glfwHideWindow(id);
    }

    public void show() {
        glfwShowWindow(id);
        glfwMakeContextCurrent(id);
        Renderer.createCapabilities();
        //DEBUG STUFF
        GLUtil.setupDebugMessageCallback();

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
        glfwFreeCallbacks(id);
        glfwDestroyWindow(id);
        glfwTerminate();
    }

    public boolean isCloseRequested() {
        return glfwWindowShouldClose(id);
    }
}
