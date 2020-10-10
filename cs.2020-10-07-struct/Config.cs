using System.Drawing;

namespace cs._2020_10_07_struct
{
    public static class Config
    {
        public static readonly Size GameFieldSize = new Size(1150, 650);
        public static readonly int InitialGamersCount = 5;
        public static readonly int HumanPlayerCount = 1;
        public static readonly int HumanPlayerIndex0 = 0;

        public static readonly int GamerBaseHp = 100;
        public static readonly int GamerBaseSpeed = 15;
        public static readonly int GamerShootCooldown = 800;
        public static readonly int AIReactionDelay = 200;

        public static readonly int CollectableSpawnInterval = 5000;
        public static readonly int MaxCollectables = 5;
    }
}