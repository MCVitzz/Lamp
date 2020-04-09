package engine.config;

public class ShaderConfig implements Config {

    public String getFolder() {
        return "./res/shaders/";
    }

    public String getExtension() {
        return ".glsl";
    }
}
