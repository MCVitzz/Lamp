package engine.renderer.camera;

import engine.events.Listener;
import engine.math.Matrix4;
import engine.math.Vector3;

public interface Camera {
    public void move(float delta);
    public Vector3 getPosition();
    public Matrix4 getViewMatrix();
    public Matrix4 getProjectionMatrix();
    public Matrix4 getProjectionViewMatrix();
    public void addMoveListener(Listener listener);
}
