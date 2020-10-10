using System.Drawing;

namespace cs._2020_10_07_struct
{
    public interface ICollectable
    {
        int X { get; }
        int Y { get; }
        string Type { get; }
        object Item { get; }

        bool Touched(Controller who);
        void Draw(Graphics agraphics);
    }
}