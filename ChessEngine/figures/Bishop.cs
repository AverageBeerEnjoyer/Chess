using System;

namespace ChessEngine.figures {
    public class Bishop : Figure {
        public Bishop(PlayerColor color, Cell cell) : base(color, cell) { }

        public override bool canMove(Cell target) {
            int dx = target.x - cell.x;
            int dy = target.y - cell.y;

            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);

            if (Math.Abs(dirX) + Math.Abs(dirY) != 2) return false;
            if (Math.Abs(dx) != Math.Abs(dy)) return false;

            int distance = Math.Abs(dx);

            return canMoveInDirection(dirX, dirY, distance);
        }

        public Bishop(Bishop figure) : base(figure) { }

        private protected override string shortCut() {
            return "B";
        }

        public override object Clone() {
            return new Bishop(this);
        }

        public override void markCoverage(bool[,] field) {
            int maxDistance = Math.Max(Math.Max(cell.x, 7 - cell.x), Math.Max(cell.y, 7 - cell.y));
            markInDirection(field, 1, 1, maxDistance);
            markInDirection(field, 1, -1, maxDistance);
            markInDirection(field, -1, 1, maxDistance);
            markInDirection(field, -1, -1, maxDistance);
        }
    }
}