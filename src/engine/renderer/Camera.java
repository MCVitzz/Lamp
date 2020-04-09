package engine.renderer;

import engine.core.Application;
import engine.math.Matrix4;
import engine.math.Vector3;

public class Camera {
    private float nearPlane = .5f;
    private float fov = 70;
    private float farPlane = 1000f;
    private Vector3 position;
    private Vector3 rotation = new Vector3(0,0,0);
    private Matrix4 viewMatrix;
    private Matrix4 projectionMatrix;

    public Camera(Vector3 position) {
        this.position = position;
        calculateProjectionMatrix();
        calculateViewMatrix();
    }


    public Vector3 getPosition() {
        return position;
    }

    public Vector3 getRotation() {
        return rotation;
    }

    public void setPosition(Vector3 position) {
        this.position = position;
        calculateViewMatrix();
    }

    public void setRotation(Vector3 rotation) {
        this.rotation = rotation;
        calculateViewMatrix();
    }

    public Matrix4 getViewMatrix() {
        return viewMatrix;
    }

    public Matrix4 getProjectionMatrix() {
        return projectionMatrix;
    }

    private void calculateViewMatrix() {
        Matrix4 viewMatrix = new Matrix4();
        viewMatrix.identity();
        viewMatrix.rotate((float) Math.toRadians(rotation.x), new Vector3(1, 0, 0));
        viewMatrix.rotate((float) Math.toRadians(rotation.y), new Vector3(0, 1, 0));
        Vector3 negativeCameraPos = new Vector3(-position.x, -position.y, -position.z);
        viewMatrix.translate(negativeCameraPos);
        this.viewMatrix = viewMatrix;
    }

    private void calculateProjectionMatrix() {
        Matrix4 projectionMatrix = new Matrix4();
        float aspectRatio = (float) Application.window.width / (float) Application.window.height;
        float yScale = (float) ((1f / Math.tan(Math.toRadians(fov / 2f))));
        float xScale = yScale / aspectRatio;
        float frustumLength = farPlane - nearPlane;

        projectionMatrix.set(0,0, xScale);
        projectionMatrix.set(1,1, (yScale));
        projectionMatrix.set(2,2, (-((farPlane + nearPlane) / frustumLength)));
        projectionMatrix.set(2,3, (-1));
        projectionMatrix.set(3,2, (-((2 * nearPlane * farPlane) / frustumLength)));
        projectionMatrix.set(3,3, (0));
        this.projectionMatrix = projectionMatrix;
    }
}

