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
    public class StringBuilder : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont font;
        private string message;
        private Vector2 pos;
        private Color colour;

        public string Message { get => message; set => message = value; }

        public StringBuilder(Game game, SpriteBatch sb, SpriteFont font, string message, Vector2 pos, Color colour) : base(game)
        {
            this.sb = sb;
            this.font = font;
            this.message = message;
            this.pos = pos;
            this.colour = colour;
        }

        public void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Enabled)
            {
                sb.Begin();
                sb.DrawString(font, message, pos, colour);
                sb.End();
                base.Draw(gameTime);
            }
            
        }

        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
        }
    }
}
