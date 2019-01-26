namespace UDPMIDI
{
    partial class FormBB7UDPMIDI
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
            this.comboBoxMidiDevice = new System.Windows.Forms.ComboBox();
            this.textBoxPacketsRec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUDPPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPacketsSent = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMidiDevice
            // 
            this.comboBoxMidiDevice.FormattingEnabled = true;
            this.comboBoxMidiDevice.Location = new System.Drawing.Point(114, 35);
            this.comboBoxMidiDevice.Name = "comboBoxMidiDevice";
            this.comboBoxMidiDevice.Size = new System.Drawing.Size(210, 21);
            this.comboBoxMidiDevice.TabIndex = 3;
            this.comboBoxMidiDevice.SelectedIndexChanged += new System.EventHandler(this.comboBoxMidiDevice_SelectedIndexChanged);
            // 
            // textBoxPacketsRec
            // 
            this.textBoxPacketsRec.Location = new System.Drawing.Point(114, 62);
            this.textBoxPacketsRec.Name = "textBoxPacketsRec";
            this.textBoxPacketsRec.ReadOnly = true;
            this.textBoxPacketsRec.Size = new System.Drawing.Size(121, 20);
            this.textBoxPacketsRec.TabIndex = 4;
            this.textBoxPacketsRec.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Packets Received";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "MIDI Device";
            // 
            // textBoxUDPPort
            // 
            this.textBoxUDPPort.Location = new System.Drawing.Point(114, 114);
            this.textBoxUDPPort.Name = "textBoxUDPPort";
            this.textBoxUDPPort.ReadOnly = true;
            this.textBoxUDPPort.Size = new System.Drawing.Size(121, 20);
            this.textBoxUDPPort.TabIndex = 7;
            this.textBoxUDPPort.Text = "1999";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "UDP Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Packets Sent";
            // 
            // textBoxPacketsSent
            // 
            this.textBoxPacketsSent.Location = new System.Drawing.Point(114, 88);
            this.textBoxPacketsSent.Name = "textBoxPacketsSent";
            this.textBoxPacketsSent.ReadOnly = true;
            this.textBoxPacketsSent.Size = new System.Drawing.Size(121, 20);
            this.textBoxPacketsSent.TabIndex = 9;
            this.textBoxPacketsSent.Text = "0";
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Yellow;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(338, 22);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = "MiSTer MidiLink UDP Listener - BB7";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(295, 117);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(29, 19);
            this.buttonReset.TabIndex = 12;
            this.buttonReset.Text = "®";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // FormBB7UDPMIDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 154);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPacketsSent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUDPPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPacketsRec);
            this.Controls.Add(this.comboBoxMidiDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormBB7UDPMIDI";
            this.Text = "UDPMIDI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBB7UDPMIDI_FormClosing);
            this.Load += new System.EventHandler(this.FormBB7UDPMIDI_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormBB7UDPMIDI_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxMidiDevice;
        private System.Windows.Forms.TextBox textBoxPacketsRec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUDPPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPacketsSent;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonReset;
    }
}

