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
    class Map
    {
        public static Map Current;

        //Properties
        //public Texture2D map;
        private Texture2D _mapPath;
        public Texture2D MapPath
        { 
            get { return _mapPath; }
            set { _mapPath = value;
                MapPath.GetData<Color>(_mapPathPixel); }
        }
        private Color[] _mapPathPixel;
        public Color[] MapPathPixel
        {
            get { return _mapPathPixel; }
        }
        public int width;
        public int height;
        public int size;
        public Vector2 position;
        
        //Constructor
        public Map()
        {
            _mapPath = Ressources.mapPath;
            _mapPathPixel = new Color[_mapPath.Width * _mapPath.Height];
            _mapPath.GetData<Color>(_mapPathPixel);
            width = MapPath.Width;
            height = MapPath.Height;
            size = width * height;
            position.X = 0;
            position.Y = 0;
        }

        //Methods


        //Update & Draw
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_mapPath, position, Color.White);
        }

    }
}
