package engine.renderer.camera;

import engine.core.Application;
import engine.core.Input;
import engine.events.Event;
import engine.events.EventTrigger;
import engine.events.Listener;
import engine.math.Matrix4;
import engine.math.SmoothFloat;
import engine.math.Vector3;

import java.util.ArrayList;

public class PerspectiveCamera implements Camera, EventTrigger<Event> {
    private static final float PITCH_SENSITIVITY = 150f;
    private static final float YAW_SENSITIVITY = 190f;
    private static final float MAX_PITCH = 90;
    private static final float Y_OFFSET = 0.02f;
    private static final float SPEED = 3;

    private static final float nearPlane = .5f;
    private static final float fov = 70;
    private static final float farPlane = 1000f;
    private final Vector3 target;
    private final Vector3 position;
    private float yaw;
    private final SmoothFloat pitch;
    private final SmoothFloat angleAroundPlayer;
    private final SmoothFloat distanceFromPlayer;
    private final SmoothFloat forwardSpeed;
    private final SmoothFloat sideSpeed;
    private final Matrix4 viewMatrix;
    private Matrix4 projectionMatrix;
    private final ArrayList<Listener> listeners;
    private final CameraController cameraController;

    public PerspectiveCamera(Input input) {
        position = new Vector3(0, 0, 0);
        viewMatrix = new Matrix4();
        cameraController = new PerspectiveCameraController(input);
        angleAroundPlayer = new SmoothFloat(0, 10);
        distanceFromPlayer = new SmoothFloat(10, 5);
        forwardSpeed = new SmoothFloat(0, 10);
        sideSpeed = new SmoothFloat(0, 10);
        pitch = new SmoothFloat(10, 10);
        target = new Vector3(0, Y_OFFSET, 0);
        calculateProjectionMatrix();
        calculateViewMatrix();
        listeners = new ArrayList<>();
    }

    @Override
    public void move(float delta) {
        calculatePitch(delta);
        calculateAngleAroundPlayer(delta);
        calculateZoom(delta);
        moveTarget(delta);
        float horizontalDistance = calculateHorizontalDistance();
        float verticalDistance = calculateVerticalDistance();
        calculateCameraPosition(horizontalDistance, verticalDistance);
        System.out.println(position);
        this.yaw = 360 - angleAroundPlayer.get();
        yaw %= 360;
        calculateViewMatrix();
    }

    @Override
    public Matrix4 getProjectionViewMatrix() {
        return null;
    }

    @Override
    public void addMoveListener(Listener listener) {

    }

    public Vector3 getPosition() {
        return new Vector3(0, 0, 0);
    }

    private void calculatePitch(float delta) {
        float pitchChange = cameraController.getPitchInput() * PITCH_SENSITIVITY;
        pitch.increaseTarget(pitchChange);
        clampPitch();
        pitch.update(delta);
    }

    private void calculateAngleAroundPlayer(float delta) {
        float angleChange = cameraController.getYawInput() * YAW_SENSITIVITY;
        angleAroundPlayer.increaseTarget(-angleChange);
        angleAroundPlayer.update(delta);
    }

    private void moveTarget(float delta) {
        dealWithInputs();
        updateTargetPosition(delta);
    }

    private void updateTargetPosition(float delta) {
        forwardSpeed.update(delta);
        sideSpeed.update(delta);
        target.x += forwardSpeed.get() * delta * SPEED * -Math.sin(Math.toRadians(yaw));
        target.z += forwardSpeed.get() * delta * SPEED * Math.cos(Math.toRadians(yaw));
        target.x += sideSpeed.get() * delta * SPEED * Math.sin(Math.toRadians(yaw + 90));
        target.z += sideSpeed.get() * delta * SPEED * -Math.cos(Math.toRadians(yaw + 90));
    }

    private void dealWithInputs() {
        if (cameraController.goForwards()) {
            forwardSpeed.setTarget(-1);
        } else if (cameraController.goBackwards()) {
            forwardSpeed.setTarget(1);
        } else {
            forwardSpeed.setTarget(0);
        }
        if (cameraController.goRight()) {
            sideSpeed.setTarget(1);
        } else if (cameraController.goLeft()) {
            sideSpeed.setTarget(-1);
        } else {
            sideSpeed.setTarget(0);
        }
    }

    private void clampPitch() {
        if (pitch.getTarget() < 0) {
            pitch.setTarget(0);
        } else if (pitch.getTarget() > MAX_PITCH) {
            pitch.setTarget(MAX_PITCH);
        }
    }

    private void calculateZoom(float delta) {
        float targetZoom = distanceFromPlayer.getTarget();
        float zoomLevel = cameraController.getZoomInput() * 0.08f * targetZoom;
        targetZoom -= zoomLevel;
        if (targetZoom < 1) {
            targetZoom = 1;
        }
        distanceFromPlayer.setTarget(targetZoom);
        distanceFromPlayer.update(delta);
    }

    public Matrix4 getViewMatrix() {
        return viewMatrix;
    }

    public Matrix4 getProjectionMatrix() {
        return projectionMatrix;
    }

    private void calculateViewMatrix() {
//        if (viewMatrix != null) {
//            Matrix4 old = viewMatrix.copy();
//            viewMatrix = Maths.createViewMatrix(this);
//            System.out.println(viewMatrix.equals(old));
//        } else
//            viewMatrix = Maths.createViewMatrix(this);
        viewMatrix.identity();
        viewMatrix.rotate((float) Math.toRadians(pitch.get()), new Vector3(1, 0, 0));
        viewMatrix.rotate((float) Math.toRadians(yaw), new Vector3(0, 1, 0));
        Vector3 negativeCameraPos = new Vector3(-position.x, -position.y, -position.z);
        viewMatrix.translate(negativeCameraPos);
    }

    private void calculateProjectionMatrix() {
        Matrix4 projectionMatrix = new Matrix4();
        float aspectRatio = (float) Application.window.width / (float) Application.window.height;
        float yScale = (float) ((1f / Math.tan(Math.toRadians(fov / 2f))));
        float xScale = yScale / aspectRatio;
        float frustumLength = farPlane - nearPlane;

        projectionMatrix.set(0, 0, xScale);
        projectionMatrix.set(1, 1, (yScale));
        projectionMatrix.set(2, 2, (-((farPlane + nearPlane) / frustumLength)));
        projectionMatrix.set(2, 3, (-1));
        projectionMatrix.set(3, 2, (-((2 * nearPlane * farPlane) / frustumLength)));
        projectionMatrix.set(3, 3, (0));
        this.projectionMatrix = projectionMatrix;
    }

    public Matrix4 getViewProjectionMatrix() {
        return Matrix4.mul(projectionMatrix, viewMatrix);
    }

    private float calculateHorizontalDistance() {
        return (float) (distanceFromPlayer.get() * Math.cos(Math.toRadians(pitch.get())));
    }

    private float calculateVerticalDistance() {
        return (float) (distanceFromPlayer.get() * Math.sin(Math.toRadians(pitch.get())));
    }

    private void calculateCameraPosition(float horizDistance, float verticDistance) {
        float theta = angleAroundPlayer.get();
        position.x = target.x + (float) (horizDistance * Math.sin(Math.toRadians(theta)));
        position.y = target.y + verticDistance;
        position.z = target.z + (float) (horizDistance * Math.cos(Math.toRadians(theta)));
    }

    public float getYaw() {
        return yaw;
    }

    public float getPitch() {
        return pitch.get();
    }

    @Override
    public void addListener(Listener listener) {
        listeners.add(listener);
    }

    @Override
    public void notifyListeners() {
        for (Listener listener : listeners) {
            listener.eventOccured();
        }
    }

    @Override
    public void eventOccurred(Event event) {
        event.eventOccurred();
    }
}

