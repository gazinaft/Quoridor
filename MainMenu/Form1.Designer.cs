namespace MainMenu
{
    partial class Menu
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
        private void InitializeComponent() {
            this.SinglePlayerButton = new System.Windows.Forms.Button();
            this.HotseatButton = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.MultiplayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SinglePlayerButton
            // 
            this.SinglePlayerButton.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SinglePlayerButton.Location = new System.Drawing.Point(212, 205);
            this.SinglePlayerButton.Margin = new System.Windows.Forms.Padding(4);
            this.SinglePlayerButton.Name = "SinglePlayerButton";
            this.SinglePlayerButton.Size = new System.Drawing.Size(513, 49);
            this.SinglePlayerButton.TabIndex = 0;
            this.SinglePlayerButton.Text = "Single Player";
            this.SinglePlayerButton.UseVisualStyleBackColor = true;
            this.SinglePlayerButton.Click += new System.EventHandler(this.SingleplayerClick);
            // 
            // HotseatButton
            // 
            this.HotseatButton.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HotseatButton.Location = new System.Drawing.Point(212, 279);
            this.HotseatButton.Margin = new System.Windows.Forms.Padding(4);
            this.HotseatButton.Name = "HotseatButton";
            this.HotseatButton.Size = new System.Drawing.Size(513, 52);
            this.HotseatButton.TabIndex = 1;
            this.HotseatButton.Text = "Two Players";
            this.HotseatButton.UseVisualStyleBackColor = true;
            this.HotseatButton.Click += new System.EventHandler(this.HotseatClick);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Modern No. 20", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Orange;
            this.labelTitle.Location = new System.Drawing.Point(209, 58);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(500, 123);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Quoridor";
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(212, 454);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(513, 52);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // MultiplayerButton
            // 
            this.MultiplayerButton.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MultiplayerButton.Location = new System.Drawing.Point(212, 353);
            this.MultiplayerButton.Margin = new System.Windows.Forms.Padding(4);
            this.MultiplayerButton.Name = "MultiplayerButton";
            this.MultiplayerButton.Size = new System.Drawing.Size(513, 52);
            this.MultiplayerButton.TabIndex = 5;
            this.MultiplayerButton.Text = "Multiplayer";
            this.MultiplayerButton.UseVisualStyleBackColor = true;
            this.MultiplayerButton.Click += new System.EventHandler(this.MultiplayerClick);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(939, 576);
            this.Controls.Add(this.MultiplayerButton);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.HotseatButton);
            this.Controls.Add(this.SinglePlayerButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Menu";
            this.Text = "Quoridor Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button SinglePlayerButton;
        private System.Windows.Forms.Button HotseatButton;
        private System.Windows.Forms.Button MultiplayerButton;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonExit;
    }
}

