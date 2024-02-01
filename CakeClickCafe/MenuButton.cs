using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class MenuButton : DrawableGameComponent
    {
        private MouseState prev;
        private Color overlay;

        private SpriteBatch sb;
        private Texture2D img;
        private Rectangle crop;
        private Rectangle destination;
        private float layer;

        private bool clicked;
        public bool Clicked { get => clicked; set => clicked = value; }

        public MenuButton(Game game, SpriteBatch sb, Texture2D img, Rectangle crop, Rectangle destination, float layer) : base(game)
        {
            this.sb = sb;
            this.img = img;
            this.crop = crop;
            this.destination = destination;
            this.layer = layer;
            overlay = Color.White;
        }


        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.Draw(img, destination, crop, overlay, 0, Vector2.Zero, SpriteEffects.None, layer);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (destination.Contains(ms.Position) && ms.LeftButton == ButtonState.Pressed)
            {
                overlay = Color.Gray;
            } else
            {
                overlay = Color.White;
            }
            if (destination.Contains(ms.Position) && ms.LeftButton == ButtonState.Released && prev.LeftButton == ButtonState.Pressed)
            {
                clicked = true;
            }
            else
            {
                clicked = false;
            }
            prev = ms;
            base.Update(gameTime);
        }
    }
}
