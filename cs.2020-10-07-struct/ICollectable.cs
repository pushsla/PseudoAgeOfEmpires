using System.Drawing;

namespace cs._2020_10_07_struct
{
    public interface ICollectable
    {
        int X { get; }
        int Y { get; }

        bool Touch(int ax, int ay, int radius);
        void Draw(Graphics agraphics);
    }
}