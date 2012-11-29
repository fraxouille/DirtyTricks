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
    class PauseScreen : Microsoft.Xna.Framework.Game
    {
        // Properties
        bool exitPauseAllowed = false;
        public bool exitGame = false;
        int _selection = 0,
            _btnNumber = 2,
            _buttonWidth,
            _buttonHeight,
            _verticalMargin = 10;
        Button[] _buttons;
        Texture2D[] _buttonsOn, _buttonsOff;
        bool _currentDownState, _currentUpState, _previousDownState, _previousUpState;
        int _buttonDelay, _downButtonDelay = 0, _upButtonDelay = 0;


        //Constructor
        public PauseScreen()
        {
            this.IsMouseVisible = true;

            _buttonsOn = new Texture2D[_btnNumber];
            _buttonsOff = new Texture2D[_btnNumber];
            _buttonsOn[0] = Ressources.btnNewGameOn;
            _buttonsOff[0] = Ressources.btnNewGameOff;
            _buttonsOn[1] = Ressources.btnExitOn;
            _buttonsOff[1] = Ressources.btnExitOff;
            _buttonWidth = _buttonsOn[0].Width;
            _buttonHeight = _buttonsOn[0].Height;
            _buttonDelay = 20;

            _buttons = new Button[_btnNumber];
            for (int i = 0; i < _btnNumber; i++)
            {
                int blockButtonsVerticalSize = _buttonHeight * _btnNumber + _verticalMargin * (_btnNumber - 1);
                _buttons[i] = new Button((Settings.Current.ScreenWidth - _buttonWidth) / 2,
                    (Settings.Current.ScreenHeight - blockButtonsVerticalSize) / 2 + (i * (_buttonHeight + _verticalMargin)),
                    _buttonsOn[i], _buttonsOff[i]);
            }
            _buttons[_selection].state = State.ON;
        }

        // Methods
        private void DownButton()
        {
            _downButtonDelay++;
            if (_downButtonDelay > _buttonDelay)
                _previousDownState = false;

            if (!_previousDownState || !_currentDownState)
            {
                _buttons[_selection].state = State.OFF;
                _selection = (_selection < _btnNumber - 1) ? _selection += 1 : 0;
                _buttons[_selection].state = State.ON;

                _previousDownState = true;
                _downButtonDelay = 0;
            }
            _currentDownState = true;
        }

        private void UpButton()
        {
            _upButtonDelay++;
            if (_upButtonDelay > _buttonDelay)
                _previousUpState = false;

            if (!_previousUpState || !_currentUpState)
            {
                _buttons[_selection].state = State.OFF;
                _selection = (_selection > 0) ? _selection -= 1 : _btnNumber - 1;
                _buttons[_selection].state = State.ON;

                _previousUpState = true;
                _upButtonDelay = 0;
            }
            _currentUpState = true;
        }

        // Update & Draw
        public void Update(MouseState mouse, KeyboardState keyboard, GamePadState gamePadState)
        {
            if (keyboard.IsKeyUp(Keys.Escape) && gamePadState.Buttons.Start == ButtonState.Released)
                exitPauseAllowed = true;
            
            if ((keyboard.IsKeyDown(Keys.Escape) || gamePadState.Buttons.Start == ButtonState.Pressed) && exitPauseAllowed)
            {
                _selection = 0;
                exitPauseAllowed = false;
                Game1.gameState = GameState.Game;
            }

            for (int i = 0; i < _btnNumber; i++)
                _buttons[i].Update(mouse, keyboard);

            if (keyboard.IsKeyDown(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown))
                DownButton();
            if (keyboard.IsKeyUp(Keys.Down) && gamePadState.IsButtonUp(Buttons.DPadDown))
                _currentDownState = false;

            if (keyboard.IsKeyDown(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp))
                UpButton();
            if (keyboard.IsKeyUp(Keys.Up) && gamePadState.IsButtonUp(Buttons.DPadUp))
                _currentUpState = false;

            if ((keyboard.IsKeyDown(Keys.Enter) || gamePadState.Buttons.Start == ButtonState.Pressed || gamePadState.Buttons.A == ButtonState.Pressed) && exitPauseAllowed)
            {
                switch (_selection)
                {
                    case 0:
                        {
                            _selection = 0;
                            exitPauseAllowed = false;
                            Game1.gameState = GameState.Game;
                            break;
                        }
                    case 1:
                        {
                            exitGame = true;
                            break;
                        }
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _btnNumber; i++)
                _buttons[i].Draw(spriteBatch);
        }

    }
}
