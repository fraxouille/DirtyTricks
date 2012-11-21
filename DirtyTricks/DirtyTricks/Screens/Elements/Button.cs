using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DirtyTricks
{
    enum State { ON, OFF };
    
    class Button
    {
        #region Properties

        public State state;
        private Texture2D _displayedTexture, _textureOn, _textureOff;
        private Rectangle _dummyRect;

        #endregion


        #region Constructor

        public Button(int x, int y, Texture2D textureOn, Texture2D textureOff)
        {
            _textureOn = textureOn;
            _textureOff = textureOff;
            _dummyRect = new Rectangle(x, y, textureOn.Width, textureOn.Height);
            state = State.OFF;
            _displayedTexture = textureOff;
        }

        #endregion
        
        
        //Methods


        //Update & Draw
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            switch (state)
            {
                case State.OFF:
                    {
                        _displayedTexture = _textureOff;
                        break;
                    }
                case State.ON:
                    {
                        _displayedTexture = _textureOn;
                        break;
                    }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_displayedTexture, _dummyRect, Color.White);
        }
    }
}
