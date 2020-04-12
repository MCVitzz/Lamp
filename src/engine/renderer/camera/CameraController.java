package engine.renderer.camera;

public interface CameraController {
    public float getZoomInput();
    public float getPitchInput();
    public float getYawInput();
    public boolean goRight();
    public boolean goLeft();
    public boolean goForwards();
    public boolean goBackwards();
}
