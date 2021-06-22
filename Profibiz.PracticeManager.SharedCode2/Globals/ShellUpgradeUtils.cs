using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.SharedCode
{
	public static class ShellUpgradeUtils
	{
		public const string EXEFILE = @"Profibiz.PraciticeManager.Shell.exe";

		public static ShellFileInfo[] GetShellFiles(string shellBaseCatalog, int all)
		{
			if (shellBaseCatalog.EndsWith(@"\"))
			{
				shellBaseCatalog = shellBaseCatalog.Substring(0, shellBaseCatalog.Length - 1);
			}
			var files = Directory.GetFiles(shellBaseCatalog, "*.*", SearchOption.AllDirectories);
			if (all != 1)
			{
				var exefile = Path.Combine(shellBaseCatalog, EXEFILE);
				files = files.Where(q => q.ToLower() == exefile.ToLower()).ToArray();
			}

			ShellFileInfo[] info = null;
			var dat1 = DateTime.Now;
			var task = Task.Run(async () =>
			{
				var ftasks = new List<Task<ShellFileInfo>>();
				foreach (var file in files)
				{
					var ftask = Task.Run(() => new ShellFileInfo
					{
						filename = file.Substring(shellBaseCatalog.Length + 1).ToLower(),
						hash = GetChecksumBuffered(file),
						lastWrieTime = File.GetLastWriteTimeUtc(file).Ticks,
					});
					ftasks.Add(ftask);
				}
				info = await Task.WhenAll(ftasks);
			});
			task.Wait();

			var dat2 = DateTime.Now;
			var ns = (dat2 - dat1).TotalMilliseconds;

			return info;
		}

		public static string GetChecksumBuffered(string file)
		{
			using (var stream = File.OpenRead(file))
			{
				var sha = new SHA256Managed();
				byte[] checksum = sha.ComputeHash(stream);
				return BitConverter.ToString(checksum).Replace("-", String.Empty);
			}
		}

		public struct ShellFileInfo
		{
			public string filename { get; set; }
			public string hash { get; set; }
			public long lastWrieTime { get; set; }

			public bool eqv(ShellFileInfo arg)
			{
				return filename.ToLower() == arg.filename.ToLower() && hash == arg.hash && lastWrieTime == arg.lastWrieTime;
			}
		}
	}
}
