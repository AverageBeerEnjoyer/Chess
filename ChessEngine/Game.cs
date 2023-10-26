
namespace ChessEngine {
    public class Game {
        private List<Tuple<Deck,Move>> history;
        private Deck deck;
        private bool gameOver = false;
        private Move lastMove;

        public Game() {
            history = new List<Tuple<Deck, Move>>();
            newGame();
        }

        public void newGame() {
            history.Clear();
            deck = new Deck();
        }

        public void makeMove(int x1, int y1, int x2, int y2) {
            move(x1, y1, x2, y2);
        }

        private void move(int x1, int y1, int x2, int y2) {
            if (gameOver) return;
            Deck next = new Deck(deck);
            Cell from = next.getCell(x1, y1);
            Cell to = next.getCell(x2, y2);
            lastMove = next.makeMove(from, to);
            if (lastMove.isCheckmate()) gameOver = true;
            history.Add(new Tuple<Deck,Move> (deck,lastMove));
            deck = next;
        }

        public Move getLastMove() {
            return lastMove;
        }

        public bool isGameOver() {
            return gameOver;
        }

        public override string ToString() {
            return deck.ToString();
        }

        public void makeMove(string move) {
            int[] coords;
            try {
                coords = Utils.parseMove(move);
            }
            catch (Exception e) {
                throw new ArgumentException();
            }

            this.move(coords[0], coords[1], coords[2], coords[3]);
            
        }
        public string getCellString(int i, int j) {
            if(!Utils.isOnField(i, j))
                throw new ArgumentOutOfRangeException();
            return deck.getCell(i, j).ToString();
        }

        public PlayerColor getMoveColor() {
            return deck.getMoveColor();
        }

        public List<Tuple<Deck,Move>> getHistory() {
            return history;
        }
    }
}