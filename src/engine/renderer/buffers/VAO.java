package engine.renderer.buffers;

import java.util.ArrayList;

import static org.lwjgl.opengl.GL11.*;
import static org.lwjgl.opengl.GL30.*;

public class VAO {

    private int id;

    private int activeVBO;

    protected ArrayList<VBO> vbos;

    protected IBO ibo;

    protected int size;

    public VAO() {
        this.id = glGenVertexArrays(); // Vertex Arrays

        vbos = new ArrayList<>(); //VBO List ready
    }

    public void draw() {
        for (VBO vbo : vbos) {
            vbo.enableAttributes();
        }
        glDrawElements(GL_TRIANGLES, size, GL_UNSIGNED_INT, 0);
        for (VBO vbo : vbos) {
            vbo.disableAttributes();
        }
    }

    public void addVBO(VBO vbo) {
        glBindVertexArray(id);
        vbo.bind();

        ArrayList<BufferElement> elements = vbo.getLayout().getElements();
        int i = 0;
        for (BufferElement element : elements) {
            //glEnableVertexAttribArray(i);
            glVertexAttribPointer(i,
                    element.size,
                    element.type.getDataType(),
                    element.normalized,
                    vbo.getLayout().getStride(),
                    element.offset);
            i++;
        }
        vbos.add(vbo);
    }

    public void setIBO(int[] indices) {
        this.size = indices.length;
        ibo = new IBO(); // Create
        ibo.allocate(indices); // Allocate
    }

    public void delete() {
        for (VBO vbo : vbos) {
            vbo.delete();
        }
        ibo.delete();
        glDeleteVertexArrays(id);
    }

    public void bind() {
        glBindVertexArray(id);
    }

    public void bindIbo() {
        ibo.bind();
    }

    public void unbind() {
        glBindVertexArray(0);
    }
}
