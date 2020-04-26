using System;

namespace Lamp.GUI.Constraints
{
    public class CenterConstraint : Constraint
    {
        public CenterConstraint() : base(0)
        {

        }

        public override float GetPixels()
        {
            return GetValue() * GUIManager.DisplayWidth;
        }

        public override float GetValue()
        {
            float size = Mode == Mode.Horizontal ? Current.Layout.W.GetValue() : Current.Layout.H.GetValue();
            return (1 - size) / 2;
        }
    }
}
