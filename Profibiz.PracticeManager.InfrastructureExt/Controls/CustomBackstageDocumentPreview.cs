using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;

namespace DevExpress.DevAV.Controls {
    public class CustomBackstageDocumentPreview : BackstageDocumentPreview {
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(int), typeof(CustomBackstageDocumentPreview), new PropertyMetadata(0));
        public static readonly DependencyProperty DocumentSourceProperty =
            DependencyProperty.Register("DocumentSource", typeof(IReport), typeof(CustomBackstageDocumentPreview), new PropertyMetadata(null, (d, e) => ((CustomBackstageDocumentPreview)d).OnDocumentSourceChanged(e)));

        public int ProgressValue {
            get { return (int)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }
        public IReport DocumentSource {
            get { return (IReport)GetValue(DocumentSourceProperty); }
            set { SetValue(DocumentSourceProperty, value); }
        }
        void OnDocumentSourceChanged(DependencyPropertyChangedEventArgs e) {
            if(e.OldValue != null)
                ((IReport)e.OldValue).PrintingSystemBase.ProgressReflector.PositionChanged -= ProgressReflector_PositionChanged;
            if(e.NewValue != null)
                ((IReport)e.NewValue).PrintingSystemBase.ProgressReflector.PositionChanged += ProgressReflector_PositionChanged;
        }

        void ProgressReflector_PositionChanged(object sender, EventArgs e) {
            var progressReflector = (ProgressReflector)sender;
            ProgressValue = progressReflector.Position;
        }
    }
}
