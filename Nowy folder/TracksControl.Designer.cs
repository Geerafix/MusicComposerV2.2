namespace MusicComposer
{
    partial class TracksControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        private void InitializeComponent()
        {
            menuButton = new Button();
            tracksLabel = new Label();
            tracksListBox = new ListBox();
            deleteTrackButton = new Button();
            modifyTrackButton = new Button();
            playTrackButton = new Button();
            SuspendLayout();
            // 
            // menuButton
            // 
            menuButton.Cursor = Cursors.Hand;
            menuButton.Font = new Font("Century Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point);
            menuButton.Location = new Point(800, 440);
            menuButton.Name = "menuButton";
            menuButton.Size = new Size(200, 100);
            menuButton.TabIndex = 2;
            menuButton.Text = "Main menu";
            menuButton.UseVisualStyleBackColor = true;
            menuButton.Click += menuButton_Click;
            // 
            // tracksLabel
            // 
            tracksLabel.AutoSize = true;
            tracksLabel.Font = new Font("Century Gothic", 75F, FontStyle.Bold, GraphicsUnit.Point);
            tracksLabel.ForeColor = Color.MintCream;
            tracksLabel.Location = new Point(80, 125);
            tracksLabel.Name = "tracksLabel";
            tracksLabel.Size = new Size(445, 147);
            tracksLabel.TabIndex = 4;
            tracksLabel.Text = "Tracks";
            // 
            // tracksListBox
            // 
            tracksListBox.BackColor = Color.Teal;
            tracksListBox.Font = new Font("Century Gothic", 13F, FontStyle.Bold, GraphicsUnit.Point);
            tracksListBox.FormattingEnabled = true;
            tracksListBox.ItemHeight = 26;
            tracksListBox.Location = new Point(121, 288);
            tracksListBox.Name = "tracksListBox";
            tracksListBox.Size = new Size(361, 420);
            tracksListBox.TabIndex = 5;
            tracksListBox.SelectedIndexChanged += tracksListBox_SelectedIndexChanged;
            // 
            // deleteTrackButton
            // 
            deleteTrackButton.Cursor = Cursors.Hand;
            deleteTrackButton.Font = new Font("Century Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            deleteTrackButton.Location = new Point(233, 714);
            deleteTrackButton.Name = "deleteTrackButton";
            deleteTrackButton.RightToLeft = RightToLeft.Yes;
            deleteTrackButton.Size = new Size(50, 50);
            deleteTrackButton.TabIndex = 6;
            deleteTrackButton.Text = "X";
            deleteTrackButton.UseVisualStyleBackColor = true;
            deleteTrackButton.Visible = false;
            deleteTrackButton.Click += deleteButton_Click;
            // 
            // modifyTrackButton
            // 
            modifyTrackButton.Cursor = Cursors.Hand;
            modifyTrackButton.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            modifyTrackButton.Location = new Point(177, 714);
            modifyTrackButton.Name = "modifyTrackButton";
            modifyTrackButton.RightToLeft = RightToLeft.Yes;
            modifyTrackButton.Size = new Size(50, 50);
            modifyTrackButton.TabIndex = 7;
            modifyTrackButton.Text = "✎";
            modifyTrackButton.UseVisualStyleBackColor = true;
            modifyTrackButton.Visible = false;
            modifyTrackButton.Click += modifyTrackButton_Click;
            // 
            // playTrackButton
            // 
            playTrackButton.Cursor = Cursors.Hand;
            playTrackButton.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            playTrackButton.Location = new Point(121, 714);
            playTrackButton.Name = "playTrackButton";
            playTrackButton.RightToLeft = RightToLeft.No;
            playTrackButton.Size = new Size(50, 50);
            playTrackButton.TabIndex = 8;
            playTrackButton.Text = ">";
            playTrackButton.UseVisualStyleBackColor = true;
            playTrackButton.Visible = false;
            playTrackButton.Click += playTrackButton_Click;
            // 
            // TracksControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateGray;
            Controls.Add(playTrackButton);
            Controls.Add(modifyTrackButton);
            Controls.Add(deleteTrackButton);
            Controls.Add(tracksListBox);
            Controls.Add(tracksLabel);
            Controls.Add(menuButton);
            Name = "TracksControl";
            Size = new Size(1200, 800);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button menuButton;
        private Label tracksLabel;
        private ListBox tracksListBox;
        private Button deleteTrackButton;
        private Button modifyTrackButton;
        private Button playTrackButton;
    }
}
