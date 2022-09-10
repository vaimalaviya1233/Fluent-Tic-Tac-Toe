﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Fluent_Tic_tac_toe.Helpers;

namespace Fluent_Tic_tac_toe;
internal class Game
{
    public static string[] gamemodes = { "Singleplayer", "Multiplayer","Spectator"};
    public static string Gamemode = gamemodes[0];
    public List<Player> players
    {
        get;
    }
    public List<Piece> pieces
    {
        get; set;
    }
    public List<Piece> winningPieces
    {
        get; set;
    }
    public Piece[,] board
    {
        get; set;
    }
    public Player winner
    {
        get; set;
    }
    public int turns
    {
        get; set;
    }
    public int time
    {
        get; set;
    }
    public bool started
    {
        get; set;
    }
    private bool timed
    {
        get; set;
    }

    public Game()
    {
    }

    public Game(List<Player> players, string gamemode)
    {
        this.players = players;
        this.board = new Piece[3, 3];
        this.winningPieces = new List<Piece>();
        this.pieces = new List<Piece>();
        this.time = 0;
        this.started = false;
        SetGamemode(gamemode);
    }

    public Game(List<Player> players)
    {
        this.players = players;
        this.board = new Piece[3, 3];
        this.winningPieces = new List<Piece>();
        this.pieces = new List<Piece>();
        this.time = 0;
        this.started = false;
    }

    public void Start()
    {
        this.players[0].symbol = "x";
        this.players[1].symbol = "o";
        this.turns = 0;
        this.winner = null;
        this.time = 0;
        this.started = true;
    }

    public void Restart()
    {
        this.winner = null;
        this.winningPieces = new List<Piece>();
        this.pieces = new List<Piece>();
        this.turns = 0;
        this.board = new Piece[3, 3];
        this.time = 0;
        this.started = false;
    }

    public bool Won()
    {
        for (var r = 0; r < board.Rank + 1; r++)
        {
            winningPieces.Clear();
            Piece initialPiece = board[r, 0];
            var foundWinner = true;
            if (initialPiece != null)
            {
                winningPieces.Add(initialPiece);

                for (var c = 1; c < board.GetLength(0); c++) // Checks horizontal wins
                {
                    Piece piece = board[r, c];
                    if (piece == null) { foundWinner = false; break; }
                    winningPieces.Add(piece);
                    if (!initialPiece.Matches(piece))
                    {
                        foundWinner = false;
                        break;
                    }
                }
                if (foundWinner)
                {
                    winner = initialPiece.player;
                    return true;
                }
            }
        }
        for (var c = 0; c < board.GetLength(0); c++)
        {
            winningPieces.Clear();
            Piece initialPiece = board[0, c];
            var foundWinner = true;
            if (initialPiece != null)
            {
                winningPieces.Add(initialPiece);

                for (var r = 1; r < board.Rank + 1; r++) // Checks vertical wins
                {
                    Piece piece = board[r, c];
                    if (piece == null) { foundWinner = false; break; }
                    winningPieces.Add(piece);
                    if (!initialPiece.Matches(piece))
                    {
                        foundWinner = false;
                        break;
                    }
                }
                if (foundWinner)
                {
                    winner = initialPiece.player;
                    return true;
                }
            }
        }

        winningPieces.Clear();
        // Check diagonal wins
        if (board[1, 1] != null)
        {
            Piece initialPiece = null;
            var foundWinner = true;
            for (var i = 0; i < board.Rank + 1 && i < board.GetLength(0); i++)
            {
                Piece piece = board[i, i];
                if (piece == null) { foundWinner = false; break; }
                winningPieces.Add(piece);

                if (initialPiece == null)
                {
                    initialPiece = piece;
                }
                else
                {
                    if (!initialPiece.Matches(piece))
                    {
                        foundWinner = false;
                        break;
                    }
                }
            }
            if (foundWinner)
            {
                winner = initialPiece.player;
                return true;
            }
            initialPiece = null;
            winningPieces.Clear();
            foundWinner = true;
            for (var i = 0; i < board.Rank + 1 && i < board.GetLength(0); i++)
            {
                Piece piece = board[i, (board.GetLength(0) - 1) - i];
                if (piece == null) { foundWinner = false; break; }
                winningPieces.Add(piece);

                if (initialPiece == null)
                {
                    initialPiece = piece;
                }
                else
                {
                    if (!initialPiece.Matches(piece))
                    {
                        foundWinner = false;
                        break;
                    }
                }

            }
            if (foundWinner)
            {
                winner = initialPiece.player;
                return true;
            }
        }

        return false;
    }

    private bool PlacePiece(Player player, int row, int col)
    {
        if (board[row, col] == null)
        {
            Piece piece = new Piece(player, row, col);
            board[row, col] = piece;
            this.pieces.Add(piece);
            this.turns++;
            return true;
        }
        return false;
    }

    public bool PlacePiece(int row, int col)
    {
        if (board[row, col] == null)
        {
            Piece piece = new Piece(GetCurrentPlayerTurn(), row, col);
            board[row, col] = piece;
            this.pieces.Add(piece);
            this.turns++;
            return true;
        }
        return false;
    }

    public bool ComputerTurn()
    {
        List<Vector2> spaces = GetEmptySpaces();
        if (spaces.Count > 0)
        {
            Vector2 selectedSpace = spaces[new Random().Next(spaces.Count)];
            return PlacePiece((int)selectedSpace.Y, (int)selectedSpace.X);
        }
        return false;
    }

    public bool IsDraw()
    {
        return this.winner == null && this.board.Length == this.turns;
    }

    public List<Vector2> GetEmptySpaces()
    {
        List<Vector2> spaces = new List<Vector2>();
        for (int r = 0; r < this.board.Rank + 1; r++)
        {
            for (int c = 0; c < this.board.GetLength(0); c++)
            {
                if (board[r, c] == null)
                {
                    spaces.Add(new Vector2(c, r));
                }
            }
        }
        return spaces;
    }

    public Player GetCurrentPlayerTurn()
    {
        return players[turns % players.Count];
    }

    public string GetGridName(int row, int col)
    {
        return "square" + ((row * 3) + col);
    }

    public string GetGamemode()
    {
        return Gamemode;
    }

    public int GetNumberOfRealPlayers() // amount of players that aren't a computer
    {
        var result = 0;
        foreach (Player player in this.players)
        {
            if (player.isComputer == false)
            {
                result++;
            }
        }
        return result;
    }

    public void SetGamemode(string gamemode)
    {
        if (gamemodes.Contains(gamemode))
        {
            Gamemode = gamemode;
        }
    }
}

public class Player
{
    public string name
    {
        get; set;
    }
    public int number
    {
        get;
    }
    public bool isComputer
    {
        get;
    }
    public string symbol
    {
        get; set;
    }
    private static int playerNumber = 1;

    public Player(string name, bool isComputer)
    {
        this.name = name;
        this.number = playerNumber;
        this.isComputer = isComputer;
        playerNumber++;
    }

    public Player(bool isComputer)
    {
        this.name = "Player " + playerNumber;
        this.number = playerNumber;
        this.isComputer = isComputer;
        playerNumber++;
    }

    public Player(string name)
    {
        this.name = name;
        this.number = playerNumber;
        this.isComputer = false;
        playerNumber++;
    }
}

public class Piece
{
    public Player player
    {
        get;
    }
    public int row
    {
        get;
    }
    public int col
    {
        get;
    }

    public Piece(Player player, int row, int col)
    {
        this.player = player;
        this.row = row;
        this.col = col;
    }

    public bool Matches(Piece otherPiece)
    {
        return this.player.Equals(otherPiece.player);
    }

    public int GetIndex()
    {
        return (this.row * 3) + this.col;
    }
}
