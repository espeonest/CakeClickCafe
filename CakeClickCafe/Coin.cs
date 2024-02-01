using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    internal class Coin : DrawableGameComponent
    {
        SpriteBatch sb;
        private Rectangle[] frames = { Shared.defaultFrame, Shared.secondFrame, Shared.thirdFrame, Shared.fourthFrame, Shared.fifthFrame };
        private Texture2D img;
        private Vector2 destination;
        private float scale = 4;

        private int delay = 6;
        private int delayCounter;
        private int defaultFrameDelay = 4;
        private int defaultFrameCounter;
        private int frameIndex = 0;
        public Coin(Game game, SpriteBatch sb, Vector2 destination) : base(game)
        {
            this.sb = sb;
            this.destination = destination;
            img = Shared.img;
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.Draw(img, destination, frames[frameIndex], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (delayCounter >= delay)
            {
                if(frameIndex == 0 && defaultFrameCounter < defaultFrameDelay)
                {
                    defaultFrameCounter++;
                }
                else if (frameIndex < 4)
                {
                    frameIndex++;
                    defaultFrameCounter = 0;
                }
                else
                {
                    frameIndex = 0;
                }
                destination.X = 76 - frames[frameIndex].Width*scale / 2;
                delayCounter = 0;
            }
            delayCounter++;
            base.Update(gameTime);
        }
    }
}
