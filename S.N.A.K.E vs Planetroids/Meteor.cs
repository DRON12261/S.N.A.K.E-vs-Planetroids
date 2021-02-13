using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace S.N.A.K.E_vs_Planetroids
{
    class Meteor
    {
        static public List<Meteor> list = new List<Meteor>() { };
        public Vector2 LOCATION;
        public DIRRECTION DIRRECTION;
        public float ROTATE_ANGLE = 0;
        public Enemy WHO;

        public Meteor(Enemy WHO_g, Vector2 LOCATION_g, DIRRECTION DIRRECTION_g)
        {
            LOCATION = LOCATION_g;
            WHO = WHO_g;
            DIRRECTION = DIRRECTION_g;
        }

        public void Update()
        {
            switch (DIRRECTION)
            {
                case DIRRECTION.UP:
                    LOCATION.Y -= 1;
                    ROTATE_ANGLE = 0;
                    break;
                case DIRRECTION.DOWN:
                    LOCATION.Y += 1;
                    ROTATE_ANGLE = 180;
                    break;
                case DIRRECTION.LEFT:
                    LOCATION.X -= 1;
                    ROTATE_ANGLE = 270;
                    break;
                case DIRRECTION.RIGHT:
                    LOCATION.X += 1;
                    ROTATE_ANGLE = 90;
                    break;
            }
        }
    }
}
