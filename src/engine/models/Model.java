package engine.models;

import engine.math.Matrix4;
import engine.math.Vector3;
import engine.renderer.buffers.VAO;

public abstract class Model {
    private Vector3[] vertices;
    private int[] indices;

    private VAO vao;

    public abstract Matrix4 getModelMatrix();

    public int[] getIndices() {
        return this.indices;
    }

    public Vector3[] getVertices() {
        return this.vertices;
    }

    public void setVao(VAO vao) {
        this.vao = vao;
    }

    public VAO getVao() {
        return this.vao;
    }
}
