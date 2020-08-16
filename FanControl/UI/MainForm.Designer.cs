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
            this.labelCpu = new System.Windows.Forms.Label();
            this.labelGPU = new System.Windows.Forms.Label();
            this._gpuValue = new System.Windows.Forms.Label();
            this._cpuValue = new System.Windows.Forms.Label();
            this._startRead = new System.Windows.Forms.Button();
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
            // labelCpu
            // 
            this.labelCpu.AutoSize = true;
            this.labelCpu.Location = new System.Drawing.Point(117, 104);
            this.labelCpu.Name = "labelCpu";
            this.labelCpu.Size = new System.Drawing.Size(29, 13);
            this.labelCpu.TabIndex = 5;
            this.labelCpu.Text = "CPU";
            // 
            // labelGPU
            // 
            this.labelGPU.AutoSize = true;
            this.labelGPU.Location = new System.Drawing.Point(116, 123);
            this.labelGPU.Name = "labelGPU";
            this.labelGPU.Size = new System.Drawing.Size(30, 13);
            this.labelGPU.TabIndex = 6;
            this.labelGPU.Text = "GPU";
            // 
            // _gpuValue
            // 
            this._gpuValue.AutoSize = true;
            this._gpuValue.Location = new System.Drawing.Point(160, 123);
            this._gpuValue.Name = "_gpuValue";
            this._gpuValue.Size = new System.Drawing.Size(13, 13);
            this._gpuValue.TabIndex = 8;
            this._gpuValue.Text = "0";
            // 
            // _cpuValue
            // 
            this._cpuValue.AutoSize = true;
            this._cpuValue.Location = new System.Drawing.Point(161, 104);
            this._cpuValue.Name = "_cpuValue";
            this._cpuValue.Size = new System.Drawing.Size(13, 13);
            this._cpuValue.TabIndex = 7;
            this._cpuValue.Text = "0";
            // 
            // _startRead
            // 
            this._startRead.Location = new System.Drawing.Point(12, 99);
            this._startRead.Name = "_startRead";
            this._startRead.Size = new System.Drawing.Size(75, 23);
            this._startRead.TabIndex = 9;
            this._startRead.Text = "Start Read";
            this._startRead.UseVisualStyleBackColor = true;
            this._startRead.Click += new System.EventHandler(this._startRead_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 159);
            this.Controls.Add(this._startRead);
            this.Controls.Add(this._gpuValue);
            this.Controls.Add(this._cpuValue);
            this.Controls.Add(this.labelGPU);
            this.Controls.Add(this.labelCpu);
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
        private System.Windows.Forms.Label labelCpu;
        private System.Windows.Forms.Label labelGPU;
        private System.Windows.Forms.Label _gpuValue;
        private System.Windows.Forms.Label _cpuValue;
        private System.Windows.Forms.Button _startRead;
    }
}

