using System.Drawing;

namespace cs._2020_10_07_struct
{
    public struct ChestCollectable : ICollectable
    {
        private Chest __chest;
        private int __xpos, __ypos, __width, __height;

        public int X => __xpos;
        public int Y => __ypos;

        public ChestCollectable(int ax, int ay, int chest_type)
        {
            __chest = new Chest(chest_type);
            __xpos = ax;
            __ypos = ay;
            __width = 10;
            __height = 10; 
        }

        public void Draw(Graphics agraphics)
        {
            
        }

        public bool Touch(int ax, int ay, int radius)
        {
            return true;
        }
    }
}