
namespace CSharp_SQLite
{
	partial class Form1
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnConection = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnExeSql = new System.Windows.Forms.Button();
			this.btnChooseFile = new System.Windows.Forms.Button();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.txtSql = new System.Windows.Forms.TextBox();
			this.dgvData = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnConection
			// 
			this.btnConection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnConection.Location = new System.Drawing.Point(501, 3);
			this.btnConection.Name = "btnConection";
			this.btnConection.Size = new System.Drawing.Size(94, 24);
			this.btnConection.TabIndex = 0;
			this.btnConection.Text = "Con";
			this.btnConection.UseVisualStyleBackColor = true;
			this.btnConection.Click += new System.EventHandler(this.btnConection_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Controls.Add(this.btnExeSql, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnConection, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnChooseFile, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtFilePath, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtSql, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.dgvData, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 372);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// btnExeSql
			// 
			this.btnExeSql.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnExeSql.Enabled = false;
			this.btnExeSql.Location = new System.Drawing.Point(501, 33);
			this.btnExeSql.Name = "btnExeSql";
			this.btnExeSql.Size = new System.Drawing.Size(94, 24);
			this.btnExeSql.TabIndex = 2;
			this.btnExeSql.Text = "ExcuteSql";
			this.btnExeSql.UseVisualStyleBackColor = true;
			this.btnExeSql.Click += new System.EventHandler(this.btnExeSql_Click);
			// 
			// btnChooseFile
			// 
			this.btnChooseFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnChooseFile.Location = new System.Drawing.Point(401, 3);
			this.btnChooseFile.Name = "btnChooseFile";
			this.btnChooseFile.Size = new System.Drawing.Size(94, 24);
			this.btnChooseFile.TabIndex = 3;
			this.btnChooseFile.Text = "ChooseFile";
			this.btnChooseFile.UseVisualStyleBackColor = true;
			this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFilePath.Enabled = false;
			this.txtFilePath.Location = new System.Drawing.Point(3, 3);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(392, 22);
			this.txtFilePath.TabIndex = 4;
			this.txtFilePath.Text = "data.db";
			// 
			// txtSql
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.txtSql, 2);
			this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSql.Location = new System.Drawing.Point(3, 33);
			this.txtSql.Name = "txtSql";
			this.txtSql.Size = new System.Drawing.Size(492, 22);
			this.txtSql.TabIndex = 5;
			this.txtSql.Text = "SELECT * FROM DATA WHERE Date_Text BETWEEN \'2022/04/01\' AND \'2022/04/15\'";
			// 
			// dgvData
			// 
			this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tableLayoutPanel1.SetColumnSpan(this.dgvData, 3);
			this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvData.Location = new System.Drawing.Point(3, 63);
			this.dgvData.Name = "dgvData";
			this.dgvData.RowTemplate.Height = 24;
			this.dgvData.Size = new System.Drawing.Size(592, 306);
			this.dgvData.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 372);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Form1";
			this.Text = "SQLite_Reader";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnConection;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnExeSql;
		private System.Windows.Forms.Button btnChooseFile;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.TextBox txtSql;
		private System.Windows.Forms.DataGridView dgvData;
	}
}

