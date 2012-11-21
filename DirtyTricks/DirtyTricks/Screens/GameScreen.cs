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
    class GameScreen
    {
        //Properties
        Player player;

        int width, height;
        bool pauseAllowed;

        //Constructeur
        public GameScreen()
        {
            width = Settings.Current.ScreenWidth;
            height = Settings.Current.ScreenHeight;
            Map.Current = new Map();
            player = new Player();
        }

        //Methods


        //Update & Draw
        public void Update(MouseState mouse, KeyboardState keyboard, GamePadState gamePadState)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
                pauseAllowed = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && pauseAllowed)
            {
                Game1.gameState = GameState.Pause;
                pauseAllowed = false;
            }

            if (mouse.X > 800)
            {
                Map.Current.position.X++;
                player.position.X++;
            }

            if (mouse.X < 200)
            {
                Map.Current.position.X--;
                player.position.X--;
            }
                

            Map.Current.Update();
            player.Update(mouse, keyboard);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Current.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }

    }
}
