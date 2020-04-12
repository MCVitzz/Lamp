package engine.scene.terrain;

import engine.core.Colour;
import engine.renderer.buffers.VAO;

public class TerrainMesh {
    private int vertexCount;
    private VAO VAO;
    private Colour grassColour;

    public TerrainMesh(VAO VAO, Colour grassColour) {
        this.VAO = VAO;
        this.grassColour = grassColour;
    }

    public VAO getVAO() {
        return this.VAO;
    }

    public Colour getGrassColour() {
        return this.grassColour;
    }
}
