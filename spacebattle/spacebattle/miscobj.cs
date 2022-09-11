using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spacebattle
{
    class miscobj
    {
        private PictureBox miscBox = new PictureBox();
        private int[] miscCords = new int[2];

        private int miscSpeed;
        public int miscType;

        public void createMisc(Form form, int cordx, int cordy, int speed, int typeofobj)
        {
            miscType = typeofobj;
            Image img;
            if (typeofobj == 0)
            {
                img = Image.FromFile(@"spacebattle\flyingdust.png");
                miscBox.Size = new Size(30, 3);

            }
            else
            {
                img = Image.FromFile(@"spacebattle\star.png");
                miscBox.Size = new Size(3, 5);
            }
            
            miscCords[0] = cordx;
            miscCords[1] = cordy;
            miscSpeed = speed;
            miscBox.Image = img;
            miscBox.BackColor = Color.Transparent;
            miscBox.Location = new Point(cordx, cordy);
            form.Controls.Add(miscBox);
            if (typeofobj == 0)
            {
                miscBox.BringToFront();
            }

        }
        public Boolean moveMisc()
        {
            miscCords[0] = miscCords[0] + miscSpeed;
            if (miscCords[0] > 1450)
            {
                return false;
            }
            miscBox.Location = new Point(miscCords[0], miscCords[1]);
            return true;
        }

        public void destroyMisc(Form form)
        {
            if (miscBox.Parent != null)
            {
                form.Controls.Remove(miscBox);
            }
        }
    }
}
