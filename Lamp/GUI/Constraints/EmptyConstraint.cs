namespace Lamp.GUI.Constraints
{
    public class EmptyConstraint : Constraint
    {
        public EmptyConstraint() : this(1)
        {
        }

        public EmptyConstraint(float value) : base(value)
        {
        }

        public override float GetValue()
        {
            return Value;
        }

        public override float GetPixels()
        {
            return Value * (Mode == Mode.Horizontal ? GUIManager.DisplayWidth : GUIManager.DisplayHeight);
        }
    }
}
