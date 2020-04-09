package engine.utils;

import engine.core.Log;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class Utils {
    public static void walk(String path) {
        Log.info("Walking " + Paths.get(path).toAbsolutePath());
        try (Stream<Path> walk = Files.walk(Paths.get(path))) {
            List<String> result = walk.filter(Files::isRegularFile).map(Path::toString).collect(Collectors.toList());
            result.forEach(Log::info);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
