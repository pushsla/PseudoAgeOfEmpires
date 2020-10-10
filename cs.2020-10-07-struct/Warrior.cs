using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Объект, который описывает "тушку" воина.
    /// На нем надета броня, у него есть здоровье и он тратит патроны из обоймы.
    /// </summary>
    public class Warrior
    {
        // идентификатор
        private int __id;
        // Количество патронов в обойме
        private const int AMMO_COUNT = 5;
        private int __health, __speed;
        private Chest __armor;
        // Обойма
        private List<Ammo> __shoots;
        // Дальность выстрела
        private int __shoot_radius;
        private Random __rnd;

        public int BASEHP => Config.GamerBaseHp;
        public int Health => __health;
        public int Speed => __speed;
        public int ID => __id;
        public int ShootRadius => __shoot_radius;
        public List<Ammo> Ammos => __shoots;

        /// <summary>
        /// Необходимо указать __уникальный__ ID для Warrior
        /// </summary>
        /// <param name="id">Уникальный идентификатор воина</param>
        public Warrior(int id)
        {
            __id = id;
            __rnd = new Random();
            __health = Config.GamerBaseHp;
            __speed = Config.GamerBaseSpeed;
            __shoots = new List<Ammo>();
            __shoot_radius = 300;

            this.Chest = new Chest(0);
            ResetAmmo();
        }

        /// <summary>
        /// Получить чистый урон от попадания снаряда.
        /// В зависимости от прочности и защиты брони, надетой на Warrior,
        /// он получит меньшее количество чистого урона, отдав взамен часть прочности брони
        /// </summary>
        /// <param name="adamage">размер урона</param>
        public void GetDamage(int adamage)
        {
            __health -= __armor.GetDamage(adamage);
            __health = (__health < 0) ? 0 : __health;
        }

        /// <summary>
        /// Потратить один снаряд из обоймы
        /// Если снарядов не осталось, обойма перезаряжается и выпускается пассивный снаряд с типом 0
        /// </summary>
        /// <returns>Снаряд</returns>
        public Ammo Shoot()
        {
            if (__shoots.Count <= 0)
            {
                ResetAmmo();
                return new Ammo(0);
            }
            Ammo shoot = __shoots.Last();
            __shoots.RemoveAt(__shoots.Count-1);
            
            return shoot;
        }
        
        /// <summary>
        /// Перезарядить обойму снарядов
        /// </summary>
        public void ResetAmmo()
        {
            __shoots.Clear();
            for (int i = 0; i < AMMO_COUNT; i++)
            {
                __shoots.Add(new Ammo(RandomQueue.Next(1, 5)));
            }
        }

        /// <summary>
        /// Броня, надетая на Warrior
        /// При присваивании значения пересчитывается скорость.
        /// </summary>
        public Chest Chest
        {
            get => __armor;
            set
            {
                __armor = value;
                __speed = Config.GamerBaseSpeed + __armor.Speedbuff;
            }
        }
    }
}