using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Main
{

    // global enum for all the settings
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
        // the variables for the settings
        public static int StartingLevel = 1;
        public static int GridWidth = 10;
        public static int GridHeight = 20;
        public static bool Animations = true;
        public static bool SpecialBlocks = true;
        public static bool HiddenMode = false;
        

        // change a setting with respect to the bounds
        public static void ChangeSetting(bool positive = true, AllSettings currentSetting = 0)
        {
            //switch on the currentSetting that needs to be changed
            switch(currentSetting)
            {
                // change setting based on bounds

                case AllSettings.Startinglevel:
                    if(positive && StartingLevel < 20)
                    {
                        StartingLevel++;
                    } else if(!positive && StartingLevel > 1)
                    {
                        StartingLevel--;
                    }
                    break;

                case AllSettings.GridWidth:
                    if (positive && GridWidth < 20)
                    {
                        GridWidth++;
                    }
                    else if (!positive && GridWidth > 6)
                    {
                        GridWidth--;
                    }
                    break;

                case AllSettings.GridHeight:
                    if (positive && GridHeight < 30)
                    {
                        GridHeight++;
                    }
                    else if (!positive && GridHeight > 10)
                    {
                        GridHeight--;
                    }
                    break;


                // invert the setting | bool setting
                
                case AllSettings.Animations:
                    Animations = !Animations;
                    break;

                case AllSettings.SpecialBlocks:
                    SpecialBlocks = !SpecialBlocks;
                    break;

                case AllSettings.HiddenMode:
                    HiddenMode = !HiddenMode;
                    break;

            }

        }

        // get the value of the setting in String-form (only for displaying)
        public static String GetValue(AllSettings currentSetting)
        {
            switch(currentSetting)
            {
                case AllSettings.Startinglevel:
                    return StartingLevel.ToString();
                case AllSettings.GridWidth:
                    return GridWidth.ToString();
                case AllSettings.GridHeight:
                    return GridHeight.ToString();
                case AllSettings.Animations: return Animations.ToString();
                case AllSettings.SpecialBlocks: return SpecialBlocks.ToString();
                case AllSettings.HiddenMode: return HiddenMode.ToString();
                default:
                    return "x";

            }
        }

        //get the setting based on the int
        public static AllSettings GetSetting(int currentSetting)
        {
            switch (currentSetting)
            {
                case 0: return AllSettings.Startinglevel;
                case 1: return AllSettings.GridWidth;
                case 2: return AllSettings.GridHeight;
                case 3: return AllSettings.Animations;
                case 4: return AllSettings.SpecialBlocks;
                case 5: return AllSettings.HiddenMode;
                default:
                    return AllSettings.None;

            }
        }

    }
}
