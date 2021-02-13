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
    public class SnakeSegment
    {
        public Vector2 LOCATION_OLD, LOCATION;
        public DIRRECTION MOVE_DIRECTION_OLD, MOVE_DIRECTION;
        public float ROTATE_ANGLE_OLD, ROTATE_ANGLE, ROTATE_ANGLE_GUN, ROTATE_ANGLE_GUN_OLD;
        public int HEALTH_OLD, HEALTH;
        public BODY_TYPE BODY_TYPE_OLD, BODY_TYPE;

        public SnakeSegment(Vector2 LOCATION_g, DIRRECTION MOVE_DIRECTION_g, float ROTATE_ANGLE_g, int HEALTH_g, BODY_TYPE BODY_TYPE_g, float ROTATE_ANGLE_GUN_g = 0)
        {
            LOCATION = LOCATION_g;
            MOVE_DIRECTION = MOVE_DIRECTION_g;
            ROTATE_ANGLE = ROTATE_ANGLE_g;
            HEALTH = HEALTH_g;
            BODY_TYPE = BODY_TYPE_g;
            ROTATE_ANGLE_GUN = ROTATE_ANGLE_GUN_g;
        }

        public void MakeBackup()
        {
            LOCATION_OLD = LOCATION;
            MOVE_DIRECTION_OLD = MOVE_DIRECTION;
            ROTATE_ANGLE_OLD = ROTATE_ANGLE;
            HEALTH_OLD = HEALTH;
            BODY_TYPE_OLD = BODY_TYPE;
            ROTATE_ANGLE_GUN_OLD = ROTATE_ANGLE_GUN;
        }
    }
}
