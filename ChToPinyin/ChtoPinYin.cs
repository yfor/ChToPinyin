/*
 * 由SharpDevelop创建。
 * 用户： xiaoqiang
 * 日期: 05/24/2015
 * 时间: 00:21
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace ChtoPinYinDic
{
	/// <summary>
	/// Description of Model.
	/// </summary>
	public class ChtoPinYin
	{
		Dictionary<string, string> dic = 
			new Dictionary<string, string>();
		Dictionary<string, string> TYdic = 
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
					string[] lkvs = line.Split(new Char[] { ',','，' });
					for (int i = 0; i < lkvs.Length; i++) {
						string[] kvs = lkvs[i].Split(new Char[] { ':','：' });
						if (kvs.Length == 2)
							if(kvs[0].Trim().ToCharArray()[0]<255)//拼音
							try {
								dic.Add(kvs[0].Trim(), kvs[1].Trim());
							} catch {
								dic[kvs[0].Trim()] = dic[kvs[0].Trim()] + kvs[1].Trim();
							}
						else
							try {
								TYdic.Add(kvs[0].Trim(), kvs[1].Trim());
							} catch {
								TYdic[kvs[0].Trim()] = kvs[1].Trim();
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
		//替换已知多音字
			public  string replaceYZtyz(string hz, bool isSz)
		{
			
			
				Dictionary<string, string>.KeyCollection tyks =TYdic.Keys;
					foreach (string s in tyks) {
					hz = isSz ? hz.Replace(s, justGetSZ(TYdic[s])) : hz.Replace(s, TYdic[s]);
					
				}
				return hz;
		}
			string justGetSZ(string s){
				string rs="";
				foreach(char c in s){
					if(c<91)//'Z'
						rs+=c;
				}
				return rs;
			}
		/**
		 * 判断是否为多音字
		 * 如果是 可以返回 行：hang;xing
		 * */
		public string isDYZ(String ch){
			string DYZ="";
					char[] chs = ch.Trim().ToCharArray();
			//拼接 字典 如 你：ni 行：hang;xing
			Dictionary<string, string> tDic;
			tDic = new Dictionary<string, string>();
			for (int i = 0; i < chs.Length; i++) {
				if (ch[i] < 255) {
				} else {
					foreach (KeyValuePair<string, string> kvp in dic) {
						if (kvp.Value.IndexOf(chs[i]) > -1) {
						try {
								tDic.Add(chs[i].ToString(), kvp.Key.Trim());
							} catch {
								tDic[chs[i].ToString()] = tDic[chs[i].ToString()] +";"+ kvp.Key.Trim();
							}
						}
								
					}
				}
	
			}
			foreach (KeyValuePair<string, string> kvp in tDic) {
				if (kvp.Value.IndexOf(';') > -1) {
					DYZ+=kvp.Key+":"+kvp.Value+" ";
						}
			}
			return DYZ;
		}
		
		//返回汉字对象的拼音   时间换内存
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
