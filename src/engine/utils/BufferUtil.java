package engine.utils;

import engine.math.Matrix4;
import engine.math.Vector3;
import org.lwjgl.BufferUtils;

import java.nio.FloatBuffer;
import java.nio.IntBuffer;

public class BufferUtil {

    public static FloatBuffer createFloatBuffer(int size)
    {
        return BufferUtils.createFloatBuffer(size);
    }

    public static FloatBuffer createFloatBuffer(Vector3[] vecs) {
        FloatBuffer buffer = BufferUtils.createFloatBuffer(vecs.length * 3);
        for(Vector3 vec : vecs) {
            buffer.put(vec.x);
            buffer.put(vec.y);
            buffer.put(vec.z);
        }
        buffer.flip();
        return buffer;
    }

    public static IntBuffer createIntBuffer(int[] arr) {
        IntBuffer buffer = BufferUtils.createIntBuffer(arr.length);
        for(int i : arr)
            buffer.put(i);
        buffer.flip();
        return buffer;
    }

    public static FloatBuffer createFlippedBuffer(Matrix4 matrix)
    {
        FloatBuffer buffer = createFloatBuffer(4 * 4);

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                buffer.put(matrix.get(i, j));

        //buffer.flip();
        return buffer;
    }
}
