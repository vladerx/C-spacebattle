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
        public PictureBox playerbox = new PictureBox();
        public int[] playerCords = new int[2];
        private Image img = Image.FromFile(@"repos\spacebattle\spacebattle\player.png");

        public int[] inverntory = { 0, 0};
        public int playerSpeed = 10;
        public int HP = 100;
        public int fireRate = 5;               //bullets per second
        public int fuelCons = 10;
        public int energyCons = 5;
        public int fuel = 100;
        public int energy = 100;
        public int coins = 0;


        public void createPlayer(Form form, int cordx, int cordy)
        {
            playerCords[0] = cordx;
            playerCords[1] = cordy;
            playerbox.Image = img;
            playerbox.Size = new Size(80, 44);
            playerbox.BackColor = Color.Transparent;
            playerbox.Location = new Point(cordx, cordy);
            form.Controls.Add(playerbox);
            playerbox.BringToFront();
       
        }
        public void movePlayer(int[] direc)
        {
            playerCords[0] = playerCords[0] + (playerSpeed * (direc[2] - direc[3]));
            playerCords[1] = playerCords[1] + (playerSpeed * (direc[1] - direc[0]));
            if ((playerCords[0] < 0) || (playerCords[0] > 1390))                //frame boundries stops player's advance
            {
                playerCords[0] = playerCords[0] - (playerSpeed * (direc[2] - direc[3]));
            }
            if ((playerCords[1] < 0) || (playerCords[1] > 710)) 
            {
                playerCords[1] = playerCords[1] - (playerSpeed * (direc[1] - direc[0]));
            }
            playerbox.Location = new Point(playerCords[0], playerCords[1]);
        }
    }
}
