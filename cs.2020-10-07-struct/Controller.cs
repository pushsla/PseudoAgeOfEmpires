using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Объект, который контролирует поведение одного Warrior на поле боя: перемещение, выстрелы.
    /// Данный объект реализует логику управления Warrior и следит за показателем его жизни.
    /// Ни один из методов не будет производить в игре действий, если Warrior мертв.
    /// </summary>
    public class Controller
    {
        // Таймер перезарядки патрона
        private Timer __cooldown_timer;
        private int __id;
        // Хранимый Warrior
        private Warrior __warrior;
        // координаты и размер
        private int __xpos, __ypos, __width, __height;
        // Жив ли хранимый Warrior
        private bool __alive;
        // Можно ли произвести выстрел
        private bool __can_shoot;
        // Спрайт отрисовки
        private List<Image> __sprites;
        
        
        /// <summary>
        /// Текущее положение Warrior на карте по X
        /// </summary>
        public int X => __xpos;
        /// <summary>
        /// Текущее положение Warrior на карте по Y
        /// </summary>
        public int Y => __ypos;

        public int Width => __width;
        public int Height => __height;
        /// <summary>
        /// Уникальный ID. Равен значению ID у хранимого Warrior при условии использования конструктора
        /// </summary>
        public int ID => __id;
        /// <summary>
        /// Скорость хранимого Warrior
        /// </summary>
        public int Speed => __warrior.Speed;
        /// <summary>
        /// Дальность стрельбы Warrior
        /// </summary>
        public int ShootRaduis => __warrior.ShootRadius;

        public int BASEHP => __warrior.BASEHP;
        public int HP => __warrior.Health;
        public Chest Chest => __warrior.Chest;
        public List<Ammo> Ammos => __warrior.Ammos;
        

        /// <summary>
        /// Необходимо укзаать __уникальный__ id. То же значение ID будет присвоено хранимому Warrior
        /// </summary>
        /// <param name="id">уникальный идентификатор для Controller и хранимого Warrior</param>
        public Controller(int id)
        {
            __can_shoot = true;
            __cooldown_timer = new Timer();
            __id = id;
            __warrior = new Warrior(id);
            __xpos = 0;
            __ypos = 0;
            __width = 30;
            __height = 50;
            __alive = true;

            __sprites = new List<Image>
            {
                Properties.Resources._0down,
                Properties.Resources._0turn,
                Properties.Resources._0turnright,
                Properties.Resources._0up,
                
                Properties.Resources._1down,
                Properties.Resources._1turn,
                Properties.Resources._1turnright,
                Properties.Resources._1up,
                
                Properties.Resources._2down,
                Properties.Resources._2turn,
                Properties.Resources._2turnright,
                Properties.Resources._2up,
                
                Properties.Resources._2down,
                Properties.Resources._2turn,
                Properties.Resources._2turn,
                Properties.Resources._2up
            };

            __cooldown_timer.Interval = Config.GamerShootCooldown;
            __cooldown_timer.Enabled = true;
            __cooldown_timer.Tick += RefreshCooldown;
            __cooldown_timer.Stop();
            
            CheckAlive();
        }

        /// <summary>
        /// Получить новую броню
        /// </summary>
        /// <param name="achest"></param>
        public void NewChest(Chest achest)
        {
            __warrior.Chest = achest;
        }

        /// <summary>
        /// Проверяет, находится ли Controller в радиусе от некоторой точки
        /// </summary>
        /// <param name="ax">x координата точки</param>
        /// <param name="ay">y координата точки</param>
        /// <param name="radius">радиус</param>
        /// <returns></returns>
        public bool Touch(int ax, int ay, int radius)
        {
            bool shut = Math.Abs(__xpos - ax) < (radius + __width/2);
            shut = shut && Math.Abs(__ypos - ay) < (radius + __height / 2);
            return shut;
        }

        public bool Touch(Controller who)
        {
            bool touched = (Math.Abs(__xpos - who.X) <= __width/2 + who.Width/2);
            touched = touched && (Math.Abs(__ypos - who.Y) <= __height / 2 + who.Height/2);

            return touched;
        }
        /// <summary>
        /// Перемещает Controller на заданное смещение по X и Y
        /// </summary>
        /// <param name="dx">Смещение по X</param>
        /// <param name="dy">Смещение по Y</param>
        public void Move(int dx, int dy)
        {
            if (!__alive) return;
            __xpos += dx;
            __ypos += dy;
            if (__xpos <= 0) __xpos = 0;
            if (__ypos <= 0) __ypos = 0;
            if (__xpos >= 1000) __xpos = 1000;
            if (__ypos >= 1000) __ypos = 1000;
        }

        public void MoveTo(int ax, int ay)
        {
            if (!__alive) return;
            __xpos = ax;
            __ypos = ay;
        }

        /// <summary>
        /// Попытка выстрела. Возвращает выпущенный снаряд.
        /// В случае, если не прошло время перезарядки или есть иная причина,
        /// по которой Warrior не может выстрелить (__can_shoot == false)
        /// </summary>
        /// <returns></returns>
        public Ammo Shoot()
        {
            if (!__alive || !__can_shoot) return new Ammo(0);
            __can_shoot = false;
            __cooldown_timer.Start();
            return __warrior.Shoot();
        }


        /// <summary>
        /// Получить чистый урон. Урон передается Warrior.
        /// </summary>
        /// <param name="adamage"></param>
        public void GetDamage(int adamage)
        {
            if (!__alive) return;
            __warrior.GetDamage(adamage);
            CheckAlive();
        }

        /// <summary>
        /// Отрисовка положения Controller на карте
        /// </summary>
        /// <param name="agraphics">Графический компонент для отрисовки</param>
        /// <param name="position_code">0 -- вверх, 1 -- влево, 2 -- вправо, 3 -- вниз</param>
        public void Draw(Graphics agraphics, int position_code)
        {
            int x = __xpos - __width / 2;
            int y = __ypos - __height / 2;

            Image img = __sprites[(Chest.TypesCount) * __warrior.Chest.Type + position_code];;

            if (!__alive)
            {
                img = Properties.Resources._dead;
            }

            agraphics.DrawImage(img, x, y, __width, __height);
        }

        /// <summary>
        /// Warrior снова разрешается отдавать снаряды из обоймы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshCooldown(object sender, EventArgs e)
        {
            __can_shoot = true;
            __cooldown_timer.Stop();
        }

        /// <summary>
        /// Проверить, жив ли хранимый Warrior
        /// </summary>
        private void CheckAlive()
        {
            __alive = __warrior.Health > 0;
        }
    }
}