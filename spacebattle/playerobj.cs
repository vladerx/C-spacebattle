using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    class playerobj
    {
        public Dictionary<int, Label> colides = new Dictionary<int, Label>();
        public Dictionary<int, int> labelcounter = new Dictionary<int, int>();

        public int[] cords = { 1200, 400 };
        public Size size = new Size(80, 44);
        public int[] inverntory = { 0, 0};
        public int playerSpeed;
        public int HP;
        public int fireRate;               //bullets per second
        public int fuelCons;
        public int energyCons;
        public int fuel = 100;
        public int energy = 100;
        public int coins = 0;

        public playerobj(int firerate, int fuelcon, int dura, int speed, int energycon) {
            fireRate = firerate;
            fuelCons = fuelcon;
            energyCons = energycon;
            playerSpeed = speed;
            HP = dura;
        }

        public void movePlayer(int[] direc)
        {

            cords[0] = cords[0] + (playerSpeed * (direc[2] - direc[3]));
            cords[1] = cords[1] + (playerSpeed * (direc[1] - direc[0]));
            if ((cords[0] < 0) || (cords[0] > 1370))                //frame boundries stops player's advance
            {
                cords[0] = cords[0] - (playerSpeed * (direc[2] - direc[3]));
            }
            if ((cords[1] < 0) || (cords[1] > 710)) 
            {
                cords[1] = cords[1] - (playerSpeed * (direc[1] - direc[0]));
            };
        }
    }
}
