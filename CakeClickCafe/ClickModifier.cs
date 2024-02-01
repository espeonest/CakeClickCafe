using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class ClickModifier : DrawableGameComponent
    {
        public Texture2D Img { get => img; set => img = value; }
        public string Name { get => name; set => name = value; }
        public long baseCost { get; set; }
        public float costMultiplier { get; set; }
        public float clickMultiplier { get; set; }
        public int amountOwned { get; set; }
        public float currentModifier { get; set; }
        public Rectangle Destination { get => destination; set => destination = value; }
        public Shared.MenuPos Position { get; set; }
        public Rectangle Crop { get => crop; set => crop = value; }

        private SpriteBatch sb;
        private Texture2D img;
        private string name;
        private Rectangle crop;
        private Rectangle destination;
        private float defaultScale;
        private float imgScale;
        private float layer;
        public float currentPrice;
        public float sellPrice;

        public ClickModifier(Game game, SpriteBatch sb, long baseCost, float costMultiplier, float clickMultiplier, Texture2D img, string name, Rectangle crop, Shared.MenuPos position) : base(game)
        {
            amountOwned = 0; //temporarily set to 1 so they have visible icon
            this.sb = sb;
            this.baseCost = baseCost;
            this.costMultiplier = costMultiplier;
            this.clickMultiplier = clickMultiplier;
            this.img = img;
            this.name = name;
            this.crop = crop;
            this.Position = position;
            if (188f / crop.Height * crop.Width > 200)
            {
                defaultScale = 200f / crop.Width;
            }
            else
            {
                defaultScale = 188f / crop.Height;
            }
            imgScale = defaultScale;
            CalcValues();
        }

        public void CalcValues()
        {
            sellPrice = (int)((baseCost + costMultiplier * (float)Math.Pow(amountOwned - 1, 2)) * 0.75f);
            currentPrice = baseCost + costMultiplier * (float)Math.Pow(amountOwned,2);
            currentModifier = amountOwned * clickMultiplier;
        }

        private void SetPosition()
        {
            switch (Position)
            {
                case Shared.MenuPos.none:
                    destination = new Rectangle();
                    layer = 2;
                    break;
                case Shared.MenuPos.tLeft:
                    destination = new Rectangle(88, (int)(810 - crop.Height * imgScale / 2), (int)(crop.Width * imgScale), (int)(crop.Height * imgScale));
                    layer = Shared.menuComponentsLayer;
                    break;
                case Shared.MenuPos.tRight:
                    destination = new Rectangle(552, (int)(810 - crop.Height * imgScale / 2), (int)(crop.Width * imgScale), (int)(crop.Height * imgScale));
                    layer = Shared.menuComponentsLayer;
                    break;
                case Shared.MenuPos.bLeft:
                    destination = new Rectangle(88, (int)(1024 - crop.Height * imgScale / 2), (int)(crop.Width * imgScale), (int)(crop.Height * imgScale));
                    layer = Shared.menuComponentsLayer;
                    break;
                case Shared.MenuPos.bRight:
                    destination = new Rectangle(552, (int)(1024 - crop.Height * imgScale / 2), (int)(crop.Width * imgScale), (int)(crop.Height * imgScale));
                    layer = Shared.menuComponentsLayer;
                    break;
                case Shared.MenuPos.overlay:
                    // note: change to a calculation based on size of square. Getting inconsistent results
                    imgScale = defaultScale * 1.3f;
                    destination = new Rectangle((int)(422 - crop.Width * imgScale / 2), (int)(470 - crop.Height * imgScale / 2), (int)(crop.Width * imgScale), (int)(crop.Height * imgScale));
                    layer = Shared.overlayComponentsLayer;
                    break;
                default:
                    break;
            }
        }

        public bool IsBuyable()
        {
            if (ClickerScene.wallet >= currentPrice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Buy()
        {
            if (IsBuyable())
            {
                ClickerScene.wallet -= (long)currentPrice;
                amountOwned++;
                CalcValues();
            }
        }

        public void Sell()
        {
            if (amountOwned > 0)
            {
                ClickerScene.wallet += sellPrice;
                amountOwned--;
                CalcValues();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            if (amountOwned > 0)
            {
                sb.Draw(img, destination, crop, Color.White, 0, Vector2.Zero, SpriteEffects.None, layer);
            }
            else
            {
                sb.Draw(img, destination, crop, Color.Black, 0, Vector2.Zero, SpriteEffects.None, layer);
            }
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            SetPosition();
            base.Update(gameTime);
        }
    }
}
