using Lamp.Core;
using Lamp.GUI.Text;
using OpenTK;
using System.Collections.Generic;

namespace Lamp.GUI
{
    public abstract class Component
    {
        public Component Parent;
        public List<Component> Children;
        public ComponentLayout Layout;
        public Colour BackgroundColour;

        public Component()
        {
            Children = new List<Component>();
            BackgroundColour = new Colour(0, 0, 0, 0);
        }

        public void AddChild(Component component)
        {
            component.Parent = this;
            component.UpdateLayout();
            Children.Add(component);
            if (component.GetType() == typeof(TextComponent))
            {
                TextManager.AddText((TextComponent)component);
            }
        }

        public void UpdateLayout()
        {
            Layout.X.Current = this;
            Layout.Y.Current = this;
            Layout.W.Current = this;
            Layout.H.Current = this;
            Layout.SetModes();
        }

        public virtual Vector4 GetTransform()
        {
            Vector4 parentTransform = Parent.GetTransform();
            float x = parentTransform.X + (Layout.X.GetValue() * parentTransform.Z);
            float y = parentTransform.Y + (Layout.Y.GetValue() * parentTransform.W);
            float width = parentTransform.Z * Layout.W.GetValue();
            float height = parentTransform.W * Layout.H.GetValue();

            Vector4 self = new Vector4(x, y, width, height);

            return self;
        }

        public float GetPixelHeight()
        {
            return Layout.H.GetValue() * GUIManager.DisplayHeight;
        }

        public float GetPixelWidth()
        {
            return Layout.W.GetValue() * GUIManager.DisplayWidth;
        }

        public virtual void PrepareRender()
        {

        }
    }
}
