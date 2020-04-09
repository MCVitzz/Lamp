package engine.math;

import engine.core.Log;

import java.nio.FloatBuffer;

public class Matrix4 {
    public float[][] values;

    public Matrix4() {
        values = new float[4][4];
    }

    public float get(int x, int y) {
        return values[x][y];
    }

    public void set(int x, int y, float val) {
        values[x][y] = val;
    }

    public void identity() {
        for (int x = 0; x < values.length; x++)
            for (int y = 0; y < values[x].length; y++)
                if (x == y)
                    values[x][y] = 1;
                else
                    values[x][y] = 0;
    }

    public void translate(Vector3 vector) {
        this.values[3][0] += this.values[0][0] * vector.x + this.values[1][0] * vector.y + this.values[2][0] * vector.z;
        this.values[3][1] += this.values[0][1] * vector.x + this.values[1][1] * vector.y + this.values[2][1] * vector.z;
        this.values[3][2] += this.values[0][2] * vector.x + this.values[1][2] * vector.y + this.values[2][2] * vector.z;
        this.values[3][3] += this.values[0][3] * vector.x + this.values[1][3] * vector.y + this.values[2][3] * vector.z;
    }


//    public void rotate(float angle, Vector3 axis) {
//        float c = (float) Math.cos(angle);
//        float s = (float) Math.sin(angle);
//        float oneminusc = 1.0f - c;
//        float xy = axis.x * axis.y;
//        float yz = axis.y * axis.z;
//        float xz = axis.x * axis.z;
//        float xs = axis.x * s;
//        float ys = axis.y * s;
//        float zs = axis.z * s;
//
//        float f00 = axis.x * axis.x * oneminusc + c;
//        float f01 = xy * oneminusc + zs;
//        float f02 = xz * oneminusc - ys;
//        // n[3] not used
//        float f10 = xy * oneminusc - zs;
//        float f11 = axis.y * axis.y * oneminusc + c;
//        float f12 = yz * oneminusc + xs;
//        // n[7] not used
//        float f20 = xz * oneminusc + ys;
//        float f21 = yz * oneminusc - xs;
//        float f22 = axis.z * axis.z * oneminusc + c;
//
//        float t00 = this.values[0][0] * f00 + this.values[1][0] * f01 + this.values[2][0] * f02;
//        float t01 = this.values[0][1] * f00 + this.values[1][1] * f01 + this.values[2][1] * f02;
//        float t02 = this.values[0][2] * f00 + this.values[1][2] * f01 + this.values[2][2] * f02;
//        float t03 = this.values[0][3] * f00 + this.values[1][3] * f01 + this.values[2][3] * f02;
//        float t10 = this.values[0][0] * f10 + this.values[1][0] * f11 + this.values[2][0] * f12;
//        float t11 = this.values[0][1] * f10 + this.values[1][1] * f11 + this.values[2][1] * f12;
//        float t12 = this.values[0][2] * f10 + this.values[1][2] * f11 + this.values[2][2] * f12;
//        float t13 = this.values[0][3] * f10 + this.values[1][3] * f11 + this.values[2][3] * f12;
//        this.values[2][0] = this.values[0][0] * f20 + this.values[1][0] * f21 + this.values[2][0] * f22;
//        this.values[2][1] = this.values[0][1] * f20 + this.values[1][1] * f21 + this.values[2][1] * f22;
//        this.values[2][2] = this.values[0][2] * f20 + this.values[1][2] * f21 + this.values[2][2] * f22;
//        this.values[2][3] = this.values[0][3] * f20 + this.values[1][3] * f21 + this.values[2][3] * f22;
//        this.values[0][0] = t00;
//        this.values[0][1] = t01;
//        this.values[0][2] = t02;
//        this.values[0][3] = t03;
//        this.values[1][0] = t10;
//        this.values[1][1] = t11;
//        this.values[1][2] = t12;
//        this.values[1][3] = t13;
//    }

    public void scale(float value) {
        scale(new Vector3(value, value, value));
    }

    public void scale(Vector3 vec) {
        this.values[0][0] = this.values[0][0] * vec.x;
        this.values[0][1] = this.values[0][1] * vec.x;
        this.values[0][2] = this.values[0][2] * vec.x;
        this.values[0][3] = this.values[0][3] * vec.x;
        this.values[1][0] = this.values[1][0] * vec.y;
        this.values[1][1] = this.values[1][1] * vec.y;
        this.values[1][2] = this.values[1][2] * vec.y;
        this.values[1][3] = this.values[1][3] * vec.y;
        this.values[2][0] = this.values[2][0] * vec.z;
        this.values[2][1] = this.values[2][1] * vec.z;
        this.values[2][2] = this.values[2][2] * vec.z;
        this.values[2][3] = this.values[2][3] * vec.z;
    }

    public void mul(Matrix4 other) {
        for (int i=0; i<4; i++)
        {
            for (int j=0; j<4; j++)
            {
                this.set(i, j, values[i][0] * other.get(0, j) +
                        values[i][1] * other.get(1, j) +
                        values[i][2] * other.get(2, j) +
                        values[i][3] * other.get(3, j));
            }
        }
    }

    public void toBuffer(FloatBuffer buffer) {
        for (float[] value : values)
            for (float v : value) buffer.put(v);
    }

    public static Matrix4 mul(Matrix4 m1, Matrix4 m2) {
        Matrix4 matrix = new Matrix4();
        for (int i=0; i<4; i++)
        {
            for (int j=0; j<4; j++)
            {
                matrix.set(i, j, m1.get(i, 0) * m2.get(0, j) +
                        m1.get(i,1) * m2.get(1, j) +
                        m1.get(i,2) * m2.get(2, j) +
                        m1.get(i,3) * m2.get(3, j));
            }
        }
        return  matrix;
    }

//    public static Matrix4 projection(float fov, float near, float far) {
//        Matrix4 projectionMatrix = new Matrix4();
//        float aspectRatio = (float) Application.window.width / (float) Application.window.height;
//        float yScale = (float) ((1f / Math.tanthis.values[ath.toRadians(fov / 2f))));
//        float xScale = yScale / aspectRatio;
//        float frustumLength = far - near;
//
//        projectionMatrix.values[0][0] = xScale;
//        projectionMatrix.values[1][1] = yScale;
//        projectionMatrix.values[2][2] = -((far + near) / frustumLength);
//        projectionMatrix.values[2][3] = -1;
//        projectionMatrix.values[3][2] = -((2 * near * far) / frustumLength);
//        projectionMatrix.values[3][3] = 0;
//
//        return  projectionMatrix;
//    }
//
    public static Matrix4 transformation(Vector3 position, Vector3 rotation, float scale) {
        Matrix4 matrix = new Matrix4();
        matrix.identity();
        matrix.translate(position);
        matrix.rotate((float) Math.toRadians(rotation.x), new Vector3(1.0f, 0.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.y), new Vector3(0.0f, 1.0f, 0.0f));
        matrix.rotate((float) Math.toRadians(rotation.z), new Vector3(0.0f, 0.0f, 1.0f));
        matrix.scale(scale);
        return matrix;
    }

    public void rotate(float angle, Vector3 axis) {
        float c = (float) Math.cos(angle);
        float s = (float) Math.sin(angle);
        float oneminusc = 1.0f - c;
        float xy = axis.x*axis.y;
        float yz = axis.y*axis.z;
        float xz = axis.x*axis.z;
        float xs = axis.x*s;
        float ys = axis.y*s;
        float zs = axis.z*s;

        float f00 = axis.x*axis.x*oneminusc+c;
        float f01 = xy*oneminusc+zs;
        float f02 = xz*oneminusc-ys;
        // n[3] not used
        float f10 = xy*oneminusc-zs;
        float f11 = axis.y*axis.y*oneminusc+c;
        float f12 = yz*oneminusc+xs;
        // n[7] not used
        float f20 = xz*oneminusc+ys;
        float f21 = yz*oneminusc-xs;
        float f22 = axis.z*axis.z*oneminusc+c;

        float t00 = this.values[0][0] * f00 + this.values[1][0] * f01 + this.values[2][0] * f02;
        float t01 = this.values[0][1] * f00 + this.values[1][1] * f01 + this.values[2][1] * f02;
        float t02 = this.values[0][2] * f00 + this.values[1][2] * f01 + this.values[2][2] * f02;
        float t03 = this.values[0][3] * f00 + this.values[1][3] * f01 + this.values[2][3] * f02;
        float t10 = this.values[0][0] * f10 + this.values[1][0] * f11 + this.values[2][0] * f12;
        float t11 = this.values[0][1] * f10 + this.values[1][1] * f11 + this.values[2][1] * f12;
        float t12 = this.values[0][2] * f10 + this.values[1][2] * f11 + this.values[2][2] * f12;
        float t13 = this.values[0][3] * f10 + this.values[1][3] * f11 + this.values[2][3] * f12;
        this.values[2][0] = this.values[0][0] * f20 + this.values[1][0] * f21 + this.values[2][0] * f22;
        this.values[2][1] = this.values[0][1] * f20 + this.values[1][1] * f21 + this.values[2][1] * f22;
        this.values[2][2] = this.values[0][2] * f20 + this.values[1][2] * f21 + this.values[2][2] * f22;
        this.values[2][3] = this.values[0][3] * f20 + this.values[1][3] * f21 + this.values[2][3] * f22;
        this.values[0][0] = t00;
        this.values[0][1] = t01;
        this.values[0][2] = t02;
        this.values[0][3] = t03;
        this.values[1][0] = t10;
        this.values[1][1] = t11;
        this.values[1][2] = t12;
        this.values[1][3] = t13;
    }

    public void store(FloatBuffer buffer) {
        buffer.put(this.values[0][0]);
        buffer.put(this.values[0][1]);
        buffer.put(this.values[0][2]);
        buffer.put(this.values[0][3]);
        buffer.put(this.values[1][0]);
        buffer.put(this.values[1][1]);
        buffer.put(this.values[1][2]);
        buffer.put(this.values[1][3]);
        buffer.put(this.values[2][0]);
        buffer.put(this.values[2][1]);
        buffer.put(this.values[2][2]);
        buffer.put(this.values[2][3]);
        buffer.put(this.values[3][0]);
        buffer.put(this.values[3][1]);
        buffer.put(this.values[3][2]);
        buffer.put(this.values[3][3]);
    }
    
}
