using System;

namespace ChessEngine.figures {
    public class Queen : Figure {
        public Queen(PlayerColor color, Cell cell) : base(color, cell) { }

        private Queen(Queen queen) : base(queen) {}

        public override bool canMove(Cell target) {
            int dx = target.x - cell.x;
            int dy = target.y - cell.y;
            
            int dirX = Math.Sign(dx);
            int dirY = Math.Sign(dy);
            
            int directionType = Math.Abs(dirX) + Math.Abs(dirY);
            int distance;
            
            switch (directionType) {
                case 1: {
                    distance = Math.Max(Math.Abs(dx), Math.Abs(dy));
                    break;
                }
                case 2: {
                    if (Math.Abs(dx) != Math.Abs(dy)) return false;
                    distance = Math.Abs(dx);
                    break;
                }
                default: return false;
            }
            return canMoveInDirection(dirX, dirY, distance);
        }

        public override string shortCut() {
            return "Q";
        }

        public override object Clone() {
            return new Queen(this);
        }

        public override void markCoverage(bool[,] field) {
            int maxDistance = Math.Max(Math.Max(cell.x, 7 - cell.x), Math.Max(cell.y, 7 - cell.y));
            markInDirection(field, 1,0,maxDistance);
            markInDirection(field, -1,0,maxDistance);
            markInDirection(field, 0,1,maxDistance);
            markInDirection(field, 0,-1,maxDistance);
            
            markInDirection(field, 1,1,maxDistance);
            markInDirection(field, 1,-1,maxDistance);
            markInDirection(field, -1,1,maxDistance);
            markInDirection(field, -1,-1,maxDistance);
        }
    }
}