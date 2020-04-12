package engine.renderer.camera;

import engine.core.Input;

public class PerspectiveCameraController implements CameraController {

    private Input input;

    public PerspectiveCameraController(Input input) {
        this.input = input;

    }

    @Override
    public float getZoomInput() {
        return input.getScroll();
    }

    @Override
    public float getPitchInput() {
        if (input.middleMouseClicked()) {
            System.out.println(input.getDelta());
            return input.getDelta().y;
        } else {
            return 0;
        }
    }

    @Override
    public float getYawInput() {
        if (input.middleMouseClicked()) {
            System.out.println("HERE" + input.getDelta());
            return input.getDelta().x;
        } else {
            return 0;
        }
    }

    @Override
    public boolean goRight() {
        return input.isKeyPressed('D');
    }

    @Override
    public boolean goLeft() {
        return input.isKeyPressed('A');
    }

    @Override
    public boolean goForwards() {
        return input.isKeyPressed('W');
    }

    @Override
    public boolean goBackwards() {
        return input.isKeyPressed('S');
    }
}
