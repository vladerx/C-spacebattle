﻿using System;
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
        public Dictionary<int, int[]> enemycords = new Dictionary<int, int[]>();
        public Dictionary<int, int> enemyframes = new Dictionary<int, int>();
        public Dictionary<int, int> enemyhps = new Dictionary<int, int>();
        public Dictionary<int, int> dmgImgframes = new Dictionary<int, int>();
        public List<int> keysRemove = new List<int>();

        public Size enemysize = new Size(50, 35);

        public int enemyFrame = 0;
        public int enemySpeed = 5;
        public int enemyBodyDMG = 20;


        public void createEnemy(int cordx, int cordy, int enemyNumber)
        {
            enemycords.Add(enemyNumber, new int[] { cordx, cordy });
            enemyframes.Add(enemyNumber, 0);
            enemyhps.Add(enemyNumber, 50);
            dmgImgframes.Add(enemyNumber, -1);
        }

        public void moveEnemy()
        {
            foreach (int enemykey in enemycords.Keys)
            {
                if (enemyframes[enemykey] == 2)
                {
                    enemycords[enemykey][1] = enemycords[enemykey][1] + (enemySpeed * 2);
                }
                else
                {
                    enemycords[enemykey][0] = enemycords[enemykey][0] + enemySpeed;
                }
                if (enemycords[enemykey][0] > 1390 || enemycords[enemykey][1] > 710)               //frame boundries removes enemy
                {
                    keysRemove.Add(enemykey);
                }
                if (dmgImgframes[enemykey] >= 0)
                {
                    dmgImgframes[enemykey] += 1;
                }
                if (dmgImgframes[enemykey] == 7)
                {
                    setEnemyFrame(0, enemykey);
                    dmgImgframes[enemykey] = -1;
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    enemycords.Remove(key);
                    enemyframes.Remove(key);
                    enemyhps.Remove(key);
                    dmgImgframes.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        public void setEnemyFrame(int frame, int key)
        {
            if (enemyframes[key] != 2)
            {
                enemyframes[key] = frame;
                if (frame == 1)
                {
                    dmgImgframes[key] = 0;
                } else if (frame == 2) {
                    dmgImgframes[key] = -1;
                }
            }
            /*else {
                enemyframes[key] = frame;
                dmgImgframes[key] = -1;
            }*/
        }

    }
}
