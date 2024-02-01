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
    public class Overlay : DrawableGameComponent
    {
        public SpriteBatch sb;
        public float overlayScale = Shared.stage.X * 6/1200;
        public Vector2 overlayDest;
        public Rectangle confirmDest;
        public Rectangle cancelDest;
        public Shared.BuySellMode mode;
        public StringBuilder itemName;
        public StringBuilder itemDetails;
        public StringBuilder multiplierInfo;
        public string details;
        public ClickModifier menuItem;
        public MenuButton confirmButton;
        public MenuButton cancelButton;
        private DisplayImage overlay;
        private SpriteFont regFont;
        private SpriteFont headFont;
        private List<DrawableGameComponent> overlayComponents;

        // TODO:
        //  - string layouts (confirm, cancel, item name, etc)
        //  - buy/sell modes
        //  - link to ClickModifier and all that, may need to move around some functionality, we will see
        //  - position buttons, give functionality
        public Overlay(Game game, SpriteBatch sb, ClickModifier menuItem) : base(game)
        {
            this.sb = sb;
            this.menuItem = new ClickModifier(game, sb, menuItem.baseCost, menuItem.costMultiplier, menuItem.clickMultiplier, Shared.img, menuItem.Name, menuItem.Crop, Shared.MenuPos.overlay);
            regFont = game.Content.Load<SpriteFont>("fonts/regular");
            headFont = game.Content.Load<SpriteFont>("fonts/heading");
            overlayDest = new Vector2(Shared.stage.X / 2 - Shared.overlay.Width * overlayScale / 2, Shared.stage.Y / 2 - Shared.overlay.Height * overlayScale / 2);
            overlay = new DisplayImage(game, sb, Shared.uiImg, Shared.overlay, overlayDest, overlayScale, Shared.overlayLayer);

            // need: current price, amount owned, modifier per, current total modifier
            // not in that order
            // I'll try a single string w line breaks?
            Vector2 namePos = new Vector2(overlayDest.X + (Shared.stage.X * 74/1200), overlayDest.Y + (Shared.stage.Y * 28/1200));
            itemName = new StringBuilder(game, sb, headFont, this.menuItem.Name, namePos, Color.Black);
            Vector2 multPos = new Vector2(overlayDest.X + (Shared.stage.X * 460 / 1200), overlayDest.Y + (Shared.stage.Y * 210 / 1200));
            multiplierInfo = new StringBuilder(game, sb, regFont, "+" + this.menuItem.clickMultiplier.ToString() + " per\nitem owned.", multPos, Color.Black);
            Vector2 detailsPos = new Vector2(overlayDest.X + (Shared.stage.X * 74 / 1200), overlayDest.Y + (Shared.stage.Y * 500 / 1200));
            itemDetails = new StringBuilder(game, sb, regFont, "TEST\n\nTEST\n\nTEST", detailsPos, Color.Black);


            confirmDest = new Rectangle((int)(overlayDest.X + Shared.overlay.Width*overlayScale - (Shared.confirmButton.Width * overlayScale + 60)), (int)(overlayDest.Y + Shared.overlay.Height*overlayScale - (Shared.confirmButton.Height * overlayScale * 2 + 80)), (int)(Shared.confirmButton.Width * overlayScale), (int)(Shared.confirmButton.Height * overlayScale));
            cancelDest = new Rectangle((int)(overlayDest.X + Shared.overlay.Width * overlayScale - (Shared.cancelButton.Width * overlayScale + 60)), (int)(overlayDest.Y + Shared.overlay.Height * overlayScale - (Shared.cancelButton.Height * overlayScale + 60)), (int)(Shared.cancelButton.Width * overlayScale), (int)(Shared.cancelButton.Height * overlayScale));
            confirmButton = new MenuButton(game, sb, Shared.uiImg, Shared.confirmButton, confirmDest, Shared.overlayComponentsLayer);
            cancelButton = new MenuButton(game, sb, Shared.uiImg, Shared.cancelButton, cancelDest, Shared.overlayComponentsLayer);
            overlayComponents = new List<DrawableGameComponent>
            {
                overlay,
                itemName,
                multiplierInfo,
                itemDetails,
                confirmButton,
                cancelButton,
                this.menuItem
            };
            foreach (DrawableGameComponent component in overlayComponents)
            {
                game.Components.Add(component);
            }

        }

        public string DetailText(int count, float coins, float multiplier, Shared.BuySellMode mode)
        {
            string message = "";
            if(mode == Shared.BuySellMode.buy)
            {
                message = $"Number owned: {count}\n\nTotal bonus: {multiplier}\n\nCost: {coins}";
            } else if(mode == Shared.BuySellMode.sell)
            {
                message = $"Number owned: {count}\n\nTotal bonus: {multiplier}\n\nSell price: {coins}";
            }
            return message;
        }

        public void Show()
        {
            foreach (DrawableGameComponent component in overlayComponents)
            {
                component.Visible = true;
                component.Enabled = true;
            }
            this.Enabled = true;
            this.Visible = true;
        }

        public void Hide()
        {
            foreach (DrawableGameComponent component in overlayComponents)
            {
                component.Visible = false;
                component.Enabled = false;
            }
            this.Enabled = false;
            this.Visible = false;
        }

        //public override void Draw(GameTime gameTime)
        //{
        //    base.Draw(gameTime);
        //}

        //public override void Update(GameTime gameTime)
        //{
            
        //    base.Update(gameTime);
        //}
    }
}
