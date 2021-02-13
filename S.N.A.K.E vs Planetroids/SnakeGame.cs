using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace S.N.A.K.E_vs_Planetroids
{
    public class SnakeGame : Game
    {
        Random RANDOM_GENERATOR = new Random();
        GraphicsDeviceManager GRAPHICS;
        SpriteBatch SPRITE_BATCH;
        static public int GFi = 11, GFj = 20;
        static public float[] TIMERS;
        int MAX_SEGMENT_COUNT = 10;
        static public Snake PLAYER = new Snake();
        Dictionary<string, Texture2D> SPRITE_BANK = new Dictionary<string, Texture2D>();
        Dictionary<string, SpriteFont> FONT_BANK = new Dictionary<string, SpriteFont>();
        Dictionary<string, SoundEffect> SOUND_BANK = new Dictionary<string, SoundEffect>();
        Dictionary<string, Song> MUSIC_BANK = new Dictionary<string, Song>();

        public SnakeGame()
        {
            GRAPHICS = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GRAPHICS.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            GRAPHICS.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            GFi = GRAPHICS.PreferredBackBufferHeight / 64 - 4;
            GFj = GRAPHICS.PreferredBackBufferWidth / 64 - 2;
            GRAPHICS.IsFullScreen = true;
            TIMERS = new float[3];
            TIMERS[0] = 0; TIMERS[1] = 0; TIMERS[2] = 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        void LoadSprite(string NAME, string PATH)
        {
            Texture2D TEXTURE = Content.Load<Texture2D>(PATH);
            SPRITE_BANK.Add(NAME, TEXTURE);
        }

        void LoadFont(string NAME, string PATH)
        {
            SpriteFont FONT = Content.Load<SpriteFont>(PATH);
            FONT_BANK.Add(NAME, FONT);
        }

        void LoadSound(string NAME, string PATH)
        {
            SoundEffect SOUND = Content.Load<SoundEffect>(PATH);
            SOUND_BANK.Add(NAME, SOUND);
        }

        void LoadMusic(string NAME, string PATH)
        {
            Song MUSIC = Content.Load<Song>(PATH);
            MUSIC_BANK.Add(NAME, MUSIC);
        }
        
        protected override void LoadContent()
        {
            SPRITE_BATCH = new SpriteBatch(GraphicsDevice);
            LoadSprite("SnakeHead1", "SnakeHead(Colored1)");
            LoadSprite("SnakeHead2", "SnakeHead(Colored2)");
            LoadSprite("SnakeConnector", "Snake_PartsConnector_32x32");
            LoadSprite("SnakeBody1", "SnakeBody(Back)");
            LoadSprite("SnakeBody2", "SnakeBody(Colored1)");
            LoadSprite("SnakeBody3", "SnakeBody(Colored2)");
            LoadSprite("SnakeTail1", "SnakeTail(Colored1)");
            LoadSprite("SnakeTail2", "SnakeTail(Colored2)");
            LoadSprite("SnakeLaserGun1", "SnakeLaserGun(Colored1)");
            LoadSprite("SnakeLaserGun2", "SnakeLaserGun(Colored2)");
            LoadSprite("Crosshair", "Crosshair_32x32");
            LoadSprite("Planetroid1", "Planetroid_1_64x64");
            LoadSprite("Planetroid2", "Planetroid_2_64x64");
            LoadSprite("BackgroundSpace", "backgroundg1_100x100");
            LoadSprite("BackgroundEarth", "background_700x394");
            LoadSprite("HealthPack", "HealthPack_32x32");
            LoadSprite("SegmentPack", "SegmentPack_32x32");
            LoadSprite("Meteor1", "Meteor1_32x32");
            LoadSprite("Meteor2", "Meteor2_32x32");
            LoadSprite("Meteor3", "Meteor3_32x32");
            LoadSprite("LaserPoint", "laser_4x4");
            LoadFont("GameFont1", "GameFont");
            LoadFont("GameFont2", "GameFont2");
            LoadSound("GameOver", "GameOver");
            LoadSound("Explose", "Explose");
            LoadSound("LaserShoot", "LaserShoot");
            LoadMusic("Bgm1", "Soundtrack");
            PLAYER.LoadResources(SPRITE_BANK, FONT_BANK, SOUND_BANK, MUSIC_BANK);
            MediaPlayer.Play(MUSIC_BANK["Bgm1"]);
            MediaPlayer.Volume = MediaPlayer.Volume / 8;
            MediaPlayer.IsRepeating = true;
        }
        
        protected override void UnloadContent()
        {

        }

        /// <param name="GAME_TIME">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime GAME_TIME)
        {
            TIMERS[0] += GAME_TIME.ElapsedGameTime.Milliseconds; TIMERS[1] += GAME_TIME.ElapsedGameTime.Milliseconds; TIMERS[2] += GAME_TIME.ElapsedGameTime.Milliseconds;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            PLAYER.Update(GAME_TIME);
            if (PLAYER.IS_ALIVE)
            {
                if (TIMERS[0] > 80)
                {
                    float GFmp = PLAYER.SNAKE[0].LOCATION.X == GFj ? 0 : PLAYER.SNAKE[0].LOCATION.X + 1;
                    float GFmm = PLAYER.SNAKE[0].LOCATION.X == 0 ? GFj : PLAYER.SNAKE[0].LOCATION.X - 1;
                    float GFnp = PLAYER.SNAKE[0].LOCATION.Y == GFi ? 0 : PLAYER.SNAKE[0].LOCATION.Y + 1;
                    float GFnm = PLAYER.SNAKE[0].LOCATION.Y == 0 ? GFi : PLAYER.SNAKE[0].LOCATION.Y - 1;
                    PLAYER.SNAKE[0].MakeBackup();
                    switch (PLAYER.SNAKE[0].MOVE_DIRECTION)
                    {
                        case DIRRECTION.UP:
                            PLAYER.SNAKE[0].ROTATE_ANGLE = 0;
                            PLAYER.SNAKE[0].LOCATION.Y = GFnm;
                            break;
                        case DIRRECTION.DOWN:
                            PLAYER.SNAKE[0].ROTATE_ANGLE = 180;
                            PLAYER.SNAKE[0].LOCATION.Y = GFnp;
                            break;
                        case DIRRECTION.LEFT:
                            PLAYER.SNAKE[0].ROTATE_ANGLE = 270;
                            PLAYER.SNAKE[0].LOCATION.X = GFmm;
                            break;
                        case DIRRECTION.RIGHT:
                            PLAYER.SNAKE[0].ROTATE_ANGLE = 90;
                            PLAYER.SNAKE[0].LOCATION.X = GFmp;
                            break;
                    }
                    for (int i = 1; i < PLAYER.SNAKE.Count; i++)
                    {
                        PLAYER.SNAKE[i].MakeBackup();
                        PLAYER.SNAKE[i].LOCATION = PLAYER.SNAKE[i - 1].LOCATION_OLD;
                        PLAYER.SNAKE[i].MOVE_DIRECTION = PLAYER.SNAKE[i - 1].MOVE_DIRECTION_OLD;
                        PLAYER.SNAKE[i].ROTATE_ANGLE = PLAYER.SNAKE[i - 1].ROTATE_ANGLE_OLD;
                        PLAYER.SNAKE[i].HEALTH = PLAYER.SNAKE[i - 1].HEALTH_OLD;
                        PLAYER.SNAKE[i].ROTATE_ANGLE_GUN = PLAYER.SNAKE[i - 1].ROTATE_ANGLE_GUN_OLD;
                    }
                    int REM_ITEM = -1, REM_ENEMY = -1;
                    for (int i = 0; i < ItemPack.list.Count; i++)
                    {
                        if (PLAYER.SNAKE[0].LOCATION == ItemPack.list[i].LOCATION)
                        {
                            REM_ITEM = i;
                        }
                    }
                    if (REM_ITEM != -1)
                    {
                        ItemPack.ItemAction(ItemPack.list[REM_ITEM].ITEMTYPE, PLAYER.SNAKE[PLAYER.SNAKE.Count - 1]);
                        if (PLAYER.SNAKE.Count <= MAX_SEGMENT_COUNT + 2 && ItemPack.list[REM_ITEM].ITEMTYPE != ItemType.Segment)
                        {
                            ItemPack.list.Add(ItemPack.ItemRandomGenerate(ItemPack.list[REM_ITEM].ITEMTYPE));
                        }
                        ItemPack.list.Remove(ItemPack.list[REM_ITEM]);
                    }
                    for (int i = 1; i < PLAYER.SNAKE.Count; i++)
                    {
                        if (PLAYER.SNAKE[0].LOCATION == PLAYER.SNAKE[i].LOCATION)
                        {
                            PLAYER.HEALTH = 0;
                        }
                        if (i < PLAYER.SNAKE.Count - 1)
                        {
                            PLAYER.SNAKE[i].ROTATE_ANGLE_GUN = 270 + MathHelper.ToDegrees((float)Math.Atan2(PLAYER.SNAKE[i].LOCATION.Y * 64 + 128 - Mouse.GetState().Y, PLAYER.SNAKE[i].LOCATION.X * 64 + 64 - Mouse.GetState().X));
                            PLAYER.SNAKE[i].ROTATE_ANGLE_GUN = (PLAYER.SNAKE[i].ROTATE_ANGLE_GUN < 0) ? PLAYER.SNAKE[i].ROTATE_ANGLE_GUN + 360 : PLAYER.SNAKE[i].ROTATE_ANGLE_GUN;
                        }
                    }
                    for (int j = 0; j < Enemy.list.Count; j++)
                    {
                        if (PLAYER.SNAKE[0].LOCATION == Enemy.list[j].LOCATION)
                        {
                            PLAYER.HEALTH -= 30;
                        }
                    }
                    for (int i = 0; i < Enemy.list.Count; i++) {
                        if (PLAYER.IS_ATTACK_KEY_DOWN)
                        {
                            if (new Vector2((int)((Mouse.GetState().X - 32) / 64), (int)((Mouse.GetState().Y - 96) / 64)) == Enemy.list[i].LOCATION)
                            {
                                Enemy.list[i].HEALTH -= 10;
                            }
                            
                        }
                        if (Enemy.list[i].HEALTH <= 0)
                        {
                            REM_ENEMY = i;
                            PLAYER.KILL_COUNT += 1;
                            PLAYER.SCORE += Enemy.list[i].ENEMY_TYPE == EnemyType.Planetroid1 ? 300 : 500;
                        }
                        Enemy.list[i].ROTATE_ANGLE = 270 + MathHelper.ToDegrees((float)Math.Atan2(Enemy.list[i].LOCATION.Y - PLAYER.SNAKE[0].LOCATION.Y, Enemy.list[i].LOCATION.X - PLAYER.SNAKE[0].LOCATION.X));
                        Enemy.list[i].ROTATE_ANGLE = (Enemy.list[i].ROTATE_ANGLE < 0) ? Enemy.list[i].ROTATE_ANGLE + 360 : Enemy.list[i].ROTATE_ANGLE;
                        
                    }
                    if (REM_ENEMY != -1)
                    {
                        Enemy.EnemyGenerate(Enemy.list[REM_ENEMY].ENEMY_TYPE);
                        Enemy.list.Remove(Enemy.list[REM_ENEMY]);
                        SOUND_BANK["Explose"].Play();
                    }

                    TIMERS[0] = 0;
                }
                if (TIMERS[1] > 1000)
                {
                    for (int i = 0; i < Enemy.list.Count; i++)
                    {
                        switch (RANDOM_GENERATOR.Next(0, 4))
                        {
                            case 0:
                                if (Enemy.list[i].LOCATION.X != GFj)// && Enemies[i].LOCATION.X != Enemies[j].LOCATION.X - 1 && Enemies[i].LOCATION.X != Snake[j].LOCATION.X - 1)
                                    Enemy.list[i].LOCATION.X += 1;
                                break;
                            case 1:
                                if (Enemy.list[i].LOCATION.X != 0)// && Enemies[i].LOCATION.X != Enemies[j].LOCATION.X + 1 && Enemies[i].LOCATION.X != Snake[j].LOCATION.X + 1)
                                    Enemy.list[i].LOCATION.X -= 1;
                                break;
                            case 2:
                                if (Enemy.list[i].LOCATION.Y != GFi)// && Enemies[i].LOCATION.Y != Enemies[j].LOCATION.Y - 1 && Enemies[i].LOCATION.Y != Snake[j].LOCATION.Y - 1)
                                    Enemy.list[i].LOCATION.Y += 1;
                                break;
                            case 3:
                                if (Enemy.list[i].LOCATION.Y != 0)// && Enemies[i].LOCATION.Y != Enemies[j].LOCATION.Y + 1 && Enemies[i].LOCATION.Y != Snake[j].LOCATION.Y + 1)
                                    Enemy.list[i].LOCATION.Y -= 1;
                                break;
                        }
                    }
                    TIMERS[1] = 0;
                }
                if (TIMERS[2] > 100)
                {
                    
                        for (int i = 0; i < Enemy.list.Count; i++)
                        {
                        if (Enemy.list[i].isShoot)
                        {
                            bool isTrue = true;
                            for (int j = 0; j < Meteor.list.Count; j++)
                            {
                                if (Meteor.list[j].WHO == Enemy.list[i])
                                {
                                    isTrue = false;
                                }
                            }
                            if (isTrue)
                            {
                                DIRRECTION DIR = DIRRECTION.RIGHT;
                                if (PLAYER.SNAKE[0].LOCATION.X <= Enemy.list[i].LOCATION.X && PLAYER.SNAKE[0].LOCATION.Y <= Enemy.list[i].LOCATION.Y)
                                {
                                    if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) > Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.LEFT;
                                    }
                                    else if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) < Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.UP;
                                    }
                                    else
                                    {
                                        int a = RANDOM_GENERATOR.Next(0, 2);
                                        if (a == 0)
                                        {
                                            DIR = DIRRECTION.LEFT;
                                        }
                                        else
                                        {
                                            DIR = DIRRECTION.UP;
                                        }
                                    }
                                }
                                if (PLAYER.SNAKE[0].LOCATION.X > Enemy.list[i].LOCATION.X && PLAYER.SNAKE[0].LOCATION.Y <= Enemy.list[i].LOCATION.Y)
                                {
                                    if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) > Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.RIGHT;
                                    }
                                    else if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) < Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.UP;
                                    }
                                    else
                                    {
                                        int a = RANDOM_GENERATOR.Next(0, 2);
                                        if (a == 0)
                                        {
                                            DIR = DIRRECTION.RIGHT;
                                        }
                                        else
                                        {
                                            DIR = DIRRECTION.UP;
                                        }
                                    }
                                }
                                if (PLAYER.SNAKE[0].LOCATION.X <= Enemy.list[i].LOCATION.X && PLAYER.SNAKE[0].LOCATION.Y > Enemy.list[i].LOCATION.Y)
                                {
                                    if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) > Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.LEFT;
                                    }
                                    else if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) < Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.DOWN;
                                    }
                                    else
                                    {
                                        int a = RANDOM_GENERATOR.Next(0, 2);
                                        if (a == 0)
                                        {
                                            DIR = DIRRECTION.LEFT;
                                        }
                                        else
                                        {
                                            DIR = DIRRECTION.DOWN;
                                        }
                                    }
                                }
                                if (PLAYER.SNAKE[0].LOCATION.X > Enemy.list[i].LOCATION.X && PLAYER.SNAKE[0].LOCATION.Y > Enemy.list[i].LOCATION.Y)
                                {
                                    if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) > Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.RIGHT;
                                    }
                                    else if (Math.Abs(PLAYER.SNAKE[0].LOCATION.X - Enemy.list[i].LOCATION.X) < Math.Abs(PLAYER.SNAKE[0].LOCATION.Y - Enemy.list[i].LOCATION.Y))
                                    {
                                        DIR = DIRRECTION.DOWN;
                                    }
                                    else
                                    {
                                        int a = RANDOM_GENERATOR.Next(0, 2);
                                        if (a == 0)
                                        {
                                            DIR = DIRRECTION.RIGHT;
                                        }
                                        else
                                        {
                                            DIR = DIRRECTION.DOWN;
                                        }
                                    }
                                }

                                Meteor.list.Add(new Meteor(Enemy.list[i], Enemy.list[i].LOCATION, DIR));
                                Enemy.list[i].isShoot = false;
                            }
                        }
                        }
                        int REM_METEOR = -1;
                        for (int i = 0; i < Meteor.list.Count; i++)
                        {
                            Meteor.list[i].Update();
                            if (Meteor.list[i].LOCATION.X > GFj || Meteor.list[i].LOCATION.X == 0 || Meteor.list[i].LOCATION.Y > GFi || Meteor.list[i].LOCATION.Y == 0)
                            {
                                REM_METEOR = i;
                            }
                            for (int j = 0; j < PLAYER.SNAKE.Count; j++)
                            {
                                if(Meteor.list[i].LOCATION == PLAYER.SNAKE[j].LOCATION)
                                {
                                    REM_METEOR = i;
                                    PLAYER.HEALTH -= 10;
                                }
                            }
                            if (PLAYER.IS_ATTACK_KEY_DOWN)
                            {
                                if (new Vector2((int)((Mouse.GetState().X - 32) / 64), (int)((Mouse.GetState().Y - 96) / 64)) == Meteor.list[i].LOCATION)
                                {
                                REM_METEOR = i;
                            }
                            }
                        }
                        if (REM_METEOR != -1)
                        {
                            Meteor.list.Remove(Meteor.list[REM_METEOR]);
                        }
                    TIMERS[2] = 0;
                }
                for (int i = 0; i < Enemy.list.Count; i++)
                {
                    if (Enemy.list[i].gTime > 3000)
                    {
                        Enemy.list[i].isShoot = true;
                        Enemy.list[i].gTime = 0;
                    }
                }
            }
            base.Update(GAME_TIME);
        }

        /// <param name="GAME_TIME">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime GAME_TIME)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SPRITE_BATCH.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,SamplerState.PointWrap,null,RasterizerState.CullNone);
            for (int i = 0; i < GRAPHICS.PreferredBackBufferWidth/100 + 2; i++)
                for (int j = 0; j < GRAPHICS.PreferredBackBufferHeight / 100 + 2; j++)
                    SPRITE_BATCH.Draw(SPRITE_BANK["BackgroundSpace"], new Vector2(i*100,j*100),null,null,new Vector2(SPRITE_BANK["BackgroundSpace"].Width/2, SPRITE_BANK["BackgroundSpace"].Height/2),MathHelper.ToRadians((i+j)*90),new Vector2(1,1));
            SPRITE_BATCH.Draw(SPRITE_BANK["BackgroundEarth"], new Vector2(GRAPHICS.PreferredBackBufferWidth/2,GRAPHICS.PreferredBackBufferHeight/2),null,null, new Vector2(SPRITE_BANK["BackgroundEarth"].Width/2, SPRITE_BANK["BackgroundEarth"].Height/2),0,null);

            for (int i = 0; i < ItemPack.list.Count; i++)
            {
                switch (ItemPack.list[i].ITEMTYPE)
                {
                    case ItemType.Health:
                        SPRITE_BATCH.Draw(SPRITE_BANK["HealthPack"], new Vector2(64 * ItemPack.list[i].LOCATION.X + 64, 64 * ItemPack.list[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["HealthPack"].Width / 2, SPRITE_BANK["HealthPack"].Height / 2), 0, new Vector2(2, 2));
                        break;
                    case ItemType.Segment:
                        SPRITE_BATCH.Draw(SPRITE_BANK["SegmentPack"], new Vector2(64 * ItemPack.list[i].LOCATION.X + 64, 64 * ItemPack.list[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["SegmentPack"].Width / 2, SPRITE_BANK["SegmentPack"].Height / 2), 0, new Vector2(2, 2));
                        break;
                }
            }

            for (int i = 0; i < Enemy.list.Count; i++)
            {
                switch (Enemy.list[i].ENEMY_TYPE)
                {
                    case EnemyType.Planetroid1:
                        SPRITE_BATCH.Draw(SPRITE_BANK["Planetroid1"], new Vector2(64 * Enemy.list[i].LOCATION.X + 64, 64 * Enemy.list[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["Planetroid1"].Width / 2, SPRITE_BANK["Planetroid1"].Height / 2), MathHelper.ToRadians(Enemy.list[i].ROTATE_ANGLE), new Vector2(1, 1));
                        break;
                    case EnemyType.Planetroid2:
                        SPRITE_BATCH.Draw(SPRITE_BANK["Planetroid2"], new Vector2(64 * Enemy.list[i].LOCATION.X + 64, 64 * Enemy.list[i].LOCATION.Y + 128), null, null, new Vector2(SPRITE_BANK["Planetroid2"].Width / 2, SPRITE_BANK["Planetroid2"].Height / 2), MathHelper.ToRadians(Enemy.list[i].ROTATE_ANGLE), new Vector2(1, 1));
                        break;
                }
            }

            for (int i = 0; i < Meteor.list.Count; i++)
            {
                SPRITE_BATCH.Draw(SPRITE_BANK["Meteor1"], new Vector2(64 * Meteor.list[i].LOCATION.X + 64, 64 * Meteor.list[i].LOCATION.Y + 128),null,null, new Vector2(SPRITE_BANK["Meteor1"].Width/2, SPRITE_BANK["Meteor1"].Height/2),MathHelper.ToRadians(Meteor.list[i].ROTATE_ANGLE),new Vector2(2,2));
            }

            PLAYER.Draw(GAME_TIME, SPRITE_BATCH);

            if (!PLAYER.IS_ALIVE)
            {
                SPRITE_BATCH.DrawString(FONT_BANK["GameFont1"], "GAME OVER", new Vector2(GRAPHICS.PreferredBackBufferWidth/2 - 180,GRAPHICS.PreferredBackBufferHeight/2), Color.White);
            }

            SPRITE_BATCH.Draw(SPRITE_BANK["Crosshair"], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), null, null, new Vector2(16, 16), 0, new Vector2(3, 3));
            SPRITE_BATCH.DrawString(FONT_BANK["GameFont1"], "Health:" + PLAYER.HEALTH, new Vector2(0,0), Color.White);
            SPRITE_BATCH.DrawString(FONT_BANK["GameFont1"], "Score: " + String.Format("{0:000000000}", PLAYER.SCORE), new Vector2(GRAPHICS.PreferredBackBufferWidth-620,0), Color.White);
            SPRITE_BATCH.DrawString(FONT_BANK["GameFont1"], "Kill Count: " + String.Format("{0:00000}", PLAYER.KILL_COUNT), new Vector2(0, GRAPHICS.PreferredBackBufferHeight - 100), Color.White);
            SPRITE_BATCH.End();

            base.Draw(GAME_TIME);
        }
    }

    public enum DIRRECTION
    {
        UP, LEFT, RIGHT, DOWN
    }

    public enum BODY_TYPE
    {
        HEAD, BODY, TALE
    }
}
