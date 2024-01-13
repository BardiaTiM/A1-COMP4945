using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Assignment1 {
	public class UploadServlet {
		public void doPost(NetworkStream networkStream) {
			try {
				MemoryStream memoryStream = new MemoryStream();
				byte[] buffer = new byte[999999999]; // Adjust this size based on expected data size
				int bytesRead;
				while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0) {
					memoryStream.Write(buffer, 0, bytesRead);
					break; // Breaking after first read; adjust if streaming
				}

				byte[] inputData = memoryStream.ToArray();
				string dataStr = Encoding.UTF8.GetString(inputData);

				// TODO: Extract the actual boundary from the Content-Type header
				string boundary = "------WebKitFormBoundary"; // Replace with actual boundary

				// Split multipart form data using the boundary
				string[] parts = dataStr.Split(new string[] { boundary }, StringSplitOptions.RemoveEmptyEntries);

				Dictionary<string, string> formFields = new Dictionary<string, string>();
				string filename = null;
				byte[] fileData = null;

				foreach (string part in parts) {
					int partStartIndex = dataStr.IndexOf(part);
					Console.WriteLine("Part: " + part);
					
					if (part.Contains("name=\"caption\"")) {
						string caption = ExtractValue(part);
						formFields.Add("caption", caption);
					}
					else if (part.Contains("name=\"date\"")) {
						string date = ExtractValue(part);
						formFields.Add("date", date);
					}
					else if (part.Contains("name=\"file\";")) {
						filename = ExtractFilename(part);
						fileData = ExtractFileData(part, inputData, partStartIndex);
					}
				}

				// Check if the keys exist before using them
				if (formFields.ContainsKey("caption") && formFields.ContainsKey("date") && filename != null) {
					filename = formFields["caption"] + "_" + formFields["date"] + "_" + filename;
					Console.WriteLine(filename);

					// Write to the specified folder
					string directoryPath = "/Users/teihyung/RiderProjects/Assignment1/Assignment1/image";
					string filePath = directoryPath + Path.DirectorySeparatorChar + filename;

					// Write the file data to the specified file
					File.WriteAllBytes(filePath, fileData);
				}
				else {
					Console.WriteLine("Required form fields not found in form data");
				}
			}
			catch (Exception e) {
				Console.WriteLine("Error in doPost: " + e.Message);
			}
		}

		private string ExtractValue(string part) {
			int startIndex = part.IndexOf("\r\n\r\n") + 4;
			return part.Substring(startIndex).Trim();
		}

		private string ExtractFilename(string part) {
			string filenameSection =
				part.Split(new string[] { "filename=\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
			return filenameSection.Split('\"')[0].Trim();
		}
		
		private byte[] ExtractFileData(string part, byte[] inputData, int partStartIndex) {
			int headerEndIndex = part.IndexOf("\r\n\r\n") + 4;
			int fileDataStartIndex = partStartIndex + headerEndIndex;

			// Find the end of the file data
			int fileDataEndIndex = inputData.Length;
			string boundaryMarker = "\r\n------WebKitFormBoundary";
			int boundaryIndex = part.IndexOf(boundaryMarker, headerEndIndex);
			if (boundaryIndex != -1) {
				fileDataEndIndex = partStartIndex + boundaryIndex;
			}

			int fileDataLength = fileDataEndIndex - fileDataStartIndex;
			byte[] fileData = new byte[fileDataLength];
			Array.Copy(inputData, fileDataStartIndex, fileData, 0, fileDataLength);
			return fileData;
		}


		public void doGet(NetworkStream networkStream) {
			string htmlContent =
				"<!DOCTYPE html>" +
				"<html>" +
				"<body>" +
				"<p>Please fill out the form below to upload your file.</p>" +
				"<form action='/upload' method='post' enctype='multipart/form-data'>" +
				"Caption: <input type='text' name='caption'><br><br>" +
				"Date: <input type='date' name='date'><br><br>" +
				"File: <input type='file' name='file'><br><br>" +
				"<input type='submit' value='Upload'>" +
				"</form>" +
				"</body>" +
				"</html>";

			string httpResponse = "HTTP/1.1 200 OK\r\n" +
			                      "Content-Type: text/html; charset=UTF-8\r\n" +
			                      "Content-Length: " + Encoding.UTF8.GetByteCount(htmlContent) + "\r\n\r\n" +
			                      htmlContent;

			byte[] responseBytes = Encoding.UTF8.GetBytes(httpResponse);
			networkStream.Write(responseBytes, 0, responseBytes.Length);
			Console.WriteLine("Form sent successfully.");
		}
	}
}