package engine.renderer.buffers;

import engine.models.Quad;
import engine.utils.BufferUtil;

import static org.lwjgl.opengl.GL15.*;
import static org.lwjgl.opengl.GL20.glEnableVertexAttribArray;
import static org.lwjgl.opengl.GL20.glVertexAttribPointer;
import static org.lwjgl.opengl.GL30.glBindVertexArray;
import static org.lwjgl.opengl.GL30.glGenVertexArrays;

public class QuadVBO extends VBO {

    private int vao;
    private int vbo;
    private int ibo;
    private int size;

    public QuadVBO() {
        this.vao = glGenVertexArrays(); // Vertex Arrays
        this.vbo = glGenBuffers(); // Vertex Buffer
        this.ibo = glGenBuffers(); //Index Buffer
    }

    public void draw() {
        glDrawElements(GL_TRIANGLES, size, GL_UNSIGNED_INT, 0);
    }

    public void delete() {

    }

    public void allocate(Quad quad) {
        this.size = quad.getVertices().length;
        glBindVertexArray(vao);
        glBindBuffer(GL_ARRAY_BUFFER, vbo);

        glBufferData(GL_ARRAY_BUFFER, BufferUtil.createFloatBuffer(quad.getVertices()) , GL_STATIC_DRAW);

        glEnableVertexAttribArray(0);
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 0, 0);

        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ibo);
        glBufferData(GL_ELEMENT_ARRAY_BUFFER, BufferUtil.createIntBuffer(quad.getIndices()), GL_STATIC_DRAW);
    }

    @Override
    public void bind() {
        glBindVertexArray(vao);
    }

    @Override
    public void unbind() {
        glBindVertexArray(0);
    }
}
