﻿using System;
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

namespace ChessApp {
    /// <summary>
    /// Interaction logic for MoveHistory.xaml
    /// </summary>
    public partial class MoveHistory : Window {
        private ChessDB db = ChessDB.getInstance();
        public MoveHistory() {
            InitializeComponent();
        }
        public void setGameId(Guid id) {
            moves.ItemsSource = db.getMovesByGameId(id);
        }
    }
}
