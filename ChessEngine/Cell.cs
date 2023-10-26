using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessEngine.figures;

namespace ChessEngine {
    public class Cell {
        public readonly int x, y;
        private Figure figure;

        public Figure Figure {
            get { return figure; }
            set {
                figure = value;
                if (value != null) value.setCell(this);
            }
        }

        public readonly Deck deck;

        public Cell(Deck deck, int x, int y) {
            this.deck = deck;
            this.x = x;
            this.y = y;
        }

        public Deck getDeck() {
            return deck;
        }

        public bool isEmpty() {
            return figure == null;
        }

        public override string ToString() {
            return figure == null ? " ": figure.ToString();
        }
    }
}