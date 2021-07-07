using Newtonsoft.Json;
using Profibiz.PracticeManager.SharedCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Start
{
	public partial class MainWindow : Window
	{
		private string _baseUrl = ConfigurationManager.AppSettings["service.url"];
		private string _shellCatalog;

		public MainWindow()
		{
			InitializeComponent();
			this.Loaded += MainWindow_Loaded;
			labStatus.Content = "Checking for updates...";
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(async () =>
			{
				await Upgrade();
			}));
		}

		async Task Upgrade()
		{
			_shellCatalog = Assembly.GetExecutingAssembly().Location;
			_shellCatalog = Directory.GetParent(Directory.GetParent(_shellCatalog).FullName).FullName;
			var exefile = Path.Combine(_shellCatalog, ShellUpgradeUtils.EXEFILE);
			if (File.Exists(exefile))
			{
				var remote = GetShellFilesRemote(0);
				var local = ShellUpgradeUtils.GetShellFiles(_shellCatalog, 0);
				if (remote.Any() && local.Any() && remote[0].eqv(local[0]))
				{
					StartShell();
					return;
				}
			}

			ShellUpgradeUtils.ShellFileInfo[] allremote = null;
			ShellUpgradeUtils.ShellFileInfo[] alllocal = null;

			var task1 = Task.Run(() =>
			{
				allremote = GetShellFilesRemote(1);
				allremote = allremote.OrderBy(q => q.filename.ToLower() == ShellUpgradeUtils.EXEFILE.ToLower() ? 2 : 1).ToArray();
			});
			var task2 = Task.Run(() =>
			{
				alllocal = ShellUpgradeUtils.GetShellFiles(_shellCatalog, 1);
			});
			Task.WaitAll(task1, task2);

			var aremotes = allremote.Where(q => !alllocal.Any(z => z.eqv(q))).ToArray();
			var n = 0;
			foreach (var rfile in aremotes)
			{
				await Task.Yield();
				n++;
				labStatus.Content = "Downloading files " + n + "/" + aremotes.Length;

				var bytes = GetShellFileRemote(rfile.filename);
				var filename = Path.Combine(_shellCatalog, rfile.filename);
				var dirname = Path.GetDirectoryName(filename);
				if (!Directory.Exists(dirname))
				{
					Directory.CreateDirectory(dirname);
				}
				File.WriteAllBytes(filename, bytes);

				var lastWrieTime = new DateTime(rfile.lastWrieTime);
				File.SetLastWriteTimeUtc(filename, lastWrieTime);
			}

			await Task.Yield();
			labStatus.Content = "Starting application...";
			StartShell();
		}

		ShellUpgradeUtils.ShellFileInfo[] GetShellFilesRemote(int all)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(_baseUrl);
				var response = client.GetAsync($"api/lookups/GetShellFiles?all={all}").Result;
				var json = response.Content.ReadAsStringAsync().Result;
				var files = JsonConvert.DeserializeObject<ShellUpgradeUtils.ShellFileInfo[]>(json);
				return files;
			}
			catch(Exception ex)
			{
				ServerError();
				return null;
			}
		}

		byte[] GetShellFileRemote(string filename)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(_baseUrl);
				var response = client.GetAsync($"api/lookups/GetShellFile?filename={filename}").Result;
				var bytes = response.Content.ReadAsByteArrayAsync().Result;
				return bytes;
			}
			catch (Exception ex)
			{
				ServerError();
				return null;
			}
		}

		void StartShell()
		{
			Process.Start(Path.Combine(_shellCatalog, ShellUpgradeUtils.EXEFILE));
			Application.Current.Shutdown();
		}

		void ServerError()
		{
			MessageBox.Show("Connection to the server failed. Please make sure your VPN connection is turned ON", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			Application.Current.Shutdown();
		}
	}
}
