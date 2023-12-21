using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChessEngine;
using System;

namespace ChessApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ChessDB db = ChessDB.getInstance();
        private Game game;
        private bool deckOriantation;


        public MainWindow() {
            InitializeComponent();
            this.game = new Game();
            newGame_Click(null, null);
        }


        private void initializeField(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    Image image = new Image() { Source = (i + j) % 2 == 0 ? Images.lightCell : Images.darkCell };
                    Label l = new Label();
                    int x = i + 1;
                    int y = j + 1;
                    l.MouseLeftButtonDown += (sender, e) => cell_Clicked(x, y);
                    addToGrid(forDeck, j + 1, i + 1, image);
                    addToGrid(forHandlers, j + 1, i + 1, l);
                }
            }
            updatePozition();
        }

        private void addToGrid(Grid grid, int row, int col, UIElement child) {
            Grid.SetRow(child, row);
            Grid.SetColumn(child, col);
            grid.Children.Add(child);
        }

        private Tuple<int, int> convDeckToGrid(int x, int y) {
            //PlayerColor color = game.getMoveColor();
            //int dx = color == PlayerColor.WHITE ? x+1 : 8-x;
            //int dy = color == PlayerColor.WHITE ? 8-y : y+1;

            int dx = x + 1;
            int dy = 8 - y;
            return new Tuple<int, int>(dx, dy);
        }

        private Tuple<int, int> convGridToDeck(int x, int y) {
            //PlayerColor color = game.getMoveColor();
            //int dx = color == PlayerColor.WHITE ? x - 1 : 8-x;
            //int dy = color == PlayerColor.WHITE ? 8-y : y - 1;
            int dx = x - 1;
            int dy = 8 - y;
            return new Tuple<int, int>(dx, dy);
        }

        private void cell_Clicked(int x, int y) {
            Tuple<int, int> to = convGridToDeck(x, y);
            try {
                if (game.chooseCell(to.Item1, to.Item2)) {
                    writeRecord();
                    updatePozition();
                }
                errorTextBlock.Text = "";
            } catch (IllegalMoveException e) {
                if (e.Message == IllegalMoveException.Causes.figureToReplaceNotChosen) {
                    while(game.isWaitingForType())
                    {
                        FigureChosing chosing = new FigureChosing(game);
                        chosing.ShowDialog();
                    }
                    updatePozition();
                    writeRecord();
                } else
                    errorTextBlock.Text = e.Message;
            } catch (Exception ex) {
                errorTextBlock.Text = ex.Message;
            }

        }

        private void writeRecord() {
            Move move = game.getLastMove();
            string moveRecord = move.getMoveNumber().ToString() + ". " + move.getRecord();
            history.Items.Add("\n" + moveRecord);

        }


        private void updatePozition() {
            forFigures.Children.Clear();
            for (int i = 0; i < 8; ++i) {
                for (int j = 0; j < 8; ++j) {
                    Tuple<int, int> coords = convDeckToGrid(i, j);
                    addToGrid(
                        forFigures,
                        coords.Item2,
                        coords.Item1,
                        Images.img(Images.getSourceByCellString(game.getCellString(i, j))));

                }
                Tuple<int, int> t = convDeckToGrid(i, i);
                addToGrid(
                    forFigures,
                    t.Item2,
                    0,
                    new Label() {
                        Content = i + 1,
                        FontSize = 40,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    });
                addToGrid(
                    forFigures,
                    9,
                    t.Item1,
                    new Label() {
                        Content = Utils.numberToLetter(i),
                        FontSize = 40,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    });
            }
        }

        private void newGame_Click(object sender, RoutedEventArgs e) {
            this.game = new Game();
            history.Items.Clear();
            deckOriantation = true;
            updatePozition();
        }

        private void giveUp(object sender, RoutedEventArgs e) {
            game.giveUp();
        }

        private void saveGame(object sender, RoutedEventArgs e) {
            db.AddGame(game);
            db.save();
        }

        private void showHistory(object sender, RoutedEventArgs e) {
            Window historyWindow = new History();
            historyWindow.ShowDialog();
        }

        private void askForDraw(object sender, RoutedEventArgs e) {
            string msg = "Вы согласны на ничью?";
            string title = "Предложение ничьей";
            MessageBoxButton btn = MessageBoxButton.YesNo;
            MessageBoxImage img = MessageBoxImage.Question;
            MessageBoxResult res = MessageBox.Show(msg, title, btn, img);
            switch(res)
            {
                case MessageBoxResult.Yes:
                {
                    game.draw();
                    break;
                }
                default: break;
            }
        }
    }
}
