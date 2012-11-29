using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Data;
using System.Configuration;

namespace DirtyTricks
{
    public enum GameState
    {
        Menu, Game, Pause
    };


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Properties
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MenuScreen menuScreen;
        GameScreen gameScreen;
        PauseScreen pauseScreen;

        MouseState mouseState;
        KeyboardState keyboardState;
        GamePadState gamePadState;

        public static GameState gameState;


        //Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = int.Parse(ConfigurationManager.AppSettings["fullScreen"]) != 0;
            int screenWidth = int.Parse(ConfigurationManager.AppSettings["screenWidth"]);
            int screenHeight = int.Parse(ConfigurationManager.AppSettings["screenHeight"]);
            graphics.PreferredBackBufferWidth = screenWidth; // 1080p=1920 - 720p=1280
            graphics.PreferredBackBufferHeight = screenHeight; // 1080p=1080 - 720p=720

            Settings.Current = Content.Load<Settings>("Settings");
            Settings.Current.UpdateSreenSize(screenWidth, screenHeight);

            this.IsMouseVisible = true;
            gameState = GameState.Menu;
        }

        //Methods
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent(Content);
            gameScreen = new GameScreen();
            menuScreen = new MenuScreen();
            pauseScreen = new PauseScreen();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        //Update & Draw
        protected override void Update(GameTime gameTime)
        {
            if (menuScreen.exitGame || pauseScreen.exitGame)
            {
                UnloadContent();
                Exit();
            }

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            switch (gameState)
            {
                case GameState.Menu:
                    {
                        menuScreen.Update(mouseState, keyboardState, gamePadState);
                        break;
                    }
                case GameState.Game :
                    {
                        gameScreen.Update(mouseState, keyboardState, gamePadState);
                        break;
                    }
                case GameState.Pause:
                    {
                        pauseScreen.Update(mouseState, keyboardState, gamePadState);
                        break;
                    }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    {
                        menuScreen.Draw(spriteBatch);
                        break;
                    }
                case GameState.Game:
                    {
                        gameScreen.Draw(spriteBatch);
                        break;
                    }
                case GameState.Pause:
                    {
                        pauseScreen.Draw(spriteBatch);
                        break;
                    }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
