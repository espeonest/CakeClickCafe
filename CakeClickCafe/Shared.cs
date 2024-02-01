using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeClickCafe
{
    public class Shared
    {
        public enum BuySellMode { buy, sell }
        public enum MenuPos { none, tLeft, tRight, bLeft, bRight, overlay}

        public static Vector2 stage;
        public static Texture2D img;
        public static Texture2D menuImg;
        public static Texture2D uiImg;

        #region layers
        public static float bgLayer = 0;
        public static float menuLayer = 0.1f;
        public static float menuComponentsLayer = 0.2f;
        public static float overlayLayer = 0.9f;
        public static float overlayComponentsLayer = 1;
        #endregion

        #region coin rectangles
        public static Rectangle defaultFrame = new Rectangle(94, 58, 23, 22);
        public static Rectangle secondFrame = new Rectangle(119, 57, 15, 23);
        public static Rectangle thirdFrame = new Rectangle(83, 57, 4, 23);
        public static Rectangle fourthFrame = new Rectangle(98, 82, 15, 23);
        public static Rectangle fifthFrame = new Rectangle(115, 83, 23, 22);
        #endregion

        #region food rectangles
        public static Rectangle tea = new Rectangle(85, 10, 42, 35);
        public static Rectangle cinnamonRoll = new Rectangle(144, 12, 45, 33);
        public static Rectangle iceCream = new Rectangle(204, 3, 41, 45);
        public static Rectangle flan = new Rectangle(6, 62, 57, 41);
        public static Rectangle croissant = new Rectangle(144, 59, 51, 43);
        public static Rectangle cupcake = new Rectangle(205, 53, 36, 48);
        public static Rectangle chocolateStrawberry = new Rectangle(3, 114, 56, 46);
        public static Rectangle macaron = new Rectangle(68, 122, 63, 35);
        public static Rectangle fruitTart = new Rectangle(137, 120, 51, 36);
        public static Rectangle jello = new Rectangle(195, 123, 52, 33);
        public static Rectangle matchaLatte = new Rectangle(6, 188, 63, 53);
        public static Rectangle latte = new Rectangle(78, 189, 63, 53);
        public static Rectangle rootBeerFloat = new Rectangle(154, 185, 36, 59);
        public static Rectangle icedCoffee = new Rectangle(208, 172, 36, 73);
        #endregion

        #region misc rectangles
        public static Rectangle menuGridRect = new Rectangle(10, 4, 200, 96);
        public static Rectangle upButton = new Rectangle(220, 4, 28, 48);
        public static Rectangle downButton = new Rectangle(220, 54, 28, 48);
        public static Rectangle buyButton = new Rectangle(10, 106, 52, 24);
        public static Rectangle sellButton = new Rectangle(10, 132, 52, 24);

        public static Rectangle confirmButton = new Rectangle(10, 160, 34, 34);
        public static Rectangle cancelButton = new Rectangle(10, 196, 34, 34);
        public static Rectangle overlay = new Rectangle(70, 106, 140, 140);
        #endregion
    }
}
