using OpenTK;

namespace Lamp.GUI
{
    public class ComponentLayout
    {
        public Constraint X;
        public Constraint Y;
        public Constraint W;
        public Constraint H;

        public Vector4 GetTransform()
        {
            return new Vector4(X.GetValue(), Y.GetValue(), W.GetValue(), H.GetValue());
        }

        public void SetModes()
        {
            X.Mode = Mode.Horizontal;
            W.Mode = Mode.Horizontal;
            Y.Mode = Mode.Vertical;
            H.Mode = Mode.Vertical;
        }
    }
}
