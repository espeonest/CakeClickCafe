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
    public class ClickerScene : GameScene
    {
        private SpriteBatch sb;
        private Cake cake;
        // cake obj should probably receive mod and wallet
        public static float coinsPerClick;
        public static float wallet;
        // notes for wallet:
        // numbers display 6 digits max, afterwards begin rounding
        // with the full word written next to it to show size
        // e.g. 1.36712 Million
        // string width will vary wildly, would be best if left aligned I think?
        private Overlay overlay;
        private SpriteFont walletFont;
        private WalletString coinsDisplay;



        public static float cornerX;
        public static float cornerY;

        private Rectangle cakeSource = new Rectangle(1, 0, 65, 42);
        private Vector2 cakeDest;
        private float cakeScale = 6.8f;

        private Vector2 counterStringDest = new Vector2(Shared.stage.X / 8, Shared.stage.Y / 37.5f);
        private Vector2 coinPos;

        private Rectangle menuSource = new Rectangle(4, 126, 248, 128);
        private float menuScale = Shared.stage.X / (1200 / 4.7f);
        public float menuUiScale = Shared.stage.X / (1200 / 9.4f);
        private Vector2 menuDest;

        private Vector2 gridDest;

        public Rectangle upButtonDest;
        public Rectangle downButtonDest;
        MenuButton upNav;
        MenuButton downNav;


        public float buysellScale = Shared.stage.X / (1000 / 6);
        public Rectangle[] buyDest;
        public Rectangle[] sellDest;
        private List<MenuButton> buyButtons;
        private List<MenuButton> sellButtons;
        private Shared.BuySellMode mode;

        public List<ClickModifier> modifiers;
        public int modifierIndex;
        public int menuIndex;
        public Alert alert;

        public ClickerScene(Game game, SpriteBatch sb) : base(game)
        {
            this.Show();
            this.sb = sb;
            // should start at zero normally
            wallet = 250000000;
            coinsPerClick = 1;
            DisplayImage background = new DisplayImage(game, sb, game.Content.Load<Texture2D>("images/sky"), new Rectangle(0, 0, 256, 256), Vector2.Zero, 5, Shared.bgLayer);
            game.Components.Add(background);

            cornerX = Shared.stage.X / 2 - cakeScale * cakeSource.Width / 2;
            cornerY = (Shared.stage.Y / 2 - cakeScale * cakeSource.Height / 2) - Shared.stage.Y / 7.5f;
            cakeDest = new Vector2(cornerX, cornerY);

            cake = new Cake(game, sb, cakeSource, cakeDest, cakeScale);
            game.Components.Add(cake);

            walletFont = game.Content.Load<SpriteFont>("fonts/coinCount");
            coinsDisplay = new WalletString(game, sb, walletFont, "GAME BEGINS", counterStringDest, Color.Black);
            game.Components.Add(coinsDisplay);
            coinPos = new Vector2(Shared.stage.X / 40, Shared.stage.Y / 40);
            Coin coin = new Coin(game, sb, coinPos);
            game.Components.Add(coin);

            menuDest = new Vector2(Shared.stage.X / 2 - (menuScale * menuSource.Width) / 2, Shared.stage.Y - (menuScale * menuSource.Height + (Shared.stage.Y * 10 / 1200)));
            gridDest = new Vector2(menuDest.X + (Shared.stage.X / 120) * menuScale, menuDest.Y + (Shared.stage.Y / (1200 / 22)) * menuScale);
            DisplayImage menu = new DisplayImage(game, sb, Shared.miscImg, menuSource, menuDest, menuScale, Shared.menuLayer);
            DisplayImage menuGrid = new DisplayImage(game, sb, Shared.uiImg, Shared.menuGridRect, gridDest, menuUiScale, Shared.menuComponentsLayer);
            game.Components.Add(menu);
            game.Components.Add(menuGrid);
            upButtonDest = new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale), (int)(gridDest.Y), (int)(Shared.upButton.Width * menuUiScale), (int)(Shared.upButton.Height * menuUiScale));
            downButtonDest = new Rectangle(upButtonDest.X, upButtonDest.Y + upButtonDest.Height, (int)(Shared.downButton.Width * menuUiScale), (int)(Shared.downButton.Height * menuUiScale));
            upNav = new MenuButton(game, sb, Shared.uiImg, Shared.upButton, upButtonDest, Shared.menuComponentsLayer);
            downNav = new MenuButton(game, sb, Shared.uiImg, Shared.downButton, downButtonDest, Shared.menuComponentsLayer);
            game.Components.Add(upNav);
            game.Components.Add(downNav);

            #region menu items declared
            ClickModifier tea = new ClickModifier(game, sb, 50, 150, 10, Shared.foodImg, "Tea", Shared.tea, Shared.MenuPos.tLeft);
            ClickModifier latte = new ClickModifier(game, sb, 2000, 4500, 50, Shared.foodImg, "Latte", Shared.latte, Shared.MenuPos.tRight);
            ClickModifier cinnamonRoll = new ClickModifier(game, sb, 30000, 55000, 250, Shared.foodImg, "Cinnamon Roll", Shared.cinnamonRoll, Shared.MenuPos.bLeft);
            ClickModifier matchaLatte = new ClickModifier(game, sb, 150000, 675000, 800, Shared.foodImg, "Matcha Latte", Shared.matchaLatte, Shared.MenuPos.bRight);
            ClickModifier croissant = new ClickModifier(game, sb, 870000, 2000000, 1250, Shared.foodImg, "Croissant", Shared.croissant, Shared.MenuPos.none);
            ClickModifier icedCoffee = new ClickModifier(game, sb, 1500000, 7000000, 2400, Shared.foodImg, "Iced Coffee", Shared.icedCoffee, Shared.MenuPos.none);
            ClickModifier iceCream = new ClickModifier(game, sb, 450000000, 2750000000, 25000, Shared.foodImg, "Ice Cream", Shared.iceCream, Shared.MenuPos.none);
            ClickModifier rootBeerFloat = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Root Beer Float", Shared.rootBeerFloat, Shared.MenuPos.none);
            ClickModifier cupcake = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Cupcake", Shared.cupcake, Shared.MenuPos.none);
            ClickModifier chocStrawb = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Choc Strawbs", Shared.chocolateStrawberry, Shared.MenuPos.none);
            ClickModifier jello = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Jello", Shared.jello, Shared.MenuPos.none);
            ClickModifier flan = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Flan", Shared.flan, Shared.MenuPos.none);
            ClickModifier macaron = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Macaron", Shared.macaron, Shared.MenuPos.none);
            ClickModifier fruitTart = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Fruit Tart", Shared.fruitTart, Shared.MenuPos.none);
            ClickModifier brownie = new ClickModifier(game, sb, 50, 75, 0, Shared.foodImg, "Brownie", Shared.fruitTart, Shared.MenuPos.none);
            ClickModifier[] modifierArray = { tea, latte, cinnamonRoll, matchaLatte, croissant, icedCoffee, iceCream, rootBeerFloat, cupcake, chocStrawb, jello, flan, macaron, fruitTart, brownie };
            #endregion
            modifiers = new List<ClickModifier>();
            modifiers.AddRange(modifierArray);
            foreach (ClickModifier cm in modifiers)
            {
                coinsPerClick += cm.currentModifier;
                game.Components.Add(cm);
            }
            menuIndex = 0;
            #region evil
            buyDest = new Rectangle[] { new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 670 / 1200)), (int)(gridDest.Y + (Shared.stage.Y * 25 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 210 / 1200)), (int)(gridDest.Y + 25), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 670 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale * 2 + (Shared.stage.Y * 68 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 210 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale * 2 + (Shared.stage.Y * 68 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)) };
            sellDest = new Rectangle[] { new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 670 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale + (Shared.stage.Y * 40 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 210 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale + (Shared.stage.Y * 40 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 670 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale * 3 + (Shared.stage.Y * 83 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)), new Rectangle((int)(gridDest.X + Shared.menuGridRect.Width * menuUiScale - (Shared.stage.X * 210 / 1200)), (int)(gridDest.Y + Shared.buyButton.Height * buysellScale * 3 + (Shared.stage.Y * 83 / 1200)), (int)(Shared.buyButton.Width * buysellScale), (int)(Shared.buyButton.Height * buysellScale)) };
            #endregion
            buyButtons = new List<MenuButton>();
            sellButtons = new List<MenuButton>();
            for (int i = 0; i < 4; i++)
            {
                MenuButton buyButton = new MenuButton(game, sb, Shared.uiImg, Shared.buyButton, buyDest[i], Shared.menuComponentsLayer);
                MenuButton sellButton = new MenuButton(game, sb, Shared.uiImg, Shared.sellButton, sellDest[i], Shared.menuComponentsLayer);
                game.Components.Add(buyButton);
                buyButtons.Add(buyButton);
                game.Components.Add(sellButton);
                sellButtons.Add(sellButton);
            }

            #region overlay stuff
            overlay = new Overlay(game, sb);
            // hide it!
            overlay.Hide();
            #endregion
            alert = new Alert(game, sb, menuUiScale);
            game.Components.Add(alert);
        }


        public override void Update(GameTime gameTime)
        {
            if (upNav.Clicked)
            {
                UpClick();
            }
            if (downNav.Clicked)
            {
                DownClick();
            }
            for (int i = 0; i < 4; i++)
            {
                if (buyButtons[i].Clicked)
                {
                    BuyClick(i);
                }
                else if (sellButtons[i].Clicked)
                {
                    SellClick(i);
                }
            }

            #region overlay functionality
            // checks to see if item can be bought
            // message in case it can't, message if successful

            // vice versa for sold


            if (overlay.Enabled)
            {
                if (modifiers[modifierIndex].amountOwned == 0)
                {
                    overlay.menuItem.amountOwned = 0;
                }
                else
                {
                    overlay.menuItem.amountOwned = 1;
                }
                if (overlay.cancelButton.Clicked)
                {
                    overlay.Hide();
                    overlay.cancelButton.Clicked = false;
                }
                if (overlay.confirmButton.Clicked)
                {
                    // for buy mode only
                    if (modifiers[modifierIndex].IsBuyable() && mode==Shared.BuySellMode.buy)
                    {
                        modifiers[modifierIndex].Buy();
                        overlay.menuItem.clickMultiplier = modifiers[modifierIndex].clickMultiplier;
                        overlay.itemDetails.Message = overlay.DetailText(modifiers[modifierIndex].amountOwned, modifiers[modifierIndex].currentPrice, modifiers[modifierIndex].currentModifier, Shared.BuySellMode.buy);
                        coinsPerClick += modifiers[modifierIndex].clickMultiplier;
                        alert.Display("Bought " + modifiers[modifierIndex].Name, Color.Green);
                    }
                    else if (modifiers[modifierIndex].amountOwned > 0 && mode == Shared.BuySellMode.sell)
                    {
                        modifiers[modifierIndex].Sell();
                        if (modifiers[modifierIndex].amountOwned == 0)
                        {
                            overlay.menuItem.amountOwned = 0;
                        }
                        overlay.menuItem.clickMultiplier = modifiers[modifierIndex].clickMultiplier;
                        overlay.itemDetails.Message = overlay.DetailText(modifiers[modifierIndex].amountOwned, modifiers[modifierIndex].sellPrice, modifiers[modifierIndex].currentModifier, Shared.BuySellMode.sell);
                        coinsPerClick -= modifiers[modifierIndex].clickMultiplier;
                        alert.Display("Sold " + modifiers[modifierIndex].Name, Color.Green);
                    }
                    else
                    {
                        Debug.WriteLine("invalid attempt");
                        if (mode == Shared.BuySellMode.buy)
                        {
                            alert.Display(modifiers[modifierIndex].Name + " is\ntoo expensive.", Color.Red);
                        }
                        else if (mode == Shared.BuySellMode.sell)
                        {
                            alert.Display("No " + modifiers[modifierIndex].Name + "\nto sell.", Color.Red);
                        }
                    }
                    overlay.confirmButton.Clicked = false;
                }
            }
            #endregion

            //int maxIndex = modifiers.Count / 2 - 2;
            int maxIndex = 13;
            if (menuIndex == 0)
            {
                // todo: make button greyed out if cannot go up or down anymore
            }
            else if (menuIndex == maxIndex)
            {
                // see above
            }
            base.Update(gameTime);
        }

        public void UpClick()
        {
            if (menuIndex != 0)
            {
                menuIndex--;
                modifiers[menuIndex * 2].Position = Shared.MenuPos.tLeft;
                modifiers[menuIndex * 2 + 1].Position = Shared.MenuPos.tRight;
                modifiers[menuIndex * 2 + 2].Position = Shared.MenuPos.bLeft;
                modifiers[menuIndex * 2 + 3].Position = Shared.MenuPos.bRight;
                modifiers[menuIndex * 2 + 4].Position = Shared.MenuPos.none;
                modifiers[menuIndex * 2 + 5].Position = Shared.MenuPos.none;
            }
        }

        public void DownClick()
        {
            // need to have an if-else for when there aren't an even number of items to display
            // so that the other slots on the menu aren't occupied and the buttons aren't visible
            int maxIndex = modifiers.Count / 2 - 2;
            if (menuIndex != maxIndex)
            {
                menuIndex++;
                modifiers[menuIndex * 2 - 2].Position = Shared.MenuPos.none;
                modifiers[menuIndex * 2 - 1].Position = Shared.MenuPos.none;
                modifiers[menuIndex * 2].Position = Shared.MenuPos.tLeft;
                modifiers[menuIndex * 2 + 1].Position = Shared.MenuPos.tRight;
                modifiers[menuIndex * 2 + 2].Position = Shared.MenuPos.bLeft;
                modifiers[menuIndex * 2 + 3].Position = Shared.MenuPos.bRight;
            }
        }
                
        public void OverlayActivator(ClickModifier cm, Shared.BuySellMode mode)
        {
            this.mode = mode;
            float coins = 0;
            if(mode == Shared.BuySellMode.buy)
            {
                coins = cm.currentPrice;
            }
            else if (mode == Shared.BuySellMode.sell)
            {
                coins = cm.sellPrice;
            }
            modifierIndex = modifiers.IndexOf(cm);
            overlay.menuItem.Crop = cm.Crop;
            overlay.menuItem.Name = cm.Name;
            overlay.itemName.Message = cm.Name;
            overlay.menuItem.clickMultiplier = cm.clickMultiplier;
            overlay.itemDetails.Message = overlay.DetailText(cm.amountOwned, coins, cm.currentModifier, mode);
            overlay.multiplierInfo.Message = "+" + Shared.NumberFormatter(cm.clickMultiplier) + " per\nitem owned.";
            overlay.Show();
        }

        

        public void BuyClick(int index)
        {
            switch (index)
            {
                case 0:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if(cm.Position == Shared.MenuPos.tLeft)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.buy);
                        }
                    }
                    break;
                case 1:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.tRight)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.buy);
                        }
                    }
                    break;
                case 2:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.bLeft)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.buy);
                        }
                    }
                    break;
                case 3:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.bRight)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.buy);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void SellClick(int index)
        {
            switch (index)
            {
                case 0:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.tLeft)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.sell);
                        }
                    }
                    break;
                case 1:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.tRight)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.sell);
                        }
                    }
                    break;
                case 2:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.bLeft)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.sell);
                        }
                    }
                    break;
                case 3:
                    foreach (ClickModifier cm in modifiers)
                    {
                        if (cm.Position == Shared.MenuPos.bRight)
                        {
                            OverlayActivator(cm, Shared.BuySellMode.sell);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
