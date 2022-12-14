using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    static class miscobj
    {
        public static Dictionary<int, int[]> miscords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> misctypes = new Dictionary<int, int>();
        public static Dictionary<int, int> miscspeeds = new Dictionary<int, int>();

        public static void createMisc(int cordx, int cordy, int speed, int type, int miscNumber)
        {
            miscords.Add(miscNumber, new int[] { cordx, cordy });
            misctypes.Add(miscNumber, type);
            miscspeeds.Add(miscNumber, speed);
        }
        public static int moveMisc()
        {
            int misctype;
            foreach (int misckey in miscords.Keys)
            {
                miscords[misckey][0] = miscords[misckey][0] + miscspeeds[misckey];
                if (miscords[misckey][0] > 1450)
                {
                    misctype = misctypes[misckey];
                    miscords.Remove(misckey);
                    misctypes.Remove(misckey);
                    miscspeeds.Remove(misckey);
                    return misctype;
                }
            }
            return -1;
        }
    }
}
