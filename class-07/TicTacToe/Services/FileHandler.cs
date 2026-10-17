using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TicTacToe.Entities;


namespace TicTacToe.Services
{
    public class FileHandler
    {
        public static string LeaderboardPath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
            "TicTacToe", "leaderboard.json");

        // A new leaderboard file starts with the players from Leaderboard/leaderboard.json
        private static readonly string _initialLeaderboardPath = Path.Combine(AppContext.BaseDirectory, "Leaderboard", "leaderboard.json");

        private string _path;
        
        public FileHandler(string path)
        {
            _path = path;

            if (!File.Exists(_path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
                File.Copy(_initialLeaderboardPath, _path);
            }
        }

        private void UpdateScore(ref List<Player> players, Player player)
        {
            foreach (var fPlayer in players)
            {
                if (fPlayer.Name == player.Name)
                {
                    fPlayer.Score += player.Score;
                }
            }
        }

        public void SaveOrUpdatePlayers(List<Player> players)
        {
            List<Player> filePlayers = ReadFromFile();
            List<bool> playerExists = new List<bool>(2) { false, false };

            foreach (var player in players)
            {
                int playerIndex = (int)player.Id - 1;

                playerExists[playerIndex] = filePlayers.Any(p => p.Name == player.Name);
             
                if (playerExists[playerIndex])
                {
                    UpdateScore(ref filePlayers, player);
                }
                else
                {
                    filePlayers.Add(player);
                }
            }

            WriteToFile(filePlayers);
        }

        private void WriteToFile(List<Player> players)
        {
            var playersJson = JsonConvert.SerializeObject(players);
            File.WriteAllText(_path, playersJson);
        }

        public List<Player> ReadFromFile()
        {
            var readJson = File.ReadAllText(_path);
            var playerList = JsonConvert.DeserializeObject<List<Player>>(readJson);
            
            return (playerList != null) ? playerList : new List<Player>();
        }
    }
}
