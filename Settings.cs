﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace Fluent_Tic_tac_toe;
internal class Settings
{
    public static string[] gamemodes = { "Singleplayer", "Multiplayer", "Spectator" };
    public static int gamemode = 0;

    public static string[] difficulties = { "Easy", "Medium", "Hard" };
    public static int difficulty = 1;

    public static List<Player> players = new();
    public static int numPlayers = 0;
    public static int numMultiplayerBots = 0;
    public static int numSpectatorBots = 2;

    public static string[] boardModes = { "Default", "Custom" };
    public static int boardMode = 0;
    public static Vector2 boardSize = new(3,3);

    public static string[] winPatterns = { "3 in a row", "Full row" };
    public static int winPattern = 0;

    public static bool matchTimerEnabled = true;
    public static bool boardInfoEnabled = true;
    public static bool playerCounterEnabled = true;

    public static void Reset()
    {
        gamemode = 0;
        boardMode = 0;
        numPlayers = 0;
        numMultiplayerBots = 0; 
        numSpectatorBots = 2;
        difficulty = 1;
        boardSize = new(3, 3);
        winPattern = 0;
        matchTimerEnabled = true;
        boardInfoEnabled = true;
        playerCounterEnabled = true;
    }
}
