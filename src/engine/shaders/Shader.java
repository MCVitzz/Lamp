package engine.shaders;

import engine.config.Config;
import engine.config.ShaderConfig;
import engine.core.Log;
import engine.utils.ShaderLoader;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;

import static org.lwjgl.opengl.GL11.GL_FALSE;
import static org.lwjgl.opengl.GL20.GL_COMPILE_STATUS;
import static org.lwjgl.opengl.GL20.glCompileShader;
import static org.lwjgl.opengl.GL20.glCreateProgram;
import static org.lwjgl.opengl.GL20.glCreateShader;
import static org.lwjgl.opengl.GL20.glGetShaderInfoLog;
import static org.lwjgl.opengl.GL20.glGetShaderi;
import static org.lwjgl.opengl.GL20.glShaderSource;
import static org.lwjgl.opengl.GL20C.*;

public class Shader {

    private int program;
    private HashMap<String, Integer> uniforms;
    public static Config config = new ShaderConfig();

    public Shader(String vertex, String fragment) {
        uniforms = new HashMap<String, Integer>();
        init(vertex, fragment);
        if (program == 0)
        {
            Log.error("Shader creation failed");
            System.exit(1);
        }
    }

    private void init(String vertex, String fragment) {
        int vertexShader = loadShader(vertex, GL_VERTEX_SHADER);
        int fragmentShader = loadShader(fragment, GL_FRAGMENT_SHADER);

        program = glCreateProgram();
        glAttachShader(program, vertexShader);
        glAttachShader(program, fragmentShader);
        glLinkProgram(program);
        glDetachShader(program, vertexShader);
        glDetachShader(program, fragmentShader);
    }

    private int loadShader(String fileName, int type) {
        String src = ShaderLoader.loadShader(config, fileName);
        int shaderID = glCreateShader(type);
        glShaderSource(shaderID, src);
        glCompileShader(shaderID);
        if (glGetShaderi(shaderID, GL_COMPILE_STATUS) == GL_FALSE) {
            Log.error(glGetShaderInfoLog(shaderID, 500));
            Log.error("Could not compile shader");
            Log.error("Shader: " + fileName);
            glDeleteShader(shaderID);
            System.exit(-1);
        }
        return shaderID;
    }

    public void addUniform(String variable) {
        int location = glGetUniformLocation(program, variable);
        if (location == 0xFFFFFFFF)
        {
            Log.error(this.getClass().getName() + " Error: Could not find uniform: " + variable);
            new Exception().printStackTrace();
            System.exit(-1);
        }
        uniforms.put(variable, location);
    }

    public void dispose() {
        glDeleteProgram(program);
    }

    public void bind() {
        glUseProgram(program);
    }

    public void unbind() {
        glUseProgram(0);
    }
}
