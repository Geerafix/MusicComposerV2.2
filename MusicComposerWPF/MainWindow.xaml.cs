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
    public partial class MainWindow : Window {
        private MidiOut play = new MidiOut(0);
        private TracksControl tc;
        private MenuControl mc;
        private ComposeControl cc;
        private EditControl ec;
        public MainWindow() {
            InitializeComponent();
            mc = (MenuControl)FindName("MenuControl");
            tc = (TracksControl)FindName("TracksControl");
            cc = (ComposeControl)FindName("ComposeControl");
            ec = (EditControl)FindName("EditControl");
            mc.Visibility = Visibility.Visible;
            tc.Visibility = Visibility.Collapsed;
            cc.Visibility = Visibility.Collapsed;
            ec.Visibility = Visibility.Collapsed;
        }    

        public MidiOut getMidi() {
            return play;
        }

        public void toTracksFromMenu() {
            tc.loadTracks();
            tc.Visibility = Visibility.Visible;
        }

        public void toMenuFromTracks() {
            mc.Visibility = Visibility.Visible;
        }

        public void toEditFromTracks(string trackName, List<Note> track) {
            ec.loadTrack(trackName, track);
            ec.Visibility = Visibility.Visible;
        }

        public void toTracksFromEdit() {
            tc.Visibility = Visibility.Visible;
        }

        public void toComposeFromMenu() {
            cc.loadTempTrack();
            cc.Visibility = Visibility.Visible;
        }

        public void toMenuFromCompose() {
            mc.Visibility = Visibility.Visible;
        }
    }
}
