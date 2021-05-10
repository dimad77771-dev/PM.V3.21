using Autofac;
using Autofac.Core;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ShowRibbonBehavior : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty NameProperty = 
			DependencyProperty.Register(nameof(Name), typeof(string), typeof(ShowRibbonBehavior));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public static readonly DependencyProperty RegionProperty = 
			DependencyProperty.Register(nameof(Region), typeof(string), typeof(ShowRibbonBehavior));
		public string Region
		{
			get { return (string)GetValue(RegionProperty); }
			set { SetValue(RegionProperty, value); }
		}

		public static readonly DependencyProperty IsVisibleProperty = 
			DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(ShowRibbonBehavior), new PropertyMetadata(true, OnIsVisibleChange));
		public bool IsVisible
		{
			get { return (bool)GetValue(IsVisibleProperty); }
			set { SetValue(IsVisibleProperty, value); }
		}

		public static readonly DependencyProperty IsUpdateOnStartProperty =
			DependencyProperty.Register(nameof(IsUpdateOnStart), typeof(bool), typeof(ShowRibbonBehavior), new PropertyMetadata(true));
		public bool IsUpdateOnStart
		{
			get { return (bool)GetValue(IsUpdateOnStartProperty); }
			set { SetValue(IsUpdateOnStartProperty, value); }
		}


		public static void OnIsVisibleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ShowRibbonBehavior)d).UpdateRibbonView();
		}



		public FrameworkElement RibbonView { get; set; }

		void UpdateRibbonView()
		{
			if (AssociatedObject == null) return;

			var parens = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualParents(AssociatedObject).ToArray();

			if (RibbonView == null)
			{
				RibbonView = (FrameworkElement)ServiceLocator.Current.GetInstance<object>(Name);
			}
			RibbonView.DataContext = AssociatedObject.DataContext;

			var regionManager = ServiceHelper.GetRegionManager();
			if (string.IsNullOrEmpty(Region))
			{
				Region = "ToolbarRegion";
			}
			var region = regionManager.Regions[Region];


			if (IsVisible)
			{
				region.RemoveAll();
				region.Add(RibbonView);
			}
		}

		protected override void OnAttached()
		{
			AssociatedObject.DataContextChanged += (s, e) =>
			{
				if (IsUpdateOnStart)
				{
					UpdateRibbonView();
				}
			};
			if (IsUpdateOnStart)
			{
				UpdateRibbonView();
			}
		}

		protected override void OnDetaching()
		{
			//AssociatedObject.Click -= OnClick;
		}
	}
}
