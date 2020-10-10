using System;

namespace cs._2020_10_07_struct
{
    /// <summary>
    /// Структура, описывающая снаряд, которым можно стрелять
    /// </summary>
    public struct Ammo
    {
        private int __damage, __speed, __radius;
        private int __type;
        
        /// <summary>
        /// Необходимо указать один из типов снаряда
        /// </summary>
        /// <param name="atype">Тип снаряда: число от 0 до 4</param>
        /// <exception cref="Exception"></exception>
        public Ammo(int atype)
        {
            __damage = 0;
            __speed = 0;
            __radius = 0;
            __type = 0;
            switch (atype)
            {
                case 0:
                    break;
                case 1:
                    __damage = 10;
                    __speed = 20;
                    __radius = 2;
                    __type = 1;
                    break;
                case 2:
                    __damage = 20;
                    __speed = 13;
                    __radius = 3;
                    __type = 2;
                    break;
                case 3:
                    __damage = 30;
                    __speed = 9;
                    __radius = 4;
                    __type = 3;
                    break;
                case 4:
                    __damage = 100;
                    __speed = 3;
                    __radius = 4;
                    __type = 4;
                    break;
                default:
                    throw new Exception("Ammo: public Ammo(int atype): invalid atype value");
            }
        }
        
        /// <summary>
        /// Тип снаряда: от 0 до 4
        /// 0 -- небоевой снаряд без скорости
        /// 1...4 увеличивается убойная сила, радиус поражения, но уменьшается скорость
        /// </summary>
        public int Type => __type;
        /// <summary>
        /// Урон, наносимый снарядом
        /// </summary>
        public int Damage => __damage;
        /// <summary>
        /// Скорость движения снаряда после выстрела
        /// </summary>
        public int Speed => __speed;
        /// <summary>
        /// Радиус поражения снаряда
        /// </summary>
        public int Radius => __radius;
    }
}