namespace Lamp.GUI.Constraints
{
    public class RelativeConstraint : Constraint
    {
        public RelativeConstraint(float value) : base(value)
        {

        }

        public override float GetPixels()
        {
            return Value * (Mode == Mode.Horizontal ? Current.Parent.GetPixelWidth() : Current.Parent.GetPixelHeight());
        }

        public override float GetValue()
        {
            return Value;
        }
    }
}
