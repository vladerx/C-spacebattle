using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    static class dropobj
    {
        public static Dictionary<int, int> droptypes = new Dictionary<int, int>();
        public static Dictionary<int, int[]> dropcords = new Dictionary<int, int[]>();
        private static List<int> keysRemove = new List<int>();

        private static int dropSpeed = 2;
        public static int[] amounts = { 30, 50, 20, 30, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        private static int[] dropRates = {50, 50, 50, 100, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };
        public static string[] dropnames = {" energy", " energy" , " fuel", " durability", " ammo up", " Laser Rain", " Bouncing Laser", " Pulse Gun", " Scattered Pulse Gun", " Canon Gun", " Missile", " Guided Missile", " Particle Gun", " Death Drone Launcher", " Replicating Bullet Gun", " Proton Gun", " Crit.Chance", " Crit.Damage", " Fire Rate", " coins", " parts" };

        public static void createDrop(int cordx, int cordy, int droptype, int dropNumber)
        {
            droptypes.Add(dropNumber, droptype);
            dropcords.Add(dropNumber, new int[] { cordx, cordy });

        }
        public static void moveDrop()
        {
            int cord;
            foreach (int dropkey in dropcords.Keys) {
                cord = dropcords[dropkey][0] + dropSpeed;
                if (cord > 1390)                            //remove drop when reached frame boundries
                {
                    keysRemove.Add(dropkey);
                }
                dropcords[dropkey][0] = cord + dropSpeed;
            }
            if (keysRemove.Count != 0) {
                foreach (int key in keysRemove) {
                    dropcords.Remove(key);
                    droptypes.Remove(key);
                }
                keysRemove.Clear();
            }

        }

        public static int calcDrop(Random randint)
        {
            int rateSum = dropRates.Sum();
            int ratenum = randint.Next(0, rateSum);
            int sum = 0;
            for (int i = 0; i < dropRates.Length; i++)
            {
                sum += dropRates[i];
                if (ratenum < sum) {
                    return i;
                }
            }
            return -1;
        }
    }
}
