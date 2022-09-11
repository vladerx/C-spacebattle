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
        public PictureBox bulletbox = new PictureBox();
        private int[] bulletCords = new int[2];
        private Dictionary<Image, Size> dropimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"spacebattle\defgun.png"), new Size(30, 12) },
            { Image.FromFile(@"spacebattle\defgunflip45.png"), new Size(30, 27)},
            { Image.FromFile(@"spacebattle\defgunflip-45.png"), new Size(30, 27)}
        };

        public int bulletSpeed = 20;
        public int bulletDamage = 20;
        public int bulletLevel = 9;
        public int booletFlipType;

        public void createBullet(Form form, int cordx, int cordy, int flip)
        {
            bulletCords[0] = cordx;
            bulletCords[1] = cordy;
            bulletbox.Image = dropimgs.ElementAt(flip).Key; ;
            bulletbox.Size = dropimgs.ElementAt(flip).Value;
            booletFlipType = flip;
            bulletbox.BackColor = Color.Transparent;
            bulletbox.Location = new Point(cordx, cordy);
            form.Controls.Add(bulletbox);
            bulletbox.BringToFront();

        }

        public Boolean moveBullet()
        {
            if (booletFlipType == 1)
            {
                bulletCords[1] = bulletCords[1] - bulletSpeed;
            } else if (booletFlipType == 2) {
                bulletCords[1] = bulletCords[1] + bulletSpeed;
            }
            bulletCords[0] = bulletCords[0] - bulletSpeed;      
            if (bulletCords[0] < 0 || bulletCords[1] < 0)                            //remove bullet when reached frame boundries
            { 
                return false;
            }
            bulletbox.Location = new Point(bulletCords[0], bulletCords[1]);
            return true;
        }

        public void destroyBullet(Form form)
        {
            if (bulletbox.Parent != null)
            {
                form.Controls.Remove(bulletbox);
            }
        }
    }
}
