namespace Junkosoft
{
	#region Namespaces
	using System;
	using System.Drawing;
	using System.Collections;
	using System.Configuration;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Data;					
	#endregion

	/// <summary>
	/// Countdown to New Year's!
	/// </summary>
	public class Countdown : System.Windows.Forms.Form
	{
		#region Private Fields
		DateTime _targetDate;
		#endregion

		#region Controls
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label DateLabel;
		private System.Windows.Forms.Label TimeLabel;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Constructors
		public Countdown()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_targetDate = DateTime.Parse(ConfigurationSettings.AppSettings["TargetDate"]);
			//_targetDate = new DateTime(DateTime.Now.Year + 1, 1, 1, 0, 0, 0, 0);
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.DateLabel = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.TimeLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// DateLabel
			// 
			this.DateLabel.BackColor = System.Drawing.Color.Transparent;
			this.DateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DateLabel.ForeColor = System.Drawing.Color.Red;
			this.DateLabel.Location = new System.Drawing.Point(0, 0);
			this.DateLabel.Name = "DateLabel";
			this.DateLabel.Size = new System.Drawing.Size(800, 88);
			this.DateLabel.TabIndex = 0;
			this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// TimeLabel
			// 
			this.TimeLabel.BackColor = System.Drawing.Color.Transparent;
			this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 96F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TimeLabel.ForeColor = System.Drawing.Color.Red;
			this.TimeLabel.Location = new System.Drawing.Point(0, 88);
			this.TimeLabel.Name = "TimeLabel";
			this.TimeLabel.Size = new System.Drawing.Size(800, 200);
			this.TimeLabel.TabIndex = 1;
			this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Countdown
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(800, 288);
			this.ControlBox = false;
			this.Controls.Add(this.TimeLabel);
			this.Controls.Add(this.DateLabel);
			this.ForeColor = System.Drawing.Color.Red;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Countdown";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clock";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Countdown_KeyPress);
			this.Load += new System.EventHandler(this.Countdown_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Control Events
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			GetSeconds();
		}
		
		private void Countdown_Load(object sender, EventArgs e)
		{
			DateLabel.Text = ConfigurationSettings.AppSettings["HeaderMessage"];
			GetSeconds();
		}

		private void Countdown_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main() 
		{
			Application.Run(new Countdown());
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Private Methods
		private void GetSeconds()
		{
			
			TimeSpan datediff = _targetDate - DateTime.Now;
			long secondsleft = datediff.Days * 86400 + datediff.Hours * 3600 + datediff.Minutes * 60 + datediff.Seconds + 1;
			if (secondsleft > 0)
			{
				switch (ConfigurationSettings.AppSettings["Mode"])
				{
					case "seconds":
						TimeLabel.Text = String.Format("{0:#,###}", secondsleft);
						timer1.Enabled = true;
						break;
					case "minutes":
						TimeLabel.Text = String.Format("{0:#,###}", datediff.Days * 1440 + datediff.Hours * 60 + 
							datediff.Minutes) + ":" + String.Format("{0:00}", datediff.Seconds);
						timer1.Enabled = true;
                        break;
					case "hours":
						TimeLabel.Text = String.Format("{0:##}", datediff.Days * 24 + datediff.Hours) +
							":" + String.Format("{0:00}", datediff.Minutes) + ":" +
							String.Format("{0:00}", datediff.Seconds);
						timer1.Enabled = true;
						break;
					default:
						throw new Exception("Invalid Mode specified in the configuration file.");
				}
				
			}
			else
			{
				timer1.Enabled = false;
				TimeLabel.Font = new Font(TimeLabel.Font.FontFamily.Name, 64);				
				TimeLabel.ForeColor = Color.Yellow;				
				TimeLabel.Text = ConfigurationSettings.AppSettings["TargetMessage"];
			}
		}
		#endregion
	}
}
