using Serilog;

namespace Lamp.GUI.Texting
{
    public class DebugChar
    {
        public char Char { get; set; }
        public float AdvanceX { get; set; }
        public float BearingX { get; set; }
        public float Width { get; set; }
        public float Underrun { get; set; }
        public float Overrun { get; set; }
        public float Kern { get; set; }
        public float RightEdge { get; set; }
        internal DebugChar(char c, float advanceX, float bearingX, float width)
        {
            Char = c; AdvanceX = advanceX; BearingX = bearingX; Width = width;
        }

        public override string ToString()
        {
            return string.Format("'{0}' {1,5:F0} {2,5:F0} {3,5:F0} {4,5:F0} {5,5:F0} {6,5:F0} {7,5:F0}",
                Char, AdvanceX, BearingX, Width, Underrun, Overrun,
                Kern, RightEdge);
        }
        public static void PrintHeader()
        {
            Log.Information("{1,5} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5}", "", "adv", "bearing", "wid", "undrn", "ovrrn", "kern", "redge");
        }
    }
}
