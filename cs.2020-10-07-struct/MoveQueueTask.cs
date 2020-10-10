using System;
using System.Windows.Forms.VisualStyles;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Структура, необходимая для помещения в очередь Game задачи на перемещение Controller в указанную точку
    /// Задачавыполняется по частям (перемещение происходит постепенно),
    /// поэтому для исполнения очередного шага применяется метод Movturn, который вернет True, если задача исполнена
    /// </summary>
    public struct MoveQueueTask
    {
        /// <summary>
        /// Controller, который перемещается в ходе исполнения задачи
        /// </summary>
        public Controller Controller;
        /// <summary>
        /// Точка, в которую по завершении задачи, будет помещен Controller
        /// </summary>
        public int destX, destY;
        /// <summary>
        /// Направление движения 0 -- вниз, 1 -- влево, 2 -- вправо, 3 -- вверх
        /// </summary>
        public int position_code;

        public MoveQueueTask(Controller controller, int ax, int ay)
        {
            Controller = controller;
            destX = ax;
            destY = ay;
            position_code = 0;
            
            UpdatePositionCode();
        }

        /// <summary>
        /// Обновить код пизиции. Этот код обозначает, в какую сторону будет двигаться объект.
        /// </summary>
        private void UpdatePositionCode()
        {
            int positiondelta = 0;
            
            if (Controller.Y > destY)
            {
                positiondelta = Controller.Y - destY;
                position_code = 3;
            }
            else if (Controller.Y < destY)
            {
                positiondelta = destY - Controller.Y;
                position_code = 0;
            }

            if (Controller.X > destX && (Controller.X - destX) >= positiondelta)
                position_code = 1;
            else if (destX > Controller.X && (destX - Controller.X) >= positiondelta)
               position_code = 2;
            
        }
        /// <summary>
        /// Частичное исполнение задачи по перемещению. Объем единичного перемещения ависит от скорости Controller.
        /// Объем перемещения рассчитывается по формуле, где скорость Controller принимается
        /// за сумму скоростей по X и по Y, также принимается, что по X и по Y Controller пройдет полный путь
        /// за одинаковое количество исполнений метода Movturn
        /// </summary>
        /// <returns>Если перемещение завершено, возвращается True</returns>
        public bool Movturn()
        {
            bool finished = (Math.Abs(destX - Controller.X) < Controller.Speed && Math.Abs(destY - Controller.Y) < Controller.Speed);
            
            if (finished) return finished;
            float dx, dy, ax, ay;
            ax = Math.Abs(destX - Controller.X);
            ay = Math.Abs(destY - Controller.Y);
            dy = (ay*Controller.Speed)/(ax+ay);
            dx = Controller.Speed - dy;

            Controller.Move(Math.Sign(destX - Controller.X)*(int)dx, Math.Sign(destY - Controller.Y)*(int)dy);
            UpdatePositionCode();
            
            return finished;
        }
        
    }
}