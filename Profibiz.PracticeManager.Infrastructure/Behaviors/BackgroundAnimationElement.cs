using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using System.Threading;
using DevExpress.Xpf.Editors;
using System.Windows.Input;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class BackgroundAnimationElement : FrameworkContentElement
    {
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(nameof(Color), typeof(Color), typeof(BackgroundAnimationElement),
            new PropertyMetadata(Colors.Transparent));
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(nameof(IsActive), typeof(Boolean), typeof(BackgroundAnimationElement),
            new PropertyMetadata(false));
        public Boolean IsActive
        {
            get { return (Boolean)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }


        
    }
}
