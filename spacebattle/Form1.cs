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
        private playerobj player = new playerobj(15, 100, 100, 10, 5, 80, 200);

        private int dropcounter = 0;
        private int miscounter = 0;
        private int bulletcounter = 0;
        private int enemycounter = 0;
        private int planetcounter = 0;
        private int colidecounter = 0;
        private int main = 0;

        private int[, ] buttoncords = new int[4,2] { { 857, 321 }, { 857, 388 }, { 857, 461 }, { 857, 534 } };
        private int[] buttonindxs = { 2, 4, 4, 4};
        private string[] buttonsnames = { "fullscreen", "controls", "quit", "resume" };
        private List<PictureBox> buttonslist = new List<PictureBox>();
        private int controlbut = 0;
        private int resumebut = 0;

        private static Random random = new Random();
        Bitmap bitmap = new Bitmap(1464, 760);
        private Timer timer = new Timer();
        private int[] direc = { 0, 0, 0, 0 };
        private Boolean keylockSpace = true;
        private Boolean keylockCTRL = true;
        private int enemySpawnCount = 0;
        private int enemySpawnInterval = 20;

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
            playerInitSpeed = player.playerSpeed;
            playerHPRatio = player.HP/100;
            timer.Interval = 10; 
            timer.Tick += new EventHandler(timerEvent);
            timer.Start();
            createmiscObj(20, 0); // creating misc objects at the start
            createmiscObj(100, 1);
            for (int i = 2; i < imgs.miscimgs.Count; i++) {
                createmiscObj(20, i);
            }
            creatEnemyObj(3, 10, 700);
        }

        private void drawGameImg() {
            int index = 0;
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            foreach (int misckey in miscobj.miscords.Keys)
            {
                g.DrawImage(imgs.miscimgs.ElementAt(miscobj.misctypes[misckey]).Key, miscobj.miscords[misckey][0], miscobj.miscords[misckey][1]);
            }
            foreach (int planetkey in planetobj.planetcords.Keys)
            {
                g.DrawImage(imgs.planetimgs.ElementAt(planetobj.planetypes[planetkey]).Key, planetobj.planetcords[planetkey][0], planetobj.planetcords[planetkey][1]);
            }
            if (bulletobj.bulletcords.Count != 0) {
                foreach (int bulletkey in bulletobj.bulletcords.Keys)
                {
                    if (bulletobj.guntype == 3 || bulletobj.guntype == 0)
                    {
                        g.DrawImage(imgs.bulletimgs.ElementAt(bulletobj.bulletypes[bulletkey]).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 1) {
                        g.DrawImage(imgs.pulseimgs.ElementAt(0).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 2)
                    {
                        g.DrawImage(imgs.pulseimgs.ElementAt(bulletobj.bulletypes[bulletkey]).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    } else if (bulletobj.guntype == 4) {
                        g.DrawImage(imgs.canonballimgs.ElementAt(3 - bulletobj.bulletbounces[bulletkey]).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 5)
                    {
                        g.DrawImage(imgs.missileimgs.ElementAt(bulletobj.bulletypes[bulletkey] % 10).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 6)
                    {
                        g.DrawImage(imgs.guidedMissileimgs.ElementAt(bulletobj.bulletypes[bulletkey]).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 7)
                    {
                        if (bulletobj.bulletypes[bulletkey] > 10) { // check if bullet or particle
                            index = bulletobj.bulletypes[bulletkey] - 10;
                        }
                        g.DrawImage(imgs.particleimgs.ElementAt(index).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                        index = 0;
                    }
                    else if (bulletobj.guntype == 8)
                    {
                        g.DrawImage(imgs.deathDroneimgs.ElementAt(bulletobj.bulletypes[bulletkey]).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 9)
                    {
                        g.DrawImage(imgs.replicatingimgs.ElementAt(0).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (bulletobj.guntype == 10)
                    {
                        int imgindx;
                        if (bulletobj.bulletypes[bulletkey] < 10)
                        {
                            imgindx = bulletobj.bulletypes[bulletkey];
                            if (imgindx == 2)
                                imgindx = 1;
                        }
                        else {
                            imgindx = (bulletobj.bulletypes[bulletkey] / 10) + 1;
                        }
                        g.DrawImage(imgs.protonimgs.ElementAt(imgindx).Key, bulletobj.bulletcords[bulletkey][0], bulletobj.bulletcords[bulletkey][1]);
                    }
                }
            }
            if (enemyobj.enemycords.Count != 0) {
                foreach (int enemykey in enemyobj.enemycords.Keys)
                {
                    g.DrawImage(imgs.enemyimgs[(enemyobj.enemyframes[enemykey])], enemyobj.enemycords[enemykey][0], enemyobj.enemycords[enemykey][1]);
                }
            }
            if (dropobj.dropcords.Count != 0) {
                foreach (int dropkey in dropobj.dropcords.Keys)
                {
                    g.DrawImage(imgs.dropimgs.ElementAt(dropobj.droptypes[dropkey]).Key, dropobj.dropcords[dropkey][0], dropobj.dropcords[dropkey][1]);
                }
            }
            if (!keylockCTRL && player.energy > 0)   // draws flame when accelerating speed.
            {
                if (Distance % 3 == 0) {
                    g.DrawImage(imgs.blastimgs[0], player.cords[0]+ 78, player.cords[1] + 27);
                } else if (Distance % 2 == 0) {
                    g.DrawImage(imgs.blastimgs[1], player.cords[0] + 78, player.cords[1] + 27);
                }
                else if (Distance % 1 == 0)
                {
                    g.DrawImage(imgs.blastimgs[2], player.cords[0] + 78, player.cords[1] + 27);
                }
            }
            g.DrawImage(imgs.playerimg, player.cords[0], player.cords[1]);
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "map";
            //mainbox.BringToFront();
        }
        private void drawSettingsImg()
        {
            if (player.colides.Count != 0) {
                foreach (Label labe in player.colides.Values) {
                    labe.Parent.Controls.Remove(labe);
                }
                player.colides.Clear();
                player.labelcounter.Clear();
            }
            for (int i = 0; i < buttonindxs.Length; i++)
            {
                PictureBox button = new PictureBox();
                button.BackColor = Color.Transparent;
                button.Image = imgs.settingsimgs[buttonindxs[i]];
                button.Location = new Point(buttoncords[i, 0], buttoncords[i, 1]);
                button.Parent = this.GetChildAtPoint(button.Location);
                button.Name = buttonsnames[i];
                button.BringToFront();
                buttonslist.Add(button);
                button.Click += new EventHandler(Button_Click);
            }
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(imgs.settingsimgs[0], 550, 125);  // print menu
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "settings";
        }

        private void drawControlsImg()
        {
            removeMenuButtons();
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(imgs.settingsimgs[1], 550, 125);  // print controls
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "controls";
        }

        private void Button_Click(object sender, EventArgs e)
        {
               var picBox = (PictureBox)sender;
            if (picBox.Name == "fullscreen")
            {
                if (picBox.Image == imgs.settingsimgs[2])
                {
                    picBox.Image = imgs.settingsimgs[3];
                }
                else
                {
                    picBox.Image = imgs.settingsimgs[2];
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
        }

        private void removeMenuButtons() {
            foreach (PictureBox buts in buttonslist)
            {
                buts.Click -= Button_Click;
                buts.Parent.Controls.Remove(buts);
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
            if (e.KeyCode == Keys.Escape) {
                if (main == 0)
                {
                    main = 1;
                    menubox.Image = imgs.menuimgs[1];
                }
                else if (main == 2) {   
                    if (controlbut == 2) // if on menu draw settings back
                    {
                        controlbut = 0;
                        drawSettingsImg();
                    }
                    else   // remove menu
                    {
                        removeMenuButtons();
                        main = 0;
                        controlbut = 0;
                        resumebut = 0;
                        menubox.Image = imgs.menuimgs[0];
                    }
                }
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
            if (main == 0) // if esc or menu button is not pressed
            {
                drawGameImg();
                Distance += 1;
                coinsLabel.Text = player.coins.ToString();
                collusionDetection();
                player.movePlayer(direc);
                createBullet(); 
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
                    else
                    {
                        keylockCTRL = true;
                        player.playerSpeed = playerInitSpeed;
                    }
                }

                if (bulletobj.bulletcords.Count != 0)
                {
                    if (bulletobj.guntype == 3 || bulletobj.guntype == 0)
                    {
                        bulletobj.moveBullet();  //move bullets  
                    }
                    else if (bulletobj.guntype == 1 || bulletobj.guntype == 2)
                    {
                        bulletobj.movePulse();
                    }
                    else if (bulletobj.guntype == 4)
                    {
                        bulletobj.moveCanonBall(true);
                    }
                    else if (bulletobj.guntype == 5)
                    {
                        bulletobj.moveMissile();
                    }
                    else if (bulletobj.guntype == 6)
                    {
                        bulletobj.moveGuidedMissile(enemyobj.enemycords, enemyobj.enemysize, imgs.guidedMissileimgs.ElementAt(0).Value, enemyobj.enemyframes);
                    }
                    else if (bulletobj.guntype == 7)
                    {
                        bulletobj.moveParticle();
                    }
                    else if (bulletobj.guntype == 8)
                    {
                        bulletobj.moveDeathDrone();
                    }
                    else if (bulletobj.guntype == 9)
                    {
                        if (Distance % 100 == 0)
                        {  // replicate every 100 intervals
                            Dictionary<int, int[]> repbullets = new Dictionary<int, int[]>(bulletobj.bulletcords);
                            foreach (int bulletkey in repbullets.Keys)
                            {
                                bulletobj.createDeathDrone(repbullets[bulletkey][0], repbullets[bulletkey][1], bulletcounter, 0, 2, random);
                                bulletcounter += 2;
                                bulletobj.bulletcords.Remove(bulletkey);
                                bulletobj.bulletypes.Remove(bulletkey);
                            }
                        }
                        bulletobj.moveDeathDrone();
                    }
                    else if (bulletobj.guntype == 10)
                    {
                        bulletobj.moveCanonBall(false);
                    }
                }
                modifyMiscObj();           //misc movement typeofobj = 0 : flying dust, typeofobj = 1 : star
                modifyEnemyObj();
                modifyPlanetObj();
                modifyDropObj();
                if (enemySpawnCount == enemySpawnInterval)
                {                  // spawn another emeny every 1 second (100 * timerTick) approx
                    creatEnemyObj(1, 1, 1);
                    enemySpawnCount = -1;
                }
                if (Distance % 500 == 0)
                {              //every 110 distace units move pointer by one pixel
                    distancepointer.Location = new Point(distancepointer.Location.X + 1, distancepointer.Location.Y);
                    /*if (enemySpawnInterval > 1) {
                        enemySpawnInterval -= 1;
                    }*/
                }
                if (Distance % 2200 == 0)
                {
                    enemySpawnInterval -= 1;
                    createplanetObj(0, imgs.planetimgs.ElementAt(0).Value.Width, 50);      // spawns planets on different intervals
                }
                if (Distance % 1800 == 0)
                {
                    createplanetObj(1, imgs.planetimgs.ElementAt(1).Value.Width, 200);
                }
                if (Distance % 2500 == 0)
                {
                    createplanetObj(2, imgs.planetimgs.ElementAt(2).Value.Width, 500);
                }
                if (Distance % 3500 == 0)
                {
                    createplanetObj(3, imgs.planetimgs.ElementAt(3).Value.Width, 300);
                }
                if (Distance % player.fuelCons == 0)       //every 1100 distace units decrease fuel amount
                {
                    player.fuel -= 1;
                    fuelbar.Width = player.fuel;
                }
                if (player.labelcounter.Count != 0)
                {        //each label that created has a countdown until it removed
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
                            if (player.colides[key].Parent != null)
                            {
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
            else {         // if esc or menu button is not pressed drawing setting and buttons
                if (main == 1) {       
                    drawSettingsImg();
                    main = 2;
                }
                if (controlbut == 1) // drawing controls
                {
                    drawControlsImg();
                    controlbut = 2;
                }
                if (resumebut == 1) // removing settings
                {
                    removeMenuButtons();
                    main = 0;
                    controlbut = 0;
                    resumebut = 0;
                    menubox.Image = imgs.menuimgs[0];
                }
            }
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
                miscobj.createMisc(random.Next(1, 1400), random.Next(1, 790), random.Next(startSpeed, startSpeed * 2), objtype, miscounter);
                i += 1;
            }
        }

        private void modifyMiscObj()
        {
            int startSpeed;
            int objtype;
            int renderMisc;
            if (miscobj.miscords.Count != 0)
            {
                renderMisc = -1;
                renderMisc = miscobj.moveMisc();   
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
                    miscobj.createMisc(1, random.Next(50, 700), random.Next(startSpeed, startSpeed*2), objtype, miscounter);    //creating stardust/star each time one is removed
                }
            }
        }

        private void createplanetObj(int objtype, int xcord, int ycord)
        {
            planetcounter += 1;
            planetobj.createplanet(-xcord, ycord, objtype, planetcounter);
        }

        private void modifyPlanetObj()
        {
            if (planetobj.planetcords.Count != 0)
            {
                planetobj.moveplanet();
            }
        }

        private void creatEnemyObj(int amount, int xStart, int xEnd)
        {
            int i = 0;
            while (i < amount)
            {
                enemycounter += 1;
                enemyobj.createEnemy(random.Next(xStart, xEnd), random.Next(1, 750), enemycounter);
                i += 1;
            }
        }

        private void modifyEnemyObj()
        {
            if (enemyobj.enemycords.Count != 0)
            {
                enemyobj.moveEnemy();
            }
        }

        private void creatDropObj(int[] cords)
        {
            dropcounter += 1;
            int droptype = dropobj.calcDrop(random);
            dropobj.createDrop(cords[0], cords[1], droptype, dropcounter);
        }

        private void modifyDropObj()
        {
            if (dropobj.dropcords.Count != 0)
            {
                dropobj.moveDrop();
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
            double damage, critDamage;
            Boolean isCrit;
            int result;
            foreach (int enemykey in enemyobj.enemycords.Keys)
            {
                if (enemyobj.enemyframes[enemykey] != 3)                  //ignore dead enemies on screen
                {
                    foreach (int bulletkey in bulletobj.bulletcords.Keys)
                    {
                        isCrit = false;
                        damage = bulletobj.bulletDamage;
                        if (bulletobj.guntype == 7 && bulletobj.bulletypes[bulletkey] > 10) // devide damage by 2 if a particle
                        {
                            damage /= 2;
                        } else if (bulletobj.guntype == 10 && bulletobj.bulletypes[bulletkey] < 10) {// devide damage by 5 if a proton
                            damage /= 5;
                        }
                        result = random.Next(0,10);                              // calc chance to crit
                        if (result >= 0 && result < (player.critChance/10)) {
                            critDamage = (player.critDamage - 100) / 100;        // add % damage to bullet damage
                            damage *= (1.0 + critDamage);
                            isCrit = true;
                        }
                        if (colideRects(enemyobj.enemycords[enemykey], enemyobj.enemysize, bulletobj.bulletcords[bulletkey], imgs.pulseimgs.ElementAt(0).Value))    // collusion Detection between bullets and enemies
                        {
                            enemyobj.enemyhps[enemykey] = enemyobj.enemyhps[enemykey] - (int)damage;
                            if (bulletobj.guntype != 4)
                            {
                                keysRemove.Add(bulletkey);
                            }
                            else {
                                bulletobj.bulletbounces[bulletkey] -= 1;
                                if (bulletobj.bulletbounces[bulletkey] == 0) {
                                    keysRemove.Add(bulletkey);
                                }
                            }
                            if (enemyobj.enemyhps[enemykey] < 0)            //choose img for enemy based on condition
                            {
                                enemyobj.setEnemyFrame(3, enemykey);
                                creatDropObj(enemyobj.enemycords[enemykey]);
                                break;
                            }
                            else
                            {
                                if (isCrit)               // set enemy crit damaged img
                                {
                                    enemyobj.setEnemyFrame(2, enemykey);
                                }
                                else                     // set enemy damaged img
                                {
                                    enemyobj.setEnemyFrame(1, enemykey);
                                }
                            }
                        }

                    }
                    if (keysRemove.Count != 0)
                    {
                        foreach (int key in keysRemove)
                        {
                            if (bulletobj.guntype == 7 && bulletobj.bulletypes[key] < 10)  // add particle before removing bullet
                            {
                                bulletcounter = bulletobj.createParticle(bulletobj.bulletcords[key][0], bulletobj.bulletcords[key][1], bulletcounter, 0, false);
                            } else if (bulletobj.guntype == 10 && bulletobj.bulletypes[key] < 10) {
                                bulletobj.createDeathDrone(bulletobj.bulletcords[key][0], bulletobj.bulletcords[key][1], bulletcounter, 10, 5, random);// proton particle type
                                bulletcounter += 5;
                            }
                            bulletobj.bulletcords.Remove(key);
                            bulletobj.bulletypes.Remove(key);
                            if (bulletobj.bulletbounces.Count != 0) {
                                bulletobj.bulletbounces.Remove(key);
                            }
                        }
                        keysRemove.Clear();
                    }
                    if (colideRects(enemyobj.enemycords[enemykey], enemyobj.enemysize, player.cords, player.size))  // collusion Detection between enemy and player
                    {
                        player.HP -= enemyobj.enemyBodyDMG;
                        HPbar.Width = (player.HP / playerHPRatio);
                        durlab.Text = player.HP.ToString() + "/100";
                        enemyobj.setEnemyFrame(3, enemykey);
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
                        duradamage.Text = "-" + enemyobj.enemyBodyDMG.ToString()+ " Durability";
                    }
                }
            }
            int dropValue;
            int droptype;
            int addcoins;
            int dropstats = 0;
            foreach (int dropkey in dropobj.dropcords.Keys)
            {
                if (colideRects(dropobj.dropcords[dropkey], imgs.dropimgs.ElementAt(dropobj.droptypes[dropkey]).Value, player.cords, player.size))  // collusion Detection between drops and player 
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
                    droptype = dropobj.droptypes[dropkey];
                    droppickup.AutoSize = true;
                    if (droptype == 16 || droptype == 17) {
                        droppickup.Text = "+10%" + dropobj.dropnames[droptype];
                    } else {
                        droppickup.Text = "+" + dropobj.amounts[droptype].ToString() + dropobj.dropnames[droptype];
                    }
                    if (droptype == dropobj.dropnames.Length - 2)
                    {

                        dropValue = dropobj.amounts[droptype];
                        addcoins = random.Next(dropValue, dropValue * 4);
                        player.coins += addcoins;
                        player.inverntory[0] += addcoins;
                        droppickup.Text = "+" + addcoins.ToString()+ " " + dropobj.dropnames[droptype];
                    }
                    else if (droptype == 0 || droptype == 1)      // adding drops based on values
                    {
                        dropValue = dropobj.amounts[droptype];
                        player.energy += dropValue;
                        if (player.energy > 100)
                        {
                            player.energy = 100;
                        }
                        energybar.Width = player.energy;
                    } else if (droptype == 2)
                    {
                        dropValue = dropobj.amounts[droptype];
                        player.fuel += dropValue;
                        if (player.fuel > 100) {
                            player.fuel = 100;
                        }
                        fuelbar.Width = player.fuel;
                    }
                    else if (droptype == 3)
                    {
                        dropValue = dropobj.amounts[droptype];
                        player.HP += dropValue;
                        if (player.HP > 100) 
                        {
                            player.HP = 100;
                        }
                        HPbar.Width = (player.HP / playerHPRatio);
                        durlab.Text = player.HP.ToString() + "/100";
                    }
                    else if (droptype == 4)
                    {
                        if (bulletobj.bulletLevel < 9)
                        {
                            bulletobj.bulletLevel += 1;
                            currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();
                        }
                        else {
                            player.coins += 10;
                            player.inverntory[0] += 10;
                            droppickup.Text = "+10 coins";
                        }
                    }
                    else if (droptype == 5) //Laser
                    {
                        bulletobj.bulletSpeed = 20;
                        bulletobj.bulletDamage = 20;
                        if (bulletobj.guntype == 3)
                        {
                            droppickup.Text = dropobj.dropnames[droptype];
                        }
                        else if (bulletobj.guntype == 0)
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 0;
                        weaponbox.Image = imgs.dropimgs.ElementAt(5).Key;
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();
                    }
                    else if (droptype == 6) // Bouncing Laser
                    {
                        bulletobj.bulletSpeed = 20;
                        bulletobj.bulletDamage = 20;
                        if (bulletobj.guntype == 0)
                        {
                            droppickup.Text = dropobj.dropnames[droptype];
                        }
                        else if (bulletobj.guntype == 3)  
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 3;
                        weaponbox.Image = imgs.dropimgs.ElementAt(6).Key;
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 7) //Pulse Gun
                    {
                        bulletobj.bulletDamage = 25;
                        bulletobj.bulletSpeed = 15;
                        if (bulletobj.guntype == 2) // Scattered Pulse Gun
                        {
                            bulletobj.bulletcords.Clear();
                            bulletobj.bulletypes.Clear();
                            droppickup.Text = dropobj.dropnames[droptype];
                        }
                        else if (bulletobj.guntype == 1) //Pulse Gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 1;
                        weaponbox.Image = imgs.dropimgs.ElementAt(7).Key;
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 8) // Scattered Pulse Gun
                    {
                        bulletobj.bulletDamage = 25;
                        bulletobj.bulletSpeed = 18;
                        if (bulletobj.guntype == 1) //Pulse Gun
                        {
                            bulletobj.bulletcords.Clear();
                            bulletobj.bulletypes.Clear();
                            droppickup.Text = dropobj.dropnames[droptype];
                        }
                        else if (bulletobj.guntype == 2) // Scattered Pulse Gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 2;
                        weaponbox.Image = imgs.dropimgs.ElementAt(8).Key;
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 9) // canon gun
                    {
                        bulletobj.bulletDamage = 17;
                        bulletobj.bulletSpeed = 10;
                        if (bulletobj.guntype == 4) // Scattered Pulse Gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 4;
                        weaponbox.Image = imgs.dropimgs.ElementAt(9).Key;
                    }
                    else if (droptype == 10) // missile
                    {
                        bulletobj.bulletDamage = 17;
                        bulletobj.bulletSpeed = 15;
                        if (bulletobj.guntype == 5) // missile
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 5;
                        weaponbox.Image = imgs.dropimgs.ElementAt(10).Key;
                    }
                    else if (droptype == 11) // guided missile
                    {
                        bulletobj.bulletDamage = 20;
                        bulletobj.bulletSpeed = 16;
                        if (bulletobj.guntype == 6) // guided missile
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 6;
                        weaponbox.Image = imgs.dropimgs.ElementAt(11).Key;
                    }
                    else if (droptype == 12) // particle gun
                    {
                        bulletobj.bulletDamage = 10;
                        bulletobj.bulletSpeed = 14;
                        if (bulletobj.guntype == 7) // particle gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 7;
                        weaponbox.Image = imgs.dropimgs.ElementAt(12).Key;
                    }
                    else if (droptype == 13) // death drone
                    {
                        bulletobj.bulletDamage = 25;
                        bulletobj.bulletSpeed = 17;
                        if (bulletobj.guntype == 8) // death drone
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 8;
                        weaponbox.Image = imgs.dropimgs.ElementAt(13).Key;
                    }
                    else if (droptype == 14) // replicating bullet gun
                    {
                        bulletobj.bulletDamage = 14;
                        bulletobj.bulletSpeed = 13;
                        if (bulletobj.guntype == 9) // replicating bullet gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 9;
                        weaponbox.Image = imgs.dropimgs.ElementAt(14).Key;
                    }
                    else if (droptype == 15) // proton gun
                    {
                        bulletobj.bulletDamage = 15;
                        bulletobj.bulletSpeed = 15;
                        if (bulletobj.guntype == 10) // proton gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                        }
                        bulletobj.guntype = 10;
                        weaponbox.Image = imgs.dropimgs.ElementAt(15).Key;
                    }
                    else if (droptype == 16) // crit.chance
                    {
                        if (player.critChance != player.maxCritChance)
                        {
                            player.critChance += 10;
                            critchanlab.Text = player.critChance.ToString() + "%";
                            if (player.critChance == player.maxCritChance)
                            {
                                critchanlab.ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else {
                            player.coins += 10;
                            player.inverntory[0] += 10;
                            droppickup.Text = "+10 coins";
                        }
                    }
                    else if (droptype == 17) // crit.damage
                    {
                        if (player.critDamage != player.maxCritDamage)
                        {
                            player.critDamage += 10;
                            critdmglab.Text = player.critDamage.ToString() + "%";
                            if (player.critDamage == player.maxCritDamage)
                            {
                                critdmglab.ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            player.coins += 10;
                            player.inverntory[0] += 10;
                            droppickup.Text = "+10 coins";
                        }
                    }
                    else if (droptype == 18) // fire rate 
                    {
                        if (player.fireRate != player.maxFireRate)
                        {
                            player.fireRate += 1;
                            fireratelab.Text = player.fireRate.ToString();
                            if (player.fireRate == player.maxFireRate) {
                                fireratelab.ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            player.coins += 10;
                            player.inverntory[0] += 10;
                            droppickup.Text = "+10 coins";
                        }
                    }
                    else {
                        player.inverntory[droptype - (dropobj.amounts.Count()-2)] += 1;
                    }
                    if (dropstats == 1)
                    {
                        player.coins += 10;
                        player.inverntory[0] += 10;
                        droppickup.Text = "+10 coins";
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();
                    }
                    else if (dropstats == 2) {
                        droppickup.Text = dropobj.dropnames[droptype];
                        bulletobj.bulletcords.Clear();
                        bulletobj.bulletypes.Clear();
                        bulletobj.bulletbounces.Clear();
                        currweaplabel.Text = "level " + bulletobj.bulletLevel.ToString();
                    }
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    dropobj.dropcords.Remove(key);
                    dropobj.droptypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        private void menuMouseHov(object sender, EventArgs e)
        {
            if (main == 0) {
                menubox.Image = imgs.menuimgs[1];
            }
        }

        private void menuMouseLeave(object sender, EventArgs e)
        {
            if (main == 0) {
                menubox.Image = imgs.menuimgs[0];
            }
        }

        private void menuButtonClick(object sender, EventArgs e) 
        {
            menubox.Image = imgs.menuimgs[1];
            if (main == 2)  //return to the game from any sub menus
            {
                removeMenuButtons();
                main = 0;
                controlbut = 0;
                resumebut = 0;
            }
            else
            {
                main = 1;
            }
        }
        private void createBullet() {
            if (!keylockSpace)  // only when spacebar is pressed generating bullets in constant fire rate.
            {
                if (bulletCounter >= (100 / player.fireRate))
                {
                    int bulletguntype = bulletobj.guntype;
                    int xcord = 0;
                    int ycord = 0;
                    int type = 0;
                    if (bulletguntype == 0 || bulletguntype == 3)
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            int[] playercords = player.cords;
                            if (bulletobj.bulletLevel == 1)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + 17;
                                type = 0;
                            }
                            else if (bulletobj.bulletLevel == 2)
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
                            else if (bulletobj.bulletLevel == 3)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + (-3 + (i * 20));
                                type = 0;
                            }
                            else if (bulletobj.bulletLevel == 4)
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
                            else if (bulletobj.bulletLevel == 5)
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
                            else if (bulletobj.bulletLevel == 6)
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
                            else if (bulletobj.bulletLevel == 7)
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
                            else if (bulletobj.bulletLevel == 8)
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
                            else if (bulletobj.bulletLevel == 9)
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
                            bulletobj.createBullet(xcord, ycord, type, bulletcounter);

                        }

                    }
                    else if (bulletguntype == 1)  //create pulse type bullet
                    {
                        xcord = player.cords[0] - 30;
                        ycord = player.cords[1] - 8;
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 3 || i == 6)
                            {     // 4th and 7th pulse will start more indented
                                xcord -= 45;
                                ycord = player.cords[1] - 8;
                            }
                            else if (i == 1 || i == 4 || i == 7)
                            {    // 2th and 5th and 8th pulse start higher
                                ycord = player.cords[1] - 78;
                            }
                            else if (i == 2 || i == 5 || i == 8)    // 3th and 6th and 9th pulse start lower
                            {
                                ycord = player.cords[1] + 62;
                            }
                            bulletobj.createPulse(xcord, ycord, bulletcounter, 0);
                        }
                    }
                    else if (bulletguntype == 2)  //create pulse type bullet
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
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
                            bulletobj.createPulse(xcord, ycord, bulletcounter, type);
                        }
                    }
                    else if (bulletguntype == 4)  //create canon ball type bullet
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
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
                            bulletobj.createCanonBall(xcord, ycord, bulletcounter, type);
                        }

                    }
                    else if (bulletguntype == 5)  //create missile type bullet
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
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
                            bulletobj.createMissile(xcord, ycord, bulletcounter, type);
                        }

                    }
                    else if (bulletguntype == 6)  //create guided missile type bullet
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
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
                            bulletobj.createGuidedMissile(xcord, ycord, bulletcounter, type);
                        }

                    }
                    else if (bulletguntype == 7)  //create particle type
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
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
                            bulletcounter = bulletobj.createParticle(xcord, ycord, bulletcounter, type, true);
                        }

                    }
                    else if (bulletguntype == 8)  //create death drone
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                            }
                            bulletobj.createDeathDrone(xcord, ycord, bulletcounter, 0, 4, random);
                            bulletcounter += 4;
                        }

                    }
                    else if (bulletguntype == 9)  //create replicating bullet
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            bulletobj.createDeathDrone(xcord, ycord, bulletcounter, type, 1, random);
                            bulletcounter += 1;
                        }

                    }
                    else if (bulletguntype == 10)  //create poronwave
                    {
                        for (int i = 0; i < bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 10;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 2)
                            {
                                xcord = player.cords[0] + 54;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] + 40;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = player.cords[0] + 94;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = player.cords[0] - 30;
                                ycord = player.cords[1] - 20;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = player.cords[0] + 14;
                                ycord = player.cords[1] + 54;
                                type = 3;
                            }
                            bulletobj.createDeathDrone(xcord, ycord, bulletcounter, type, 1, random); // proton bullet type
                            bulletcounter += 1;
                        }
                    }
                    bulletCounter = 0;
                }
            }
        }
    }
}
