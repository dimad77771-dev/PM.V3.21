using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using DevExpress.DevAV.Common;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using AutoMapper;
using Prism.Regions;
using Autofac;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.ComponentModel;
using System.IO;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class EmailSendsListViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public virtual ObservableCollection<EmailSend> Entities { get; set; }
		public virtual EmailSend SelectedEntity { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }

		~EmailSendsListViewModel()
		{
			NLog.Trace("~EmailSendsListViewModel=" + this.GetHashCode());
		}


		public EmailSendsListViewModel() : base()
		{
			NLog.vv(() => "EmailSendsListViewModel.create=" + this.GetHashCode());
			var ret = GlobalSettings.Instance.EmailSendList.Get();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			//MessengerHelper.Register<MsgRowChange<EmailSend>>(this, OnMsgRowChange);
			DispatcherUIHelper.Run2(LoadData());
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		public async Task LoadData()
		{
			ShowWaitIndicator.Show();

			var query = "sendDateFrom=" + FilterFrom.ToWebQuery() + "&" + "sendDateTo=" + FilterTo.ToWebQuery() + "&";
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetEmailSendList(query));
			Entities = rows.OrderByDescending(q => q.SendDate).ToObservableCollection();
			Entities.ForEach(q => SubscribeListRow(q));

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void SubscribeListRow(EmailSend row)
		{
			row.OnOpenAttachment = () =>
			{
				DispatcherUIHelper.Run(async () =>
				{
					ShowWaitIndicator.Show();
					var query = "emailSendRowId=" + row.RowId;
					var attachments = await businessService.GetEmailSendAttachmentList(query);
					ShowWaitIndicator.Hide();


					var button = row.ButtonAttachment;
					var menu = new System.Windows.Controls.ContextMenu();
					foreach (var attachment in attachments)
					{
						var fileName = attachment.FileName;
						if (string.IsNullOrEmpty(fileName)) fileName = "<No Name>";
						var menuitem = new System.Windows.Controls.MenuItem()
						{
							Header = fileName,
						};
						menuitem.Click += (s, e) =>
						{
							var mouseWaitCursor = new MouseWaitCursor();

							var fileBytes = attachment.FileBytes;
							var pdffile = GetPdfFile(attachment.FileName);
							File.WriteAllBytes(pdffile, fileBytes);
							System.Diagnostics.Process.Start(pdffile);
							mouseWaitCursor.Dispose();
						};
						menu.Items.Add(menuitem);
					}
					button.ContextMenu = menu;
					button.ContextMenu.IsOpen = true;
				});
			};
		}

		public static string GetPdfDirectory()
		{
			var pdfDirectory = Path.Combine(AssemblyHelper.GetMainPath(), "PdfOutput");
			if (!File.Exists(pdfDirectory))
			{
				Directory.CreateDirectory(pdfDirectory);
			}
			return pdfDirectory;
		}

		public static string GetPdfFile(string pdffile0)
		{
			int npp = 0;
			while (true)
			{
				var file = Path.GetFileNameWithoutExtension(pdffile0);
				var ext = Path.GetExtension(pdffile0);
				var pdffile = file + (npp == 0 ? "" : "(" + npp + ")") + ext;
				pdffile = Path.Combine(GetPdfDirectory(), pdffile);
				if (File.Exists(pdffile))
				{
					try
					{
						File.Delete(pdffile);
					}
					catch (Exception) { }
				}
				if (!File.Exists(pdffile))
				{
					return pdffile;
				}
				npp++;
			}
		}


		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg = "", FinanceDateClass preset = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (preset != null)
				{
					var cret = preset.Get();
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "PreviousMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(-1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "NextMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}

				GlobalSettings.Instance.EmailSendList.Set(FilterFrom, FilterTo);
				await LoadData();
			});
		}
	}
}
