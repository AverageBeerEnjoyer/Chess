using System.Buffers.Text;

namespace ChessEngine.figures {
    public abstract class MoveNumberDependentFigure : Figure {
        protected bool firstMove = true;
        protected MoveNumberDependentFigure(PlayerColor color, Cell cell) : base(color, cell) { }

        public void incrementMoveNumber() {
            firstMove = false;
        }
        
        public bool isFirstMove() {
            return firstMove;
        }
        protected MoveNumberDependentFigure(MoveNumberDependentFigure figure) : base(figure) {
            this.firstMove = figure.firstMove;
        }
    }
}