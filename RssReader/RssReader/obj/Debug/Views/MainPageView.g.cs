﻿

#pragma checksum "C:\Users\Alex\Documents\GitHub\RssReader\RssReader\RssReader\Views\MainPageView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "13D650DA2351242C22CBAE3BABD301BF"
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
    partial class MainPageView : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 17 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ExceptionFlyoutButtonClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 67 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ItemClicked;
                 #line default
                 #line hidden
                #line 68 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.OnLoaded;
                 #line default
                 #line hidden
                #line 69 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).RightTapped += this.ListViewRightTapped1;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 77 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ButtonClick;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 91 "..\..\Views\MainPageView.xaml"
                ((global::Windows.UI.Xaml.Controls.Image)(target)).ImageFailed += this.ImageImageFailed;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


