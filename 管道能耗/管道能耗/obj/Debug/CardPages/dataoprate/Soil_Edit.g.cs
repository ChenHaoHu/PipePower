﻿#pragma checksum "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9FA0258F3B4ED6027CAEEB66ABD81A885A59B9E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using 管道能耗.CardPages.dataoprate;


namespace 管道能耗.CardPages.dataoprate {
    
    
    /// <summary>
    /// Soil_Edit
    /// </summary>
    public partial class Soil_Edit : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox soidId;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox soilName;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox soilTemp;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox soilDiff;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/管道能耗;component/cardpages/dataoprate/soil_edit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.soidId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.soilName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.soilTemp = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.soilDiff = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 49 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_build);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 50 "..\..\..\..\CardPages\dataoprate\Soil_Edit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_cancel);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

