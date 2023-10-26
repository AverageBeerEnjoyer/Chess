using System;

namespace ChessEngine.figures {
    public class Knight : Figure {

        public Knight(PlayerColor color, Cell cell) : base(color,cell) { }
        public override bool canMove(Cell target) {
            int dx = target.x - cell.x;
            int dy = target.y - cell.y;

            if(Math.Abs(dx) < 1 || Math.Abs(dy) < 1) return false;
            if(Math.Abs(dx)+Math.Abs(dy)!=3) return false;
            return target.isEmpty() || target.Figure.getColor() != color;
        }
        
        public Knight(Knight figure) : base(figure){}
        
        
        private protected override string shortCut() {
            return "K";
        }

        public override object Clone() {
            return new Knight(this);
        }

        public override void markCoverage(bool[,] field) {
            for (int i = -2; i <= 2; ++i) {
                if (i == 0) continue;
                int dy = 3 - Math.Abs(i);
                if (Utils.isOnField(cell.x + i, cell.y + dy)) {
                    field[cell.x + i, cell.y + dy] = true;
                }
                if (Utils.isOnField(cell.x + i, cell.y - dy)) {
                    field[cell.x + i, cell.y - dy] = true;
                }
            }
        }
    }
}
