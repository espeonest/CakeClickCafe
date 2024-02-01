using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class Cake : DrawableGameComponent
    {
        
        private int clickDelay = 10;
        private int delayCounter = 0;

        private SpriteBatch sb;
        private Texture2D img;
        private Rectangle crop;
        private Vector2 destination;
        private float scale;
        private float scaleInitial;
        private float scaleGrow;

        private MouseState ms;
        private MouseState prevState;
        public Cake(Game game, SpriteBatch sb,  Rectangle crop, Vector2 destination, float scale) : base(game)
        {
            this.sb = sb;
            this.img = Shared.img;
            this.crop = crop;
            this.destination = destination;
            this.scale = scale;
            this.scaleInitial = scale;
            this.scaleGrow = scale * 1.1f;
        }


        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.Draw(img, destination, crop, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Shared.menuLayer);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            if(delayCounter >= clickDelay)
            {
                if (ms.X >= ClickerScene.cornerX && ms.Y >= ClickerScene.cornerY && ms.X <= ClickerScene.cornerX + crop.Width*scaleInitial && ms.Y <= ClickerScene.cornerY + crop.Height*scaleInitial && ms.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
                {
                    scale = scaleGrow;
                    destination.X = ClickerScene.cornerX - 20;
                    destination.Y = ClickerScene.cornerY - 14;
                    ClickerScene.wallet += (float)(1 * Math.Ceiling(ClickerScene.modifier)); // whole #s only!
                    delayCounter = 0;
                }
                else
                {
                    scale = scaleInitial;
                    destination.X = ClickerScene.cornerX;
                    destination.Y = ClickerScene.cornerY;
                }
            }
            delayCounter++;
            prevState = ms;

            base.Update(gameTime);
        }
    }
}
