using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicComposer
{
    public partial class TracksControl : UserControl
    {
        private string path = "../../../tracks/";
        private int id;
        private FileInfo[] tracks;
        private DirectoryInfo directory;
        private Thread thread;

        public TracksControl()
        {
            InitializeComponent();
            directory = new DirectoryInfo(path);
        }

        public void loadTracks()
        {
            tracksListBox.Items.Clear();
            tracks = directory.GetFiles("*.txt");
            foreach (FileInfo file in tracks)
            {
                tracksListBox.Items.Add(file.Name.Substring(0, file.Name.Length - 4));
            }
        }
        private void menuButton_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainFrame).toMenuFromTracks();
        }

        private void tracksListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = tracksListBox.SelectedIndex;
            if (id != -1)
            {
                deleteTrackButton.Show();
                modifyTrackButton.Show();
                playTrackButton.Show();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            tracks[id].Delete();
            tracksListBox.Items.RemoveAt(id);
            hideButtons();
        }

        private void modifyTrackButton_Click(object sender, EventArgs e)
        {

        }

        private void playTrackButton_Click(Object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(path + tracksListBox.Items[id].ToString() + ".txt");
            thread = new Thread(() =>
            {
                int i = 0;
                foreach (string line in lines)
                {
                    if (i % 2 == 0)
                    {
                        (this.ParentForm as MainFrame).getMidi().Send(MidiMessage.StartNote(Int32.Parse(line), 127, 1).RawData);
                    }
                    else
                    {
                        Thread.Sleep(Int32.Parse(line));
                    }
                    i++;
                }
            });
            thread.Start();
        }
        public void hideButtons()
        {
            deleteTrackButton.Hide();
            modifyTrackButton.Hide();
            playTrackButton.Hide();
        }
    }
}
