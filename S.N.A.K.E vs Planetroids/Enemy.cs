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
    class Enemy
    {
        static Random RandomGenerator = new Random();
        static public List<Enemy> list = new List<Enemy>()             {        new Enemy(new Vector2(2, 6), EnemyType.Planetroid1),
                                                                                new Enemy(new Vector2(3, 5), EnemyType.Planetroid2), };
        //new Enemy(new Vector2(10, 5), EnemyType.Planetroid2),new Enemy(new Vector2(3, 5), EnemyType.Planetroid2),new Enemy(new Vector2(2, 5), EnemyType.Planetroid2),
        //new Enemy(new Vector2(5, 5), EnemyType.Planetroid2),new Enemy(new Vector2(3, 3), EnemyType.Planetroid2),new Enemy(new Vector2(2, 2), EnemyType.Planetroid2),
        //new Enemy(new Vector2(3, 6), EnemyType.Planetroid2),new Enemy(new Vector2(1, 5), EnemyType.Planetroid2)};
        public Vector2 LOCATION;
        public EnemyType ENEMY_TYPE;
        public float ROTATE_ANGLE = 0, gTime = 0;
        public int HEALTH, DAMAGE;
        public bool isShoot = true;

        public Enemy(Vector2 LOCATION_g, EnemyType ENEMY_TYPE_g)
        {
            LOCATION = LOCATION_g;
            ENEMY_TYPE = ENEMY_TYPE_g;
            switch (ENEMY_TYPE)
            {
                case EnemyType.Planetroid1:
                    HEALTH = 30; DAMAGE = 10;
                    break;
                case EnemyType.Planetroid2:
                    HEALTH = 50; DAMAGE = 20;
                    break;
            }
        }

        static public void EnemyGenerate(EnemyType ENEMY_TYPE)
        {
            bool isgen = true, isgenA = false, isgenB = false, isgenC = false; int x = 0, y = 0, z = 0, xr = 0, yr = 0;
            while (isgen)
            {
                x = RandomGenerator.Next(0, SnakeGame.GFj + 1); y = RandomGenerator.Next(0, SnakeGame.GFi + 1); z = RandomGenerator.Next(0, 2); xr = RandomGenerator.Next(0, 2); yr = RandomGenerator.Next(0, 2);
                if (z == 0)
                {
                    x = xr == 0 ? 0 : SnakeGame.GFj;
                }
                else if (z == 1)
                {
                    y = yr == 0 ? 0 : SnakeGame.GFi;
                }
                isgen = false;
                for (int i = 0; i < SnakeGame.PLAYER.SNAKE.Count; i++)
                {
                    if (SnakeGame.PLAYER.SNAKE[i].LOCATION == new Vector2(x, y))
                    {
                        isgenA = true;
                    }
                }
                for (int i = 0; i < ItemPack.list.Count; i++)
                {
                    if (ItemPack.list[i].LOCATION == new Vector2(x, y))
                    {
                        isgenB = true;
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].LOCATION == new Vector2(x, y))
                    {
                        isgenC = true;
                    }
                }
                if (isgenA && isgenB && isgenC)
                {
                    isgen = true;
                }
            }
            list.Add(new Enemy(new Vector2(x, y), ENEMY_TYPE));
        }

        public void Shoot()
        {

        }

        static public void Update(GameTime gameTime)
        {
            for (int i = 0; i < list.Count; i++) { list[i].gTime += gameTime.ElapsedGameTime.Milliseconds; }
        }

        static public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }

    enum EnemyType
    {
        Planetroid1, Planetroid2
    }
}
