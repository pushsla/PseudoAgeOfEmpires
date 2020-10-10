using System;
using System.Drawing;

namespace cs._2020_10_07_struct
{
    public struct ChestCollectable : ICollectable
    {
        private Chest __chest;
        private int __xpos, __ypos, __width, __height;

        public int X => __xpos;
        public int Y => __ypos;
        public string Type => "Chest";
        public object Item => __chest;

        public ChestCollectable(int ax, int ay, int chest_type)
        {
            __chest = new Chest(chest_type);
            __xpos = ax;
            __ypos = ay;
            __width = 20;
            __height = 26;
        }

        public void Draw(Graphics agraphics)
        {
            int x, y;
            Image img;
            switch ((Item as Chest).Type)
            {
                case 1:
                    img = Properties.Resources._armor1;
                    break;
                case 2:
                    img = Properties.Resources._armor2;
                    break;
                case 3:
                    img = Properties.Resources._armor3;
                    break;
                default:
                    img = img = Properties.Resources._armor1;
                    break;
            }
            
            x = __xpos - __width / 2;
            y = __ypos - __height / 2;
            agraphics.DrawImage(img, x, y, __width, __height);
            agraphics.DrawString($"{__chest.Type}", new Font("Arial", 10), Brushes.Aqua, x, y);
        }

        public bool Touched(Controller who)
        {
            bool touched = (Math.Abs(__xpos - who.X) <= __width/2 + who.Width/2);
            touched = touched && (Math.Abs(__ypos - who.Y) <= __height / 2 + who.Height/2);

            return touched;
        }
    }
}