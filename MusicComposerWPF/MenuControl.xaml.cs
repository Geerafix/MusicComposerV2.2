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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicComposerWPF {
    /// <summary>
    /// Logika interakcji dla klasy MenuControl.xaml
    /// </summary>
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

        }
    }
}
