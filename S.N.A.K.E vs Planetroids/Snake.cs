using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace S.N.A.K.E_vs_Planetroids
{
    public class Snake
    {
        public int      HEALTH = 100,
                        MAX_HEALTH = 200,
                        MIN_HEALTH = 20,
                        SPEED = 0,
                        SEGMENTS = 0,
                        MAX_SEGMENTS = 10,
                        SCORE = 0,
                        KILL_COUNT = 0;
        public float    BODY_ANGLE_ROTATION = 0;
        public bool     IS_ALIVE = true,
                        IS_ATTACK_KEY_DOWN = false,
                        PLAY_SHOOT_SND = false;
        Dictionary<string, Texture2D> SPRITE_BANK;
        Dictionary<string, SpriteFont> FONT_BANK;
        Dictionary<string, SoundEffect> SOUND_BANK;
        Dictionary<string, Song> MUSIC_BANK;
        public List<SnakeSegment> SNAKE = new List<SnakeSegment>() {    new SnakeSegment(new Vector2(10,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.HEAD),
                                                                        new SnakeSegment(new Vector2(9,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(8,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(7,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(6,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(5,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(4,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.BODY),
                                                                        new SnakeSegment(new Vector2(3,4), DIRRECTION.RIGHT, 0, 100, BODY_TYPE.TALE) };

        public Snake()
        {

        }

        public void LoadResources(  Dictionary<string, Texture2D> _SPRITE_BANK, 
                                    Dictionary<string, SpriteFont> _FONT_BANK, 
                                    Dictionary<string, SoundEffect> _SOUND_BANK, 
                                    Dictionary<string, Song> _MUSIC_BANK)
        {
            this.SPRITE_BANK = _SPRITE_BANK;
            this.FONT_BANK = _FONT_BANK;
            this.SOUND_BANK = _SOUND_BANK;
            this.MUSIC_BANK = _MUSIC_BANK;
        }

        public void Update(GameTime GAME_TIME)
        {
            if (IS_ALIVE)
            {
                BODY_ANGLE_ROTATION += 1; if (BODY_ANGLE_ROTATION >= 360) BODY_ANGLE_ROTATION = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(1).IsButtonDown(Buttons.DPadUp) || GamePad.GetState(1).IsButtonDown(Buttons.LeftThumbstickUp)) { SNAKE[0].MOVE_DIRECTION = SNAKE[0].MOVE_DIRECTION == DIRRECTION.DOWN || SNAKE[0].LOCATION.Y - 1 == SNAKE[1].LOCATION.Y ? SNAKE[0].MOVE_DIRECTION : DIRRECTION.UP; }
                if (Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(1).IsButtonDown(Buttons.DPadDown) || GamePad.GetState(1).IsButtonDown(Buttons.LeftThumbstickDown)) { SNAKE[0].MOVE_DIRECTION = SNAKE[0].MOVE_DIRECTION == DIRRECTION.UP || SNAKE[0].LOCATION.Y + 1 == SNAKE[1].LOCATION.Y ? SNAKE[0].MOVE_DIRECTION : DIRRECTION.DOWN; }
                if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(1).IsButtonDown(Buttons.DPadLeft) || GamePad.GetState(1).IsButtonDown(Buttons.LeftThumbstickLeft)) { SNAKE[0].MOVE_DIRECTION = SNAKE[0].MOVE_DIRECTION == DIRRECTION.RIGHT || SNAKE[0].LOCATION.X - 1 == SNAKE[1].LOCATION.X ? SNAKE[0].MOVE_DIRECTION : DIRRECTION.LEFT; }
                if (Keyboard.GetState().IsKeyDown(Keys.D) || GamePad.GetState(1).IsButtonDown(Buttons.DPadRight) || GamePad.GetState(1).IsButtonDown(Buttons.LeftThumbstickRight)) { SNAKE[0].MOVE_DIRECTION = SNAKE[0].MOVE_DIRECTION == DIRRECTION.LEFT || SNAKE[0].LOCATION.X + 1 == SNAKE[1].LOCATION.X ? SNAKE[0].MOVE_DIRECTION : DIRRECTION.RIGHT; }

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (PLAY_SHOOT_SND) { SOUND_BANK["LaserShoot"].Play(); PLAY_SHOOT_SND = false; }
                    IS_ATTACK_KEY_DOWN = true;
                }
                else
                {
                    IS_ATTACK_KEY_DOWN = false;
                    PLAY_SHOOT_SND = true;
                }
                if (HEALTH <= 0)
                {
                    IS_ALIVE = false;
                    SOUND_BANK["GameOver"].Play();
                    MediaPlayer.Stop();
                }
            }
        }

        public void Draw(GameTime GAME_TIME, SpriteBatch SPRITE_BATCH)
        {
            for (int i = 0; i < SNAKE.Count; i++)
            {
                switch (SNAKE[i].BODY_TYPE)
                {
                    case BODY_TYPE.HEAD:
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeHead1"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeHead1"].Width / 2, SPRITE_BANK["SnakeHead1"].Height / 4), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1));
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeHead2"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeHead2"].Width / 2, SPRITE_BANK["SnakeHead2"].Height / 4), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1), Color.CornflowerBlue);
                        break;
                    case BODY_TYPE.BODY:
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeBody1"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeBody1"].Width / 2, SPRITE_BANK["SnakeBody1"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1));
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeBody2"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeBody2"].Width / 2, SPRITE_BANK["SnakeBody2"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1), Color.CornflowerBlue);
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeBody3"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeBody3"].Width / 2, SPRITE_BANK["SnakeBody3"].Height / 2), MathHelper.ToRadians(BODY_ANGLE_ROTATION), new Vector2(1, 1), Color.CornflowerBlue);
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeLaserGun2"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeLaserGun2"].Width / 2, SPRITE_BANK["SnakeLaserGun2"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE_GUN), new Vector2(1, 1), Color.CornflowerBlue);
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeLaserGun1"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeLaserGun1"].Width / 2, SPRITE_BANK["SnakeLaserGun1"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE_GUN), new Vector2(1, 1));
                        break;
                    case BODY_TYPE.TALE:
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeTail1"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeTail1"].Width / 2, 3 * SPRITE_BANK["SnakeTail1"].Height / 4), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1));
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeTail2"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeTail2"].Width / 2, 3 * SPRITE_BANK["SnakeTail2"].Height / 4), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE), new Vector2(1, 1), Color.CornflowerBlue);
                        break;
                }
            }

            for (int i = 0; i < SNAKE.Count; i++)
            {
                switch (SNAKE[i].BODY_TYPE)
                {
                    case BODY_TYPE.BODY:
                        if (IS_ATTACK_KEY_DOWN)
                        {
                            if (IS_ALIVE)
                            {
                                DrawLine(SPRITE_BATCH, new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                            }
                        }
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeLaserGun2"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeLaserGun2"].Width / 2, SPRITE_BANK["SnakeLaserGun2"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE_GUN), new Vector2(1, 1), Color.CornflowerBlue);
                        SPRITE_BATCH.Draw(SPRITE_BANK["SnakeLaserGun1"], new Vector2(64 * SNAKE[i].LOCATION.X + 64, 64 * SNAKE[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SnakeLaserGun1"].Width / 2, SPRITE_BANK["SnakeLaserGun1"].Height / 2), MathHelper.ToRadians(SNAKE[i].ROTATE_ANGLE_GUN), new Vector2(1, 1));
                        break;
                }
            }
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            sb.Draw(SPRITE_BANK["LaserPoint"], new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 2), null, Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
