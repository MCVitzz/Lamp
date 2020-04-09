package engine.models;

import engine.math.Vector3;

public class Quad {

    Vector3[] vertices;
    int[] indices;

    public Quad(Vector3[] vertices, int[] indices) {
        this.vertices = vertices;
        this.indices = indices;
    }

    public Vector3[] getVertices() {
        return vertices;
    }

    public int[] getIndices() {
        return indices;
    }
}
