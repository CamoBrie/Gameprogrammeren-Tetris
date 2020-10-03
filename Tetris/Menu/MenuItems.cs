using Main;
using Microsoft.Xna.Framework;
using System;
using System.Windows.Forms;

namespace Menu
{
     class MenuItems
    {
        public string text;
        public Color color;
        public AllSettings setting;
        public Func<int> callBack;

        public MenuItems(string s, Color c) : this(s,c, () => { return 0; })
        {
       
        }

        public MenuItems(string text, Color color, Func<int> callBack)
        {
            this.text = text;
            this.color = color;
            this.callBack = callBack;

        }
 
    }
}