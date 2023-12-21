using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChessEngine;
using ChessEngine.figures;
namespace ChessApp {
    /// <summary>
    /// Interaction logic for FigureChosing.xaml
    /// </summary>
    public partial class FigureChosing : Window {
        private Game game;
        private PlayerColor color;
        public FigureChosing() {
            InitializeComponent();
        }

        public FigureChosing(Game game):this() {
            this.game = game;
            this.color = game.getLastMove().getMoveColor();
            initImages();
        }

        private void initImages() {
            Image image = Images.img(Images.getSourceByCellString(color == PlayerColor.WHITE ? "WQ" : "BQ"));
            image.MouseDown += (sender, args) => game.setType(typeof(Queen));
            image.MouseDown += (sender, args) => Close();
            image.Height = image.Width = 50;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Margin = new Thickness(30, 30, 0, 0);
            grid.Children.Add(image);

            image = Images.img(Images.getSourceByCellString(color == PlayerColor.WHITE ? "WR" : "BR"));
            image.MouseDown += (sender, args) => game.setType(typeof(Rook));
            image.MouseDown += (sender, args) => Close();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = image.Width = 50;
            image.Margin = new Thickness(140, 30, 0, 0);
            grid.Children.Add(image);

            image = Images.img(Images.getSourceByCellString(color == PlayerColor.WHITE ? "WN" : "BN"));
            image.MouseDown += (sender, args) => game.setType(typeof(Knight));
            image.MouseDown += (sender, args) => Close();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = image.Width = 50;
            image.Margin = new Thickness(250, 30, 0, 0);
            grid.Children.Add(image);

            image = Images.img(Images.getSourceByCellString(color == PlayerColor.WHITE ? "WB" : "BB"));
            image.MouseDown += (sender, args) => game.setType(typeof(Bishop));
            image.MouseDown += (sender, args) => Close();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = image.Width = 50;
            image.Margin = new Thickness(360, 30, 0, 0);
            grid.Children.Add(image);

        }
    }
}
