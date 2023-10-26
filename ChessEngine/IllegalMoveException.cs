using System;

namespace ChessEngine {
    public class IllegalMoveException : Exception{
        public IllegalMoveException(string message) : base(message) {}
        public static class Causes {
            public static string noFigure = "Фигура не выбрана";
            public static string wrongColor = "В этот ход нельзя сходить фигурой этого цвета";
            public static string canNotMoveHere = "Сюда нельзя сходить";
            public static string kingUnderCheck = "Король под шахом";
            public static string castleUnderCheck = "Нельзя рокироваться под шахом";
            public static string castleMoveCondition = "Нельзя рокироваться, если король или ладья уже делали ход";
        }
    }
}