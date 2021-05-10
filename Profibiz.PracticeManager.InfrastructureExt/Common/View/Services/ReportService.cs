using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;

namespace DevExpress.DevAV.Common.View {
    public abstract class ReportServiceBase : ServiceBase, IReportService {
        bool isVisible;
        IReportInfo defaultReportInfo;
        IReportInfo reportInfo;
        IReportInfo actualReportInfo;

        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(int), typeof(ReportServiceBase), new PropertyMetadata(0));

        public int ProgressValue {
            get { return (int)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        protected bool IsVisible {
            get { return isVisible; }
            set {
                isVisible = value;
                UpdateReport();
                if(!isVisible)
                    this.reportInfo = null;
            }
        }
        protected virtual void SetDefaultReport(IReportInfo reportInfo) {
            this.defaultReportInfo = reportInfo;
            UpdateReport();
        }
        protected virtual void ShowReport(IReportInfo reportInfo) {
            this.reportInfo = reportInfo;
            UpdateReport();
        }
        void UpdateReport() {
            UpdateReportCore(IsVisible ? (reportInfo ?? defaultReportInfo) : null);
        }
        protected virtual void UpdateReportCore(IReportInfo actualReportInfo) {
            UnsubscribeFromParametersViewModel();
            this.actualReportInfo = actualReportInfo;
            SubscribeToParametersViewModel();
            if(this.actualReportInfo == null)
                DestroyReport();
            else
                CreateReport();
        }
        void OnParametersViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            CreateReport();
        }

        protected void CreateReport() {
            IReport report = actualReportInfo.CreateReport();
            report.PrintingSystemBase.ProgressReflector.PositionChanged -= ProgressReflector_PositionChanged;
            report.PrintingSystemBase.ProgressReflector.PositionChanged += ProgressReflector_PositionChanged;

            SetCustomSettingsViewModel(actualReportInfo.ParametersViewModel);
            SetDocumentSource(report);
            report.PrintingSystemBase.ClearContent();
            report.CreateDocument(true);
        }
        void DestroyReport() {
            SetCustomSettingsViewModel(null);
        }
        protected abstract void SetDocumentSource(IReport report);
        protected abstract void SetCustomSettingsViewModel(object customSettingsViewModel);
        object ActualParametersViewModel { get { return this.actualReportInfo == null ? null : this.actualReportInfo.ParametersViewModel; } }
        void SubscribeToParametersViewModel() {
            INotifyPropertyChanged parametersViewModel = ActualParametersViewModel as INotifyPropertyChanged;
            if(parametersViewModel != null)
                parametersViewModel.PropertyChanged += OnParametersViewModelPropertyChanged;
        }
        void UnsubscribeFromParametersViewModel() {
            INotifyPropertyChanged parametersViewModel = ActualParametersViewModel as INotifyPropertyChanged;
            if(parametersViewModel != null)
                parametersViewModel.PropertyChanged -= OnParametersViewModelPropertyChanged;
        }
        void ProgressReflector_PositionChanged(object sender, EventArgs e) {
            ProgressValue = ((ProgressReflector)sender).Position;
        }
        #region IReportService
        void IReportService.SetDefaultReport(IReportInfo reportInfo) {
            SetDefaultReport(reportInfo);
        }
        void IReportService.ShowReport(IReportInfo reportInfo) {
            ShowReport(reportInfo);
        }
        #endregion
    }

    public class DocumentViewerReportService : ReportServiceBase {

        DocumentPreviewControl DocumentViewer { get { return (DocumentPreviewControl)AssociatedObject; } }

        public ZoomMode ZoomMode { get; set; }

        protected override void OnAttached() {
            base.OnAttached();
            IsVisible = true;
            ZoomMode = ZoomMode.FitToWidth;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            IsVisible = false;
        }
        protected override void SetCustomSettingsViewModel(object customSettingsViewModel) { }

        protected override void SetDocumentSource(IReport report) {
            DocumentViewer.DocumentSource = report;
            DocumentViewer.ZoomMode = ZoomMode;
        }
    }
}
