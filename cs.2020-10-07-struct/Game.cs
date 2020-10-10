using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using cs._2020_10_07_struct.Properties;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Объект, осуществляющий взаимодействие между различными Controller;
    /// Отслеживающий перемещение и попадание снарядов;
    /// Реализующий процессы перемещения и выстрела Controller посредствам очереди;
    /// Выполняющий отрисовку всей игры в целом;
    /// Управляющий поведением персонажей ИИ;
    /// </summary>
    public class Game
    {
        private Random __rnd = new Random();
        // Таймер, выполняющий события из очередей
        private Timer __timer = new Timer();
        // Таймер, управляющий поведением противников
        private Timer __enemy_mov_timer = new Timer();
        // Таймер, отвечающий за появление на карте объектов
        private Timer __collectable_spawn_timer = new Timer();
        // Очередь выстрелов (выполняется параллельно)
        private List<ShootQueueTask> __shoot_queue = new List<ShootQueueTask>();
        // Очередь перемещений (выполняется параллельно)
        private List<MoveQueueTask> __move_queue = new List<MoveQueueTask>();
        // Список предметов на карте, которые можно подобрать
        private List<ICollectable> __collectable_items = new List<ICollectable>();
        // Список всех игроков Controller
        private List<Controller> __gamers = new List<Controller>();
        // Количество игроков, не управляемых автоматически
        private uint __human_players;
        
        /// <summary>
        /// Список всех активных игроков Controller
        /// </summary>
        public List<Controller> Players => __gamers.Take((int)__human_players).ToList();
        /// <summary>
        /// Список текущих задач по перемещению снаряда.
        /// </summary>
        public List<ShootQueueTask> Shoots => __shoot_queue;
        /// <summary>
        /// Список текущих задач по перемещению игроков Controller
        /// </summary>
        public List<MoveQueueTask> Moves => __move_queue;

        public Game(int gamers, uint players)
        {
            CreateGamers(gamers);
            __human_players = players;
            __timer.Interval = 100;
            __timer.Tick += onTimerEvent;
            __timer.Enabled = false;
            
            __enemy_mov_timer.Interval = Config.AIReactionDelay;
            __enemy_mov_timer.Tick += onEnemyMovTimerEvent;
            __enemy_mov_timer.Enabled = false;

            __collectable_spawn_timer.Interval = Config.CollectableSpawnInterval;
            __collectable_spawn_timer.Tick += onCollectableSpawnEvent;
            __collectable_spawn_timer.Enabled = false;
        }

        public void Start()
        {
            __timer.Enabled = true;
            __timer.Start();
            
            __enemy_mov_timer.Enabled = true;
            __enemy_mov_timer.Start();
            
            __collectable_spawn_timer.Enabled = true;
            __collectable_spawn_timer.Start();
        }

        public void Stop()
        {
            __timer.Stop();
            __enemy_mov_timer.Stop();
            __collectable_spawn_timer.Stop();
        }

        public void AddNewPlayer()
        {
            __gamers.Add(new Controller(__gamers.Count));
        }

        public void AddNewPlayer(int ax, int ay)
        {
            AddNewPlayer();
            __gamers.Last().MoveTo(ax, ay);
        }

        /// <summary>
        /// Создать новую задачу выстрела.
        /// Если выпущенный снаряд имеет тип, отличный от 0,
        /// то будет создана задача по его постепенному полету к цели на карте.
        /// При выстреле игрок __останавливается__: удаляются все задачи по его перемещению.
        /// Если Игрок слишком далеко от точки, в которую производится выстрел, то
        /// автоматически __вместо__ задачи по выстрелу создается задача для его перемещения.
        /// </summary>
        /// <param name="controller">Выпустивший снаряд игрок</param>
        /// <param name="destx">Точка назначения по X</param>
        /// <param name="desty">Точка назначения по Y</param>
        public void AddShootTask(Controller controller, int destx, int desty)
        {
            double far = Math.Pow((destx - controller.X), 2) + Math.Pow((desty - controller.Y), 2);
            far = Math.Sqrt(far);
            if (far > controller.ShootRaduis)
            {
                AddMovTask(controller, destx, desty);
                return;
            }
            RemoveMovTasks(controller);
            Ammo shut = controller.Shoot();
            if (shut.Type == 0) return;
            __shoot_queue.Add(new ShootQueueTask(controller, shut , controller.X, controller.Y, destx, desty));
        }

        /// <summary>
        /// Создает задачу по перемещению игрока в заданную точку.
        /// Перед тем, как создавать новую задачу, удалаются все старые задачи по перемещению данного игрока.
        /// </summary>
        /// <param name="controller">Игрок</param>
        /// <param name="destx">Точка аназначения по X</param>
        /// <param name="desty">Точка назначения по Y</param>
        public void AddMovTask(Controller controller, int destx, int desty)
        {
            RemoveMovTasks(controller);
            __move_queue.Add(new MoveQueueTask(controller, destx, desty));
        }

        /// <summary>
        /// Удаляет все задачи по перемещению данного игрока.
        /// Не используй этот метод перед AddMovTask, он вызывается в AddMovTask автоматически
        /// </summary>
        /// <param name="controller">Игрок</param>
        public void RemoveMovTasks(Controller controller)
        {
            for (int i = 0; i < __move_queue.Count; i++)
            {
                if (__move_queue[i].Controller.ID == controller.ID)
                {
                    __move_queue.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Получить список всех задач на перемещение одного контроллера
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <returns></returns>
        private List<MoveQueueTask> GetMovTasks(Controller controller)
        {
            var result = new List<MoveQueueTask>();
            foreach (var t in __move_queue)
            {
                if (t.Controller.ID == controller.ID)
                    result.Add(t);
            }

            return result;
        }

        /// <summary>
        ///Отрисовать всеь игровой процесс: игроков, снаряды и т.д.
        /// </summary>
        /// <param name="agraphics">Графический компонент для отрисовки</param>
        public void Draw(Graphics agraphics)
        {
            foreach (var g in __gamers)
            {
                var movs = GetMovTasks(g);
                int position_code = (movs.Count > 0) ? movs[0].position_code : 0;

                g.Draw(agraphics, position_code);
            }

            foreach (var shotask in __shoot_queue)
            {
                agraphics.FillEllipse(Brushes.Crimson, shotask.X, shotask.Y, 5, 5);
            }

            foreach (var collectable in __collectable_items)
            {
                collectable.Draw(agraphics);
            }
        }

        /// <summary>
        /// Выполнение всех задач.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimerEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < __shoot_queue.Count; i++)
            {
                ShootQueueTask shotask = __shoot_queue[i];

                if (shotask.Movturn())
                {
                    __shoot_queue.RemoveAt(i);
                    i--;
                    continue;
                }

                __shoot_queue[i] = shotask;
                foreach (var controller in __gamers)
                {
                    if (shotask.Sender.ID != controller.ID && controller.Touch(shotask.X, shotask.Y, shotask.Ammo.Radius))
                    {
                        __shoot_queue.RemoveAt(i);
                        controller.GetDamage(shotask.Ammo.Damage);
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < __gamers.Count; i++)
            {
                for (int c = 0; c < __collectable_items.Count; c++)
                {
                    if (__collectable_items[c].Touched(__gamers[i]))
                    {
                        switch (__collectable_items[c].Type)
                        {
                            case "Chest":
                                __gamers[i].NewChest(__collectable_items[c].Item as Chest);
                                break;
                        }

                        __collectable_items.RemoveAt(c);
                        c--;
                    }
                }
            }

            for (int i = 0; i < __move_queue.Count; i++)
            {
                MoveQueueTask movtask = __move_queue[i];

                if (movtask.Movturn())
                {
                    __move_queue.RemoveAt(i);
                    i--;
                    continue;
                }

                __move_queue[i] = movtask;
            }
            
        }

        /// <summary>
        /// Перемещение всех ИИ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onEnemyMovTimerEvent(object sender, EventArgs e)
        {
            int trytofind_whodies_count = 0;
            for (int i = (int)__human_players; i < __gamers.Count; i++)
            {
                int who_will_die = __rnd.Next(__gamers.Count);
                
                while ((!__gamers[who_will_die].Alive || __gamers[who_will_die].Team == __gamers[i].Team))
                {
                    __rnd = new Random(__rnd.Next()*i);
                    who_will_die = __rnd.Next(__gamers.Count);
                    if (++trytofind_whodies_count > __gamers.Count*20)
                    {
                        who_will_die = i;
                        break;
                    }
                }
                
                __rnd = new Random(__rnd.Next()*i);
                double far = Math.Pow(Math.Abs(__gamers[i].X - __gamers[who_will_die].X), 2);
                far += Math.Pow(Math.Abs(__gamers[i].Y - __gamers[who_will_die].Y), 2);
                far = Math.Sqrt(far);

                if (far < __gamers[i].ShootRaduis/2.0)
                {
                    AddMovTask(__gamers[i], __rnd.Next(Config.GameFieldSize.Width), __rnd.Next(Config.GameFieldSize.Height));
                }
                else
                {
                    AddShootTask(__gamers[i], __gamers[who_will_die].X,
                        __gamers[who_will_die].Y);
                }
            }
        }

        private void onCollectableSpawnEvent(object sender, EventArgs e)
        {
            switch (__rnd.Next(1))
            {
                case 0:
                    __rnd = new Random(__rnd.Next());
                    int x = __rnd.Next(Config.GameFieldSize.Width);
                    int y = __rnd.Next(Config.GameFieldSize.Height);
                    __rnd = new Random(__rnd.Next());
                    __collectable_items.Add(new ChestCollectable(x, y, __rnd.Next(1,4)));
                    break;
            }

            if (__collectable_items.Count > Config.MaxCollectables)
            {
                __collectable_items.RemoveAt(0);
            }
        }

        /// <summary>
        /// Создание нового набора игроков
        /// </summary>
        /// <param name="n"></param>
        private void CreateGamers(int n)
        {
            __gamers.Clear();
            for (int i = 0; i < n; i++)
            {
                __rnd = new Random(__rnd.Next()*i);
                __gamers.Add(new Controller(i));
                __gamers.Last().MoveTo(__rnd.Next(Config.GameFieldSize.Width), __rnd.Next(Config.GameFieldSize.Height));
            }
        }
    }
}