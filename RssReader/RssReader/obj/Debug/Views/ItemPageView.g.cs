﻿

#pragma checksum "C:\Users\Alex\Documents\GitHub\RssReader\RssReader\RssReader\Views\ItemPageView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "824FDFB2AE1A69C6E8FF9F4595DDDAE3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RssReader.Views
{
    partial class ItemPageView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 28 "..\..\Views\ItemPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ItemClicked;
                 #line default
                 #line hidden
                #line 29 "..\..\Views\ItemPageView.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.OnLoaded;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


