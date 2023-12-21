using System;
using System.Windows;
using System.Windows.Controls;
using ChessApp;
namespace ChessApp {
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window {
        ChessDB db = ChessDB.getInstance();
        public History() {
            InitializeComponent();

            games.ItemsSource = db.getGames();
            games.Loaded += (sender, args)=> setHandlers();
        }

        private void setHandlers() {
            foreach (ChessDB.GameRecord v in games.ItemContainerGenerator.Items) {
                (games.ItemContainerGenerator.ContainerFromItem(v) as ListViewItem).MouseDoubleClick += (sender, args) => showMovesList(((ChessDB.GameRecord)v).id);
            }
        }
        private void showMovesList(Guid id) {
            MoveHistory mh = new MoveHistory();
            mh.setGameId(id);
            mh.Show();
        }

    }
}
