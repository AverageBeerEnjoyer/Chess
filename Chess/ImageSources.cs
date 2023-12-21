using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XamlReader = System.Windows.Markup.XamlReader;

namespace ChessApp {
    public static class Images {

        public static readonly DrawingImage darkCell = new DrawingImage(loadGroup("assets/xaml/dark-cell.xaml"));
        public static readonly DrawingImage lightCell = new DrawingImage(loadGroup("assets/xaml/light-cell.xaml"));

        public static readonly DrawingImage whitePawn = loadImage("assets/xaml/w-pawn.xaml");
        public static readonly DrawingImage whiteRook = loadImage("assets/xaml/w-rook.xaml");
        public static readonly DrawingImage whiteKnight = loadImage("assets/xaml/w-knight.xaml");
        public static readonly DrawingImage whiteBishop = loadImage("assets/xaml/w-bishop.xaml");
        public static readonly DrawingImage whiteQueen = loadImage("assets/xaml/w-queen.xaml");
        public static readonly DrawingImage whiteKing = loadImage("assets/xaml/w-king.xaml");

        public static readonly DrawingImage blackPawn = loadImage("assets/xaml/b-pawn.xaml");
        public static readonly DrawingImage blackRook = loadImage("assets/xaml/b-rook.xaml");
        public static readonly DrawingImage blackKnight = loadImage("assets/xaml/b-knight.xaml");
        public static readonly DrawingImage blackBishop = loadImage("assets/xaml/b-bishop.xaml");
        public static readonly DrawingImage blackQueen = loadImage("assets/xaml/b-queen.xaml");
        public static readonly DrawingImage blackKing = loadImage("assets/xaml/b-king.xaml");

        private static DrawingGroup loadGroup(string path) {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            DrawingGroup dg = (DrawingGroup) XamlReader.Load(fileStream);
            return dg;
        }
        private static DrawingImage loadImage(string path) {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            return (DrawingImage)XamlReader.Load(fileStream);
        }
        public static DrawingImage getSourceByCellString(string figure) {
            switch (figure) {
                case "W ": return whitePawn;
                case "WR": return whiteRook;
                case "WN": return whiteKnight;
                case "WB": return whiteBishop;
                case "WQ": return whiteQueen;
                case "WK": return whiteKing;

                case "B ": return blackPawn;
                case "BR": return blackRook;
                case "BN": return blackKnight;
                case "BB": return blackBishop;
                case "BQ": return blackQueen;
                case "BK": return blackKing;

                default: return null;
            }
        }

        public static Image img(DrawingImage image) { return new Image() { Source = image, Margin = new Thickness(5) }; }
    }
}