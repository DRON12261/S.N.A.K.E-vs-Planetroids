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
    class ItemPack
    {
        static Random RandomGenerator = new Random();
        static public List<ItemPack> list = new List<ItemPack>()         {      new ItemPack(ItemType.Health, new Vector2(2,2)),
                                                                                new ItemPack(ItemType.Health, new Vector2(5,1)),
                                                                                new ItemPack(ItemType.Segment, new Vector2(4,6)),
                                                                                new ItemPack(ItemType.Segment, new Vector2(9,6))};
        public ItemType ITEMTYPE;
        public Vector2 LOCATION;

        public ItemPack(ItemType ITEMTYPE_G, Vector2 LOCATION_G)
        {
            ITEMTYPE = ITEMTYPE_G;
            LOCATION = LOCATION_G;
        }

        static public void ItemAction(ItemType ITEMTYPE, SnakeSegment bufferSegment)
        {
            switch (ITEMTYPE)
            {
                case ItemType.Health:
                    SnakeGame.PLAYER.HEALTH += 10;
                    break;
                case ItemType.Segment:
                    SnakeGame.PLAYER.SNAKE.Insert(SnakeGame.PLAYER.SNAKE.Count - 2, new SnakeSegment(bufferSegment.LOCATION, bufferSegment.MOVE_DIRECTION, bufferSegment.ROTATE_ANGLE, bufferSegment.HEALTH, BODY_TYPE.BODY));
                    SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1] = new SnakeSegment(SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1].LOCATION_OLD, SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1].MOVE_DIRECTION_OLD, SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1].ROTATE_ANGLE_OLD, SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1].HEALTH_OLD, SnakeGame.PLAYER.SNAKE[SnakeGame.PLAYER.SNAKE.Count - 1].BODY_TYPE_OLD);
                    break;
            }
            SnakeGame.PLAYER.SCORE += 100;
        }

        static public ItemPack ItemRandomGenerate(ItemType itemtype)
        {
            bool isgen = true, isgenA = false, isgenB = false, isgenC = false; int x = 0, y = 0;
            while (isgen)
            {
                x = RandomGenerator.Next(-1, SnakeGame.GFj + 1); y = RandomGenerator.Next(-1, SnakeGame.GFi + 1);
                isgen = false;
                for (int i = 0; i < SnakeGame.PLAYER.SNAKE.Count; i++)
                {
                    if (SnakeGame.PLAYER.SNAKE[i].LOCATION == new Vector2(x, y))
                    {
                        isgenA = true;
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].LOCATION == new Vector2(x, y))
                    {
                        isgenB = true;
                    }
                }
                for (int i = 0; i < Enemy.list.Count; i++)
                {
                    if (Enemy.list[i].LOCATION == new Vector2(x, y))
                    {
                        isgenC = true;
                    }
                }
                if (isgenA && isgenB && isgenC)
                {
                    isgen = true;
                }
            }
            return new ItemPack(itemtype, new Vector2(RandomGenerator.Next(SnakeGame.GFj), RandomGenerator.Next(SnakeGame.GFi)));
        }

        static public void Update(GameTime gameTime)
        {

        }

        static public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }

    enum ItemType
    {
        Health, Segment
    }
}
