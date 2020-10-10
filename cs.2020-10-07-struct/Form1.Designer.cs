namespace cs._2020_10_07_struct
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureGame = new System.Windows.Forms.PictureBox();
            this.timerDraw = new System.Windows.Forms.Timer(this.components);
            this.progressHP = new System.Windows.Forms.ProgressBar();
            this.progressChest = new System.Windows.Forms.ProgressBar();
            this.labelHP = new System.Windows.Forms.Label();
            this.labelChest = new System.Windows.Forms.Label();
            this.labelAmmo = new System.Windows.Forms.Label();
            this.panelHUD = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize) (this.pictureGame)).BeginInit();
            this.panelHUD.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureGame
            // 
            this.pictureGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureGame.Image = ((System.Drawing.Image) (resources.GetObject("pictureGame.Image")));
            this.pictureGame.Location = new System.Drawing.Point(12, 12);
            this.pictureGame.Name = "pictureGame";
            this.pictureGame.Size = new System.Drawing.Size(700, 700);
            this.pictureGame.TabIndex = 0;
            this.pictureGame.TabStop = false;
            this.pictureGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureGame_Paint);
            this.pictureGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureGame_MouseClick);
            // 
            // timerDraw
            // 
            this.timerDraw.Enabled = true;
            this.timerDraw.Tick += new System.EventHandler(this.timerDraw_Tick);
            // 
            // progressHP
            // 
            this.progressHP.Location = new System.Drawing.Point(59, 14);
            this.progressHP.Name = "progressHP";
            this.progressHP.Size = new System.Drawing.Size(100, 23);
            this.progressHP.TabIndex = 1;
            // 
            // progressChest
            // 
            this.progressChest.Location = new System.Drawing.Point(234, 14);
            this.progressChest.Name = "progressChest";
            this.progressChest.Size = new System.Drawing.Size(100, 23);
            this.progressChest.TabIndex = 2;
            this.progressChest.Click += new System.EventHandler(this.progressChest_Click);
            // 
            // labelHP
            // 
            this.labelHP.Location = new System.Drawing.Point(14, 14);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(39, 23);
            this.labelHP.TabIndex = 3;
            this.labelHP.Text = "HP:";
            this.labelHP.Click += new System.EventHandler(this.labelHP_Click);
            // 
            // labelChest
            // 
            this.labelChest.Location = new System.Drawing.Point(165, 14);
            this.labelChest.Name = "labelChest";
            this.labelChest.Size = new System.Drawing.Size(75, 23);
            this.labelChest.TabIndex = 4;
            this.labelChest.Text = "Chest: ()";
            // 
            // labelAmmo
            // 
            this.labelAmmo.Location = new System.Drawing.Point(351, 14);
            this.labelAmmo.Name = "labelAmmo";
            this.labelAmmo.Size = new System.Drawing.Size(193, 23);
            this.labelAmmo.TabIndex = 5;
            this.labelAmmo.Text = "label1";
            // 
            // panelHUD
            // 
            this.panelHUD.Controls.Add(this.labelHP);
            this.panelHUD.Controls.Add(this.labelAmmo);
            this.panelHUD.Controls.Add(this.progressHP);
            this.panelHUD.Controls.Add(this.progressChest);
            this.panelHUD.Controls.Add(this.labelChest);
            this.panelHUD.Location = new System.Drawing.Point(12, 718);
            this.panelHUD.Name = "panelHUD";
            this.panelHUD.Size = new System.Drawing.Size(557, 45);
            this.panelHUD.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 896);
            this.Controls.Add(this.panelHUD);
            this.Controls.Add(this.pictureGame);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize) (this.pictureGame)).EndInit();
            this.panelHUD.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label labelAmmo;
        private System.Windows.Forms.Label labelChest;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.Panel panelHUD;
        private System.Windows.Forms.PictureBox pictureGame;
        private System.Windows.Forms.ProgressBar progressChest;
        private System.Windows.Forms.ProgressBar progressHP;
        private System.Windows.Forms.Timer timerDraw;

        #endregion
    }
}