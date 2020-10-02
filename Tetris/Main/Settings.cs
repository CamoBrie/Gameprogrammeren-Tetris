using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Main
{
    class Settings
    {

        public enum AllSettings
        {
            StartingLevel,
            GridHeight,
            GridWidth,
            Animations,
            SpecialBlocks,
            HiddenMode
        }

        public int StartingLevel;
        public int GridWidth;
        public int GridHeight;
        public bool Animations;
        public bool SpecialBlocks;
        public bool HiddenMode;
        

        public Settings(int StartingLevel = 1, int GridWidth = 10, int GridHeight = 20, bool Animations = true, bool SpecialBlocks = true, bool HiddenMode = false)
        {
            this.StartingLevel = StartingLevel;
            this.GridWidth = GridWidth;
            this.GridHeight = GridHeight;
            this.Animations = Animations;
            this.SpecialBlocks = SpecialBlocks;
            this.HiddenMode = HiddenMode;
        }

        public void ChangeSetting(bool positive = true, AllSettings currentSetting = 0)
        {
            switch(currentSetting)
            {
                case AllSettings.StartingLevel:
                    if(positive && this.StartingLevel < 20)
                    {
                        this.StartingLevel++;
                    } else if(this.StartingLevel > 1)
                    {
                        this.StartingLevel--;
                    }
                    break;

                case AllSettings.GridWidth:
                    if (positive && this.GridWidth < 20)
                    {
                        this.GridWidth++;
                    }
                    else if (this.GridWidth > 6)
                    {
                        this.GridWidth--;
                    }
                    break;

                case AllSettings.GridHeight:
                    if (positive && this.GridHeight < 30)
                    {
                        this.GridHeight++;
                    }
                    else if (this.GridHeight > 10)
                    {
                        this.GridHeight--;
                    }
                    break;

                case AllSettings.Animations:
                    this.Animations = !this.Animations;
                    break;

                case AllSettings.SpecialBlocks:
                    this.SpecialBlocks = !this.SpecialBlocks;
                    break;

                case AllSettings.HiddenMode:
                    this.HiddenMode = !this.HiddenMode;
                    break;

            }

        }
    }
}
