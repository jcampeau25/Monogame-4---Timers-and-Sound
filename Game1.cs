using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Monogmae_4___Timers_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture, explosionTexture, pliersTexture;
        Rectangle bombRect, wireRect, explosionRect, pliersRect;

        SpriteFont timefont;

        SoundEffect explode;

        float seconds, postBoom;

        bool boom = false;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            bombRect = new Rectangle(50, 50, 700, 400);
            wireRect = new Rectangle(490, 160, 160, 15);
            explosionRect = new Rectangle(70, 50, 600, 400);
            pliersRect = new Rectangle(mouseState.X, mouseState.Y, 100, 100);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            seconds = 0f;
            postBoom = 0f;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("boom");
            pliersTexture = Content.Load<Texture2D>("pliers");
            timefont = Content.Load<SpriteFont>("TimeFont");
            explode = Content.Load<SoundEffect>("explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            //this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            this.Window.Title = postBoom.ToString();
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 15)
            {
                boom = true;
                seconds = 0;
                explode.Play();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            if (postBoom > 11)
                Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timefont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            if (boom == true)
            {

                postBoom += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
                
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
