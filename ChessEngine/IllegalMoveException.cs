using System;

namespace ChessEngine {
    public class IllegalMoveException : Exception{
        public IllegalMoveException(string message) : base(message) {}
        public static class Causes {
            public static readonly string noFigure = "Фигура не выбрана";
            public static readonly string wrongColor = "В этот ход нельзя сходить фигурой этого цвета";
            public static readonly string canNotMoveHere = "Сюда нельзя сходить";
            public static readonly string kingUnderCheck = "Король под шахом";
            public static readonly string castleUnderCheck = "Нельзя рокироваться под шахом";
            public static readonly string castleMoveCondition = "Нельзя рокироваться, если король или ладья уже делали ход";
            public static readonly string gameOver = "Игра закончена";
            public static readonly string figureToReplaceNotChosen = "Фигура для замены пешки не выбрана";
        }
    }
}