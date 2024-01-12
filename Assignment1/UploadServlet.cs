using System;
using System.Net.Sockets;
using System.Text;

namespace Assignment1 {
	public class UploadServlet {
		
		
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