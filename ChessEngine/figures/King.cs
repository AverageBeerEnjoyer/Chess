using System;
using System.Collections.Generic;
using System.Reflection;

namespace ChessEngine.figures {
    public class King : MoveNumberDependentFigure {
        public King(PlayerColor color, Cell cell) : base(color, cell) { }

        public override bool canMove(Cell target) {
            int dx = target.x - cell.x;
            int dy = target.y - cell.y;

            if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1) return false;

            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);

            if (Math.Abs(dirX) + Math.Abs(dirY) == 0) return false;

            return target.isEmpty() || target.Figure.getColor() != color;
        }

        public King(King figure) : base(figure) { }


        private protected override string shortCut() {
            return " ";
        }

        public override object Clone() {
            return new King(this);
        }

        public List<Figure> getAttackingFigures() {
            List<Figure> res = new List<Figure>();
            res.Add(checkAttackFromPawns());
            res.Add(checkAttackFromKnights());
            res.Add(checkAttackFromDirection(0, 1));
            res.Add(checkAttackFromDirection(0, -1));
            res.Add(checkAttackFromDirection(1, 0));
            res.Add(checkAttackFromDirection(-1, 0));
            res.Add(checkAttackFromDirection(1, 1));
            res.Add(checkAttackFromDirection(-1, 1));
            res.Add(checkAttackFromDirection(1, -1));
            res.Add(checkAttackFromDirection(-1, -1));
            res.RemoveAll(f => f == null);
            return res;
        }

        private Figure checkAttackFromPawns() {
            Figure figure = getIf(f => f is Pawn, cell.x - 1, cell.y + PlayerColors.directionModifier(color));
            if (figure != null) return figure;

            figure = getIf(f => f is Pawn, cell.x + 1, cell.y + PlayerColors.directionModifier(color));
            if (figure != null) return figure;

            return null;
        }

        private Figure getIf(Predicate<Figure> classPredicate, int x, int y) {
            if (!Utils.isOnField(x, y)) return null;
            Figure figure = cell.deck.getCell(x, y).Figure;
            if (figure == null || figure.getColor() == color) return null;
            return classPredicate.Invoke(figure) ? figure : null;
        }


        private Figure checkAttackFromKnights() {
            for (int i = -2; i <= 2; ++i) {
                if (i == 0) continue;
                int dy = 3 - Math.Abs(i);
                Figure figure = getIf(f => f is Knight, cell.x + i, cell.y + dy);
                if (figure != null) return figure;

                figure = getIf(f => f is Knight, cell.x + i, cell.y - dy);
                if (figure != null) return figure;
            }

            return null;
        }

        private Figure checkAttackFromDirection(int dirX, int dirY) {
            int directionType = Math.Abs(dirX) + Math.Abs(dirY);
            for (int i = 1; i <= 7; ++i) {
                int x = cell.x + i * dirX;
                int y = cell.y + i * dirY;
                
                if (!Utils.isOnField(x, y)) break;

                if(cell.deck.getCell(x, y).Figure==null) continue;
                
                return getIf(
                    fig => fig is Queen || directionType == 2 && fig is Bishop || directionType == 1 && fig is Rook,
                    x, y);
            }

            return null;
        }

        public override void markCoverage(bool[,] field) {
            for (int i = cell.x - 1; i <= cell.x + 1; ++i) {
                for (int j = cell.x - 1; j <= cell.x + 1; ++j) {
                    if (Utils.isOnField(i, j)) field[i, j] = true;
                }
            }
        }

        public bool hasOuts(bool[,] coverage) {
            for (int i = cell.x - 1; i <= cell.x + 1; ++i) {
                for (int j = cell.x - 1; j <= cell.x + 1; ++j) {
                    if (Utils.isOnField(i, j) && !coverage[i, j] && canMove(cell.deck.getCell(i, j))) return true;
                }
            }

            return false;
        }
    }
}