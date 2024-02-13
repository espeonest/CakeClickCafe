using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class Alert : DrawableGameComponent
    {
        // a message appears in the top right of the screen for a few seconds and automatically goes away
        // would be nice to have a fade out effect.
        private const float fadeSpeed = 0.02f;
        private const float delay = 90;
        private float counter;
        private SpriteBatch sb;
        public Vector2 dest;
        public SpriteFont font;
        public SpriteFont regularFont;
        public SpriteFont smallFont;
        public Color colour;
        public float opacity;
        public string message;
        public Vector2 topCorner;
        public float scale;
        public Alert(Game game, SpriteBatch sb, float scale) : base(game)
        {
            this.sb = sb;
            this.scale = scale;
            topCorner = new Vector2(Shared.stage.X / 7 * 4, Shared.stage.Y / 30);
            colour = Color.Black;
            regularFont = game.Content.Load<SpriteFont>("fonts/regular");
            smallFont = game.Content.Load<SpriteFont>("fonts/small");
            // new Rectangle(Shared.stage.X / 7 * 4, Shared.stage.Y / 30, (Shared.alertRect.width*menuUiScale), (Shared.alertRect.height*menuUiScale);
        }
        public void Display(string message, Color colour)
        {
            this.Enabled = true;
            this.Visible = true;
            opacity = 1;
            counter = 0;
            this.message = message;
            this.colour = colour;
            if (regularFont.MeasureString(message).X > (49*scale))
            {
                font = smallFont;
            }
            else
            {
                font = regularFont;
            }
            // may need to change into calculation if alerts end up more complicated than 1-2 lines
            dest = new Vector2(topCorner.X + (Shared.stage.X * 28 / 1200), topCorner.Y + (Shared.stage.Y * 42 / 1200));
        }

        public override void Update(GameTime gameTime)
        {
            if (opacity <= 0)
            {
                counter = 0;
                this.Enabled = false;
                this.Visible = false;
            }
            if (this.Enabled)
            {
                counter++;
                if (counter >= delay)
                {
                    opacity -= fadeSpeed;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.Draw(Shared.uiImg, topCorner, Shared.alertRect, new Color(255, 255, 255, opacity), 0, Vector2.Zero, scale, SpriteEffects.None, Shared.overlayComponentsLayer);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            sb.DrawString(font, message, dest, new Color(colour.R, colour.G, colour.B, opacity));
            sb.End();

            base.Draw(gameTime);
        }
    }
}
