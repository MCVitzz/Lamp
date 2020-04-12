package engine.utils;

import engine.math.Vector3;
import org.lwjgl.BufferUtils;

import java.nio.FloatBuffer;
import java.nio.IntBuffer;

public class BufferUtil {
    public static FloatBuffer createFloatBuffer(Vector3[] vecs) {
        return createFloatBuffer(Vector3.toFloatArray(vecs));
    }

    public static FloatBuffer createFloatBuffer(float[] array) {
        FloatBuffer buffer = BufferUtils.createFloatBuffer(array.length);
        for (float f : array) {
            buffer.put(f);
        }
        return buffer;
    }

    public static FloatBuffer createFlippedFloatBuffer(float[] vecs) {
        FloatBuffer buffer = createFloatBuffer(vecs);
        buffer.flip();
        return buffer;
    }

    public static FloatBuffer createFlippedFloatBuffer(Vector3[] vecs) {
        FloatBuffer buffer = createFloatBuffer(vecs);
        buffer.flip();
        return buffer;
    }

    public static IntBuffer createFlippedIntBuffer(int[] arr) {
        IntBuffer buffer = createIntBuffer(arr);
        buffer.flip();
        return buffer;
    }

    public static IntBuffer createIntBuffer(int[] arr) {
        IntBuffer buffer = BufferUtils.createIntBuffer(arr.length);
        for (int i : arr)
            buffer.put(i);
        return buffer;
    }
}
