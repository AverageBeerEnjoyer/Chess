using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessEngine.figures;

namespace ChessEngine {
    public class Position {
        private readonly Move lastMove;
        private readonly PlayerColor moveColor;
        private string[,] cells;

        public Position(Deck deck) {
            
            moveColor = deck.getMoveColor();
            cells = new string[8, 8];
            for(int i = 0; i < 8; ++i) {
                for(int j = 0; j < 8; ++j) {
                    Figure figure = deck.getCell(i, j).Figure;
                    cells[i,j] = figure == null ? "" : figure.ToString();
                }
            }
        }
    }
}
