package engine.models;

import engine.math.Vector3;
import engine.renderer.buffers.QuadVBO;

public class Quad {

    Vector3[] vertices;
    int[] indices;
    private Vector3 position;
    private Vector3 rotation;
    private float scale;

    public Quad(Vector3[] vertices, int[] indices) {
        this.vertices = vertices;
        this.indices = indices;
        position = new Vector3(1,0,0);
        rotation = new Vector3(0,0,0);
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

    public void getModelMatrix() {

    }
}
