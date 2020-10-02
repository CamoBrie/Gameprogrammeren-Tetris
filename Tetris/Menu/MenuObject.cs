using Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    class MenuObject
    {
        private List<MenuItems> items;
        private String name;
        public int currentItem;
        private int height;

        public MenuObject(List<MenuItems> items, String name, int height)
        {
            this.items = items;
            this.name = name;
            this.currentItem = 0;
            this.height = height;
        }

        public void AddItem(MenuItems item)
        {
            this.items.Add(item);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (this.name == "Settings" && i != items.Count-1)
                {
                    DrawCenteredString(spriteBatch, font, $"{items[i].text} <{Settings.getValue(i)}>", new Vector2(450, height + 40 * i), items[i].color);
                }
                else
                {
                    DrawCenteredString(spriteBatch, font, items[i].text, new Vector2(450, height + 40 * i), items[i].color);
                }
            }

            DrawCenteredString(spriteBatch, font, "________", new Vector2(450, height + 2 + 40 * currentItem), Color.White);
            DrawCenteredString(spriteBatch, font, name, new Vector2(450, height - 40), Color.Gold);
        }

        public void OnAction()
        {
            this.items[currentItem].callBack();
        }

        //return coordinates for the center of a string, instead of the upper-left corner
        private Vector2 CenterString(SpriteFont font, String text)
        {
            return new Vector2((font.MeasureString(text).X / 2), (font.MeasureString(text).Y / 2));
        }

        //draw a string, centered on its position.
        private void DrawCenteredString(SpriteBatch spriteBatch, SpriteFont font, String text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, Vector2.Subtract(position, CenterString(font, text)), color);
        }

        public int getLength()
        {
            return this.items.Count;
        }
    }
}
