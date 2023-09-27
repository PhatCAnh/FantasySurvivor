using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	string fileName = "example.txt"; // Thay đổi thành tên tệp tin bạn muốn truy cập
	string filePath = Path.Combine(Application.streamingAssetsPath, "example.txt");

	private void Start()
	{
		if(File.Exists(filePath))
		{
			string fileContents = File.ReadAllText(filePath);
			Debug.Log("Nội dung của tệp tin: " + fileContents);
		}
		else
		{
			Debug.LogError("Không tìm thấy tệp tin: " + fileName);
		}
	}
}