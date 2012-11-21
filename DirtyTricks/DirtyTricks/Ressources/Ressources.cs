using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace DirtyTricks
{
    class Ressources
    {
        //Static properties
        public static Texture2D player, playerHitbox, mapPath;
        public static Texture2D btnNewGameOn, btnNewGameOff, btnLoadGameOn, btnLoadGameOff, btnExitOn, btnExitOff;

        //Load content
        public static void LoadContent(ContentManager content)
        {
            player = content.Load<Texture2D>("gfx/Game/player0");
            playerHitbox = content.Load<Texture2D>("gfx/Game/player0_hitbox");
            mapPath = content.Load<Texture2D>("gfx/Game/map01Path");

            btnNewGameOn = content.Load<Texture2D>("gfx/Menu/btn_newGame_on");
            btnNewGameOff = content.Load<Texture2D>("gfx/Menu/btn_newGame_off");
            btnLoadGameOn = content.Load<Texture2D>("gfx/Menu/btn_loadGame_on");
            btnLoadGameOff = content.Load<Texture2D>("gfx/Menu/btn_loadGame_off");
            btnExitOn = content.Load<Texture2D>("gfx/Menu/btn_exit_on");
            btnExitOff = content.Load<Texture2D>("gfx/Menu/btn_exit_off");
        }

    }
}
