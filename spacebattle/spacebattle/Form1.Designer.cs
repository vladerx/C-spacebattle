
namespace spacebattle
{
    partial class game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(game));
            this.coinsLabel = new System.Windows.Forms.Label();
            this.coin = new System.Windows.Forms.PictureBox();
            this.energy = new System.Windows.Forms.PictureBox();
            this.fuel = new System.Windows.Forms.PictureBox();
            this.fuelbar = new System.Windows.Forms.PictureBox();
            this.distancebar = new System.Windows.Forms.PictureBox();
            this.distancepointer = new System.Windows.Forms.PictureBox();
            this.energybar = new System.Windows.Forms.PictureBox();
            this.playerHP = new System.Windows.Forms.PictureBox();
            this.HPbar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.coin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.energy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancepointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.energybar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HPbar)).BeginInit();
            this.SuspendLayout();
            // 
            // coinsLabel
            // 
            this.coinsLabel.AutoSize = true;
            this.coinsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coinsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.coinsLabel.Location = new System.Drawing.Point(1515, 24);
            this.coinsLabel.Name = "coinsLabel";
            this.coinsLabel.Size = new System.Drawing.Size(0, 20);
            this.coinsLabel.TabIndex = 0;
            // 
            // coin
            // 
            this.coin.Image = ((System.Drawing.Image)(resources.GetObject("coin.Image")));
            this.coin.Location = new System.Drawing.Point(1485, 27);
            this.coin.Name = "coin";
            this.coin.Size = new System.Drawing.Size(18, 17);
            this.coin.TabIndex = 1;
            this.coin.TabStop = false;
            // 
            // energy
            // 
            this.energy.Image = ((System.Drawing.Image)(resources.GetObject("energy.Image")));
            this.energy.Location = new System.Drawing.Point(1481, 62);
            this.energy.Name = "energy";
            this.energy.Size = new System.Drawing.Size(24, 32);
            this.energy.TabIndex = 2;
            this.energy.TabStop = false;
            // 
            // fuel
            // 
            this.fuel.Image = ((System.Drawing.Image)(resources.GetObject("fuel.Image")));
            this.fuel.Location = new System.Drawing.Point(1481, 112);
            this.fuel.Name = "fuel";
            this.fuel.Size = new System.Drawing.Size(24, 32);
            this.fuel.TabIndex = 4;
            this.fuel.TabStop = false;
            // 
            // fuelbar
            // 
            this.fuelbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(101)))));
            this.fuelbar.Location = new System.Drawing.Point(1515, 117);
            this.fuelbar.Name = "fuelbar";
            this.fuelbar.Size = new System.Drawing.Size(100, 23);
            this.fuelbar.TabIndex = 5;
            this.fuelbar.TabStop = false;
            // 
            // distancebar
            // 
            this.distancebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.distancebar.Location = new System.Drawing.Point(1496, 221);
            this.distancebar.Name = "distancebar";
            this.distancebar.Size = new System.Drawing.Size(100, 8);
            this.distancebar.TabIndex = 7;
            this.distancebar.TabStop = false;
            // 
            // distancepointer
            // 
            this.distancepointer.BackColor = System.Drawing.Color.Aqua;
            this.distancepointer.Location = new System.Drawing.Point(1496, 216);
            this.distancepointer.Name = "distancepointer";
            this.distancepointer.Size = new System.Drawing.Size(5, 20);
            this.distancepointer.TabIndex = 8;
            this.distancepointer.TabStop = false;
            // 
            // energybar
            // 
            this.energybar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.energybar.Location = new System.Drawing.Point(1515, 67);
            this.energybar.Name = "energybar";
            this.energybar.Size = new System.Drawing.Size(100, 23);
            this.energybar.TabIndex = 9;
            this.energybar.TabStop = false;
            // 
            // playerHP
            // 
            this.playerHP.Image = ((System.Drawing.Image)(resources.GetObject("playerHP.Image")));
            this.playerHP.Location = new System.Drawing.Point(1485, 166);
            this.playerHP.Name = "playerHP";
            this.playerHP.Size = new System.Drawing.Size(14, 24);
            this.playerHP.TabIndex = 10;
            this.playerHP.TabStop = false;
            // 
            // HPbar
            // 
            this.HPbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HPbar.Location = new System.Drawing.Point(1515, 167);
            this.HPbar.Name = "HPbar";
            this.HPbar.Size = new System.Drawing.Size(100, 23);
            this.HPbar.TabIndex = 11;
            this.HPbar.TabStop = false;
            // 
            // game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1624, 761);
            this.Controls.Add(this.HPbar);
            this.Controls.Add(this.playerHP);
            this.Controls.Add(this.energybar);
            this.Controls.Add(this.distancepointer);
            this.Controls.Add(this.distancebar);
            this.Controls.Add(this.fuelbar);
            this.Controls.Add(this.fuel);
            this.Controls.Add(this.energy);
            this.Controls.Add(this.coin);
            this.Controls.Add(this.coinsLabel);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "game";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "SpaceBattle";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Game_Start);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.isKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.coin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.energy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancepointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.energybar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HPbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label coinsLabel;
        private System.Windows.Forms.PictureBox coin;
        private System.Windows.Forms.PictureBox energy;
        private System.Windows.Forms.PictureBox fuel;
        private System.Windows.Forms.PictureBox fuelbar;
        private System.Windows.Forms.PictureBox distancebar;
        private System.Windows.Forms.PictureBox distancepointer;
        private System.Windows.Forms.PictureBox energybar;
        private System.Windows.Forms.PictureBox playerHP;
        private System.Windows.Forms.PictureBox HPbar;
    }
}

