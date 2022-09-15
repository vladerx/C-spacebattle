using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacebattle
{
    class imgs
    {
        public Image playerimg = Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\player.png");

        public Dictionary<Image, Size> dropimgs = new Dictionary<Image, Size>(){
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\energy30.png"), new Size (24,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\energy50.png"), new Size (24,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\fuel.png"), new Size (25,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\PlayerHP.png"), new Size (14,24) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\ammoup.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\defgundrop.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\boungundrop.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\pulsegun.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\pulsegunscatter.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\canongun.png"), new Size (30,30) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\coin.png"), new Size (15,15) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\drops\parts.png"), new Size (25,29) }
        };

        public Dictionary<Image, Size> bulletimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\defgun\defgun.png"), new Size(30, 12) },       //defgun
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip45.png"), new Size(30, 27)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip-45.png"), new Size(30, 27)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungun.png"), new Size(30, 12) },       //bounce gun
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip45.png"), new Size(30, 27)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip-45.png"), new Size(30, 27)}
        };
        public Dictionary<Image, Size> pulseimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\pulse\pulse.png"), new Size(5, 60) },       //pulse gun
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter.png"), new Size(50, 60) },      //scattered pulse gun
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter45flip.png"), new Size(50, 60) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter-45flip.png"), new Size(50, 60) }
        };
        public Dictionary<Image, Size> canonballimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\canonball\canonball.png"), new Size(25, 25) },       //canonballs
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\canonball\canonballdmg.png"), new Size(25, 25) },      
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\canonball\canonballcrutial.png"), new Size(25, 25) }
        };

        public Dictionary<Image, Size> missileimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\missile\missile.png"), new Size(20, 20) },       //missiles
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\missile\missileflip45.png"), new Size(23, 23) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\missile\missileflip-45.png"), new Size(23, 23) }

        };
        public Dictionary<Image, Size> guidedMissileimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissile.png"), new Size(20, 20) },       //missiles
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip45.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-45.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip90.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-90.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip135.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-135.png"), new Size(20, 20) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip180.png"), new Size(20, 20) }

        };

        public Image[] enemyimgs = {
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\enemies\blueplate\enemy.png"),
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\enemies\blueplate\enemydmg.png"),
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\enemies\blueplate\enemydead.png")
        };

        public Dictionary<Image, Size> miscimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\flyingdust.png"), new Size(30, 3) },
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\star.png"), new Size(3, 3)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\mediummoonyellow.png"), new Size(10, 10)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\mediummoonblue.png"), new Size(10, 10)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\mediummoonred.png"), new Size(10, 10)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\smallmoonred.png"), new Size(6, 6)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\smallmoonblue.png"), new Size(6, 6)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\misc\smallmoonyellow.png"), new Size(6, 6)}
        };

        public Dictionary<Image, Size> planetimgs = new Dictionary<Image, Size>() {
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\planets\moon.png"), new Size(200, 200)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\planets\redmoon.png"), new Size(100, 100)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\planets\greymoon.png"), new Size(400, 400)},
            { Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\planets\orangemoon.png"), new Size(600, 600)}
        };
        public Image[] blastimgs = {
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\blast\blast.png"),
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\blast\blast1.png"),
            Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\blast\blast2.png")
        };
    }
}
