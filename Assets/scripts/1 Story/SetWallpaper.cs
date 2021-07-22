using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using UnityEngine;
using System.Reflection;

// в конце игры рабочий стол пользователя меняется на другую картинку.
static class SetWallpaper 
{ 
	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)] 
	static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni); 
	const int SPI_SETDESKWALLPAPER = 20; 
	const int SPIF_UPDATEINIFILE = 0x1; 
	const int SPIF_SENDWININICHANGE = 0x2; 
	const string path = "desktop.bmp"; 

	public static void change() 
	{ 
		string folder = Application.dataPath;
		SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, Marshal.StringToBSTR(folder+"\\"+path), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE); 
	} 

	public static string getPath()
	{
		return Application.dataPath;
	}
}

