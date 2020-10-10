using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



///Игра с танчиками с ИИ
/// Есть у нас танк. Танк. Э, у танка есть ряд параметров:
///     Тип брони -- структура. Их есть три вида: light(минимум массы и защиты), mid, hard(максимум массы и защиты)
///     Скорость танка -- свойство (зависит от массы)
///     Здоровье -- свойство
///     Снаряжение -- коллекция из некоторых снарядов. Типы:
///         Ближнебойные, средней дальности, дальнебойные, БОМБАБЛЯТЬВСЕМПИЗДЕЦ
/// Методы танка:
///     получение урона при попадании противника
///     движение
///     очищение набора снаряжения и загрузка нового случайным образом
///     стреляние
/// Но это еще не все!
/// Есть еще один класс контроллера, который будет управлять танком.
///     перемещать, стрелять, следить за уроном и т.д.
///     два контролера: пользовательский и автоматический(ИИ)
///     будет ссылка на управляемый танк
///     свойства:
///         alive ли наш танк
///         position чтобы отслеживать состояние танка
///     методы:
///         движение, хит, шут
///         отрисовать
///         ход
/// Но и это еще не все! Только сегодня уникальное предложение: три класса по цене двух!
/// Третий класс -- Game. Собственно в классе гейм будут прописываться правила, игровой процесс и прочее счастье.
///
/// Задача минимум -- пошаговая игра для двух игроков. Все это, сабо сомой, с картинками.
/// Обойму патронов -- ОТОБРАЖАТЬ ВИЗУАЛЬНО!!!!
/// 

namespace cs._2020_10_07_struct
{
    public partial class Form1 : Form
    {
        private Game __game = new Game(Config.InitialGamersCount,(uint)Config.HumanPlayerCount);
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureGame.Size = Config.GameFieldSize;
            pictureGame.Location = new Point(5, 5);
            panelHUD.Size = new Size(550, 50);
            panelHUD.Location = new Point(5, Config.GameFieldSize.Height+10);
            this.Size = new Size(Math.Max(Config.GameFieldSize.Width+25, panelHUD.Size.Width+10), Config.GameFieldSize.Height+100);
        }

        private void pictureGame_Paint(object sender, PaintEventArgs e)
        {
            __game.Draw(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            pictureGame.Refresh();
            
            int basehp = __game.Players[Config.HumanPlayerIndex0].BASEHP;
            int hp = __game.Players[Config.HumanPlayerIndex0].HP;
            int chestbasehp = __game.Players[Config.HumanPlayerIndex0].Chest.BASEHP;
            int chesthp = __game.Players[Config.HumanPlayerIndex0].Chest.Health;

            progressHP.Maximum = basehp;
            progressHP.Value = hp;
            progressChest.Maximum = chestbasehp;
            progressChest.Value = chesthp;

            string ammotext = "";
            foreach (var a in __game.Players[Config.HumanPlayerIndex0].Ammos)
                ammotext += $"{a.Type} ";
            labelAmmo.Text = ammotext;

            string chesttext = $"Chest ({__game.Players[Config.HumanPlayerIndex0].Chest.Type}): ";
            labelChest.Text = chesttext;
        }

        private void pictureGame_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    __game.AddShootTask(__game.Players[Config.HumanPlayerIndex0], e.X, e.Y);
                    break;
                case MouseButtons.Right:
                    __game.AddMovTask(__game.Players[Config.HumanPlayerIndex0], e.X, e.Y);
                    break;
                case MouseButtons.Middle:
                    __game.AddNewPlayer(e.X, e.Y);
                    break;
            }
        }

        private void labelHP_Click(object sender, EventArgs e)
        {
        }

        private void progressChest_Click(object sender, EventArgs e)
        {
        }
    }
}