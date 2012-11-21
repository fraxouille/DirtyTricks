using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Data
{
    public class Settings
    {
        public static Settings Current;

        //Properties
        public int ScreenWidth;
        public int ScreenHeight;

        public int StartPositionX;
        public int StartPositionY;

        public int SpriteHeight;
        public int SpriteWidth;

        public float MaxSpeed;
        public float Acceleration;

        public int AnimationSpeed;

        //Constructor
        public Settings()
        {
        }

        //Methods

        public void UpdateSreenSize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }
        
    }
}
