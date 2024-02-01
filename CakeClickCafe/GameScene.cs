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
    public class GameScene : DrawableGameComponent
    {        
        public List<GameComponent> Components { get; set; }

        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        protected GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            Hide();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
    }
}
