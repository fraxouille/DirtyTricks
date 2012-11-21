using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Data;

namespace DirtyTricks
{
    class Sprite
    {
        //Properties
        public Color[] pixelData;

        //Constructor
        public Sprite(Texture2D sprite)
        {
            pixelData = new Color[sprite.Width * sprite.Height];
            sprite.GetData<Color>(pixelData);
        }

        //Methods
        /*
        public static Color[] MapCrop(int positionX, int positionY, int sizeX, int sizeY)
        {
            Color[] cropData = new Color[sizeX*sizeY];

            int index = 0;
            for (int y = positionY; y < positionY + sizeY; y++)
            {

                for (int x = positionX; x < positionX + sizeX; x++)
                {
                    cropData[index] = Map.Current.MapPathPixel[x + (y * Map.Current.width)];
                    index++;
                }
            }
            return cropData;
        }

        public static bool Collision(Color[] dataA, Color[] dataB)
        {
            for (int x = 0; x < dataA.Length; x++)
            {
                if (dataA[x] != Color.White && dataB[x] != Color.White)
                {
                    return true;
                }
            }
            return false;
        }
        */


    }
}