using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    public enum PlayerColor {
        WHITE,
        BLACK
    }

    public static class PlayerColors {
        public static string shortCut(PlayerColor color) {
            return color == PlayerColor.WHITE ? "W" : "B";
        }

        public static int directionModifier(PlayerColor color) { 
            return color == PlayerColor.WHITE ? 1 : -1;
        }
        public static int firstRow(PlayerColor color) {
            return color == PlayerColor.WHITE ? 0 : 7;
        }
        public static int secondRow(PlayerColor color) {
            return color == PlayerColor.WHITE ? 1 : 6;
        }

        public static PlayerColor not(PlayerColor color) {
            return color == PlayerColor.WHITE ? PlayerColor.BLACK : PlayerColor.WHITE;
        }
    }
}
