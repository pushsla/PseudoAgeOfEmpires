using System;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Класс, описывающая броню Warrior.
    /// </summary>
    public class Chest
    {
        public static readonly int TypesCount = 4;
        private int __defence_buff, __speed_buff, __health;
        private int __type;
        private const int BASE_HP = 100;

        public int BASEHP => BASE_HP;

        /// <summary>
        /// Необходимо указать тип брони
        /// </summary>
        /// <param name="atype">Тип брони: число от 0 до 3</param>
        /// <exception cref="Exception"></exception>
        public Chest(int atype)
        {
            __defence_buff = 0;
            __speed_buff = 0;
            __type = 0;
            __health = BASE_HP;
            switch (atype)
            {
                case 0:
                    break;
                case 1:
                    __type = atype;
                    __defence_buff = 10;
                    __speed_buff = -2;
                    break;
                case 2:
                    __type = atype;
                    __defence_buff = 20;
                    __speed_buff = -4;
                    break;
                case 3:
                    __type = atype;
                    __defence_buff = 30;
                    __speed_buff = -8;
                    break;
                default:
                    throw new Exception("Chest: public Chest()int atype: invalid atype value");
            }
        }
    
        /// <summary>
        /// Если надета броня, то она поглощает часть урона, при этом изнашиваясь
        /// Метод изнашивает броню и возвращает, сколько урона получает в результате сам танк
        /// </summary>
        /// <param name="adamage">Общий нанесенный урон</param>
        /// <returns>Остаток урона для Warrior</returns>
        public int GetDamage(int adamage)
        {
            if (__health <= 0)
            {
                __health = 0;
                return adamage;
            }

            __health -= adamage;
            __health = (__health < 0) ? 0 : __health;
            
            int delta = __defence_buff - adamage;
            if (delta > 0) return 0;
            
            return -delta;

        }

        /// <summary>
        /// Степень защиты брони
        /// </summary>
        public int Defencebuff => __defence_buff;
        /// <summary>
        /// Бонус к скорости Warrior в этой броне
        /// </summary>
        public int Speedbuff => __speed_buff;
        /// <summary>
        /// Тип брони от 0 до 3
        /// 0 -- броня без защиты и без бонусов
        /// 1..3 -- увеличивается защита и именьшается скорость
        /// </summary>
        public int Type => __type;
        /// <summary>
        /// Остаток прочности брони
        /// </summary>
        public int Health => __health;
    }
}