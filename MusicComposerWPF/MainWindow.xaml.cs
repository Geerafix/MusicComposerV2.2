using MusicComposer;
using NAudio.Midi;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MidiOut play = new MidiOut(0);
        TracksControl tc;
        MenuControl mc;
        public MainWindow() {
            InitializeComponent();
            mc = (MenuControl)FindName("MenuControl");
            tc = (TracksControl)FindName("TracksControl");
            mc.Visibility = Visibility.Visible;
            tc.Visibility = Visibility.Collapsed;
        }    

        public MidiOut getMidi() {
            return play;
        }

        public void toTracksFromMenu() {
            tc.Visibility = Visibility.Visible;
        }

        public void toMenuFromTracks() {
            mc.Visibility = Visibility.Visible;
        }

        public void toEditFromTracks(string trackName, List<Note> track) {

        }

        public void toTracksFromEdit() {
            
        }

        public void toComposeFromMenu() {
            
        }
        public void toMenuFromCompose() {
            
        }
    }
}
