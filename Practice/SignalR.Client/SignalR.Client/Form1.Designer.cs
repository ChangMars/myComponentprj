
namespace SignalR.Client
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.MessagesBlock = new System.Windows.Forms.TextBox();
			this.SendMessageButton = new System.Windows.Forms.Button();
			this.MessageTextBox = new System.Windows.Forms.TextBox();
			this.NameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 39);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 12);
			this.label2.TabIndex = 11;
			this.label2.Text = "Message:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "Name:";
			// 
			// MessagesBlock
			// 
			this.MessagesBlock.Location = new System.Drawing.Point(6, 92);
			this.MessagesBlock.Margin = new System.Windows.Forms.Padding(2);
			this.MessagesBlock.Multiline = true;
			this.MessagesBlock.Name = "MessagesBlock";
			this.MessagesBlock.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MessagesBlock.Size = new System.Drawing.Size(411, 301);
			this.MessagesBlock.TabIndex = 9;
			// 
			// SendMessageButton
			// 
			this.SendMessageButton.Location = new System.Drawing.Point(93, 60);
			this.SendMessageButton.Margin = new System.Windows.Forms.Padding(2);
			this.SendMessageButton.Name = "SendMessageButton";
			this.SendMessageButton.Size = new System.Drawing.Size(74, 27);
			this.SendMessageButton.TabIndex = 8;
			this.SendMessageButton.Text = "向大家問好";
			this.SendMessageButton.UseVisualStyleBackColor = true;
			this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
			// 
			// MessageTextBox
			// 
			this.MessageTextBox.Location = new System.Drawing.Point(53, 35);
			this.MessageTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.Size = new System.Drawing.Size(115, 22);
			this.MessageTextBox.TabIndex = 7;
			// 
			// NameTextBox
			// 
			this.NameTextBox.Location = new System.Drawing.Point(53, 11);
			this.NameTextBox.Margin = new System.Windows.Forms.Padding(2);
			this.NameTextBox.Name = "NameTextBox";
			this.NameTextBox.Size = new System.Drawing.Size(115, 22);
			this.NameTextBox.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 398);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.MessagesBlock);
			this.Controls.Add(this.SendMessageButton);
			this.Controls.Add(this.MessageTextBox);
			this.Controls.Add(this.NameTextBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox MessagesBlock;
		private System.Windows.Forms.Button SendMessageButton;
		private System.Windows.Forms.TextBox MessageTextBox;
		private System.Windows.Forms.TextBox NameTextBox;
	}
}

