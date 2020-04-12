package engine.math;

import engine.core.Cloneable;

public class Vector3 implements Cloneable<Vector3> {

    public float x, y, z;

    public Vector3(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 add(Vector3 other) {
        return new Vector3(this.x + other.x, this.y + other.y, this.z + other.z);
    }


    public Vector3 sub(Vector3 other) {
        return new Vector3(this.x - other.x, this.y - other.y, this.z - other.z);
    }

    public Vector3 cross(Vector3 other) {
        return new Vector3(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.z);
    }

    public Vector3 copy() {
        return new Vector3(x, y, z);
    }

    public Vector3 normalize() {
        float length = length();

        x /= length;
        y /= length;
        z /= length;

        return this;
    }

    public float length() {
        return (float) Math.sqrt(x * x + y * y + z * z);
    }

    public static float[] toFloatArray(Vector3[] array) {
        float[] floats = new float[array.length * 3];
        int i = 0;
        for (Vector3 v : array) {
            floats[i] = v.x;
            floats[i + 1] = v.y;
            floats[i + 2] = v.z;
            i += 3;
        }
        return floats;
    }

    public String toString() {
        StringBuilder sb = new StringBuilder(64);
        sb.append("Vector3f[");
        sb.append(this.x);
        sb.append(", ");
        sb.append(this.y);
        sb.append(", ");
        sb.append(this.z);
        sb.append(']');
        return sb.toString();
    }
}
