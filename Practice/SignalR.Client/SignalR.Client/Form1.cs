using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Client
{
	public partial class Form1 : Form
	{
		private static readonly string HubName = "chathub"; // Hub名稱
		private static readonly string Host = "https://ylcwss01.yulon-motor.com.tw/pmswagger/"; // 主機位置
		private HubConnection connection;
		public Form1()
		{
			InitializeComponent();
			connection = new HubConnectionBuilder()
			  .WithUrl(Host + HubName)
			  .Build();

			connection.Closed += async (error) =>
			{
				await Task.Delay(new Random().Next(0, 5) * 1000);
				await connection.StartAsync();
			};
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// 註冊給伺服端呼叫的方
			connection.On("ReceiveMessage", (string s1) => OnSend(s1));
			// 連線到 SignalR 伺服器
			//this.hubConnection.Start().Wait();
			try
			{
				connection.StartAsync();
			}
			catch (Exception ex)
			{
			}
		}

		private void OnSend(string name)
		{
			this.MessagesBlock.InvokeIfNecessary(() =>
			{
				this.MessagesBlock.AppendText($"{name}\r\n");
			});
		}

		private async void SendMessageButton_Click(object sender, EventArgs e)
		{
			await this.connection.InvokeAsync("SendMessage", this.NameTextBox.Text, this.MessageTextBox.Text);
		}
	}
}
