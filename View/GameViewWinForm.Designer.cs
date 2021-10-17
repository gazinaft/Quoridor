namespace View
{
    partial class GameViewWinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GamePanel = new System.Windows.Forms.Panel();
            this.listBoxPlayers = new System.Windows.Forms.ListBox();
            this.labelPlayersAndWalls = new System.Windows.Forms.Label();
            this.labelCurPlayer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GamePanel
            // 
            this.GamePanel.Location = new System.Drawing.Point(33, 28);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.Size = new System.Drawing.Size(529, 515);
            this.GamePanel.TabIndex = 0;
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.ItemHeight = 21;
            this.listBoxPlayers.Location = new System.Drawing.Point(671, 81);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.Size = new System.Drawing.Size(170, 67);
            this.listBoxPlayers.TabIndex = 1;
            // 
            // labelPlayersAndWalls
            // 
            this.labelPlayersAndWalls.AutoSize = true;
            this.labelPlayersAndWalls.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayersAndWalls.Location = new System.Drawing.Point(671, 41);
            this.labelPlayersAndWalls.Name = "labelPlayersAndWalls";
            this.labelPlayersAndWalls.Size = new System.Drawing.Size(155, 18);
            this.labelPlayersAndWalls.TabIndex = 2;
            this.labelPlayersAndWalls.Text = "PlayerID      Walls";
            // 
            // labelCurPlayer
            // 
            this.labelCurPlayer.AutoSize = true;
            this.labelCurPlayer.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurPlayer.Location = new System.Drawing.Point(671, 188);
            this.labelCurPlayer.Name = "labelCurPlayer";
            this.labelCurPlayer.Size = new System.Drawing.Size(122, 18);
            this.labelCurPlayer.TabIndex = 3;
            this.labelCurPlayer.Text = "CurrentPlayer :";
            // 
            // GameViewWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(853, 619);
            this.Controls.Add(this.labelCurPlayer);
            this.Controls.Add(this.labelPlayersAndWalls);
            this.Controls.Add(this.listBoxPlayers);
            this.Controls.Add(this.GamePanel);
            this.Name = "GameViewWinForm";
            this.Text = "Quoridor Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.ListBox listBoxPlayers;
        private System.Windows.Forms.Label labelPlayersAndWalls;
        private System.Windows.Forms.Label labelCurPlayer;
    }
}