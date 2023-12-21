using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessEngine.figures;

namespace ChessEngine {

    public class Move {
        private PlayerColor moveColor;
        private Figure fromFigure, toFigure;
        private int x1, y1;
        private int x2, y2;
        private bool check;
        private bool checkmate;
        private string record;
        private int moveNumber;

        public Move(Cell from, Cell to, Figure fromFigure, Figure toFigure, string rec = "", bool check = false, bool checkmate = false, PlayerColor moveColor = PlayerColor.WHITE) {
            x1 = from.x;
            y1 = from.y;
            x2 = to.x;
            y2 = to.y;
            this.fromFigure = fromFigure;
            this.toFigure = toFigure;
            this.check = check;
            this.checkmate = checkmate;
            this.moveColor = moveColor;
            if (rec == "")
                record = buildRecord();
            else
                record = rec;
        }

        public int[] getCoords() {
            return new int[] { x1, y1, x2, y2 };
        }
        private string buildRecord() {
            StringBuilder sb = new StringBuilder();

            if (fromFigure != null && fromFigure is not Pawn) sb.Append(fromFigure.shortCut());
            sb.Append(Utils.numberToLetter(x1));
            sb.Append(y1 + 1);

            if (toFigure != null)
                sb.Append('x');
            else
                sb.Append('-');

            if (toFigure != null && toFigure is not Pawn) sb.Append(toFigure.shortCut());
            sb.Append(Utils.numberToLetter(x2));
            sb.Append(y2 + 1);
            if (checkmate) {
                sb.Append('#');
                return sb.ToString();
            }

            if (check) sb.Append('+');
            return sb.ToString();
        }

        public void setMoveNumber(int n) {
            this.moveNumber = n;
        }

        public int getMoveNumber() {
            return moveNumber;
        }

        public void setCheck(bool check) {
            this.check = check;
            if (check) {
                if (record.EndsWith('+') || record.EndsWith('#'))
                    return;
                record.Append('+');
                return;
            }
            if(record.EndsWith('+') || record.EndsWith('#')) {
                record = record.Substring(0, record.Length - 1);
            }
        }

        public void setCheckmate(bool checkmate) {
            this.checkmate = checkmate;
            if(checkmate)
            {
                if(record.EndsWith('#'))
                    return;
                if(record.EndsWith('+'))
                    record = record.Substring(0, record.Length - 1);
                record.Append('#');
                return;
            }
            setCheck(this.check);
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
