﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <author name="Kumar Devvrat"/>
//     <version>$Revision: $</version>
// </file>
using System;
using System.Windows;
using System.Windows.Controls;

using ICSharpCode.WpfDesign.Designer.Controls;
using ICSharpCode.WpfDesign.Extensions;
using ICSharpCode.WpfDesign.Adorners;

namespace ICSharpCode.WpfDesign.Designer.Extensions
{
	/// <summary>
	/// Extends the Quick operation menu for the designer.
	/// </summary>
	[ExtensionFor(typeof (FrameworkElement))]
    class QuickOperationMenuExtension : PrimarySelectionAdornerProvider
    {
        private QuickOperationMenu _menu;
        
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _menu = new QuickOperationMenu();
            _menu.Loaded += OnMenuLoaded;
            var placement = new RelativePlacement(HorizontalAlignment.Right, VerticalAlignment.Top) {XOffset = 7};
            this.AddAdorners(placement, _menu);
        }

        private void OnMenuLoaded(object sender, EventArgs e)
        {
            _menu.MainHeader.Click += MainHeaderClick;
            int menuItemsAdded = 0;
            var view = this.ExtendedItem.View;

            if (view != null) {
                string setValue;
                if (view is StackPanel) {
                    var ch = new MenuItem() {Header = "Change Orientation"};
                    _menu.AddSubMenuInTheHeader(ch);
                    setValue = this.ExtendedItem.Properties[StackPanel.OrientationProperty].ValueOnInstance.ToString();
                    _menu.AddSubMenuCheckable(ch, Enum.GetValues(typeof (Orientation)), Orientation.Vertical.ToString(), setValue);
                    _menu.MainHeader.Items.Add(new Separator());
                    menuItemsAdded++;
                }

                var ha = new MenuItem() {Header = "Horizontal Alignment"};
                _menu.AddSubMenuInTheHeader(ha);
                setValue = this.ExtendedItem.Properties[FrameworkElement.HorizontalAlignmentProperty].ValueOnInstance.ToString();
                _menu.AddSubMenuCheckable(ha, Enum.GetValues(typeof (HorizontalAlignment)), HorizontalAlignment.Stretch.ToString(), setValue);
                menuItemsAdded++;

                var va = new MenuItem() {Header = "Vertical Alignment"};
                _menu.AddSubMenuInTheHeader(va);
                setValue = this.ExtendedItem.Properties[FrameworkElement.VerticalAlignmentProperty].ValueOnInstance.ToString();
                _menu.AddSubMenuCheckable(va, Enum.GetValues(typeof (VerticalAlignment)), VerticalAlignment.Stretch.ToString(), setValue);
                menuItemsAdded++;
            }

            if (menuItemsAdded == 0) {
                OnRemove();
            }
        }

        private void MainHeaderClick(object sender, RoutedEventArgs e)
        {
            var clickedOn = e.Source as MenuItem;
            if (clickedOn != null) {
                var parent = clickedOn.Parent as MenuItem;
                if (parent != null) {
                    if ((string) parent.Header == "Change Orientation") {
                        var value = _menu.UncheckChildrenAndSelectClicked(parent, clickedOn);
                        if (value != null) {
                            var orientation = Enum.Parse(typeof (Orientation), value);
                            if (orientation != null)
                                this.ExtendedItem.Properties[StackPanel.OrientationProperty].SetValue(orientation);
                        }
                    }

                    if ((string) parent.Header == "Horizontal Alignment") {
                        var value = _menu.UncheckChildrenAndSelectClicked(parent, clickedOn);
                        if (value != null) {
                            var ha = Enum.Parse(typeof (HorizontalAlignment), value);
                            if (ha != null)
                                this.ExtendedItem.Properties[FrameworkElement.HorizontalAlignmentProperty].SetValue(ha);
                        }
                    }

                    if ((string) parent.Header == "Vertical Alignment") {
                        var value = _menu.UncheckChildrenAndSelectClicked(parent, clickedOn);
                        if (value != null) {
                            var va = Enum.Parse(typeof (VerticalAlignment), value);
                            if (va != null)
                                this.ExtendedItem.Properties[FrameworkElement.VerticalAlignmentProperty].SetValue(va);
                        }
                    }
                }
            }
        }

        protected override void OnRemove()
        {
            base.OnRemove();
            _menu.Loaded -= OnMenuLoaded;
        }
    }
}
