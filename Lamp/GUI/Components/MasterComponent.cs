using Lamp.GUI.Constraints;
using OpenTK;

namespace Lamp.GUI.Components
{
    public class MasterComponent : Component
    {
        public MasterComponent()
        {
            Layout = new ComponentLayout()
            {
                X = new EmptyConstraint(0),
                Y = new EmptyConstraint(0),
                W = new EmptyConstraint(),
                H = new EmptyConstraint(),
            };
        }

        public override Vector4 GetTransform()
        {
            return new Vector4(Layout.X.GetValue(), Layout.Y.GetValue(), Layout.W.GetValue(), Layout.H.GetValue());
        }
    }
}
