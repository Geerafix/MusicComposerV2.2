namespace MusicComposer
{
    public class MenuControl : UserControl
    {
        private Button exitButton;

        public MenuControl()
        {
            InitializeComponent();
        }

        private void tracksButton_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainFrame).toTracksFromMenu();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void composeButton_Click(object sender, EventArgs e)
        {
            (this.ParentForm as MainFrame).toComposeFromMenu();
            
        }

        private void InitializeComponent()
        {
            exitButton = new Button();
            composeButton = new Button();
            tracksButton = new Button();
            titleLabel = new Label();
            authorLabel = new Label();
            SuspendLayout();
            // 
            // exitButton
            // 
            exitButton.Cursor = Cursors.Hand;
            exitButton.Font = new Font("Century Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point);
            exitButton.Location = new Point(500, 540);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(200, 100);
            exitButton.TabIndex = 4;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // composeButton
            // 
            composeButton.Cursor = Cursors.Hand;
            composeButton.Font = new Font("Century Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point);
            composeButton.Location = new Point(800, 440);
            composeButton.Name = "composeButton";
            composeButton.Size = new Size(200, 100);
            composeButton.TabIndex = 3;
            composeButton.Text = "Compose";
            composeButton.UseVisualStyleBackColor = true;
            composeButton.Click += composeButton_Click;
            // 
            // tracksButton
            // 
            tracksButton.Cursor = Cursors.Hand;
            tracksButton.Font = new Font("Century Gothic", 16F, FontStyle.Regular, GraphicsUnit.Point);
            tracksButton.Location = new Point(200, 440);
            tracksButton.Name = "tracksButton";
            tracksButton.Size = new Size(200, 100);
            tracksButton.TabIndex = 2;
            tracksButton.Text = "Tracks";
            tracksButton.UseVisualStyleBackColor = true;
            tracksButton.Click += tracksButton_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Century Gothic", 75F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.MintCream;
            titleLabel.Location = new Point(80, 125);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(1046, 147);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "MusicComposer";
            // 
            // authorLabel
            // 
            authorLabel.AutoSize = true;
            authorLabel.Font = new Font("Avatarock", 22.8000011F, FontStyle.Italic, GraphicsUnit.Point);
            authorLabel.ForeColor = Color.LightSeaGreen;
            authorLabel.Location = new Point(983, 765);
            authorLabel.Name = "authorLabel";
            authorLabel.Size = new Size(214, 31);
            authorLabel.TabIndex = 0;
            authorLabel.Text = "by Adam Grzeszczuk";
            // 
            // MenuControl
            // 
            BackColor = Color.DarkSlateGray;
            Controls.Add(authorLabel);
            Controls.Add(titleLabel);
            Controls.Add(tracksButton);
            Controls.Add(composeButton);
            Controls.Add(exitButton);
            Name = "MenuControl";
            Size = new Size(1200, 800);
            ResumeLayout(false);
            PerformLayout();
        }

        private Button composeButton;
        private Button tracksButton;
        private Label authorLabel;
        private Label titleLabel;
    }
}
