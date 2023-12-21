using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine.figures {
    public class Pawn : MoveNumberDependentFigure {
        private bool enPassant;
        public Pawn(PlayerColor color, Cell cell) : base(color, cell) { }
        public override bool canMove(Cell target) {
            int dm = PlayerColors.directionModifier(color);
            int dx = target.x - cell.x;
            int dy = (target.y - cell.y) * dm;
            Deck deck = cell.getDeck();
            
            if(dy < 1 || dy > 2 || Math.Abs(dx) > 1) return false;

            if(dx == 0) {
                if (!deck.getCell(cell.x, cell.y + dm).isEmpty()) return false;
                if (dy == 1) return true;
                if(!firstMove || !deck.getCell(cell.x, cell.y + 2 * dm).isEmpty()) return false;
                if (cell.y != PlayerColors.secondRow(color)) return false;
                return true;
            }
            if (dy == 2) return false;
            if (canTakeEnPassant(target.x)) return true;
            if(target.isEmpty()) return false;
            if(target.Figure.getColor() == color) return false;
            return true;
        }

        private Pawn(Pawn figure) : base(figure) {
            enPassant = figure.enPassant;
        }

        public override string shortCut() {
            return " ";
        }

        public override object Clone() {
            return new Pawn(this);
        }
        public override void markCoverage(bool[,] field) {
            markInDirection(field,1,PlayerColors.directionModifier(color), 1);
            markInDirection(field,-1,PlayerColors.directionModifier(color), 1);
        }

        public bool getEnPassant() {
            return enPassant;
        }

        public void setEnPassant(bool value) {
            enPassant = value;
        }

        public bool canTakeEnPassant(int x) {
            if (Math.Abs(cell.x - x) != 1) return false;
            Figure fig = cell.deck.getCell(x, cell.y).Figure;
            return fig is Pawn pawn && pawn.getColor() != color && pawn.enPassant;
        }
    }
}
