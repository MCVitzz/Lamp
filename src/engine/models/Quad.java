package engine.models;

import engine.math.Matrix4;
import engine.math.Vector3;

public class Quad extends Model {

    Vector3[] vertices;
    int[] indices;
    private Vector3 position;
    private Vector3 rotation;
    private float scale;

    public Quad(Vector3[] vertices, int[] indices) {
        this.vertices = vertices;
        this.indices = indices;
        position = new Vector3(0, 0, 0);
        rotation = new Vector3(0, 0, 0);
        scale = 1;
    }

    public Vector3 getPosition() {
        return position;
    }

    public void setPosition(Vector3 position) {
        this.position = position;
    }

    public Vector3 getRotation() {
        return rotation;
    }

    public float getScale() {
        return scale;
    }

    public Vector3[] getVertices() {
        return vertices;
    }

    public int[] getIndices() {
        return indices;
    }

    public Matrix4 getModelMatrix() {
        Matrix4 matrix = new Matrix4();
        matrix.identity();
        matrix.translate(position);
        matrix.rotate((float) Math.toRadians(rotation.x), new Vector3(1.0f, 0.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.y), new Vector3(0.0f, 1.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.z), new Vector3(0.0f, 0.0f, 1.0f));
        matrix.scale(scale);
        return matrix;
    }
}
