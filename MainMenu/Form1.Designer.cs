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
        private void InitializeComponent()
        {
            this.SinglePlayerButton = new System.Windows.Forms.Button();
            this.buttonTwoPlayers = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelBy = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SinglePlayerButton
            // 
            this.SinglePlayerButton.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SinglePlayerButton.Location = new System.Drawing.Point(159, 216);
            this.SinglePlayerButton.Name = "SinglePlayerButton";
            this.SinglePlayerButton.Size = new System.Drawing.Size(385, 40);
            this.SinglePlayerButton.TabIndex = 0;
            this.SinglePlayerButton.Text = "Single Player";
            this.SinglePlayerButton.UseVisualStyleBackColor = true;
            this.SinglePlayerButton.Click += new System.EventHandler(this.SinglePlayerButton_Click);
            // 
            // buttonTwoPlayers
            // 
            this.buttonTwoPlayers.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTwoPlayers.Location = new System.Drawing.Point(159, 283);
            this.buttonTwoPlayers.Name = "buttonTwoPlayers";
            this.buttonTwoPlayers.Size = new System.Drawing.Size(385, 42);
            this.buttonTwoPlayers.TabIndex = 1;
            this.buttonTwoPlayers.Text = "Two Players";
            this.buttonTwoPlayers.UseVisualStyleBackColor = true;
            this.buttonTwoPlayers.Click += new System.EventHandler(this.buttonTwoPlayers_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Modern No. 20", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Orange;
            this.labelTitle.Location = new System.Drawing.Point(157, 47);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(402, 98);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Quoridor";
            // 
            // labelBy
            // 
            this.labelBy.AutoSize = true;
            this.labelBy.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBy.ForeColor = System.Drawing.Color.Orange;
            this.labelBy.Location = new System.Drawing.Point(274, 145);
            this.labelBy.Name = "labelBy";
            this.labelBy.Size = new System.Drawing.Size(154, 21);
            this.labelBy.TabIndex = 3;
            this.labelBy.Text = "by Team Foxtrot";
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(159, 356);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(385, 42);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(704, 468);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelBy);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonTwoPlayers);
            this.Controls.Add(this.SinglePlayerButton);
            this.Name = "Menu";
            this.Text = "Quoridor Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SinglePlayerButton;
        private System.Windows.Forms.Button buttonTwoPlayers;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelBy;
        private System.Windows.Forms.Button buttonExit;
    }
}

