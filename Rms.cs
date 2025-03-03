using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

public class Rms
{
	private const int INTERVAL = 5;

	private const int MAXTIME = 500;

	public static int status;

	public static sbyte[] data;

	public static string filename;

	public static void writeLog(string sLog)
	{
		try
		{
			sLog = DateTime.Now.ToString("h:mm:ss tt") + "--- " + sLog;
			int length = Application.dataPath.LastIndexOf("/");
			using StreamWriter streamWriter = new StreamWriter(Application.dataPath.Substring(0, length).Replace("/", "\\") + "\\KnightOnline_300_Data\\Managed\\log.txt", append: true, Encoding.UTF8);
			streamWriter.WriteLine(sLog);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Lỗi khi ghi log: " + ex.Message);
		}
	}

	public static void saveRMS(string filename, sbyte[] data)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			__saveRMS(filename, data);
		}
		else
		{
			_saveRMS(filename, data);
		}
	}

	public static sbyte[] loadRMS(string filename)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return __loadRMS(filename);
		}
		return _loadRMS(filename);
	}

	public static string loadRMSString(string fileName)
	{
		sbyte[] array = loadRMS(fileName);
		if (array == null)
		{
			return null;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		try
		{
			string result = dataInputStream.readUTF();
			dataInputStream.close();
			return result;
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
		return null;
	}

	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if (var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)(var[i] + 256);
			}
		}
		return array;
	}

	public static void saveRMSString(string filename, string data)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(data);
			saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
	}

	private static void _saveRMS(string filename, sbyte[] data)
	{
		if (status != 0)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
			return;
		}
		Rms.filename = filename;
		Rms.data = data;
		status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO SAVE RMS " + filename);
		}
	}

	private static sbyte[] _loadRMS(string filename)
	{
		if (status != 0)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
			return null;
		}
		Rms.filename = filename;
		data = null;
		status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO LOAD RMS " + filename);
		}
		return data;
	}

	public static void update()
	{
		if (status == 2)
		{
			status = 1;
			__saveRMS(filename, data);
			status = 0;
		}
		else if (status == 3)
		{
			status = 1;
			data = __loadRMS(filename);
			status = 0;
		}
	}

	public static int loadRMSInt(string file)
	{
		sbyte[] array = loadRMS(file);
		if (array == null)
		{
			return -1;
		}
		return array[0];
	}

	public static void saveRMSInt(string file, int x)
	{
		try
		{
			saveRMS(file, new sbyte[1] { (sbyte)x });
		}
		catch (Exception)
		{
		}
	}

	public static string GetiPhoneDocumentsPath()
	{
		return Application.persistentDataPath;
	}

	private static void __saveRMS(string filename, sbyte[] data)
	{
		string text = GetiPhoneDocumentsPath() + "/" + filename;
		FileStream fileStream = new FileStream(text, FileMode.Create);
		fileStream.Write(ArrayCast.cast(data), 0, data.Length);
		fileStream.Flush();
		fileStream.Close();
		Main.setBackupIcloud(text);
	}

	private static sbyte[] __loadRMS(string filename)
	{
		try
		{
			FileStream fileStream = new FileStream(GetiPhoneDocumentsPath() + "/" + filename, FileMode.Open);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			ArrayCast.cast(array);
			return ArrayCast.cast(array);
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static void deleteAllRecord()
	{
		FileInfo[] files = new DirectoryInfo(GetiPhoneDocumentsPath() + "/").GetFiles();
		for (int i = 0; i < files.Length; i++)
		{
			files[i].Delete();
		}
	}

	public static string ByteArrayToString(byte[] ba)
	{
		return BitConverter.ToString(ba).Replace("-", string.Empty);
	}

	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	public static void deleteRecordByName(string path)
	{
		try
		{
			File.Delete(GetiPhoneDocumentsPath() + "/" + path);
		}
		catch (Exception)
		{
		}
	}

	public static void deleteRecordCompareToName()
	{
		try
		{
			FileInfo[] files = new DirectoryInfo(GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				if (CRes.CheckDelRMS(files[i].Name))
				{
					files[i].Delete();
				}
			}
		}
		catch (Exception)
		{
		}
	}
}
