using MusicComposer;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicComposerWPF {
    public partial class EditControl : System.Windows.Controls.UserControl {
        private List<Note> track;
        private Thread thread;
        private string tName;
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

        public EditControl() {
            InitializeComponent();

        }

        public void loadTrack(string tName, List<Note> track) {
            this.tName = tName;
            this.track = track;
            foreach (Note note in track) {
                trackNotesListBox.Items.Add(" " + noteConv(note.getNumber()) + " " + note.getDuration());
            }

            if (track.Count > 0) {
                currentNote = track[0].getNumber() - 24;
                currentDuration = track[0].getDuration();
                noteLabel.Content = noteConv(notes[currentNote]).ToString();
                durationLabel.Content = currentDuration.ToString();
                noteCount.Content = track.Count.ToString();
                addNoteButton.Content = "✎";
            }

            pos = 0;
            position.Content = (pos + 1).ToString();
            trackName.Content = tName;

            Dispatcher.BeginInvoke(new Action(() => { highlightItem(0); }), DispatcherPriority.ApplicationIdle);
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

        private void saveButton_Click(object sender, EventArgs e) {
            if (track.Count > 2) {
                StreamWriter writer = new StreamWriter("../../../Tracks/" + tName + ".txt");
                foreach (Note note in track) {
                    writer.WriteLine(note.getNumber());
                    writer.WriteLine(note.getDuration());
                }
                writer.Close();
                Thread saveThread = new Thread(() => {
                    Dispatcher.Invoke(() => { infoTextBox.Content = "Saved"; });
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => { infoTextBox.Content = ""; });
                });
                saveThread.Start();
            } else {
                Thread saveThread = new Thread(() => {
                    Dispatcher.Invoke(() => { infoTextBox.Content = "Add some notes (min. 3)"; });
                    Thread.Sleep(1000);
                    Dispatcher.Invoke(() => { infoTextBox.Content = ""; });
                });
                saveThread.Start();
            }
        }

        private void toTracksButton_Click(object sender, EventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win.toTracksFromEdit();
            this.Visibility = Visibility.Collapsed;
            this.tName = null;
            this.track = null;
            pos = 0;
            noteCount.Content = 0.ToString();
            trackNotesListBox.Items.Clear();
        }

        private string noteConv(int noteNumber) {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            return noteNames[noteNumber % 12] + ((noteNumber / 12) - 1);
        }

        private void highlightItem(int index) {
            if (index >= 0 && index < trackNotesListBox.Items.Count) {
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
