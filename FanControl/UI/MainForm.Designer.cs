namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this._trackBar = new System.Windows.Forms.TrackBar();
            this._connectBtn = new System.Windows.Forms.Button();
            this._disconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _trackBar
            // 
            this._trackBar.Location = new System.Drawing.Point(111, 41);
            this._trackBar.Maximum = 100;
            this._trackBar.Name = "_trackBar";
            this._trackBar.Size = new System.Drawing.Size(661, 45);
            this._trackBar.TabIndex = 0;
            this._trackBar.Scroll += new System.EventHandler(this._trackBar_Scroll);
            // 
            // _connectBtn
            // 
            this._connectBtn.Location = new System.Drawing.Point(12, 12);
            this._connectBtn.Name = "_connectBtn";
            this._connectBtn.Size = new System.Drawing.Size(75, 23);
            this._connectBtn.TabIndex = 1;
            this._connectBtn.Text = "Connect";
            this._connectBtn.UseVisualStyleBackColor = true;
            this._connectBtn.Click += new System.EventHandler(this._connectBtn_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Location = new System.Drawing.Point(12, 41);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(75, 23);
            this._disconnect.TabIndex = 2;
            this._disconnect.Text = "Disconnect";
            this._disconnect.UseVisualStyleBackColor = true;
            this._disconnect.Click += new System.EventHandler(this._disconnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Speed";
            // 
            // _reset
            // 
            this._reset.Location = new System.Drawing.Point(12, 70);
            this._reset.Name = "_reset";
            this._reset.Size = new System.Drawing.Size(75, 23);
            this._reset.TabIndex = 4;
            this._reset.Text = "Reset";
            this._reset.UseVisualStyleBackColor = true;
            this._reset.Click += new System.EventHandler(this._reset_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 115);
            this.Controls.Add(this._reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._disconnect);
            this.Controls.Add(this._connectBtn);
            this.Controls.Add(this._trackBar);
            this.Name = "MainForm";
            this.Text = "Fan Control UI";
            ((System.ComponentModel.ISupportInitialize)(this._trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar _trackBar;
        private System.Windows.Forms.Button _connectBtn;
        private System.Windows.Forms.Button _disconnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _reset;
    }
}

