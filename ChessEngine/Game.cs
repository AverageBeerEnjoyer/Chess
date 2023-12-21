
using ChessEngine.figures;

namespace ChessEngine {
    public class Game {
        private List<Deck> history;
        private Deck deck;
        private bool gameOver = false;
        private Move lastMove;
        private int moveNumber;
        private Tuple<int, int> chozenCell;
        private PlayerColor? winner;
        private bool waitingForType;

        public Game() {
            history = new List<Deck>();
            newGame();
        }

        public PlayerColor? getWinner() {
            return winner;
        }

        private void newGame() {
            history.Clear();
            deck = new Deck();
            moveNumber = 0;
            lastMove = null;
            winner = null;
        }
        public void setType(Type type) {
            if (!waitingForType)
                return;
            if (type.BaseType != typeof(Figure) && type != typeof(Rook) || type == typeof(Pawn))
                return;
            deck.setType(type);
            waitingForType = false;
            if (lastMove.isCheckmate()) {
                gameOver = true;
                winner = lastMove.getMoveColor();
            }
        }
        public void makeMove(int x1, int y1, int x2, int y2) {
            chozenCell = null;
            this.chooseCell(x1, y1);
            this.chooseCell(x2, y2);
        }

        private bool move(int x1, int y1, int x2, int y2) {
            if (gameOver) throw new IllegalMoveException(IllegalMoveException.Causes.gameOver);
            Deck next = new Deck(deck);
            Cell from = next.getCell(x1, y1);
            Cell to = next.getCell(x2, y2);
            if (from.Figure is King && from.y == to.y && Math.Abs(from.x - to.x) == 2) {
                if (from.x - to.x == 2) {
                    next.longCastle();
                } else {
                    next.shortCastle();
                }
            } else {
                try {
                    next.makeMove(from, to);
                } catch (IllegalMoveException e) {
                    if (e.Message == IllegalMoveException.Causes.figureToReplaceNotChosen) {
                        waitingForType = true;
                    } else
                        throw e;
                }
            }
            lastMove = next.getMove();
            ++moveNumber;
            lastMove.setMoveNumber(moveNumber);
            history.Add(next);
            deck = next;

            if (waitingForType)
                throw new IllegalMoveException(IllegalMoveException.Causes.figureToReplaceNotChosen);

            if (lastMove.isCheckmate()) {
                gameOver = true;
                winner = lastMove.getMoveColor();
            }
            return true;
        }
        public bool isWaitingForType() {
            return waitingForType;
        }
        public void giveUp() {
            gameOver = true;
            winner = PlayerColors.not(getMoveColor());
        }

        public void draw() {
            gameOver = true;
            winner = null;
        }

        public bool chooseCell(int x, int y) {
            if (gameOver) throw new IllegalMoveException(IllegalMoveException.Causes.gameOver);
            if (!Utils.isOnField(x, y))
                throw new ArgumentException();
            Figure f = deck.getCell(x, y).Figure;
            if (chozenCell == null) {
                if (f == null)
                    throw new IllegalMoveException(IllegalMoveException.Causes.noFigure);
                if (f.getColor() != getMoveColor())
                    throw new IllegalMoveException(IllegalMoveException.Causes.wrongColor);
                chozenCell = new Tuple<int, int>(x, y);
                return false;
            }
            if (x == chozenCell.Item1 && y == chozenCell.Item2) {
                chozenCell = null;
                return false;
            }
            if (f is not null && f.getColor() == getMoveColor()) {
                chozenCell = new Tuple<int, int>(x, y);
                return false;
            }
            int x0, y0;
            x0 = chozenCell.Item1;
            y0 = chozenCell.Item2;
            chozenCell = null;

            this.move(x0, y0, x, y);
            return true;
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

        public int getMoveNumber() {
            return moveNumber;
        }

        public bool makeMove(string move) {
            int[] coords;
            try {
                coords = Utils.parseMove(move);
            } catch (Exception) {
                throw new ArgumentException();
            }
            makeMove(coords[0], coords[1], coords[2], coords[3]);
            return true;
        }
        public string getCellString(int i, int j) {
            if (!Utils.isOnField(i, j))
                throw new ArgumentOutOfRangeException();
            return deck.getCell(i, j).ToString();
        }

        public PlayerColor getMoveColor() {
            return deck.getMoveColor();
        }

        public List<Deck> getHistory() {
            return history;
        }
    }
}