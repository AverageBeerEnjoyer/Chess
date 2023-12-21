using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChessEngine;
using System.Linq;
//using System.IO;

namespace ChessApp {
    internal class ChessDB {
        private static ChessDB instance;

        public static ChessDB getInstance() {
            if (instance is null)
                instance = new ChessDB();
            return instance;
        }

        private readonly string moveRepoPath = "moves.db";
        private readonly string gameRepoPath = "games.db";
        private List<GameRecord> games;
        private List<MoveRecord> moves;


        public class GameRecord {
            public GameRecord() {
            }
            public GameRecord(Game game) {
                DateTime = DateTime.Now;
                id = Guid.NewGuid();
                gameOver = game.isGameOver();
                PlayerColor? w = game.getWinner();
                if (w.HasValue) {
                    winner = w == PlayerColor.WHITE ? Winner.white : Winner.black;
                } else if (gameOver)
                    winner = Winner.draw;
                else
                    winner = Winner.no;
                numberOfMoves = game.getMoveNumber();
            }
            public enum Winner {
                black,
                white,
                draw,
                no
            }

            public DateTime DateTime { get; set; }
            public Guid id { get; set; }

            public Winner winner { get; set; }

            public int numberOfMoves { get; set; }

            public bool gameOver { get; set; }
        }

        public class MoveRecord {

            public MoveRecord() { }
            public MoveRecord(Move move, Guid gameId) {
                id = Guid.NewGuid();
                this.gameId = gameId;
                record = move.getRecord();
                int[] coords = move.getCoords();
                xfrom = coords[0];
                yfrom = coords[1];
                xto = coords[2];
                yto = coords[3];
                moveNumber = move.getMoveNumber();
                moveColor = move.getMoveColor();
            }
            public Guid id { get; set; }
            public Guid gameId { get; set; }
            public string record { get; set; }
            public int xfrom { get; set; }
            public int yfrom { get; set; }
            public int xto { get; set; }
            public int yto { get; set; }
            public int moveNumber { get; set; }
            public PlayerColor moveColor { get; set; }
        }
        private ChessDB() {
            moves = new List<MoveRecord>();
            games = new List<GameRecord>();
            loadGames();
            loadMoves();
        }

        private void loadGames() {
            StreamReader fstream = new StreamReader(gameRepoPath, new FileStreamOptions() { Mode = FileMode.OpenOrCreate });

            string data = fstream.ReadToEnd();
            string[] spl = data.Split("\n");
            foreach (string s in spl) {
                try {
                    string cur = s;
                    if (cur.EndsWith('\r')) {
                        cur = cur.Substring(0, cur.Length - 1);
                    }
                    string[] fields = cur.Split("$");
                    games.Add(
                        new GameRecord {
                            id = Guid.Parse(fields[0]),
                            DateTime = DateTime.Parse(fields[1]),
                            winner = parseWinner(fields[2]),
                            numberOfMoves = int.Parse(fields[3]),
                            gameOver = bool.Parse(fields[4])
                        });
                } catch (Exception e) {
                    continue;
                }
            }
            fstream.Close();
        }

        private void loadMoves() {
            StreamReader fstream = new StreamReader(moveRepoPath, new FileStreamOptions() { Mode = FileMode.OpenOrCreate });

            string data = fstream.ReadToEnd();
            string[] spl = data.Split("\n");
            foreach (string s in spl) {
                try {
                    string cur = s;
                    if (cur.EndsWith('\r')) {
                        cur = cur.Substring(0, cur.Length - 1);
                    }
                    string[] fields = cur.Split("$");
                    moves.Add(
                        new MoveRecord {
                            id = Guid.Parse(fields[0]),
                            gameId = Guid.Parse(fields[1]),
                            record = fields[2],
                            xfrom = int.Parse(fields[3]),
                            yfrom = int.Parse(fields[4]),
                            xto = int.Parse(fields[5]),
                            yto = int.Parse(fields[6]),
                            moveNumber = int.Parse(fields[7]),
                            moveColor = PlayerColors.fromString(fields[8])
                        });
                } catch (Exception e) {
                    continue;
                }
            }
        }

        private GameRecord.Winner parseWinner(string s) {
            switch (s) {
                case "white":
                return GameRecord.Winner.white;
                case "black": return GameRecord.Winner.black;
                case "draw": return GameRecord.Winner.draw;
                case "no": return GameRecord.Winner.no;
                default: throw new ArgumentException();
            }
        }
        public void save() {
            saveGames();
            saveMoves();
        }
        private void saveGames() {
            StreamWriter streamWriter = new StreamWriter(gameRepoPath, false);
            foreach (GameRecord gr in games) {
                StringBuilder sb = new StringBuilder();
                sb.Append(gr.id).Append("$")
                    .Append(gr.DateTime).Append("$")
                    .Append(gr.winner).Append("$")
                    .Append(gr.numberOfMoves).Append("$")
                    .Append(gr.gameOver).Append("\n");
                streamWriter.Write(sb.ToString());
                streamWriter.Flush();
            }
            streamWriter.Close();
        }

        private void saveMoves() {
            StreamWriter streamWriter = new StreamWriter(moveRepoPath, false);
            foreach (MoveRecord mr in moves) {
                StringBuilder sb = new StringBuilder();
                sb.Append(mr.id).Append("$")
                    .Append(mr.gameId).Append("$")
                    .Append(mr.record).Append("$")
                    .Append(mr.xfrom).Append("$")
                    .Append(mr.yfrom).Append("$")
                    .Append(mr.xto).Append("$")
                    .Append(mr.yto).Append("$")
                    .Append(mr.moveNumber).Append("$")
                    .Append(mr.moveColor).Append("\n");
                streamWriter.Write(sb.ToString());
                streamWriter.Flush();
            }
            streamWriter.Close();
        }

        public void AddGame(Game game) {
            GameRecord gameRecord = new GameRecord(game);
            games.Add(gameRecord);

            foreach (Deck pos in game.getHistory()) {
                MoveRecord moveRecord = new MoveRecord(pos.getMove(), gameRecord.id);
                moves.Add(moveRecord);
            }
        }

        public List<GameRecord> getGames() {
            return games;
        }
        public List<MoveRecord> getMovesByGameId(Guid id) {
            return moves.Where(m => m.gameId == id).ToList();
        }
    }
}
