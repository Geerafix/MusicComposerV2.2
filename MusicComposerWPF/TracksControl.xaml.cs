using MusicComposer;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class TracksControl : UserControl {
        private FileInfo[] tracks;
        private DirectoryInfo directory;
        private Thread thread;
        private string path = "../../../Tracks/";
        private int id;

        public TracksControl() {
            InitializeComponent();
            directory = new DirectoryInfo(path);
        }

        public void loadTracks() {
            tracksListBox.Items.Clear();
            tracks = directory.GetFiles("*.txt");
            foreach (FileInfo file in tracks) {
                tracksListBox.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
            }
        }
        private void menuButton_Click(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toMenuFromTracks();
            this.Visibility = Visibility.Collapsed;
            tracksListBox.UnselectAll();
            hideButtons();
        }

        private void modifyTrackButton_Click(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toEditFromTracks(tracksListBox.Items[id].ToString(), toList(tracksListBox.Items[id].ToString()));
            this.Visibility = Visibility.Collapsed;
            tracksListBox.UnselectAll();
            hideButtons();
        }

        private void tracksListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if ((id = tracksListBox.SelectedIndex) != -1) {
                deleteTrackButton.Visibility = Visibility.Visible;
                modifyTrackButton.Visibility = Visibility.Visible;
                playTrackButton.Visibility = Visibility.Visible;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            tracks[id].Delete();
            tracksListBox.Items.RemoveAt(id);
            hideButtons();
        }

        private void playTrackButton_Click(Object sender, EventArgs e) {
            string[] lines = File.ReadAllLines(path + tracksListBox.Items[id].ToString() + ".txt");
            MainWindow win = (MainWindow)Window.GetWindow(this);
            MidiOut play = win.getMidi();
            thread = new Thread(() => {
                for (int i = 0 ; i < lines.Length ; i += 2) {
                    play.Send(MidiMessage.StartNote(Int32.Parse(lines[i]), 127, 1).RawData);
                    Thread.Sleep(Int32.Parse(lines[i + 1]));
                    play.Send(MidiMessage.StopNote(Int32.Parse(lines[i]), 127, 1).RawData);
                }
            });
            thread.Start();
        }

        public void hideButtons() {
            deleteTrackButton.Visibility = Visibility.Collapsed;
            modifyTrackButton.Visibility = Visibility.Collapsed;
            playTrackButton.Visibility = Visibility.Collapsed;
        }

        private List<Note> toList(string filename) {
            List<Note> track = new List<Note>();
            string[] lines = File.ReadAllLines(path + filename + ".txt");
            for (int i = 0 ; i < lines.Length ; i += 2) {
                track.Add(new Note(Int32.Parse(lines[i]), Int32.Parse(lines[i + 1])));
            }
            return track;
        }
    }
}
