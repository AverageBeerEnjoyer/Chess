using System;
using System.Collections.Generic;
using System.Text;
using ChessEngine.figures;
using System.Reflection;

namespace ChessEngine {
    public class Deck {
        private PlayerColor moveColor = PlayerColor.WHITE;
        private King[] kings = new King[2];

        private Pawn enPassant;

        private Cell[,] cells;

        private Move record;

        private bool waitingForType = false;
        private Cell needToReplace;

        public Deck() {
            initCells();
            startPosition();
        }
        public Move getMove() {
            return record;
        }
        public void setType(Type type) {
            if (waitingForType) { 
                ConstructorInfo ci = type.GetConstructor(new[] { typeof(PlayerColor), typeof(Cell) });
                needToReplace.Figure = (Figure)ci.Invoke(new object[] { PlayerColors.not(moveColor), needToReplace });
                waitingForType = false;
                record.setCheck(isCheck(moveColor));
                record.setCheckmate(isCheckMate(moveColor));
                needToReplace = null;
            }
        }

        private int kingNum(PlayerColor color) {
            return color == PlayerColor.WHITE ? 0 : 1;
        }

        private void startPosition() {
            initFigures(PlayerColor.WHITE);
            initFigures(PlayerColor.BLACK);
        }

        private void initFigures(PlayerColor playerColor) {
            int row1 = PlayerColors.firstRow(playerColor);
            int row2 = PlayerColors.secondRow(playerColor);

            for (int i = 0; i < 8; ++i) {
                cells[i, row2].Figure = new Pawn(playerColor, cells[i, row2]);
            }

            cells[0, row1].Figure = new Rook(playerColor, cells[0, row1]);
            cells[7, row1].Figure = new Rook(playerColor, cells[7, row1]);

            cells[1, row1].Figure = new Knight(playerColor, cells[1, row1]);
            cells[6, row1].Figure = new Knight(playerColor, cells[6, row1]);

            cells[2, row1].Figure = new Bishop(playerColor, cells[2, row1]);
            cells[5, row1].Figure = new Bishop(playerColor, cells[5, row1]);

            cells[3, row1].Figure = new Queen(playerColor, cells[3, row1]);

            King king = new King(playerColor, cells[4, row1]);
            cells[4, row1].Figure = king;
            kings[this.kingNum(playerColor)] = king;
        }

        private void initCells() {
            cells = new Cell[8, 8];
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    cells[i, j] = new Cell(this, i, j);
                }
            }
        }


        public Deck(Deck deck) {
            initCells();
            moveColor = deck.moveColor;
            if (enPassant != null) enPassant = (Pawn)deck.enPassant.Clone();
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    Figure figure = deck.cells[i, j].Figure;
                    if (figure != null) {
                        cells[i, j].Figure = (Figure)figure.Clone();
                        if (figure is King) {
                            kings[kingNum(figure.getColor())] = (King)cells[i, j].Figure;
                        }
                    }
                }
            }
        }

        public Cell getCell(int i, int j) {
            return cells[i, j];
        }

        public Move makeMove(Cell from, Cell to) {
            if (!from.Figure.canMove(to))
                throw new IllegalMoveException(IllegalMoveException.Causes.canNotMoveHere);
            Figure moved = from.Figure;
            Figure taken = to.Figure;

            move(from, to);
            if (isCheck(moveColor)) throw new IllegalMoveException(IllegalMoveException.Causes.kingUnderCheck);

            if (moved is Pawn && to.y == PlayerColors.firstRow(PlayerColors.not(moveColor))) {
                waitingForType = true;
                needToReplace = to;
            }

            moveColor = PlayerColors.not(moveColor);

            record = new Move(from, to, moved, taken, moveColor: PlayerColors.not(moveColor));


            if (waitingForType)
                throw new IllegalMoveException(IllegalMoveException.Causes.figureToReplaceNotChosen);
            record.setCheck(isCheck(moveColor));
            record.setCheckmate(isCheckMate(moveColor));
            
            return record;
        }

        private void move(Cell from, Cell to) {
            if (enPassant != null) enPassant.setEnPassant(false);
            if (from.Figure is MoveNumberDependentFigure figure && figure.isFirstMove()) {
                figure.incrementMoveNumber();
                if (figure is Pawn pawn1 && Math.Abs(from.y - to.y) == 2) {
                    pawn1.setEnPassant(true);
                    enPassant = pawn1;
                }
            }

            if (from.Figure is Pawn pawn && pawn.canTakeEnPassant(to.x)) {
                cells[to.x, from.y].Figure = null;
            }

            to.Figure = from.Figure;
            to.Figure.setCell(to);
            from.Figure = null;
        }

        private bool[,] coverage(PlayerColor color) {
            bool[,] field = new bool[8, 8];
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    Figure figure = cells[i, j].Figure;
                    if (figure == null) continue;
                    if (figure.getColor() != color) continue;
                    figure.markCoverage(field);
                }
            }

            return field;
        }

        public Move shortCastle() {
            int row = PlayerColors.firstRow(moveColor);
            bool[,] coverage = this.coverage(PlayerColors.not(moveColor));
            checkRookAndKing(cells[7, row]);
            for (int i = 4; i < 7; ++i) {
                if (coverage[i, row]) throw new IllegalMoveException(IllegalMoveException.Causes.castleUnderCheck);
            }

            move(cells[4, row], cells[6, row]);
            move(cells[7, row], cells[5, row]);
            moveColor = PlayerColors.not(moveColor);
            record = new Move(
                cells[4, row],
                cells[2, row],
                null,
                null,
                "00",
                isCheck(moveColor),
                isCheckMate(moveColor),
                PlayerColors.not(moveColor));
            return record;
        }

        private void checkRookAndKing(Cell rookCell) {
            Figure figure = rookCell.Figure;
            if (!kings[kingNum(moveColor)].isFirstMove() ||
                figure == null ||
                !(figure is Rook rook) ||
                rook.getColor() != moveColor ||
                !rook.isFirstMove()) throw new IllegalMoveException(IllegalMoveException.Causes.castleMoveCondition);
        }

        public Move longCastle() {
            int row = PlayerColors.firstRow(moveColor);
            bool[,] coverage = this.coverage(PlayerColors.not(moveColor));
            checkRookAndKing(cells[0, row]);
            for (int i = 2; i < 5; ++i) {
                if (coverage[i, row]) throw new IllegalMoveException(IllegalMoveException.Causes.castleUnderCheck);
            }

            move(cells[4, row], cells[2, row]);
            move(cells[0, row], cells[3, row]);
            moveColor = PlayerColors.not(moveColor);
            record = new Move(
                cells[4, row],
                cells[2, row],
                null,
                null,
                "00",
                isCheck(moveColor),
                isCheckMate(moveColor),
                PlayerColors.not(moveColor));
            return record;
        }

        public bool isCheck(PlayerColor color) {
            bool[,] coverage = this.coverage(PlayerColors.not(color));
            King king = kings[this.kingNum(color)];
            return coverage[king.getCell().x, king.getCell().y];
        }

        public bool isCheckMate(PlayerColor color) {
            King king = kings[kingNum(color)];
            List<Figure> attackingFigures = king.getAttackingFigures();
            bool[,] coverage = this.coverage(PlayerColors.not(color));
            if (attackingFigures.Count == 0) return false;
            bool hasOuts = king.hasOuts(coverage);
            if (hasOuts) return false;
            if (attackingFigures.Count == 2) return true;
            return !canAvoidCheck(attackingFigures[0], color);
        }

        private bool canAvoidCheckByTakingCell(Cell cell, PlayerColor color) {
            Deck deck;
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    deck = new Deck(this);
                    Figure figure = deck.cells[i, j].Figure;
                    Cell to = deck.cells[cell.x, cell.y];
                    if (figure == null || figure.getColor() != color) continue;
                    if (!figure.canMove(to)) continue;
                    try {
                        deck.makeMove(deck.cells[i, j], to);
                    } catch (IllegalMoveException e) {
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }


        private bool canAvoidCheck(Figure figure, PlayerColor color) {
            King king = kings[kingNum(color)];
            if (figure is Knight) {
                return canAvoidCheckByTakingCell(figure.getCell(), color);
            }

            int dx = figure.getCell().x - king.getCell().x;
            int dy = figure.getCell().y - king.getCell().y;

            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);

            int distance = Math.Max(Math.Abs(dx), Math.Abs(dy));

            for (int i = 1; i <= distance; ++i) {
                if (canAvoidCheckByTakingCell(cells[king.getCell().x + dirX * i, king.getCell().y + dirY * i], color))
                    return true;
            }

            return false;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    if (j == 0) sb.Append("|");
                    sb.Append(cells[i, j].ToString());
                    sb.Append("|");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public PlayerColor getMoveColor() {
            return moveColor;
        }
    }
}