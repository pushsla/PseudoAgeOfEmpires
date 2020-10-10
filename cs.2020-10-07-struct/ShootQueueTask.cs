using System;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Структура, необходимая для помещения в очередь Game задачи по выстрелу некоторым Controller в некоторую точку
    /// Задачавыполняется по частям (перемещение происходит постепенно),
    /// поэтому для исполнения очередного шага применяется метод Movturn, который вернет True, если задача исполнена
    /// </summary>
    public struct ShootQueueTask
    {
        /// <summary>
        /// Снаряд, которым производится выстрел
        /// </summary>
        public Ammo Ammo;
        /// <summary>
        /// Controller, которым был выпущен снаряд
        /// </summary>
        public Controller Sender;
        /// <summary>
        /// Текущее положение снаряда
        /// </summary>
        public int X, Y;
        /// <summary>
        /// Конечная точка, куда был направлен снаряд
        /// </summary>
        public int destX, destY;

        public ShootQueueTask(Controller sender, Ammo ammo, int ax, int ay, int adestx, int adesty)
        {
            Sender = sender;
            Ammo = ammo;
            X = ax;
            Y = ay;
            destX = adestx;
            destY = adesty;
        }
        
        /// <summary>
        /// Частичное исполнение задачи по перемещению снаряда. Объем единичного перемещения ависит от скорости Ammo.
        /// Объем перемещения рассчитывается по формуле, где скорость Ammo принимается
        /// за сумму скоростей по X и по Y, также принимается, что по X и по Y Ammo пройдет полный путь
        /// за одинаковое количество исполнений метода Movturn
        /// </summary>
        /// <returns>Если перемещение снаряда (выстрел) завершено, возвращается True</returns>
        public bool Movturn()
        {
            bool burnt = (Math.Abs(destX - X) < Ammo.Speed && Math.Abs(destY - Y) < Ammo.Speed);
            
            if (burnt) return burnt;
            float dx, dy, ax, ay;
            ax = Math.Abs(destX - X);
            ay = Math.Abs(destY - Y);
            dy = (ay*Ammo.Speed)/(ax+ay);
            dx = Ammo.Speed - dy;

            X += Math.Sign(destX-X)*(int)dx;
            Y += Math.Sign(destY-Y)*(int)dy;
            
            return burnt;
        }
    }
}