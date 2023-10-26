using System;

namespace ChessEngine.figures {
    public abstract class Figure : ICloneable {
        private protected Cell cell;
        private protected PlayerColor color;

        protected Figure(PlayerColor color, Cell cell) {
            this.color = color;
            this.cell = cell;
        }

        public abstract bool canMove(Cell target);

        public void setCell(Cell cell) {
            this.cell = cell;
        }

        public Cell getCell() {
            return cell;
        }

        private protected abstract string shortCut();

        public override string ToString() {
            return PlayerColors.shortCut(color) + shortCut();
        }

        public abstract object Clone();

        public PlayerColor getColor() {
            return color;
        }

        protected Figure(Figure figure) {
            this.color = figure.color;
            this.cell = null;
        }

        public abstract void markCoverage(bool[,] field);

        protected void markInDirection(bool[,] field, int dirX, int dirY, int distance) {
            for (int i = 1; i <= distance; ++i) {
                int x = cell.x + i * dirX;
                int y = cell.y + i * dirY;

                if (!Utils.isOnField(x, y)) return;
                field[x, y] = true;
                if (cell.deck.getCell(x, y).Figure != null) return;
            }
        }

        protected bool canMoveInDirection(int dirX, int dirY, int distance) {
            for (int i = 1; i < distance; ++i) {
                if (!cell.getDeck().getCell(cell.x + dirX * i, cell.y + dirY * i).isEmpty()) return false;
            }

            Cell target = cell.deck.getCell(cell.x + dirX * distance, cell.y + dirY * distance);
            return target.isEmpty() || target.Figure.getColor() != color;
        }
    }
}