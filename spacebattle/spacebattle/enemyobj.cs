using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    class enemyobj
    {
        public PictureBox enemybox = new PictureBox();
        public int[] enemyCords = new int[2];
        private int dmgImgCount = -1;
        private Image[] enemyImgs = { 
            Image.FromFile(@"spacebattle\enemy.png"), 
            Image.FromFile(@"spacebattle\enemydmg.png"), 
            Image.FromFile(@"enemydead.png") 
        };

        public int enemyFrame = 0;

        public int enemySpeed = 5;
        public int enemyBodyDMG = 20;
        public int Hp = 50;


        public void createEnemy(Form form, int cordx, int cordy)
        {
            enemyCords[0] = cordx;
            enemyCords[1] = cordy;
            enemybox.Image = enemyImgs[enemyFrame];
            enemybox.Size = new Size(50, 35);
            enemybox.BackColor = Color.Transparent;
            enemybox.Location = new Point(cordx, cordy);
            form.Controls.Add(enemybox);
            enemybox.BringToFront();

        }

        public Boolean moveEnemy()
        {

            if (enemyFrame == 2) {
                enemyCords[1] = enemyCords[1] + (enemySpeed*2);
            } else {
                enemyCords[0] = enemyCords[0] + enemySpeed;
            }
            if (enemyCords[0] > 1390 || enemyCords[1] > 710)               //frame boundries removes enemy
            {
                return false;
            }
            enemybox.Location = new Point(enemyCords[0], enemyCords[1]);
            if (dmgImgCount >= 0) {
                dmgImgCount += 1;
            }
            if (dmgImgCount == 7) {
                setEnemyFrame(0);
                dmgImgCount = -1;
            }
            return true;
        }

        public void setEnemyFrame(int frame)
        {
            if (frame != 2)
            {
                enemyFrame = frame;
                enemybox.Image = enemyImgs[frame];
                if (frame == 1)
                {
                    dmgImgCount = 0;
                }
            }
            else {
                enemyFrame = frame;
                enemybox.Image = enemyImgs[frame];
                dmgImgCount = -1;
            }
        }

        public void destroyEnemy(Form form)
        {
            if (enemybox.Parent != null)
            {
                form.Controls.Remove(enemybox);
            }
        }

    }
}
