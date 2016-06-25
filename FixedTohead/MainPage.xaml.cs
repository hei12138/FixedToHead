using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace FixedTohead
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //定义用于header的平移对象
        private TranslateTransform _tt;
        public MainPage()
        {
            this.InitializeComponent();
            //给pivot注册 鼠标滚轮路由事件，否则鼠标滚轮的滑动 会变成切换pivotitem
            pivot.AddHandler(PointerWheelChangedEvent, new PointerEventHandler(OnChanged), true);
            //初始化平移对象
            _tt = header.RenderTransform as TranslateTransform;
            if (_tt == null)
            {
                header.RenderTransform = _tt = new TranslateTransform();
            }
        }

        private void OnChanged(object sender, PointerRoutedEventArgs e)
        {
            //判断鼠标滚动方向
            if (e.GetCurrentPoint(pivot).Properties.MouseWheelDelta < 0)
            {
                scroll.ChangeView(0, scroll.VerticalOffset + 75, 1);
            }
            else
            {
                scroll.ChangeView(0, scroll.VerticalOffset - 75, 1);
            }
            e.Handled = true;
        }

        //当scrollview 滚动时改变header的位置，使其往上移动
        private void scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //当滚动条到达导航栏时，停止移动
            if (scroll.VerticalOffset >= 150)
            {
                _tt.Y = -150;
            }
            else
            {
                _tt.Y = -scroll.VerticalOffset;
            }
        }
    }
}
