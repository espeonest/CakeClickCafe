using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class WalletString : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont font;
        private string message;
        private Vector2 pos;
        private Color colour;

        public WalletString(Game game, SpriteBatch sb, SpriteFont font, string message, Vector2 pos, Color colour) : base(game)
        {
            this.sb = sb;
            this.font = font;
            this.message = message;
            this.pos = pos;
            this.colour = colour;
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(font, message, pos, colour);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            message = "Coins: " + ClickerScene.wallet;
            // center aligns
            // pos.X = GameStuff.stage.X / 2 - font.MeasureString(message).X / 2;
            base.Update(gameTime);
        }
    }
}
