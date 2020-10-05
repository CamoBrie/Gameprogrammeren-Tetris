using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Menu
{
    class MenuObject
    {
        /// <summary>
        /// items: a list of menuItems
        /// name: the name of the menu
        /// currentItem: the current selected item
        /// height: the height at which the menu needs to be displayed
        /// </summary>
        readonly private List<MenuItems> items;
        readonly private String name;
        public int currentItem;
        readonly private int height;

        //set variables
        public MenuObject(List<MenuItems> items, String name, int height)
        {
            this.items = items;
            this.name = name;
            this.currentItem = 0;
            this.height = height;
        }

        /*  Add an item afterwards
 
        public void AddItem(MenuItems item)
        {
            this.items.Add(item);
        }

        */

        /// <summary>
        /// The function that draws the menu to the spritebatch
        /// </summary>
        /// <param name="spriteBatch">the spritebatch to which it needs to be drawn</param>
        /// <param name="font">the font to be used</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < items.Count; i++)
            {
                // only for settings, change the text to also include the value of the setting
                if (this.name == "Settings" && i != items.Count - 1)
                {
                    DrawCenteredString(spriteBatch, font, $"{items[i].text} <{Settings.GetValue(Settings.GetSetting(i))}>", new Vector2(450, height + 40 * i), items[i].color);
                }
                else
                {
                    DrawCenteredString(spriteBatch, font, items[i].text, new Vector2(450, height + 40 * i), items[i].color);
                }
            }

            // draw the selector and the menu title
            DrawCenteredString(spriteBatch, font, "________", new Vector2(450, height + 2 + 40 * currentItem), Color.White);
            DrawCenteredString(spriteBatch, font, name, new Vector2(450, height - 40), Color.Gold);
        }

        // call the callback function of the selected item
        public void OnAction()
        {
            this.items[currentItem].callBack();
        }

        // return coordinates for the center of a string, instead of the upper-left corner
        private Vector2 CenterString(SpriteFont font, String text)
        {
            return new Vector2((font.MeasureString(text).X / 2), (font.MeasureString(text).Y / 2));
        }

        // draw a string, centered on its position.
        private void DrawCenteredString(SpriteBatch spriteBatch, SpriteFont font, String text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, Vector2.Subtract(position, CenterString(font, text)), color);
        }

        // get the length of the menu
        public int GetLength()
        {
            return this.items.Count;
        }
    }
}
