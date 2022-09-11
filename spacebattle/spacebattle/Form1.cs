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
        private playerobj player = new playerobj();
        private bulletobj bullet = new bulletobj();
        private List<dropobj> droplist = new List<dropobj>();
        private List<bulletobj> bulletlist = new List<bulletobj>();
        private List<miscobj> misclist = new List<miscobj>();
        private List<enemyobj> enemylist = new List<enemyobj>();
        private Timer timer = new Timer();
        private int[] direc = { 0, 0, 0, 0 };
        private int enemySpawnCount = 0;
        private Boolean keylockSpace = true;
        private Boolean keylockCTRL = true;

        private int Distance = 0; /// 18000 intervals in 5 mins
        private int bulletCounter = 0;
        private int playerInitSpeed = 0;
        private int playerHPRatio = 0;

        public game()
        {
            InitializeComponent();
        }

        private void Game_Start(object sender, EventArgs e)
        {
            
            player.createPlayer(this, 1000, 400); // creating player
            playerInitSpeed = player.playerSpeed;
            playerHPRatio = player.HP/100;
            timer.Interval = 10; 
            timer.Tick += new EventHandler(timerEvent);
            timer.Start();
            createmiscObj(10, 0); // creating misc objects at the start
            createmiscObj(20, 1);
            creatEnemyObj(3, 10, 700);
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
            Distance += 1;   
            coinsLabel.Text = player.coins.ToString();
            collusionDetection();
            player.movePlayer(direc);
            if (!keylockSpace)  // only when spacebar is pressed generating bullets in constant fire rate.
            {
                if (bulletCounter >= (100 / player.fireRate))
                {
                    for (int i = 0; i < bullet.bulletLevel; i++)
                    {
                        bulletobj bullet1 = new bulletobj();
                        bulletlist.Add(bullet1);
                        int[] playercords = player.playerCords;
                        if (bullet.bulletLevel == 1) {
                            bullet1.createBullet(this, playercords[0] - 35, playercords[1] + 17, 0);
                        }
                        else if (bullet.bulletLevel == 2)
                        {
                            if (i == 0)
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + 7, 0);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + 27, 0);
                            }
                        }
                        else if (bullet.bulletLevel == 3) {
                            bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-3 + (i * 20)), 0);
                        }
                        else if (bullet.bulletLevel == 4)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);
                                
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        else if (bullet.bulletLevel == 5)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);

                            } else if (i == 4) {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] + 67, 2);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        else if (bullet.bulletLevel == 6)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);

                            }
                            else if (i == 4)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] + 67, 2);
                            }
                            else if (i == 5)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] - 43, 1);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        else if (bullet.bulletLevel == 7)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);

                            }
                            else if (i == 4)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] + 67, 2);
                            }
                            else if (i == 5)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] - 43, 1);
                            }
                            else if (i == 6)
                            {
                                bullet1.createBullet(this, playercords[0] + 5 , playercords[1] + 67, 2);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        else if (bullet.bulletLevel == 8)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);

                            }
                            else if (i == 4)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] + 67, 2);
                            }
                            else if (i == 5)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] - 43, 1);
                            }
                            else if (i == 6)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] + 67, 2);
                            }
                            else if (i == 7)
                            {
                                bullet1.createBullet(this, playercords[0] + 40, playercords[1] - 43, 1);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        else if (bullet.bulletLevel == 9)
                        {
                            if (i == 3)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] - 43, 1);

                            }
                            else if (i == 4)
                            {
                                bullet1.createBullet(this, playercords[0] - 30, playercords[1] + 67, 2);
                            }
                            else if (i == 5)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] - 43, 1);
                            }
                            else if (i == 6)
                            {
                                bullet1.createBullet(this, playercords[0] + 5, playercords[1] + 67, 2);
                            }
                            else if (i == 7)
                            {
                                bullet1.createBullet(this, playercords[0] + 40, playercords[1] - 43, 1);
                            }
                            else if (i == 8)
                            {
                                bullet1.createBullet(this, playercords[0] + 40, playercords[1] + 67, 2);
                            }
                            else
                            {
                                bullet1.createBullet(this, playercords[0] - 35, playercords[1] + (-13 + (i * 20)), 0);
                            }

                        }
                        bulletCounter = 0;
                    }
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

            if (bulletlist.Count != 0)
            {
                Boolean renderBullet = false;
                foreach (bulletobj bullet in bulletlist)
                {
                    renderBullet = false;
                    renderBullet = bullet.moveBullet();    
                    if (!renderBullet)
                    {
                        bullet.destroyBullet(this);           //removing projectile that went off the frame
                        bulletlist.Remove(bullet);
                        break;
                    }
                }
            }
            modifyMiscObj();           //misc movement typeofobj = 0 : flying dust, typeofobj = 1 : star
            modifyEnemyObj();
            modifyDropObj();
            if (enemySpawnCount == 50) {                  // spawn another emeny every 1 second (100 * timerTick)
                creatEnemyObj(1, 1, 1);
                enemySpawnCount = -1;
            }
            if (Distance % 1800 == 0) {              //every 110 distace units move pointer by one pixel
                distancepointer.Location = new Point(distancepointer.Location.X+1, distancepointer.Location.Y);
            }
            if (Distance % 18000 == 0)       //every 1100 distace units decrease fuel amount
            {
                player.fuel -= player.fuelCons;
                fuelbar.Width = player.fuel;
            }
            enemySpawnCount += 1;
            bulletCounter += 1;
        }

        private void createmiscObj(int amount, int objtype) {
            int i = 0;
            int startSpeed;
            Random randint = new Random();
            if (objtype == 0) {
                startSpeed = 15;
            } else {
                startSpeed = 1;
            }
            while (i < amount)
            {
                miscobj misc = new miscobj();
                misc.createMisc(this, randint.Next(1, 1400), randint.Next(1, 750), randint.Next(startSpeed, startSpeed*2), objtype);
                misclist.Add(misc);
                i += 1;
            }
        }

        private void modifyMiscObj()
        {
            int startSpeed;
            int objtype;
            Boolean renderMisc;
            Random randint = new Random();
            if (misclist.Count != 0)
            {

                foreach (miscobj misc in misclist)
                {
                    renderMisc = false;
                    renderMisc = misc.moveMisc();   
                    if (!renderMisc)
                    {
                        objtype = misc.miscType;
                        if (objtype == 0)
                        {
                            startSpeed = 15;
                        }
                        else
                        {
                            startSpeed = 1;
                        }
                        misc.destroyMisc(this);
                        misclist.Remove(misc);            //removing stardust/star that went off the frame
                        miscobj misc1 = new miscobj();
                        misc1.createMisc(this, 1, randint.Next(50, 700), randint.Next(startSpeed, startSpeed*2), objtype);    //creating stardust/star each time one is removed
                        misclist.Add(misc1);
                        break;
                    }
                }
            }
        }

        private void creatEnemyObj(int amount, int xStart, int xEnd)
        {
            int i = 0;
            Random randint = new Random();
            while (i < amount)
            {
                enemyobj enemy = new enemyobj();
                enemy.createEnemy(this, randint.Next(xStart, xEnd), randint.Next(10, 750));
                enemylist.Add(enemy);
                i += 1;
            }
        }

        private void modifyEnemyObj()
        {
            Boolean renderMisc;
            if (enemylist.Count != 0)
            {
                foreach (enemyobj enemy in enemylist)
                {
                    
                    renderMisc = false;
                    renderMisc = enemy.moveEnemy();
                    if (!renderMisc)
                    {
                        enemy.destroyEnemy(this);
                        enemylist.Remove(enemy);       
                        break;
                    }
                }
            }
        }

        private void creatDropObj(int[] cords)
        {
            dropobj drop = new dropobj();
            drop.createDrop(this, cords[0], cords[1]);
            droplist.Add(drop);
        }

        private void modifyDropObj()
        {
            Boolean renderMisc;
            if (droplist.Count != 0)
            {
                foreach (dropobj drop in droplist)
                {

                    renderMisc = false;
                    renderMisc = drop.moveDrop();
                    if (!renderMisc)
                    {
                        drop.destroyDrop(this);
                        droplist.Remove(drop);
                        break;
                    }
                }
            }
        }

        private void collusionDetection()
        {
            foreach (enemyobj enemy in enemylist)
            {
                if (enemy.enemyFrame != 2)                  //ignore dead enemies on screen
                {
                    foreach (bulletobj bullet in bulletlist)
                    {
                        if (enemy.enemybox.Bounds.IntersectsWith(bullet.bulletbox.Bounds))    // collusion Detection between bullets and enemies
                        {
                            enemy.Hp = enemy.Hp - bullet.bulletDamage;
                            bullet.destroyBullet(this);
                            bulletlist.Remove(bullet);
                            if (enemy.Hp < 0)            //choose img for enemy based on condition
                            {
                                enemy.setEnemyFrame(2);
                                creatDropObj(enemy.enemyCords);
                            }
                            else
                            {
                                enemy.setEnemyFrame(1);
                            }
                            break;
                        }
                    }
                    if (enemy.enemybox.Bounds.IntersectsWith(player.playerbox.Bounds)) {  // collusion Detection between enemy and player
                        player.HP -= enemy.enemyBodyDMG;
                        HPbar.Width = (player.HP / playerHPRatio);
                        enemy.setEnemyFrame(2);
                        break;
                    }
                }
            }
            int dropValue;
            int droptype;
            int addcoins;
            Random randint = new Random();
            foreach (dropobj drop in droplist)
            {
                if (drop.dropbox.Bounds.IntersectsWith(player.playerbox.Bounds))  // collusion Detection between drops and player 
                {
                    droptype = drop.dropType;
                    if (droptype == 5)
                    {
                        dropValue = drop.amount;
                        addcoins = randint.Next(dropValue, dropValue * 4);
                        player.coins += addcoins;
                        player.inverntory[0] += addcoins;
                    }
                    else if (droptype == 0 || droptype == 1)
                    {
                        dropValue = drop.amount;
                        player.energy += dropValue;
                        if (player.energy > 100)
                        {
                            player.energy = 100;
                        }
                        energybar.Width = player.energy;
                    } else if (droptype == 2)
                    {
                        dropValue = drop.amount;
                        player.fuel += dropValue;
                        if (player.fuel > 100) {
                            player.fuel = 100;
                        }
                        fuelbar.Width = player.fuel;
                    }
                    else if (droptype == 3)
                    {
                        dropValue = drop.amount;
                        player.HP += dropValue;
                        if (player.HP > 100) 
                        {
                            player.HP = 100;
                        }
                        HPbar.Width = (player.HP / playerHPRatio);
                    }
                    else if (droptype == 4)
                    {
                        if (bullet.bulletLevel < 7)
                        {
                            bullet.bulletLevel += 1;
                        }
                        else {
                            player.coins += 20;
                            player.inverntory[0] += 20;
                        }
                    }
                    else {
                        player.inverntory[droptype - 5] += 1;
                    }
                    drop.destroyDrop(this);
                    droplist.Remove(drop);
                    break;
                }
            }
        }
    }
}
