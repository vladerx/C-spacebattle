
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
            this.mainbox = new System.Windows.Forms.PictureBox();
            this.frame = new System.Windows.Forms.PictureBox();
            this.HPbar = new System.Windows.Forms.PictureBox();
            this.energybar = new System.Windows.Forms.PictureBox();
            this.distancepointer = new System.Windows.Forms.PictureBox();
            this.distancebar = new System.Windows.Forms.PictureBox();
            this.fuelbar = new System.Windows.Forms.PictureBox();
            this.coinsLabel = new System.Windows.Forms.Label();
            this.weaponbox = new System.Windows.Forms.PictureBox();
            this.currweaplabel = new System.Windows.Forms.Label();
            this.critdmglab = new System.Windows.Forms.Label();
            this.critchanlab = new System.Windows.Forms.Label();
            this.durlab = new System.Windows.Forms.Label();
            this.fuelconlab = new System.Windows.Forms.Label();
            this.energyconlab = new System.Windows.Forms.Label();
            this.fireratelab = new System.Windows.Forms.Label();
            this.maxlvlweaplab = new System.Windows.Forms.Label();
            this.menubox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HPbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.energybar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancepointer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menubox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainbox
            // 
            this.mainbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(6)))));
            this.mainbox.Location = new System.Drawing.Point(0, 2);
            this.mainbox.Name = "mainbox";
            this.mainbox.Size = new System.Drawing.Size(1443, 758);
            this.mainbox.TabIndex = 12;
            this.mainbox.TabStop = false;
            // 
            // frame
            // 
            this.frame.Image = ((System.Drawing.Image)(resources.GetObject("frame.Image")));
            this.frame.Location = new System.Drawing.Point(1449, 2);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(185, 758);
            this.frame.TabIndex = 13;
            this.frame.TabStop = false;
            // 
            // HPbar
            // 
            this.HPbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HPbar.Location = new System.Drawing.Point(1511, 167);
            this.HPbar.Name = "HPbar";
            this.HPbar.Size = new System.Drawing.Size(100, 23);
            this.HPbar.TabIndex = 23;
            this.HPbar.TabStop = false;
            // 
            // energybar
            // 
            this.energybar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.energybar.Location = new System.Drawing.Point(1511, 75);
            this.energybar.Name = "energybar";
            this.energybar.Size = new System.Drawing.Size(100, 23);
            this.energybar.TabIndex = 21;
            this.energybar.TabStop = false;
            // 
            // distancepointer
            // 
            this.distancepointer.BackColor = System.Drawing.Color.Red;
            this.distancepointer.Location = new System.Drawing.Point(1514, 212);
            this.distancepointer.Name = "distancepointer";
            this.distancepointer.Size = new System.Drawing.Size(5, 20);
            this.distancepointer.TabIndex = 20;
            this.distancepointer.TabStop = false;
            // 
            // distancebar
            // 
            this.distancebar.BackColor = System.Drawing.Color.Yellow;
            this.distancebar.Location = new System.Drawing.Point(1514, 217);
            this.distancebar.Name = "distancebar";
            this.distancebar.Size = new System.Drawing.Size(100, 8);
            this.distancebar.TabIndex = 19;
            this.distancebar.TabStop = false;
            // 
            // fuelbar
            // 
            this.fuelbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(101)))));
            this.fuelbar.Location = new System.Drawing.Point(1511, 121);
            this.fuelbar.Name = "fuelbar";
            this.fuelbar.Size = new System.Drawing.Size(100, 23);
            this.fuelbar.TabIndex = 18;
            this.fuelbar.TabStop = false;
            // 
            // coinsLabel
            // 
            this.coinsLabel.AutoSize = true;
            this.coinsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.coinsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.coinsLabel.ForeColor = System.Drawing.Color.White;
            this.coinsLabel.Location = new System.Drawing.Point(1520, 30);
            this.coinsLabel.Name = "coinsLabel";
            this.coinsLabel.Size = new System.Drawing.Size(0, 20);
            this.coinsLabel.TabIndex = 14;
            // 
            // weaponbox
            // 
            this.weaponbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.weaponbox.Image = ((System.Drawing.Image)(resources.GetObject("weaponbox.Image")));
            this.weaponbox.Location = new System.Drawing.Point(1484, 275);
            this.weaponbox.Name = "weaponbox";
            this.weaponbox.Size = new System.Drawing.Size(30, 30);
            this.weaponbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.weaponbox.TabIndex = 32;
            this.weaponbox.TabStop = false;
            // 
            // currweaplabel
            // 
            this.currweaplabel.AutoSize = true;
            this.currweaplabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.currweaplabel.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currweaplabel.ForeColor = System.Drawing.Color.Transparent;
            this.currweaplabel.Location = new System.Drawing.Point(1520, 279);
            this.currweaplabel.Name = "currweaplabel";
            this.currweaplabel.Size = new System.Drawing.Size(64, 25);
            this.currweaplabel.TabIndex = 33;
            this.currweaplabel.Text = "level 1";
            // 
            // critdmglab
            // 
            this.critdmglab.AutoSize = true;
            this.critdmglab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.critdmglab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.critdmglab.ForeColor = System.Drawing.Color.White;
            this.critdmglab.Location = new System.Drawing.Point(1529, 347);
            this.critdmglab.Name = "critdmglab";
            this.critdmglab.Size = new System.Drawing.Size(58, 25);
            this.critdmglab.TabIndex = 34;
            this.critdmglab.Text = "100%";
            this.critdmglab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // critchanlab
            // 
            this.critchanlab.AutoSize = true;
            this.critchanlab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.critchanlab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.critchanlab.ForeColor = System.Drawing.Color.White;
            this.critchanlab.Location = new System.Drawing.Point(1529, 394);
            this.critchanlab.Name = "critchanlab";
            this.critchanlab.Size = new System.Drawing.Size(39, 25);
            this.critchanlab.TabIndex = 35;
            this.critchanlab.Text = "0%";
            // 
            // durlab
            // 
            this.durlab.AutoSize = true;
            this.durlab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.durlab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.durlab.ForeColor = System.Drawing.Color.White;
            this.durlab.Location = new System.Drawing.Point(1523, 440);
            this.durlab.Name = "durlab";
            this.durlab.Size = new System.Drawing.Size(80, 25);
            this.durlab.TabIndex = 36;
            this.durlab.Text = "100/100";
            // 
            // fuelconlab
            // 
            this.fuelconlab.AutoSize = true;
            this.fuelconlab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.fuelconlab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fuelconlab.ForeColor = System.Drawing.Color.White;
            this.fuelconlab.Location = new System.Drawing.Point(1529, 488);
            this.fuelconlab.Name = "fuelconlab";
            this.fuelconlab.Size = new System.Drawing.Size(42, 25);
            this.fuelconlab.TabIndex = 37;
            this.fuelconlab.Text = "100";
            // 
            // energyconlab
            // 
            this.energyconlab.AutoSize = true;
            this.energyconlab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.energyconlab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.energyconlab.ForeColor = System.Drawing.Color.White;
            this.energyconlab.Location = new System.Drawing.Point(1529, 535);
            this.energyconlab.Name = "energyconlab";
            this.energyconlab.Size = new System.Drawing.Size(23, 25);
            this.energyconlab.TabIndex = 38;
            this.energyconlab.Text = "5";
            // 
            // fireratelab
            // 
            this.fireratelab.AutoSize = true;
            this.fireratelab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.fireratelab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fireratelab.ForeColor = System.Drawing.Color.White;
            this.fireratelab.Location = new System.Drawing.Point(1529, 582);
            this.fireratelab.Name = "fireratelab";
            this.fireratelab.Size = new System.Drawing.Size(23, 25);
            this.fireratelab.TabIndex = 39;
            this.fireratelab.Text = "5";
            // 
            // maxlvlweaplab
            // 
            this.maxlvlweaplab.AutoSize = true;
            this.maxlvlweaplab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.maxlvlweaplab.Font = new System.Drawing.Font("Script MT Bold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxlvlweaplab.ForeColor = System.Drawing.Color.White;
            this.maxlvlweaplab.Location = new System.Drawing.Point(1530, 628);
            this.maxlvlweaplab.Name = "maxlvlweaplab";
            this.maxlvlweaplab.Size = new System.Drawing.Size(23, 25);
            this.maxlvlweaplab.TabIndex = 40;
            this.maxlvlweaplab.Text = "9";
            // 
            // menubox
            // 
            this.menubox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(22)))), ((int)(((byte)(255)))));
            this.menubox.Image = ((System.Drawing.Image)(resources.GetObject("menubox.Image")));
            this.menubox.Location = new System.Drawing.Point(1491, 695);
            this.menubox.Name = "menubox";
            this.menubox.Size = new System.Drawing.Size(100, 30);
            this.menubox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.menubox.TabIndex = 41;
            this.menubox.TabStop = false;
            this.menubox.Click += new System.EventHandler(this.menuButtonClick);
            this.menubox.MouseLeave += new System.EventHandler(this.menuMouseLeave);
            this.menubox.MouseHover += new System.EventHandler(this.menuMouseHov);
            // 
            // game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1636, 761);
            this.Controls.Add(this.menubox);
            this.Controls.Add(this.maxlvlweaplab);
            this.Controls.Add(this.fireratelab);
            this.Controls.Add(this.energyconlab);
            this.Controls.Add(this.fuelconlab);
            this.Controls.Add(this.durlab);
            this.Controls.Add(this.critchanlab);
            this.Controls.Add(this.critdmglab);
            this.Controls.Add(this.currweaplabel);
            this.Controls.Add(this.weaponbox);
            this.Controls.Add(this.HPbar);
            this.Controls.Add(this.energybar);
            this.Controls.Add(this.distancepointer);
            this.Controls.Add(this.distancebar);
            this.Controls.Add(this.fuelbar);
            this.Controls.Add(this.coinsLabel);
            this.Controls.Add(this.frame);
            this.Controls.Add(this.mainbox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "game";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "SpaceBattle";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Game_Start);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.isKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.mainbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HPbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.energybar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancepointer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distancebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menubox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox mainbox;
        private System.Windows.Forms.PictureBox frame;
        private System.Windows.Forms.PictureBox HPbar;
        private System.Windows.Forms.PictureBox energybar;
        private System.Windows.Forms.PictureBox distancepointer;
        private System.Windows.Forms.PictureBox distancebar;
        private System.Windows.Forms.PictureBox fuelbar;
        private System.Windows.Forms.Label coinsLabel;
        private System.Windows.Forms.PictureBox weaponbox;
        private System.Windows.Forms.Label currweaplabel;
        private System.Windows.Forms.Label critdmglab;
        private System.Windows.Forms.Label critchanlab;
        private System.Windows.Forms.Label durlab;
        private System.Windows.Forms.Label fuelconlab;
        private System.Windows.Forms.Label energyconlab;
        private System.Windows.Forms.Label fireratelab;
        private System.Windows.Forms.Label maxlvlweaplab;
        private System.Windows.Forms.PictureBox menubox;
    }
}

