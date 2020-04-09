package engine.math;

public class Maths {

    public static Matrix4 createModelMatrix(Vector3 position, Vector3 rotation, float scale) {
        Matrix4 matrix = new Matrix4();
        matrix.translate(position);
        matrix.rotate((float) Math.toRadians(rotation.x), new Vector3(1.0f, 0.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.y), new Vector3(0.0f, 1.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.z), new Vector3(0.0f, 0.0f, 1.0f));
        matrix.scale(scale);
        return matrix;
    }

//    public static Matrix4 createViewMatrix(Camera camera) {
//        Matrix4 viewMatrix = new Matrix4();
//        viewMatrix.identity();
//        viewMatrix.rotate((float) Math.toRadians(camera.getPitch()), new Vector3(1, 0, 0));
//        viewMatrix.rotate((float) Math.toRadians(camera.getYaw()), new Vector3(0, 1, 0));
//        Vector3 cameraPos = camera.getPosition();
//        Vector3 negativeCameraPos = new Vector3(-cameraPos.x, -cameraPos.y, -cameraPos.z);
//        viewMatrix.translate(negativeCameraPos);
//        return viewMatrix;
//    }
}
