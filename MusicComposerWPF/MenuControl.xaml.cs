using System;
using System.Windows;
using System.Windows.Controls;

namespace MusicComposerWPF {
    public partial class MenuControl : UserControl {
        public MenuControl() {
            InitializeComponent();
        }

        public void toTracks(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toTracksFromMenu();
            this.Visibility = Visibility.Collapsed;
        }

        public void toExit(object sender, EventArgs e) {
            Environment.Exit(0);
        }

        public void toCompose(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toComposeFromMenu();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
