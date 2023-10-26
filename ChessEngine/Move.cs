using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessEngine.figures;

namespace ChessEngine {

    public class Move {
        private PlayerColor moveColor;
        private int x1, y1;
        private int x2, y2;
        private bool check;
        private bool checkmate;
        private string record;
        public Move(Cell from, Cell to) {
            x1 = from.x;
            y1 = from.y;
            x2 = to.x;
            y2 = to.y;
            record = buildRecord();
            moveColor = from.Figure.getColor();
        }

        private string buildRecord() {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(Utils.numberToLetter(x1));
            sb.Append(y1+1);
            
            sb.Append(" ");

            sb.Append(Utils.numberToLetter(x2));
            sb.Append(y2+1);
            if (checkmate) {
                sb.Append("#");
                return sb.ToString();
            }

            if (check) sb.Append("+");
            return sb.ToString();
        }

        public void setCheck(bool check) {
            this.check = check;
        }

        public void setCheckmate(bool checkmate) {
            this.checkmate = checkmate;
        }

        public bool isCheck() {
            return check;
        }

        public bool isCheckmate() {
            return checkmate;
        }
        public string getRecord() {
            return record;
        }
        public PlayerColor getMoveColor() {
            return moveColor;
        }
    }
}
