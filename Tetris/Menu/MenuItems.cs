using Main;
using Microsoft.Xna.Framework;
using System;
using System.Windows.Forms;

namespace Menu
{
     class MenuItems
    {
        /// <summary>
        /// text: the text to display
        /// color: the color the text is
        /// callback: the function that is called when the user presses the action button
        /// </summary>
        public string text;
        public Color color;
        public Func<int> callBack;

        // Create a menuItem without a callback function
        public MenuItems(string s, Color c) : this(s,c, () => { return 0; })
        {
       
        }

        // Create a menuItem with a callback function
        public MenuItems(string text, Color color, Func<int> callBack)
        {
            this.text = text;
            this.color = color;
            this.callBack = callBack;

        }
 
    }
}