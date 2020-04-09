package engine.renderer.buffers;

import engine.models.Quad;

public abstract class VBO {
    public abstract void draw();
    public abstract void delete();
    public abstract void allocate(Quad quad);
    public abstract void bind();
    public abstract void unbind();
}
