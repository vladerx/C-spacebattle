using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    class bulletobj
    {
        public Dictionary<int, int[]> bulletcords = new Dictionary<int, int[]>();
        public Dictionary<int, int> bulletypes = new Dictionary<int, int>();
        public Dictionary<int, int> bulletbounces = new Dictionary<int, int>();

        private int[] missileYcords = {20, 109, 198, 287, 376, 475, 564, 653, 750};

        public int bulletSpeed = 10;
        public int bulletDamage = 17;
        public int bulletLevel = 9;
        public int guntype = 2; // (0) bulllet, (3) bounce bullets, (1) pulse, (2) scattered pulse, (4) canonball, (5) missile, (6) guided missile

        public void createBullet(int cordx, int cordy, int bulletype, int bulletNumber)
        {
            int bulletgun = bulletype + guntype;
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletgun);
            if (bulletgun == 4 || bulletgun == 5)
            {
                bulletbounces.Add(bulletNumber, 1);
            }

        }

        public void moveBullet()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletcords.Keys)
            {
                if (bulletypes[bulletkey] == (1 + guntype))  //flipped bullets will fly vertically in negative direction 
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == (2 + guntype))  //flipped bullets will fly vertically in positive direction 
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < -50 || bulletcords[bulletkey][1] > 810)                            //remove bullet when reached frame boundries
                {
                    if (guntype == 3 && bulletbounces.Count != 0 && (bulletypes[bulletkey] == (1 + guntype) || bulletypes[bulletkey] == (2 + guntype)))                             // checking for bounce gun
                    {
                        if (bulletbounces[bulletkey] == 1)
                        {
                            bulletbounces[bulletkey] -= 1;
                            if (bulletypes[bulletkey] == (1 + guntype))      // if bullet reached frame border flip it once
                            {
                                bulletypes[bulletkey] = (2 + guntype);
                            }
                            else
                            {
                                bulletypes[bulletkey] = (1 + guntype);
                            }
                        }
                        else
                        {
                            keysRemove.Add(bulletkey);
                        }
                    }
                    else
                    {
                        keysRemove.Add(bulletkey);
                    }
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                    bulletbounces.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        public void createPulse(int cordx, int cordy, int bulletNumber, int pulsetype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            if (guntype == 2) {
                bulletypes.Add(bulletNumber, pulsetype);
            }
        }

        public void movePulse()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletcords.Keys)
            {
                if (guntype == 2) {
                    if (bulletypes[bulletkey] == (guntype))   //flipped pulse will fly vertically in negative direction 
                    {
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                    }
                    else if (bulletypes[bulletkey] == (guntype+1)) //flipped pulse will fly vertically in negative direction 
                    {
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                    }
                }
                bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 780)                            //remove pulse when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    if (guntype == 2)
                    {
                        bulletypes.Remove(key);
                    }
                }
                keysRemove.Clear();
            }
        }

        public void createCanonBall(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
            bulletbounces.Add(bulletNumber, 3);
        }

        public void moveCanonBall()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletypes.Keys)
            {
                if (bulletypes[bulletkey] == 0)  // different types different directions
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 1)  
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 2)  
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                    bulletbounces.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        public void createMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }

        public void moveMissile()
        {
            List<int> keysRemove = new List<int>();
            double angle;
            int ydistance;
            foreach (int bulletkey in bulletcords.Keys)
            {
                ydistance = bulletcords[bulletkey][1] - missileYcords[(bulletypes[bulletkey] - 10) / 10];
                if (bulletypes[bulletkey] % 10 == 0)    // type 0 missiles get fliped to 45 or-45 type according to distantion point 
                {
                    angle = calcAngle((bulletypes[bulletkey] - 10) / 10 , bulletkey, 3); // calc angle base on difference between missile point and target point
                    //label.Text = angle.ToString();
                    if (angle >= 40 && angle <= 50)
                    {
                        if (ydistance > 0)
                        {
                            bulletypes[bulletkey] += 1;
                        }
                        else if (ydistance < 0)
                        {
                            bulletypes[bulletkey] += 2;
                        }
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 1)   // type 1 missiles get fliped to 0 type according to distantion point 
                {
                    if (ydistance < 0)
                    {
                        bulletypes[bulletkey] += 1;
                    }
                    else
                    {
                        angle = calcAngle((bulletypes[bulletkey] - 10) / 10, bulletkey, 0); // calc angle base on difference between missile point and target point
                        if ((angle >= -10 && angle <= 10))
                        {
                            bulletypes[bulletkey] -= 1;
                        }
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 2)   // type 2 missiles get fliped to 0 type according to distantion point 
                {
                    if (ydistance > 0)
                    {
                        bulletypes[bulletkey] -= 1;
                    }
                    else
                    {
                        angle = calcAngle((bulletypes[bulletkey] - 10) / 10, bulletkey, 6); // calc angle base on difference between missile point and target point
                        if ((angle >= -10 && angle <= 10))
                        {
                            bulletypes[bulletkey] -= 2;
                        }
                    }

                }
                if (bulletypes[bulletkey] % 10 == 0) {                     // flying directions according to missile type
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                } else if (bulletypes[bulletkey] % 10 == 1) {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                } else if (bulletypes[bulletkey] % 10 == 2) {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        private double calcAngle(int num, int key, int startindx) {
            double ydistance = Math.Sqrt(Math.Pow(bulletcords[key][1] - missileYcords[num], 2));
            double distance = Math.Sqrt(Math.Pow(bulletcords[key][0], 2) + Math.Pow(ydistance, 2)); // calc distance from given points on screen
            return Math.Round((180 / Math.PI) * Math.Asin(ydistance / distance), 1);
        }

        public void createGuidedMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }
        public void moveGuidedMissile(Dictionary<int, int[]> enemyCords, Size imgSize, Size bulletSize)
        {
            List<int> keysRemove = new List<int>();
            double angle;
            int ydistance;
            foreach (int bulletkey in bulletcords.Keys)
            {
                ydistance = bulletcords[bulletkey][1] - missileYcords[(bulletypes[bulletkey] - 10) / 10];
                if (bulletypes[bulletkey] % 10 == 0)    // type 0 missiles get fliped to 45 or-45 type according to distantion point 
                {
                    angle = calcAngle((bulletypes[bulletkey] - 10) / 10, bulletkey, 3); // calc angle base on difference between missile point and target point
                    //label.Text = angle.ToString();
                    if (angle >= 40 && angle <= 50)
                    {
                        if (ydistance > 0)
                        {
                            bulletypes[bulletkey] += 1;
                        }
                        else if (ydistance < 0)
                        {
                            bulletypes[bulletkey] += 2;
                        }
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 1)   // type 1 missiles get fliped to 0 type according to distantion point 
                {
                    if (ydistance < 0)
                    {
                        bulletypes[bulletkey] += 1;
                    }
                    else
                    {
                        angle = calcAngle((bulletypes[bulletkey] - 10) / 10, bulletkey, 0); // calc angle base on difference between missile point and target point
                        if ((angle >= -10 && angle <= 10))
                        {
                            bulletypes[bulletkey] -= 1;
                        }
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 2)   // type 2 missiles get fliped to 0 type according to distantion point 
                {
                    if (ydistance > 0)
                    {
                        bulletypes[bulletkey] -= 1;
                    }
                    else
                    {
                        angle = calcAngle((bulletypes[bulletkey] - 10) / 10, bulletkey, 6); // calc angle base on difference between missile point and target point
                        if ((angle >= -10 && angle <= 10))
                        {
                            bulletypes[bulletkey] -= 2;
                        }
                    }

                }
                if (bulletypes[bulletkey] % 10 == 0)
                {                     // flying directions according to missile type
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] % 10 == 1)
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] % 10 == 2)
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        private Point getClosestEnemy(Dictionary<int, int[]> enemyCords, Size enemySize, int bulletkey, Size bulletSize) {
            List<int> keysRemove = new List<int>();
            Dictionary<int, int> distances = new Dictionary<int, int>();
            double distance;
            double xdistancePow;
            double ydistancePow;
            foreach (int enemykey in enemyCords.Keys) {
                xdistancePow = Math.Pow((enemyCords[enemykey][0] + enemySize.Width / 2) - (bulletcords[bulletkey][0] + bulletSize.Width / 2),2);
                ydistancePow = Math.Pow((enemyCords[enemykey][1] + enemySize.Height / 2) - (bulletcords[bulletkey][1] + bulletSize.Height / 2), 2);
                distance = Math.Sqrt(xdistancePow + ydistancePow);    // calc distance from the middle of the img 
                if (distances.Count == 0)
                {
                    distances.Add(enemykey, (int)distance);
                }
                else {
                    foreach (int distkey in distances.Keys) {
                        if (distances[distkey] > distance)
                        {
                            distances.Add(enemykey, (int)distance);
                            keysRemove.Add(distkey);
                        }
                    }
                    if (keysRemove.Count != 0)
                    {
                        foreach (int key in keysRemove)
                        {
                            bulletcords.Remove(key);
                            bulletypes.Remove(key);
                        }
                        keysRemove.Clear();
                    }
                }
            }
            int keycount = distances.Count;
            if (keycount != 0)
            {
                Point closestPoint = new Point(enemyCords[distances.ElementAt(0).Key][0], enemyCords[distances.ElementAt(0).Key][1]);
                return closestPoint;
            }
            else
            {
                return new Point(bulletcords[bulletkey][0], 0);
            }
        }
    }
}
