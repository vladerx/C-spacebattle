using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacebattle
{
    static class planetobj
    {
        public static Dictionary<int, int[]> planetcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> planetypes = new Dictionary<int, int>();
        public static Dictionary<int, int> planetspeeds = new Dictionary<int, int>();
        private static List<int> keysRemove = new List<int>();

        public static void createplanet(int cordx, int cordy, int type, int planetNumber)
        {
            int speed = type+1;
            planetcords.Add(planetNumber, new int[] { cordx, cordy });
            planetypes.Add(planetNumber, type);
            planetspeeds.Add(planetNumber, speed);
        }
        public static void moveplanet()
        {
            foreach (int planetkey in planetcords.Keys)
            {
                planetcords[planetkey][0] = planetcords[planetkey][0] + planetspeeds[planetkey];
                if (planetcords[planetkey][0] > 1450)
                {
                    keysRemove.Add(planetkey);
                    
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    planetcords.Remove(key);
                    planetypes.Remove(key);
                    planetspeeds.Remove(key);
                }
                keysRemove.Clear();
            }
        }
    }
}
