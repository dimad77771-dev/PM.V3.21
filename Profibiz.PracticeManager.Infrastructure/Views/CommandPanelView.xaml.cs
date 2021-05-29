using System.Linq;
using DevExpress.Mvvm.UI;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Grid;

namespace Profibiz.PracticeManager.Infrastructure
{
    public partial class CommandPanelView
	{
		public CommandPanelView()
		{
			InitializeComponent();
            UpdateCloseOnly();

        }
        public void UpdateCloseOnly()
        {
            butSaveAndClose.Visibility = CloseOnly ? Visibility.Hidden : Visibility.Visible;
            butSave.Visibility = butSaveAndClose.Visibility;
            if (HideSave)
            {
                butSave.Visibility = Visibility.Collapsed;
            }
			if (HideSaveAndClose)
			{
				butSaveAndClose.Visibility = Visibility.Collapsed;
			}
		}
		public void UpdateSaveEnabled()
		{
			butSave.IsEnabled = SaveEnabled;
		}

		public static readonly DependencyProperty CloseOnlyProperty =
            DependencyProperty.Register(nameof(CloseOnly), typeof(Boolean), typeof(CommandPanelView), new PropertyMetadata(OnCloseOnlyChange));
        public Boolean CloseOnly
        {
            get { return (Boolean)GetValue(CloseOnlyProperty); }
            set { SetValue(CloseOnlyProperty, value); }
        }
        public static void OnCloseOnlyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CommandPanelView)d).UpdateCloseOnly();
        }

        public static readonly DependencyProperty HideSaveProperty =
            DependencyProperty.Register(nameof(HideSave), typeof(Boolean), typeof(CommandPanelView), new PropertyMetadata(OnHideSaveChange));
        public Boolean HideSave
        {
            get { return (Boolean)GetValue(HideSaveProperty); }
            set { SetValue(HideSaveProperty, value); }
        }
        public static void OnHideSaveChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CommandPanelView)d).UpdateCloseOnly();
        }

		public static readonly DependencyProperty HideSaveAndCloseProperty =
			DependencyProperty.Register(nameof(HideSaveAndClose), typeof(Boolean), typeof(CommandPanelView), new PropertyMetadata(OnHideSaveAndCloseChange));
		public Boolean HideSaveAndClose
		{
			get { return (Boolean)GetValue(HideSaveAndCloseProperty); }
			set { SetValue(HideSaveAndCloseProperty, value); }
		}
		public static void OnHideSaveAndCloseChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((CommandPanelView)d).UpdateCloseOnly();
		}


		public static readonly DependencyProperty SaveEnabledProperty =
			DependencyProperty.Register(nameof(SaveEnabled), typeof(Boolean), typeof(CommandPanelView), new PropertyMetadata(true, OnSaveEnabledChange));
		public Boolean SaveEnabled
		{
			get { return (Boolean)GetValue(SaveEnabledProperty); }
			set { SetValue(SaveEnabledProperty, value); }
		}
		public static void OnSaveEnabledChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((CommandPanelView)d).UpdateSaveEnabled();
		}


	}
}
