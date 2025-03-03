using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class FileWatcher : MonoBehaviour
{
	public string filePath;

	private float checkInterval = 1f;

	private string lastFileContent;

	private void Start()
	{
		StartCoroutine(CheckFileChange());
	}

	private IEnumerator CheckFileChange()
	{
		while (true)
		{
			yield return new WaitForSeconds(checkInterval);
			if (File.Exists(filePath))
			{
				string text = File.ReadAllText(filePath);
				if (text != lastFileContent)
				{
					lastFileContent = text;
					OnFileChanged();
				}
			}
		}
	}

	private void OnFileChanged()
	{
		try
		{
			DocFile();
		}
		catch (Exception ex)
		{
			Debug.LogError("Error in OnFileChanged: " + ex.Message);
		}
	}

	public void DocFile()
	{
		try
		{
			Debug.Log("File changed and read successfully.");
		}
		catch (Exception ex)
		{
			Debug.LogError("Error in DocFile: " + ex.Message);
		}
	}
}
