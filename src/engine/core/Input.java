package engine.core;

import engine.math.Vector2;

import java.util.HashMap;

import static org.lwjgl.glfw.GLFW.*;

public class Input {

    private HashMap<Integer, Character> keys;
    private Vector2 cursorPosition, oldCursorPosition, delta;
    private boolean leftButton, middleButton, rightButton;
    private float scrollOffset;

    Input(long window) {
        this.cursorPosition = new Vector2(0, 0);
        this.oldCursorPosition = new Vector2(0, 0);
        this.keys = new HashMap<>();
        glfwSetKeyCallback(window, this::keyCallback);
        glfwSetCursorPosCallback(window, this::cursorPositionCallback);
        glfwSetMouseButtonCallback(window, this::mouseClickedCallback);
        glfwSetScrollCallback(window, this::mouseScrollCallback);
    }

    void keyCallback(long window, int key, int scancode, int action, int mods) {
        if (action == GLFW_PRESS) {
            keys.put(key, (char) key);
            Log.log("Key " + (char) key + " was pressed.");
        } else if (action == GLFW_RELEASE) {
            keys.remove(key);
            Log.log("Key " + (char) key + " was released.");
        }
    }

    void cursorPositionCallback(long window, double xPos, double yPos) {
        oldCursorPosition = cursorPosition;
        cursorPosition = new Vector2((float) xPos / Application.window.width, (float) yPos / Application.window.height);
        Log.log("Mouse at " + cursorPosition.toString() + ".");
    }

    void mouseClickedCallback(long window, int button, int action, int mods) {
        if (button == GLFW_MOUSE_BUTTON_1) {
            if (action == GLFW_PRESS) {
                leftButton = true;
                Log.log("Left button was pressed.");
            } else if (action == GLFW_RELEASE) {
                leftButton = false;
                Log.log("Left button was released.");
            }
        }
        if (button == GLFW_MOUSE_BUTTON_2) {
            if (action == GLFW_PRESS) {
                rightButton = true;
                Log.log("Right button was pressed.");
            } else if (action == GLFW_RELEASE) {
                rightButton = false;
                Log.log("Right button was released.");
            }
        }
        if (button == GLFW_MOUSE_BUTTON_3) {
            if (action == GLFW_PRESS) {
                middleButton = true;
                Log.log("Middle button was pressed.");
            } else if (action == GLFW_RELEASE) {
                middleButton = false;
                Log.log("Middle button was released.");
            }
        }
    }

    void mouseScrollCallback(long window, double xOffset, double yOffset) {
        scrollOffset = (float) yOffset;
        Log.log("Scrolled for " + yOffset + " units.");
    }

    public Vector2 getCursorPosition() {
        return this.cursorPosition.copy();
    }

    public boolean leftMouseClicked() {
        return leftButton;
    }

    public boolean rightMouseClicked() {
        return rightButton;
    }

    public boolean middleMouseClicked() {
        return middleButton;
    }

    public boolean isKeyPressed(char c) {
        //System.out.println("Looking for " + (int)c);;
        if (keys.get((int) c) == null) {
            return false;
        }
        return keys.get((int) c) != 0;
    }

    public float getScroll() {
        return scrollOffset;
    }

    public Vector2 getDelta() {
        return delta;
    }

    public void update() {
        updateDeltas();
        scrollOffset = 0;
    }

    private void updateDeltas() {
        this.delta = Vector2.sub(cursorPosition, oldCursorPosition);
        this.oldCursorPosition = cursorPosition.copy();
    }

    //Mouse pos, click, scroll
    //Keyboard key pressed, released


}
