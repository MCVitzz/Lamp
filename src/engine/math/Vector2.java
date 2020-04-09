package engine.math;

import engine.core.Cloneable;

public class Vector2 implements Cloneable<Vector2> {
    public float x, y;

    public Vector2(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public void sub(Vector2 other) {
        x -= other.x;
        y -= other.y;
    }

    public static Vector2 sub(Vector2 a, Vector2 b) {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public Vector2 copy() {
        return new Vector2(x, y);
    }

    @Override
    public String toString() {
        return "x: " + x + " y: " + y;
    }
}
