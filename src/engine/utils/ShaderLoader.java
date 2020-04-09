package engine.utils;

import engine.config.Config;
import engine.core.Log;

import java.io.*;

public class ShaderLoader {
    public static String loadShader(Config config, String fileName) {
        String path = config.getFolder() + fileName + config.getExtension();
        try {
            StringBuilder str = new StringBuilder();
            BufferedReader reader = new BufferedReader(new FileReader(path));
            String line = "";
            while((line = reader.readLine()) != null)
                str.append(line).append("\n");
            reader.close();
            Log.info(str.toString());
            return str.toString();
        } catch (IOException e) {
            Log.error("Couldn't read shader file.");
            Log.error("File: " + fileName + "(" + config.getFolder() + fileName + config.getExtension() + ").");
            e.printStackTrace();
            System.exit(-1);
        }
        return "";
    }
}
