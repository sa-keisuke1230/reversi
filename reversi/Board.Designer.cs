namespace reversi
{
    partial class Board
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_borad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_borad)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_borad
            // 
            this.pictureBox_borad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_borad.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_borad.Name = "pictureBox_borad";
            this.pictureBox_borad.Size = new System.Drawing.Size(280, 280);
            this.pictureBox_borad.TabIndex = 0;
            this.pictureBox_borad.TabStop = false;
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_borad);
            this.Name = "Board";
            this.Size = new System.Drawing.Size(280, 280);
            this.Load += new System.EventHandler(this.Board_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_borad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_borad;
    }
}
