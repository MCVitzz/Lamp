namespace Lamp.GUI.Constraints
{
    public class PixelConstraint : Constraint
    {
        public PixelConstraint(float value) : base(value)
        {

        }

        public override float GetPixels()
        {
            return GetValue();
        }

        public override float GetValue()
        {
            float parent = Mode == Mode.Horizontal ? Current.Parent.GetPixelWidth() : Current.Parent.GetPixelHeight();

            return Value / parent;
        }
    }
}
