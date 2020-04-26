using Lamp.Core;
using Lamp.GUI.Constraints;
using Lamp.GUI.Text;

namespace Lamp.GUI.Components
{
    public class Button : Component
    {
        private string Value;

        public Button(string text)
        {
            Value = text;
        }

        public void Setup()
        {

            var txt = new TextComponent(Value)
            {
                Parent = this,
                Layout = new ComponentLayout()
                {
                    X = new CenterConstraint(),
                    Y = new CenterConstraint(),
                    W = new RelativeConstraint(0.9f),
                    H = new RelativeConstraint(0.9f),
                },
            };
            var ctr = new Container(new Colour(0.4f, 0.7f, 0.2f, 0.4f))
            {
                Parent = this,
                Layout = new ComponentLayout()
                {
                    X = new CenterConstraint(),
                    Y = new CenterConstraint(),
                    W = new RelativeConstraint(0.9f),
                    H = new RelativeConstraint(0.9f),
                },
            };
            AddChild(txt);
            AddChild(ctr);
        }
    }
}
