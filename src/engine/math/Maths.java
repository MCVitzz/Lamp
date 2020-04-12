package engine.math;

import engine.core.Application;
import engine.renderer.camera.PerspectiveCamera;

public class Maths {

//    public static Matrix4 createModelMatrix(Vector3 position, Vector3 rotation, float scale) {
//        Matrix4 matrix = new Matrix4();
//        matrix.translate(position);
//        matrix.rotate((float) Math.toRadians(rotation.x), new Vector3(1.0f, 0.0f, 0.0f));
//        matrix.rotate((float) Math.toRadians(rotation.y), new Vector3(0.0f, 1.0f, 0.0f));
//        matrix.rotate((float) Math.toRadians(rotation.z), new Vector3(0.0f, 0.0f, 1.0f));
//        matrix.scale(scale);
//        return matrix;
//    }

    public static Matrix4 createViewMatrix(PerspectiveCamera camera) {
        Matrix4 viewMatrix = new Matrix4();
        viewMatrix.identity();
        viewMatrix.rotate((float) Math.toRadians(camera.getPitch()), new Vector3(1, 0, 0));
        viewMatrix.rotate((float) Math.toRadians(camera.getYaw()), new Vector3(0, 1, 0));
        Vector3 cameraPos = camera.getPosition();
        Vector3 negativeCameraPos = new Vector3(-cameraPos.x, -cameraPos.y, -cameraPos.z);
        viewMatrix.translate(negativeCameraPos);
        return viewMatrix;
    }

    public static Matrix4 getProjectionMatrix(float near, float far, float fov) {

        Matrix4 matrix = new Matrix4();

        float tanFOV = (float) Math.tan(Math.toRadians(fov / 2));
        float aspectRatio = (float) Application.window.width / Application.window.height;

        matrix.values[0][0] = 1 / (tanFOV * aspectRatio);
        matrix.values[1][1] = 1 / tanFOV;
        matrix.values[2][2] = far / (far - near);
        matrix.values[2][3] = far * near / (far - near);
        matrix.values[3][2] = 1;
        matrix.values[3][3] = 1;

        return matrix;
        /*
        Matrix4 projectionMatrix = new Matrix4();
        float aspectRatio = (float) Application.window.width / (float) Application.window.height;
        float yScale = (float) ((1f / Math.tan(projectionMatrix.values[Math.toRadians(fov / 2f))));
        float xScale = yScale / aspectRatio;
        float frustumLength = far - near;

        projectionMatrix.values[0][0] = xScale;
        projectionMatrix.values[1][1] = yScale;
        projectionMatrix.values[2][2] = -((far + near) / frustumLength);
        projectionMatrix.values[2][3] = -1;
        projectionMatrix.values[3][2] = -((2 * near * far) / frustumLength);
        projectionMatrix.values[3][3] = 0;

        return projectionMatrix;*/
    }
}
