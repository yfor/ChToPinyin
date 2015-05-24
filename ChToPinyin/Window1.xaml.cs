/*
 * Created by SharpDevelop.
 * User: xiaoqiang
 * Date: 2015/5/23
 * Time: 23:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace ChtoPinYinDic
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class DicWindow : Window
	{
		
		
		ChtoPinYin c;
	
		public DicWindow()
		{
			InitializeComponent();
			//设置图标
			Uri iconUri = new Uri("re/dic.ico", UriKind.RelativeOrAbsolute);
			this.Icon = BitmapFrame.Create(iconUri);
			
			
			ViewAddContorer();
			//初始化转换模型
			c = new ChtoPinYin();
			ShowLoading();
	
		}
		//给视图层加入控制器
		void ViewAddContorer()
		{
			
			Pin.TextChanged += PinYinTextChanged;
	
			Hanzi.TextChanged += HanziTextChanged;

			Sz.Click += (object sender, RoutedEventArgs e) => {
				PinYIn.Text = c.HanziChange(Hanzi.Text, Sz.IsChecked.Value);
			};
		}
		//其实故弄玄虚  以及载人过了   显得运行很快而已
		void ShowLoading()
		{
		
			Action a = () => {
				Dictionary<string, string>.KeyCollection keys = c.getPinYins();
				foreach (string s in keys) {
					Pin.Dispatcher.Invoke(new Action(() => {
						Pin.Text = s;
					}));
					Thread.Sleep(1);
				}
				
				Pin.Dispatcher.Invoke(new Action(() => {
					Pin.Text = "a";
				}));
			};
			Thread loadThread;
			loadThread = new Thread(new ThreadStart(a));
			loadThread.Start();
		}

		//控制器
		void PinYinTextChanged(object sender, RoutedEventArgs e)
		{
			TY.Text = c.PinYinChange(Pin.Text);
		}
		void HanziTextChanged(object sender, RoutedEventArgs e)
		{
			
			PinYIn.Text = c.HanziChange(Hanzi.Text, Sz.IsChecked.Value);
		}
		


	}
}

//  	Pin.Dispatcher.Invoke(
//   	DispatcherPriority.Normal, TimeSpan.FromSeconds(1), new Action<string>(UpdatePinYin),s);
//		private void UpdatePinYin(String s)
//        {
//		 	this.Pin.Text =s;
//		}