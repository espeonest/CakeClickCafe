using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CakeClickCafe
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ClickerScene clickerScene;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Debug.WriteLine("constructor called");
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1200;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // So I want to have a resize option that increases or decreases the game window by a fixed amount so the res remains square
            // this would be part of a general options menu that I have yet to plan out ^^;
            // so ignore for now
            //Window.AllowUserResizing = true;
            Shared.stage = new Vector2((int)_graphics.PreferredBackBufferWidth, (int)_graphics.PreferredBackBufferHeight);
            Shared.img = this.Content.Load<Texture2D>("images/spritesheet");
            Shared.menuImg = this.Content.Load<Texture2D>("images/menuItems");
            Shared.uiImg = this.Content.Load<Texture2D>("images/ui");
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D cursor = this.Content.Load<Texture2D>("images/cursor");
            MouseCursor cakeCursor = MouseCursor.FromTexture2D(cursor, 0, 0);
            Mouse.SetCursor(cakeCursor);
            clickerScene = new ClickerScene(this, _spriteBatch);
            this.Components.Add(clickerScene);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Debug.Write(Window.ClientBounds.ToString());
            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}