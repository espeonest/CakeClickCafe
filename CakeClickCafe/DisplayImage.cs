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
    public class DisplayImage : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D img;
        private Rectangle crop;
        private Vector2 destination;
        private float scale;
        private float layer;
        public float Scale { get => scale; set => scale = value; }
        public DisplayImage(Game game, SpriteBatch sb, Texture2D img, Rectangle crop, Vector2 destination, float scale, float layer) : base(game)
        {
            this.sb = sb;
            this.img = img;
            this.crop = crop;
            this.destination = destination;
            this.scale = scale;
            this.layer = layer;
        }


        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.Draw(img, destination, crop, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layer);
            sb.End();
            base.Draw(gameTime);
        }

        // shouldn't need updates since this class is for static images
        // children can add one if they want ig
    }
}
