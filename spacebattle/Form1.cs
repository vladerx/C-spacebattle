using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    public partial class game : Form
    {
        private playerobj player = new playerobj(5, 100, 100, 10, 5);
        private dropobj drop = new dropobj();
        private miscobj misc = new miscobj();
        private bulletobj bullet = new bulletobj();
        private enemyobj enemy = new enemyobj();
        private planetobj planet = new planetobj();
        private imgs imgS = new imgs();

        private int dropcounter = 0;
        private int miscounter = 0;
        private int bulletcounter = 0;
        private int enemycounter = 0;
        private int planetcounter = 0;
        private int colidecounter = 0;

        private static Random random = new Random();
        Bitmap bitmap = new Bitmap(1464, 760);
        private Timer timer = new Timer();
        private int[] direc = { 0, 0, 0, 0 };
        private Boolean keylockSpace = true;
        private Boolean keylockCTRL = true;
        private int enemySpawnCount = 0;
        private int enemySpawnInterval = 50;

        private int Distance = 0; /// 18000 intervals in 5 mins approx
        private int bulletCounter = 0;
        private int playerInitSpeed = 0;
        private int playerHPRatio = 0;

        public game()
        {
            InitializeComponent();
        }

        private void Game_Start(object sender, EventArgs e)
        {
            coin.Parent = this;
            coin.BringToFront();
            playerInitSpeed = player.playerSpeed;
            playerHPRatio = player.HP/100;
            timer.Interval = 10; 
            timer.Tick += new EventHandler(timerEvent);
            timer.Start();
            createmiscObj(20, 0); // creating misc objects at the start
            createmiscObj(100, 1);
            for (int i = 2; i < imgS.miscimgs.Count; i++) {
                createmiscObj(20, i);
            }
            creatEnemyObj(3, 10, 700);
        }

        private void drawMiscImg() {
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            foreach (int misckey in misc.miscords.Keys)
            {
                g.DrawImage(imgS.miscimgs.ElementAt(misc.misctypes[misckey]).Key, misc.miscords[misckey][0], misc.miscords[misckey][1]);
            }
            foreach (int planetkey in planet.planetcords.Keys)
            {
                g.DrawImage(imgS.planetimgs.ElementAt(planet.planetypes[planetkey]).Key, planet.planetcords[planetkey][0], planet.planetcords[planetkey][1]);
            }
            if (bullet.bulletcords.Count != 0) {
                foreach (int bulletkey in bullet.bulletcords.Keys)
                {
                    if (bullet.guntype == 3 || bullet.guntype == 0)
                    {
                        g.DrawImage(imgS.bulletimgs.ElementAt(bullet.bulletypes[bulletkey]).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    }
                    else if (bullet.guntype == 1) {
                        g.DrawImage(imgS.pulseimgs.ElementAt(0).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    }
                    else if (bullet.guntype == 2)
                    {
                        g.DrawImage(imgS.pulseimgs.ElementAt(bullet.bulletypes[bulletkey]).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    } else if (bullet.guntype == 4) {
                        g.DrawImage(imgS.canonballimgs.ElementAt(3 - bullet.bulletbounces[bulletkey]).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    }
                    else if (bullet.guntype == 5)
                    {
                        g.DrawImage(imgS.missileimgs.ElementAt(bullet.bulletypes[bulletkey] % 10).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    }
                    else if (bullet.guntype == 6)
                    {
                        g.DrawImage(imgS.guidedMissileimgs.ElementAt(bullet.bulletypes[bulletkey]).Key, bullet.bulletcords[bulletkey][0], bullet.bulletcords[bulletkey][1]);
                    }
                }
            }
            if (enemy.enemycords.Count != 0) {
                foreach (int enemykey in enemy.enemycords.Keys)
                {
                    g.DrawImage(imgS.enemyimgs[(enemy.enemyframes[enemykey])], enemy.enemycords[enemykey][0], enemy.enemycords[enemykey][1]);
                }
            }
            if (drop.dropcords.Count != 0) {
                foreach (int dropkey in drop.dropcords.Keys)
                {
                    g.DrawImage(imgS.dropimgs.ElementAt(drop.droptypes[dropkey]).Key, drop.dropcords[dropkey][0], drop.dropcords[dropkey][1]);
                }
            }
            if (!keylockCTRL && player.energy > 0)   // draws flame when accelerating speed.
            {
                if (Distance % 3 == 0) {
                    g.DrawImage(imgS.blastimgs[0], player.cords[0]+ 78, player.cords[1] + 27);
                } else if (Distance % 2 == 0) {
                    g.DrawImage(imgS.blastimgs[1], player.cords[0] + 78, player.cords[1] + 27);
                }
                else if (Distance % 1 == 0)
                {
                    g.DrawImage(imgS.blastimgs[2], player.cords[0] + 78, player.cords[1] + 27);
                }
            }
            g.DrawImage(imgS.playerimg, player.cords[0], player.cords[1]);
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "map";
            //mainbox.BringToFront();
        }
        private void drawMainMenuImg() //form game size = 1652, 815
        {
            this.Size = new Size(715, 815);
            Graphics g = Graphics.FromImage(mainbitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(imgS.mainMenuimgs[0], 0, 0);
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Location = new Point(0, 0);
            mainbox.Image = mainbitmap;
            mainbox.Parent = this;
            mainbox.Name = "mainMenu";

            for (int i = 0; i < 4; i++) {
                PictureBox startbut = new PictureBox();
                startbut.Location = new Point(buttoncords[i+4,0], buttoncords[i + 4, 1]);
                startbut.SizeMode = PictureBoxSizeMode.AutoSize;
                startbut.Image = imgS.mainMenuimgs[buttonindxs[i + 4]];
                startbut.Name = buttonsnames[i+4];
                startbut.Parent = this.GetChildAtPoint(startbut.Location);
                buttonslist.Add(startbut);
                startbut.MouseHover += new EventHandler(Mouse_Hover);
                startbut.MouseLeave += new EventHandler(Mouse_Leave);
                startbut.Click += new EventHandler(Button_Click);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var picBox = (PictureBox)sender;
            if (picBox.Name == "fullscreen")
            {
                if (picBox.Image == imgS.settingsimgs[2])
                {
                    picBox.Image = imgS.settingsimgs[3];
                }
                else
                {
                    picBox.Image = imgS.settingsimgs[2];
                }
            }
            else if (picBox.Name == "controls")
            {
                controlbut = 1;
                return;
            }
            else if (picBox.Name == "resume")
            {
                resumebut = 1;
                return;
            }
            else if (picBox.Name == "settings")
            {
                settingsbut = 1;
                return;
            }
            else if (picBox.Name == "quit")
            {
                Application.Exit();
            }
        }

        private void Mouse_Hover(object sender, EventArgs e) {
            var picBox = (PictureBox)sender;
            if (picBox.Name == "start")
            {
                picBox.Image = imgS.mainMenuimgs[2];
                return;
            } else if (picBox.Name == "shop") {
                picBox.Image = imgS.mainMenuimgs[4];
            }
            else if (picBox.Name == "settings")
            {
                picBox.Image = imgS.mainMenuimgs[6];
            }
            else if (picBox.Name == "quit")
            {
                picBox.Image = imgS.mainMenuimgs[8];
            }
        }

        private void Mouse_Leave(object sender, EventArgs e)
        {
            var picBox = (PictureBox)sender;
            if (picBox.Name == "start")
            {
                picBox.Image = imgS.mainMenuimgs[1];
                return;
            }
            else if (picBox.Name == "shop")
            {
                picBox.Image = imgS.mainMenuimgs[3];
            }
            else if (picBox.Name == "settings")
            {
                picBox.Image = imgS.mainMenuimgs[5];
            }
            else if (picBox.Name == "quit")
            {
                picBox.Image = imgS.mainMenuimgs[7];
            }
        }

        private void removeMenuButtons()
        {
            foreach (PictureBox buts in buttonslist)
            {
                buts.Click -= Button_Click;
                buts.Parent.Controls.Remove(buts);
                if (settingsbut == 2) {
                    buts.MouseHover -= Mouse_Hover;
                    buts.MouseLeave -= Mouse_Leave;
                }
            }
            buttonslist.Clear();
        }    

        private void isKeyDown(object sender, KeyEventArgs e) //upon keydown changing movement in a direction
        {
            if (e.KeyCode == Keys.Up)
            {
                direc[0] = 1;
            }
            if (e.KeyCode == Keys.Down)
            {
                direc[1] = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                direc[2] = 1;
            }
            if (e.KeyCode == Keys.Left)
            {
                direc[3] = 1;
            }
            if (e.KeyCode == Keys.Space)          //creating bullet when spacebar is pressed
            {
                keylockSpace = false;                //timing constant fire
            }
            if (e.KeyCode == Keys.ControlKey)          //creating speed accelaration when ALT is pressed
            {
                keylockCTRL = false;
            }
        }

        private void isKeyUp(object sender, KeyEventArgs e) //upon keyup reseting movement in a direction
        {
            if (e.KeyCode == Keys.Up)
            {
                direc[0] = 0;
            }
            if (e.KeyCode == Keys.Down)
            {
                direc[1] = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                direc[2] = 0;
            }
            if (e.KeyCode == Keys.Left)
            {
                direc[3] = 0;
            }
            if (e.KeyCode == Keys.Space)          //cancel bullet fire on spacebar key release
            {
                keylockSpace = true;
            }
            if (e.KeyCode == Keys.ControlKey)          //canceling speed accelaration when ALT is released
            {
                keylockCTRL = true;
                player.playerSpeed = playerInitSpeed;
            }

        }

        private void timerEvent(object sender, EventArgs e)
        {
            drawMiscImg();
            Distance += 1;
            coinsLabel.Text = player.coins.ToString();
            collusionDetection();
            player.movePlayer(direc);
            if (!keylockSpace)  // only when spacebar is pressed generating bullets in constant fire rate.
            {
                if (bulletCounter >= (100 / player.fireRate))
                {
                    int bulletguntype = bullet.guntype;
                    int xcord = 0;
                    int ycord = 0;
                    int type = 0;
                    if (bulletguntype == 0 || bulletguntype == 3)
                    {
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            int[] playercords = player.cords;
                            if (bullet.bulletLevel == 1)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + 17;
                                type = 0;
                            }
                            else if (bullet.bulletLevel == 2)
                            {
                                if (i == 0)
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + 7;
                                    type = 0;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + 27;
                                    type = 0;
                                }
                            }
                            else if (bullet.bulletLevel == 3)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + (-3 + (i * 20));
                                type = 0;
                            }
                            else if (bullet.bulletLevel == 4)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-13 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (bullet.bulletLevel == 5)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (bullet.bulletLevel == 6)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (bullet.bulletLevel == 7)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (bullet.bulletLevel == 8)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 7)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (bullet.bulletLevel == 9)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 7)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 8)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                    
                                }
                            }
                            bullet.createBullet(xcord, ycord, type, bulletcounter);

                        }

                    }
                    else if (bulletguntype == 1)  //create pulse type bullet
                    {
                        xcord = player.cords[0] - 30;
                        ycord = player.cords[1] - 8;
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 3 || i == 6) {     // 4th and 7th pulse will start more indented
                                xcord -= 45;
                                ycord = player.cords[1] - 8;
                            } else if (i == 1 || i == 4 || i == 7) {    // 2th and 5th and 8th pulse start higher
                                ycord = player.cords[1] - 78;
                            }
                            else if (i == 2 || i == 5 || i == 8)    // 3th and 6th and 9th pulse start lower
                            {
                                ycord = player.cords[1] + 62;
                            }
                            bullet.createPulse(xcord, ycord, bulletcounter, 0);
                        }
                    }
                    else if (bulletguntype == 2)  //create pulse type bullet
                    {
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0) 
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 10;
                                ycord = player.cords[1] - 40;
                                type = 2;
                            }
                            else if (i == 2) 
                            {
                                xcord = player.cords[0] + 10;
                                ycord = player.cords[1] + 40;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 75;
                                ycord = player.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 80;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 80;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 120;
                                ycord = player.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] - 70;
                                ycord = player.cords[1] - 120;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] - 70;
                                ycord = player.cords[1] + 120;
                                type = 3;
                            }
                            bullet.createPulse(xcord, ycord, bulletcounter, type);
                        }
                    }
                    else if (bulletguntype == 4)  //create canon ball type bullet
                    {
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                                type = 0;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                                type = 0;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                                type = 0;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                                type = 2;
                            }
                            bullet.createCanonBall(xcord, ycord, bulletcounter, type);
                        }

                    }
                    else if (bulletguntype == 5)  //create missile type bullet
                    {
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                                type = 10;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                                type = 21;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                                type = 32;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                                type = 40;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                                type = 51;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                                type = 62;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                                type = 70;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                                type = 81;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                                type = 92;
                            }
                            bullet.createMissile(xcord, ycord, bulletcounter, type);
                        }

                    }
                    else if (bulletguntype == 6)  //create guided missile type bullet
                    {
                        for (int i = 0; i < bullet.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                                type = 0;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                                type = 0;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                                type = 0;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                                type = 2;
                                
                            }
                            bullet.createGuidedMissile(xcord, ycord, bulletcounter, type);
                        }

                    }
                    bulletCounter = 0;
                }
            }
            if (!keylockCTRL)  // only when ALT is pressed accelarating player speed consuming energy.
            {
                if (player.energy - player.energyCons >= 0)
                {
                    if (player.playerSpeed < 20)
                    {
                        player.playerSpeed += 1;
                        
                    }
                    player.energy -= player.energyCons;
                    energybar.Width -= player.energyCons;
                }
                else {
                    keylockCTRL = true;
                    player.playerSpeed = playerInitSpeed;
                }
            }

            if (bullet.bulletcords.Count != 0)  
            {
                if (bullet.guntype == 3 || bullet.guntype == 0)
                {
                    bullet.moveBullet();  //move bullets  
                }
                else if (bullet.guntype == 1 || bullet.guntype == 2) {
                    bullet.movePulse();  
                } else if (bullet.guntype == 4) {
                    bullet.moveCanonBall();
                }
                else if (bullet.guntype == 5)
                {
                    bullet.moveMissile();
                }
                else if (bullet.guntype == 6)
                {
                    bullet.moveGuidedMissile(enemy.enemycords, enemy.enemysize, imgS.guidedMissileimgs.ElementAt(0).Value, coinsLabel, enemy.enemyframes);
                }
            }
            modifyMiscObj();           //misc movement typeofobj = 0 : flying dust, typeofobj = 1 : star
            modifyEnemyObj();
            modifyPlanetObj();
            modifyDropObj();
            if (enemySpawnCount == enemySpawnInterval) {                  // spawn another emeny every 1 second (100 * timerTick) approx
                creatEnemyObj(1, 1, 1);
                enemySpawnCount = -1;
            }
            if (Distance % 500 == 0) {              //every 110 distace units move pointer by one pixel
                distancepointer.Location = new Point(distancepointer.Location.X+1, distancepointer.Location.Y);
                if (enemySpawnInterval > 1) {
                    enemySpawnInterval -= 1;
                }
            }
            if (Distance % 2200 == 0)
            { 
                enemySpawnInterval -= 1;
                createplanetObj(0,imgS.planetimgs.ElementAt(0).Value.Width, 50);      // spawns planets on different intervals
            }
            if (Distance % 1800 == 0)
            {
                createplanetObj(1, imgS.planetimgs.ElementAt(1).Value.Width, 200);
            }
            if (Distance % 2500 == 0)
            {
                createplanetObj(2, imgS.planetimgs.ElementAt(2).Value.Width, 500);
            }
            if (Distance % 3500 == 0)
            {
                createplanetObj(3, imgS.planetimgs.ElementAt(3).Value.Width, 300);
            }
            if (Distance % player.fuelCons == 0)       //every 1100 distace units decrease fuel amount
            {
                player.fuel -= 1;
                fuelbar.Width = player.fuel;
            }
            if (player.labelcounter.Count != 0) {        //each label that created has a countdown until it removed
                List<int> keysRemove = new List<int>();
                foreach (int labelkey in player.colides.Keys) 
                {
                    if (player.labelcounter[labelkey] == 0)
                    {
                        keysRemove.Add(labelkey);
                    }
                    player.labelcounter[labelkey] -= 1;
                }
                if (keysRemove.Count != 0)
                {
                    foreach (int key in keysRemove)
                    {
                        if (player.colides[key].Parent != null) {
                            player.colides[key].Parent.Controls.Remove(player.colides[key]);
                        }
                        player.labelcounter.Remove(key);
                        player.colides.Remove(key);
                    }
                    keysRemove.Clear();
                }
            }
            enemySpawnCount += 1;
            bulletCounter += 1;
        }

        private void createmiscObj(int amount, int objtype) {
            int i = 0;
            int startSpeed;
            if (objtype == 0) {
                startSpeed = 15;
            } else {
                startSpeed = 1;
            }
            while (i < amount)
            {
                miscounter += 1;
                misc.createMisc(random.Next(1, 1400), random.Next(1, 790), random.Next(startSpeed, startSpeed * 2), objtype, miscounter);
                i += 1;
            }
        }

        private void modifyMiscObj()
        {
            int startSpeed;
            int objtype;
            int renderMisc;
            if (misc.miscords.Count != 0)
            {
                renderMisc = -1;
                renderMisc = misc.moveMisc();   
                if (renderMisc != -1)
                {
                    objtype = renderMisc;
                    if (objtype == 0)
                    {
                        startSpeed = 15;
                    }
                    else
                    {
                        startSpeed = 1;
                    }
                    miscounter += 1;
                    misc.createMisc(1, random.Next(50, 700), random.Next(startSpeed, startSpeed*2), objtype, miscounter);    //creating stardust/star each time one is removed
                }
            }
        }

        private void createplanetObj(int objtype, int xcord, int ycord)
        {
            planetcounter += 1;
            planet.createplanet(-xcord, ycord, objtype, planetcounter);
        }

        private void modifyPlanetObj()
        {
            if (planet.planetcords.Count != 0)
            {
                planet.moveplanet();
            }
        }

        private void creatEnemyObj(int amount, int xStart, int xEnd)
        {
            int i = 0;
            while (i < amount)
            {
                enemycounter += 1;
                enemy.createEnemy(random.Next(xStart, xEnd), random.Next(1, 750), enemycounter);
                i += 1;
            }
        }

        private void modifyEnemyObj()
        {
            if (enemy.enemycords.Count != 0)
            {
                enemy.moveEnemy();
            }
        }

        private void creatDropObj(int[] cords)
        {
            dropcounter += 1;
            int droptype = drop.calcDrop(random);
            drop.createDrop(cords[0], cords[1], droptype, dropcounter);
        }

        private void modifyDropObj()
        {
            if (drop.dropcords.Count != 0)
            {
                drop.moveDrop();
            }
        }

        private Boolean colideRects(int[] startCords, Size size, int[] startCords1, Size size1) {       // checking if two rectangles intersect 
            Rectangle rect1 = new Rectangle(startCords[0], startCords[1], size.Width, size.Height);
            Rectangle rect2 = new Rectangle(startCords1[0], startCords1[1], size1.Width, size1.Height);
            if (rect1.IntersectsWith(rect2)) {
                return true;
            }
            return false;
        }

        private void collusionDetection()
        {
            List<int> keysRemove = new List<int>();
            foreach (int enemykey in enemy.enemycords.Keys)
            {
                if (enemy.enemyframes[enemykey] != 2)                  //ignore dead enemies on screen
                {
                    foreach (int bulletkey in bullet.bulletcords.Keys)
                    {
                        if (colideRects(enemy.enemycords[enemykey], enemy.enemysize, bullet.bulletcords[bulletkey], imgS.pulseimgs.ElementAt(0).Value))    // collusion Detection between bullets and enemies
                        {
                            enemy.enemyhps[enemykey] = enemy.enemyhps[enemykey] - bullet.bulletDamage;
                            if (bullet.guntype != 4)
                            {
                                keysRemove.Add(bulletkey);
                            }
                            else {
                                bullet.bulletbounces[bulletkey] -= 1;
                                if (bullet.bulletbounces[bulletkey] == 0) {
                                    keysRemove.Add(bulletkey);
                                }
                            }
                            if (enemy.enemyhps[enemykey] < 0)            //choose img for enemy based on condition
                            {
                                enemy.setEnemyFrame(2, enemykey);
                                creatDropObj(enemy.enemycords[enemykey]);
                                break;
                            }
                            else
                            {
                                enemy.setEnemyFrame(1, enemykey);
                            }
                        }

                    }
                    if (keysRemove.Count != 0)
                    {
                        foreach (int key in keysRemove)
                        {
                            bullet.bulletcords.Remove(key);
                            bullet.bulletypes.Remove(key);
                            if (bullet.bulletbounces.Count != 0) {
                                bullet.bulletbounces.Remove(key);
                            }
                        }
                        keysRemove.Clear();
                    }
                    if (colideRects(enemy.enemycords[enemykey], enemy.enemysize, player.cords, player.size))  // collusion Detection between enemy and player
                    {
                        player.HP -= enemy.enemyBodyDMG;
                        HPbar.Width = (player.HP / playerHPRatio);
                        enemy.setEnemyFrame(2, enemykey);
                        colidecounter += 1;
                        Label duradamage = new Label();    // creating a label above the player on collusion with enemy to show durability decrease
                        player.colides.Add(colidecounter, duradamage);
                        player.labelcounter.Add(colidecounter, 100);
                        Font font = new Font("Cooper Black", 12);
                        duradamage.Font = font;
                        duradamage.Location = new Point(player.cords[0], player.cords[1] - 40);
                        duradamage.Parent = this.GetChildAtPoint(new Point(duradamage.Location.X, duradamage.Location.Y));
                        duradamage.BackColor = Color.Transparent;
                        duradamage.ForeColor = Color.FromArgb(255, 43, 0);
                        duradamage.AutoSize = true;
                        duradamage.Text = "-" + enemy.enemyBodyDMG.ToString()+ " Durability";
                    }
                }
            }
            int dropValue;
            int droptype;
            int addcoins;
            foreach (int dropkey in drop.dropcords.Keys)
            {
                if (colideRects(drop.dropcords[dropkey], imgS.dropimgs.ElementAt(drop.droptypes[dropkey]).Value, player.cords, player.size))  // collusion Detection between drops and player 
                {
                    colidecounter += 1;                 
                    Label droppickup = new Label();          // creating a label above the player on drop collusion to mark the item gain     
                    player.colides.Add(colidecounter, droppickup);
                    player.labelcounter.Add(colidecounter, 100);
                    Font font = new Font("Cooper Black", 12);
                    droppickup.Font = font;
                    droppickup.Location = new Point(player.cords[0], player.cords[1] - 40);
                    droppickup.BackColor = Color.Transparent;
                    droppickup.ForeColor = Color.FromArgb(9, 255, 0);
                    droppickup.Parent = this.GetChildAtPoint(new Point(droppickup.Location.X, droppickup.Location.Y));
                    keysRemove.Add(dropkey);
                    droptype = drop.droptypes[dropkey];
                    droppickup.AutoSize = true;
                    droppickup.Text = "+" + drop.amounts[droptype].ToString() + drop.dropnames[droptype];
                    if (droptype == drop.dropnames.Length - 2)
                    {

                        dropValue = drop.amounts[droptype];
                        addcoins = random.Next(dropValue, dropValue * 4);
                        player.coins += addcoins;
                        player.inverntory[0] += addcoins;
                        droppickup.Text = "+" + addcoins.ToString()+ " " + drop.dropnames[droptype];
                    }
                    else if (droptype == 0 || droptype == 1)      // adding drops based on values
                    {
                        dropValue = drop.amounts[droptype];
                        player.energy += dropValue;
                        if (player.energy > 100)
                        {
                            player.energy = 100;
                        }
                        energybar.Width = player.energy;
                    } else if (droptype == 2)
                    {
                        dropValue = drop.amounts[droptype];
                        player.fuel += dropValue;
                        if (player.fuel > 100) {
                            player.fuel = 100;
                        }
                        fuelbar.Width = player.fuel;
                    }
                    else if (droptype == 3)
                    {
                        dropValue = drop.amounts[droptype];
                        player.HP += dropValue;
                        if (player.HP > 100) 
                        {
                            player.HP = 100;
                        }
                        HPbar.Width = (player.HP / playerHPRatio);
                    }
                    else if (droptype == 4)
                    {
                        if (bullet.bulletLevel < 9)
                        {
                            bullet.bulletLevel += 1;
                        }
                        else {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                    }
                    else if (droptype == 5) //Laser
                    {
                        bullet.bulletSpeed = 20;
                        bullet.bulletDamage = 20;
                        if (bullet.guntype == 3)
                        {
                            droppickup.Text = drop.dropnames[droptype];
                        }
                        else if (bullet.guntype == 0)
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 0;

                    }
                    else if (droptype == 6) // Bouncing Laser
                    {
                        bullet.bulletSpeed = 20;
                        bullet.bulletDamage = 20;
                        if (bullet.guntype == 0)
                        {
                            droppickup.Text = drop.dropnames[droptype];
                        }
                        else if (bullet.guntype == 3)  
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";

                        } 
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 3;

                    }
                    else if (droptype == 7) //Pulse Gun
                    {
                        bullet.bulletDamage = 25;
                        bullet.bulletSpeed = 15;
                        if (bullet.guntype == 2) // Scattered Pulse Gun
                        {
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            droppickup.Text = drop.dropnames[droptype];
                        }
                        else if (bullet.guntype == 1) //Pulse Gun
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 1;

                    }
                    else if (droptype == 8) // Scattered Pulse Gun
                    {
                        bullet.bulletDamage = 25;
                        bullet.bulletSpeed = 15;
                        if (bullet.guntype == 1) //Pulse Gun
                        {
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            droppickup.Text = drop.dropnames[droptype];
                        }
                        else if (bullet.guntype == 2) // Scattered Pulse Gun
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 2;

                    }
                    else if (droptype == 9) // canon gun
                    {
                        bullet.bulletDamage = 17;
                        bullet.bulletSpeed = 10;
                        if (bullet.guntype == 4) // Scattered Pulse Gun
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 4;
                    }
                    else if (droptype == 10) // missile
                    {
                        bullet.bulletDamage = 17;
                        bullet.bulletSpeed = 15;
                        if (bullet.guntype == 5) // missile
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 5;
                    }
                    else if (droptype == 11) // guided missile
                    {
                        bullet.bulletDamage = 20;
                        bullet.bulletSpeed = 16;
                        if (bullet.guntype == 6) // guided missile
                        {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                            droppickup.Text = "+20 coins";
                        }
                        else
                        {
                            droppickup.Text = drop.dropnames[droptype];
                            bullet.bulletcords.Clear();
                            bullet.bulletypes.Clear();
                            bullet.bulletbounces.Clear();
                        }
                        bullet.guntype = 6;
                    }
                    else {
                        player.inverntory[droptype - 12] += 1;
                    }
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    drop.dropcords.Remove(key);
                    drop.droptypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }
    }
}
