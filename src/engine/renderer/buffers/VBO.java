package engine.renderer.buffers;

import engine.utils.BufferUtil;

import static org.lwjgl.opengl.GL33.*;

public class VBO {
    protected int id;
    protected BufferLayout layout;

    public VBO() {
        id = glGenBuffers();
    }

    public int getId() {
        return this.id;
    }

    public void allocate(float[] data) {
        glBindBuffer(GL_ARRAY_BUFFER, id);
        glBufferData(GL_ARRAY_BUFFER, BufferUtil.createFlippedFloatBuffer(data), GL_STATIC_DRAW);
    }

    public void setLayout(BufferLayout layout) {
        this.layout = layout;
    }

    public void enableAttributes() {
        bind();
        for (int i = 0; i < layout.getElements().size(); i++) {
            glEnableVertexAttribArray(i);
        }
    }

    public void disableAttributes() {
        for (int i = 0; i < layout.getElements().size(); i++) {
            glDisableVertexAttribArray(i);
        }
    }

    public BufferLayout getLayout() {
        return this.layout;
    }

    public void bind() {
        glBindBuffer(GL_ARRAY_BUFFER, id);
    }

    public void unbind() {
        glBindBuffer(GL_ARRAY_BUFFER, 0);
    }

    public void delete() {
        glDeleteBuffers(id);
    }

}
