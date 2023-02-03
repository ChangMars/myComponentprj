using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_SQLite
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();			
			//// 建立資料表 TestTable
			//var createtablestring = @"CREATE TABLE TestTable (Foo double, Bar double);";
			//CreateTable("data.db", createtablestring);

			//// 插入資料到 TestTable 表中
			//var insertstring = @"
			//	INSERT INTO TestTable (Foo, Bar) VALUES ('10', '100');
			//	INSERT INTO TestTable (Foo, Bar) VALUES ('20', '200');
			//";
			//Manipulate("data.db", insertstring);	
		}

		/// <summary>建立資料庫連線</summary>
		/// <param name="database">資料庫名稱</param>
		/// <returns></returns>
		public SQLiteConnection OpenConnection(string database)
		{
			var conntion = new SQLiteConnection()
			{
				ConnectionString = $"Data Source={database};Version=3;New=False;Compress=True;"
			};
			if (conntion.State == ConnectionState.Open) conntion.Close();
			conntion.Open();
			return conntion;
		}

		/// <summary>建立新資料庫</summary>
		/// <param name="database">資料庫名稱</param>
		public void CreateDatabase(string database)
		{
			string path = @"d:\test\"+ database;
			SQLiteConnection cn = new SQLiteConnection("data source=" + path+ ";Version=3;New=True;Compress=True;");
			cn.Open();
			cn.Close();
		}

		/// <summary>建立新資料表</summary>
		/// <param name="database">資料庫名稱</param>
		/// <param name="sqlCreateTable">建立資料表的 SQL 語句</param>
		public void CreateTable(string database, string sqlCreateTable)
		{
			var connection = OpenConnection(database);
			//connection.Open();
			var command = new SQLiteCommand(sqlCreateTable, connection);
			var mySqlTransaction = connection.BeginTransaction();
			try
			{
				command.Transaction = mySqlTransaction;
				command.ExecuteNonQuery();
				mySqlTransaction.Commit();
			}
			catch (Exception ex)
			{
				mySqlTransaction.Rollback();
				throw (ex);
			}
			if (connection.State == ConnectionState.Open) connection.Close();
		}

		/// <summary>新增\修改\刪除資料</summary>
		/// <param name="database">資料庫名稱</param>
		/// <param name="sqlManipulate">資料操作的 SQL 語句</param>
		public void Manipulate(string database, string sqlManipulate)
		{
			var connection = OpenConnection(database);
			var command = new SQLiteCommand(sqlManipulate, connection);
			var mySqlTransaction = connection.BeginTransaction();
			try
			{
				command.Transaction = mySqlTransaction;
				command.ExecuteNonQuery();
				mySqlTransaction.Commit();
			}
			catch (Exception ex)
			{
				mySqlTransaction.Rollback();
				throw (ex);
			}
			if (connection.State == ConnectionState.Open) connection.Close();
		}

		/// <summary>讀取資料</summary>
		/// <param name="database">資料庫名稱</param>
		/// <param name="sqlQuery">資料查詢的 SQL 語句</param>
		/// <returns></returns>
		public DataTable GetDataTable(string database, string sqlQuery)
		{
			try
			{
				var connection = OpenConnection(database);
				var dataAdapter = new SQLiteDataAdapter(sqlQuery, connection);
				var myDataTable = new DataTable();
				var myDataSet = new DataSet();
				myDataSet.Clear();
				dataAdapter.Fill(myDataSet);
				myDataTable = myDataSet.Tables[0];
				if (connection.State == ConnectionState.Open) connection.Close();
				return myDataTable;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Excute SQL Command Error {0}",ex.Message);
				return null;
			}
		}

		private void btnConection_Click(object sender, EventArgs e)
		{
			// 建立 SQLite 資料庫 連線
			if (OpenConnection(txtFilePath.Text).State == ConnectionState.Open)
			{
				MessageBox.Show("Connection Success");
				btnExeSql.Enabled = true;
			}
			else
			{
				MessageBox.Show("Connection Faild");
				btnExeSql.Enabled = false;
			}
		}

		private void btnExeSql_Click(object sender, EventArgs e)
		{
			// 讀取資料
			var dataTable = GetDataTable(txtFilePath.Text, txtSql.Text);
			dgvData.DataSource = dataTable;
		}

		private void btnChooseFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Multiselect = false;//該值確定是否可以選擇多個檔案
			dialog.Title = "請選擇資料庫";
			dialog.Filter = "所有檔案(*.*)|*.*";
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtFilePath.Text = dialog.FileName;
			}
		}
	}
}
