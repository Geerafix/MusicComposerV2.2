using MusicComposer;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace MusicComposerWPF {
    /// <summary>
    /// Logika interakcji dla klasy ComposeControl.xaml
    /// </summary>
    public partial class ComposeControl : UserControl {
        private string path = "../../../Resources/tempTrack.txt";
        private List<Note> track = new List<Note>();
        private Thread thread;
        private int pos = 0, currentNote = 0, currentDuration = 500;

        private int[] notes = {
            24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
            34, 35, 36, 37, 38, 39, 40, 41, 42, 43,
            44, 45, 46, 47, 48, 49, 50, 51, 52, 53,
            54, 55, 56, 57, 58, 59, 60, 61, 62, 63,
            64, 65, 66, 67, 68, 69, 70, 71, 72, 73,
            74, 75, 76, 77, 78, 79, 80, 81, 82, 83,
            84, 85, 86, 87, 88, 89, 90, 91, 92, 93,
            94, 95, 96, 97, 98, 99, 100, 101, 102
        };

        public ComposeControl() {
            InitializeComponent();
        }

        private void toMenuButton_Click(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toMenuFromCompose();
            this.Visibility = Visibility.Collapsed;
            StreamWriter writer = new StreamWriter(path);
            foreach (Note note in track) {
                writer.WriteLine(note.getNumber());
                writer.WriteLine(note.getDuration());
            }
            writer.Close();
            track.Clear();
            saveButton.Content = "Save";
            trackNameTextBox.Visibility = Visibility.Collapsed;
            trackNotesListBox.Items.Clear();
        }

        private void saveButton_Click(object sender, EventArgs e) {
            if (track.Count > 2) {
                if (!trackNameTextBox.IsVisible) {
                    saveButton.Content = "✓";
                    trackNameTextBox.Visibility = Visibility.Visible;
                } else if (trackNameTextBox.IsVisible) {
                    if (trackNameTextBox.Text != "") {
                        string trackName = trackNameTextBox.Text;
                        trackNameTextBox.Text = null;
                        StreamWriter writer = new StreamWriter("../../../Tracks/" + trackName + ".txt");
                        foreach (Note note in track) {
                            writer.WriteLine(note.getNumber());
                            writer.WriteLine(note.getDuration());
                        }
                        writer.Close();
                        track.Clear();
                        pos = 0;
                        currentNote = 0;
                        currentDuration = 500;
                        noteLabel.Content = noteConv(notes[currentNote]);
                        position.Content = (pos + 1).ToString();
                        noteCount.Content = pos.ToString();
                        durationLabel.Content = currentDuration.ToString();
                        trackNotesListBox.Items.Clear();
                        saveButton.Content = "Save";
                        addNoteButton.Content = "+";
                        trackNameTextBox.Visibility = Visibility.Collapsed;
                        Thread saveThread = new Thread(() => {
                            Dispatcher.Invoke(() => { infoTextBox.Content = "Saved"; });
                            Thread.Sleep(1000);
                            Dispatcher.Invoke(() => { infoTextBox.Content = ""; });
                        });
                        saveThread.Start();
                    } else {
                        Thread saveThread = new Thread(() => {
                            Dispatcher.Invoke(() => { 
                                infoTextBox.Content = "Enter track name";
                                saveButton.Content = "Save";
                                trackNameTextBox.Visibility = Visibility.Collapsed;
                            });
                            Thread.Sleep(1000);
                            Dispatcher.Invoke(() => {  infoTextBox.Content = ""; });
                        });
                        saveThread.Start();
                    }
                }
            } else {
                Thread saveThread = new Thread(() => {
                    Dispatcher.Invoke(() => { infoTextBox.Content = "Add some notes (min. 3)"; });
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => { infoTextBox.Content = ""; });
                });
                saveThread.Start();
            }
        }

        private void noteUp_Click(object sender, EventArgs e) {
            if (currentNote == notes.Length - 1) {
                currentNote = 0;
            } else {
                ++currentNote;
            }
            noteLabel.Content = noteConv(notes[currentNote]);
        }

        private void noteDown_Click(object sender, EventArgs e) {
            if (currentNote == 0) {
                currentNote = notes.Length - 1;
            } else {
                --currentNote;
            }
            noteLabel.Content = noteConv(notes[currentNote]);
        }

        private void durationUp_Click(object sender, EventArgs e) {
            if (currentDuration != 5000) {
                currentDuration += 50;
            }
            durationLabel.Content = currentDuration.ToString();
        }

        private void durationDown_Click(object sender, EventArgs e) {
            if (currentDuration != 50) {
                currentDuration -= 50;
            }
            durationLabel.Content = currentDuration.ToString();
        }

        private void playNoteButton_Click(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            MidiOut play = win.getMidi();
            thread = new Thread(() => {
                play.Send(MidiMessage.StartNote(notes[currentNote], 127, 1).RawData);
                Thread.Sleep(currentDuration);
                play.Send(MidiMessage.StopNote(notes[currentNote], 0, 1).RawData);
            });
            thread.Start();
        }

        private void addNoteButton_Click(object sender, EventArgs e) {
            if (pos == track.Count && track.Count < 60) {
                track.Add(new Note(notes[currentNote], currentDuration));
                noteCount.Content = track.Count.ToString();
                pos += 1;
                position.Content = (pos + 1).ToString();
                trackNotesListBox.Items.Add(" " + noteConv(notes[currentNote]) + " " + currentDuration);
            } else if (pos < track.Count) {
                track[pos] = new Note(notes[currentNote], currentDuration);
                trackNotesListBox.Items[pos] = " " + noteConv(notes[currentNote]) + " " + currentDuration;
            }
        }

        private void deleteNoteButton_Click(object sender, EventArgs e) {
            if (pos < track.Count) {
                track.Remove(track[pos]);
                if (track.Count != 0 && pos != track.Count) {
                    currentNote = track[pos].getNumber() - 24;
                    currentDuration = track[pos].getDuration();
                }

                noteLabel.Content = noteConv(notes[currentNote]);
                durationLabel.Content = currentDuration.ToString();
                noteCount.Content = track.Count.ToString();
                trackNotesListBox.Items.RemoveAt(pos);
            }

            if (pos == track.Count) {
                addNoteButton.Content = "+";
            }
        }

        public void previousNoteButton_Click(object sender, EventArgs e) {
            if (pos > 0) {
                pos -= 1;
                currentNote = track[pos].getNumber() - 24;
                currentDuration = track[pos].getDuration();
                position.Content = (pos + 1).ToString();
                noteLabel.Content = noteConv(notes[currentNote]).ToString();
                durationLabel.Content = currentDuration.ToString();
            }

            if (pos < track.Count) {
                addNoteButton.Content = "✎";
            }

            clearHighlight();
            highlightItem(pos);
        }

        public void nextNoteButton_Click(object sender, EventArgs e) {
            if (pos < track.Count) {
                pos += 1;
            }

            if (pos < track.Count) {
                currentNote = track[pos].getNumber() - 24;
                currentDuration = track[pos].getDuration();
                noteLabel.Content = noteConv(notes[currentNote]).ToString();
                durationLabel.Content = currentDuration.ToString();
            }

            if (pos == track.Count) {
                addNoteButton.Content = "+";
            }

            position.Content = (pos + 1).ToString();

            clearHighlight();
            highlightItem(pos);
        }

        public string noteConv(int noteNumber) {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            return noteNames[noteNumber % 12] + ((noteNumber / 12) - 1);
        }

        public void loadTempTrack() {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0 ; i < lines.Length ; i += 2) {
                track.Add(new Note(Int32.Parse(lines[i]), Int32.Parse(lines[i + 1])));
                trackNotesListBox.Items.Add(" " + noteConv(Int32.Parse(lines[i])) + " " + lines[i + 1]);
            }

            if (track.Count > 0) {
                addNoteButton.Content = "✎";
                currentNote = track[0].getNumber() - 24;
                currentDuration = track[0].getDuration();
                noteLabel.Content = noteConv(notes[currentNote]);
                durationLabel.Content = currentDuration.ToString();
                noteCount.Content = track.Count.ToString();
            }

            pos = 0;
            position.Content = (pos + 1).ToString();

            Dispatcher.BeginInvoke(new Action(() => { highlightItem(0); }), DispatcherPriority.ApplicationIdle);
        }

        private void highlightItem(int index) {
            if (index >= 0 && index <= trackNotesListBox.Items.Count) {
                ListBoxItem listBoxItem = (ListBoxItem)trackNotesListBox.ItemContainerGenerator.ContainerFromIndex(index);
                if (listBoxItem != null) {
                    listBoxItem.Background = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        private void clearHighlight() {
            for (int i = 0 ; i < trackNotesListBox.Items.Count ; i++) {
                ListBoxItem listBoxItem = (ListBoxItem)trackNotesListBox.ItemContainerGenerator.ContainerFromIndex(i);
                if (listBoxItem != null) {
                    listBoxItem.ClearValue(BackgroundProperty);
                }
            }
        }
    }
}
