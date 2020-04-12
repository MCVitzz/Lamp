package engine.utils;

import engine.math.Vector3;
import engine.models.Model;
import engine.renderer.buffers.BufferLayout;
import engine.renderer.buffers.VBO;

public class Factory {
    public static VBO createVBO(Model model, BufferLayout layout) {
        VBO vbo = new VBO();
        vbo.allocate(Vector3.toFloatArray(model.getVertices()));
        vbo.setLayout(layout);
        return vbo;
    }
}
