using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    class dropobj
    {
        public PictureBox dropbox = new PictureBox();
        private int[] dropCords = new int[2];
        private Dictionary<Image, Size>  dropimgs = new Dictionary<Image, Size> (){
            { Image.FromFile(@"spacebattle\energy30.png"), new Size (24,30) },
            { Image.FromFile(@"spacebattle\energy50.png"), new Size (24,30) },
            { Image.FromFile(@"spacebattle\fuel.png"), new Size (25,30) },
            { Image.FromFile(@"spacebattle\PlayerHP.png"), new Size (14,24) },
            { Image.FromFile(@"spacebattle\ammoup.png"), new Size (30,30) },
            { Image.FromFile(@"pacebattle\coin.png"), new Size (15,15) },
            { Image.FromFile(@"spacebattle\parts.png"), new Size (25,29) },
        };

        private int dropSpeed = 5;
        private int[] amounts = { 30, 50, 20, 30, 1, 1, 1};
        public int amount;
        public int dropType;
        private int[] dropRates = {50, 50, 50, 100, 20, 20, 20};

        public void createDrop(Form form, int cordx, int cordy)
        {
            dropCords[0] = cordx;
            dropCords[1] = cordy;
            dropType = calcDrop();
            amount = amounts[dropType];
            dropbox.Image = dropimgs.ElementAt(dropType).Key;
            dropbox.Size = dropimgs.ElementAt(dropType).Value;
            dropbox.BackColor = Color.Transparent;
            dropbox.Location = new Point(cordx, cordy);
            form.Controls.Add(dropbox);

        }
        public Boolean moveDrop()
        {
            dropCords[0] = dropCords[0] + dropSpeed;
            if (dropCords[0] > 1390)                            //remove drop when reached frame boundries
            {
                return false;
            }
            dropbox.Location = new Point(dropCords[0], dropCords[1]);
            return true;
        }

        public void destroyDrop(Form form)
        {
            if (dropbox.Parent != null)
            {
                form.Controls.Remove(dropbox);
            }
        }

        public int calcDrop()
        {
            Random randint = new Random();
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
