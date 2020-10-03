using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Main
{

    enum AllSettings
    {
        Startinglevel,
        GridWidth,
        GridHeight,
        Animations,
        SpecialBlocks,
        HiddenMode,
        None
    }
    class Settings
    {

        public static int StartingLevel = 1;
        public static int GridWidth = 10;
        public static int GridHeight = 20;
        public static bool Animations = true;
        public static bool SpecialBlocks = true;
        public static bool HiddenMode = false;
        

        public static void ChangeSetting(bool positive = true, int currentSetting = 0)
        {
            switch(currentSetting)
            {
                case 0:
                    if(positive && StartingLevel < 20)
                    {
                        StartingLevel++;
                    } else if(!positive && StartingLevel > 1)
                    {
                        StartingLevel--;
                    }
                    break;

                case 1:
                    if (positive && GridWidth < 20)
                    {
                        GridWidth++;
                    }
                    else if (!positive && GridWidth > 6)
                    {
                        GridWidth--;
                    }
                    break;

                case 2:
                    if (positive && GridHeight < 30)
                    {
                        GridHeight++;
                    }
                    else if (!positive && GridHeight > 10)
                    {
                        GridHeight--;
                    }
                    break;

                case 3:
                    Animations = !Animations;
                    break;

                case 4:
                    SpecialBlocks = !SpecialBlocks;
                    break;

                case 5:
                    HiddenMode = !HiddenMode;
                    break;

            }

        }

        public static String getValue(int currentSetting)
        {
            switch(currentSetting)
            {
                case 0:
                    return StartingLevel.ToString();
                case 1:
                    return GridWidth.ToString();
                case 2:
                    return GridHeight.ToString();
                case 3: return Animations.ToString();
                case 4: return SpecialBlocks.ToString();
                case 5: return HiddenMode.ToString();
                default:
                    return "x";

            }
        }
    }
}
