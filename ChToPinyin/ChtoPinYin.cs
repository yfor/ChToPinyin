/*
 * 由SharpDevelop创建。
 * 用户： xiaoqiang
 * 日期: 05/24/2015
 * 时间: 00:21
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace ChtoPinYinDic
{
	/// <summary>
	/// Description of Model.
	/// </summary>
	public class ChtoPinYin
	{
		Dictionary<string, string> dic = 
			new Dictionary<string, string>();
		
		public ChtoPinYin()
		{
			
		
			iniDic();
				
		}
		//初始化字典信息
		void iniDic()
		{
			StreamReader sr = new StreamReader("re/value.txt");
			String line = "";
			while (line != null) {
				line = sr.ReadLine();
				if (line != null) {
					string[] lkvs = line.Split(new Char[] { ',' });
					for (int i = 0; i < lkvs.Length; i++) {
						string[] kvs = lkvs[i].Split(new Char[] { ':' });
						if (kvs.Length == 2)
							try {
								dic.Add(kvs[0].Trim(), kvs[1].Trim());
							} catch {
								dic[kvs[0].Trim()] = dic[kvs[0].Trim()] + kvs[1].Trim();
							}
							
					}
					
				}
					
			}
			sr.Close();
		}
		//返回拼音对应的同音字
		public  String PinYinChange(String py)
		{
			try {
				return dic[py];
			} catch {
				return "没找到";
			}
			
		}
		//
		public  Dictionary<string, string>.KeyCollection getPinYins()
		{
			
			return dic.Keys;
		}
		//返回汉字对象的拼音
		public String HanziChange(String ch, bool isSz)
		{
			String pinYins = "";
			char[] chs = ch.Trim().ToCharArray();
			for (int i = 0; i < chs.Length; i++) {
				if (ch[i] < 255) {
					//不转换asci
					pinYins += chs[i];
				} else {
					foreach (KeyValuePair<string, string> kvp in dic) {
						if (kvp.Value.IndexOf(chs[i]) > -1) {
							string s = kvp.Key.ToCharArray()[0].ToString().ToUpper();
							if (isSz) {
								//只显示首字母
								pinYins += s;
							} else {
								pinYins += s + kvp.Key.Substring(1);
							}
							break;
						}
								
					}
				}
	
			}
			
			return pinYins;
		}
	}
}
