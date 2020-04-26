namespace Lamp.GUI
{
    public abstract class Constraint
    {
        public Mode Mode;
        public float Value;
        public Component Current;

        public Constraint(float value)
        {
            Value = value;
        }

        public abstract float GetValue();
        public abstract float GetPixels();
    }

    public enum Mode { Vertical, Horizontal }
}
