using System;

namespace ChessEngine.figures {
    public class Rook : MoveNumberDependentFigure {
        public Rook(PlayerColor color, Cell cell) : base(color, cell) { }

        public override bool canMove(Cell target) {
            int dx = target.x - cell.x;
            int dy = target.y - cell.y;
            
            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);
            
            if (Math.Abs(dirX) + Math.Abs(dirY) != 1) return false;
            int distance = Math.Max(Math.Abs(dx), Math.Abs(dy));

            return canMoveInDirection(dirX, dirY, distance);
        }

        public Rook(Rook figure) : base(figure) {}

        private protected override string shortCut() {
            return "R";
        }

        public override object Clone() {
            return new Rook(this);
        }

        public override void markCoverage(bool[,] field) {
            int maxDistance = Math.Max(Math.Max(cell.x, 7 - cell.x), Math.Max(cell.y, 7 - cell.y));
            markInDirection(field, 1,0,maxDistance);
            markInDirection(field, -1,0,maxDistance);
            markInDirection(field, 0,1,maxDistance);
            markInDirection(field, 0,-1,maxDistance);
        }
    }
}