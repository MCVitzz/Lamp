using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamp.Utilities
{

    public class SmoothFloat
    {

        private readonly float agility;

        public float Target;
        private float Actual;

        public SmoothFloat(float initialValue, float agility)
        {
            Target = initialValue;
            Actual = initialValue;
            this.agility = agility;
        }

        public void Update(float delta)
        {
            float offset = Target - Actual;
            float change = offset * delta * agility;
            Actual += change;
        }

        public void IncreaseTarget(float dT)
        {
            Target += dT;
        }

        public float Get()
        {
            return Actual;
        }

    }
}
