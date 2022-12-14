using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    static class bulletobj
    {
        public static Dictionary<int, int[]> bulletcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> bulletypes = new Dictionary<int, int>();
        public static Dictionary<int, int> bulletbounces = new Dictionary<int, int>();

        private static int[] missileYcords = {20, 109, 198, 287, 376, 475, 564, 653, 750};

        public static int bulletSpeed = 20;
        public static int bulletDamage = 20 ;
        public static int bulletLevel = 9;
        public static int guntype = 5; // (0) bulllet, (3) bounce bullets, (1) pulse, (2) scattered pulse, (4) canonball, (5) missile, (6) guided missile, (7) particle gun, (8) death drone, (9) replicating bullet gun (10) proton gun

        public static void createBullet(int cordx, int cordy, int bulletype, int bulletNumber)
        {
            int bulletgun = bulletype + guntype;
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletgun);
            if (bulletgun == 4 || bulletgun == 5)
            {
                bulletbounces.Add(bulletNumber, 1);
            }

        }

        public static void moveBullet()
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

        public static void createPulse(int cordx, int cordy, int bulletNumber, int pulsetype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            if (guntype == 2) {
                bulletypes.Add(bulletNumber, pulsetype);
            }
        }

        public static void movePulse()
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

        public static void createCanonBall(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
            bulletbounces.Add(bulletNumber, 3);
        }

        public static void moveCanonBall(Boolean isCanonBall)// move canonball or protron
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
                else if (bulletypes[bulletkey] == 10) {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 20)
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed ;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810)         //remove bullet when reached frame boundries
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
                    if (isCanonBall) {
                        bulletbounces.Remove(key);
                    }
                }
                keysRemove.Clear();
            }
        }

        public static void createMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }

        public static void moveMissile()
        {
            List<int> keysRemove = new List<int>();
            double angle;
            Point bulletPoint = new Point();
            Point targetPoint = new Point();
            foreach (int bulletkey in bulletcords.Keys)
            {
                bulletPoint.X = bulletcords[bulletkey][0];
                bulletPoint.Y = bulletcords[bulletkey][1];
                targetPoint.X = 0;
                targetPoint.Y = missileYcords[(bulletypes[bulletkey] - 10) / 10];
                if (bulletypes[bulletkey] % 10 == 0)    // type 0 missiles get fliped to 45 or-45 type according to distantion point 
                {
                    angle = calcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle >= 40 && angle <= 50)
                    {
                        bulletypes[bulletkey] += 1;
                    }
                    else if (angle <= -40 && angle >= -50)
                    {
                        bulletypes[bulletkey] += 2;
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 1)   // type 1 missiles get fliped to 0 type according to distantion point 
                {
                    angle = calcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle < -5)
                    {
                        bulletypes[bulletkey] += 1;
                    } else if (angle >= -5 && angle <= 5) {
                        bulletypes[bulletkey] -= 1;
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 2)   // type 2 missiles get fliped to 0 type according to distantion point 
                {
                    angle = calcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle > 0)
                    {
                        bulletypes[bulletkey] -= 1;
                    }
                    else if ((angle <= 0 && angle >= -10))
                    {
                        bulletypes[bulletkey] -= 2;
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
        private static double calcDistance(Point bulletPoint, Point targetPoint)
        {
            double ydistance = bulletPoint.Y - targetPoint.Y;
            return Math.Sqrt(Math.Pow(bulletPoint.X - targetPoint.X, 2) + Math.Pow(ydistance, 2)); // calc distance from given points on screen
        }

        private static double calcAngle(Point bulletPoint, Point targetPoint) {
            double ydistance = bulletPoint.Y - targetPoint.Y;
            double dist = calcDistance(bulletPoint, targetPoint); // calc distance from given points on screen
            return Math.Round((180 / Math.PI) * Math.Asin(ydistance / dist), 1);
        }

        public static void createGuidedMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }

        public static void moveGuidedMissile(Dictionary<int, int[]> enemyCords, Size enemySize, Size bulletSize, Dictionary<int, int> enemyType) //move guided missle
        {
            List<int> keysRemove = new List<int>();
            Point enemyPoint;
            Point bulletPoint = new Point();
            double angle;
            foreach (int bulletkey in bulletcords.Keys)
            {
                enemyPoint = getClosestEnemy(enemyCords, enemySize, bulletkey, bulletSize, enemyType);
                bulletPoint.X = bulletcords[bulletkey][0] + bulletSize.Width / 2;
                bulletPoint.Y = bulletcords[bulletkey][1] + bulletSize.Height / 2;
                angle = calcAngle(bulletPoint, enemyPoint); // check angle between bullet and closest enemy
                if (angle >= -5 && angle <= 5)
                {
                    if (bulletPoint.X > enemyPoint.X)
                    {
                        bulletypes[bulletkey] = 0;
                    }
                    else
                    {
                        bulletypes[bulletkey] = 7;
                    }
                }
                else if (angle > 40 && angle <= 50)
                {
                    bulletypes[bulletkey] = 1;
                }
                else if (angle > 85 && angle <= 95)
                {
                    bulletypes[bulletkey] = 3;
                }
                else if (angle > 130 && angle <= 140)
                {
                    bulletypes[bulletkey] = 5;
                }

                else if (angle < -130 && angle >= -140)
                {
                    bulletypes[bulletkey] = 6;
                }
                else if (angle < -85 && angle >= -95)
                {
                    bulletypes[bulletkey] = 4;
                }
                else if (angle < -40 && angle >= -50)
                {
                    bulletypes[bulletkey] = 2;
                }
                switch (bulletypes[bulletkey]) // choose fly direction based on enemy angle (enemy img type)
                {
                    case 0:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        break;
                    case 1:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 2:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 3:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 4:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 5:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 6:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 7:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        break;
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
        private static Point getClosestEnemy(Dictionary<int, int[]> enemyCords, Size enemySize, int bulletkey, Size bulletSize, Dictionary<int, int> enemyType) {
            List<int> keysRemove = new List<int>();
            Dictionary<int, int> distances = new Dictionary<int, int>();
            double distance;
            double xdistancePow;
            double ydistancePow;
            foreach (int enemykey in enemyCords.Keys) {                    // calc distance from a bullet to each enemy
                if (enemyType[enemykey] != 3) {
                    xdistancePow = Math.Pow((enemyCords[enemykey][0] + enemySize.Width / 2) - (bulletcords[bulletkey][0] + bulletSize.Width / 2), 2);
                    ydistancePow = Math.Pow((enemyCords[enemykey][1] + enemySize.Height / 2) - (bulletcords[bulletkey][1] + bulletSize.Height / 2), 2);
                    distance = Math.Sqrt(xdistancePow + ydistancePow);       // calc distance from the middle of the img 
                    if (distances.Count == 0)
                    {
                        distances.Add(enemykey, (int)distance); // just adding at the beginning
                    }
                    else {
                        foreach (int distkey in distances.Keys) {
                            if (distances[distkey] > distance)   // if lesser distance found remove the current and add the new one
                            {
                                distances.Add(enemykey, (int)distance);
                                keysRemove.Add(distkey);
                                break;
                            }
                        }
                        if (keysRemove.Count != 0)
                        {
                            foreach (int key in keysRemove)
                            {
                                distances.Remove(key);
                            }
                            keysRemove.Clear();
                        }
                    }
                }
            }
            if (distances.Count != 0)
            {
                Point closestPoint = new Point(enemyCords[distances.ElementAt(0).Key][0]+ (enemySize.Width / 2), enemyCords[distances.ElementAt(0).Key][1]+(enemySize.Height / 2)); // return the closest point that left
                return closestPoint;
            }
            else
            {
                return new Point(0, bulletcords[bulletkey][1]);
            }
        }
        public static int createParticle(int cordx, int cordy, int bulletNumber, int bulletype, Boolean isBullet)
        {
            if (isBullet)
            {
                bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                bulletypes.Add(bulletNumber, bulletype);
            }
            else {
                for (int i = 1; i < 9; i++) {
                    bulletNumber += 1;
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, i+10);
                }
            }
            return bulletNumber;
        }

        public static void moveParticle()
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
                if (bulletypes[bulletkey] > 10) {
                    switch (bulletypes[bulletkey] - 10) // choose fly direction based on img angle
                    {
                        case 1:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            break;
                        case 2:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 3:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 4:
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 5:
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 6:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 7:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 8:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            break;
                    }
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

        public static void createDeathDrone(int cordx, int cordy, int bulletNumber, int type, int bulletAmount, Random rand) // deathdrone or replicating bullet or proton
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                bulletNumber += 1;
                if (type == 0)
                {
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, rand.Next(0, 8));
                } else if (type == 10) {
                    bulletcords.Add(bulletNumber, new int[] { rand.Next(cordx-50, cordx+50), rand.Next(cordy-50, cordy+50) });
                    bulletypes.Add(bulletNumber, rand.Next(1,3)*10);
                }
                else {
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, type-1);
                }
            }
        }

        public static void moveDeathDrone() // deathdrone or replicating bullet
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletypes.Keys)
            {
                switch (bulletypes[bulletkey]) // choose fly direction based on img angle
                {
                    case 0:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        break;
                    case 1:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 2:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 3:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 4:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 5:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 6:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 7:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        break;
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
    }
}
