using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class UploadServerThread
{
	private Socket socket;

	public UploadServerThread(Socket socket)
	{
		this.socket = socket;
	}

	public void Run()
	{
		try
		{
			// Create a NetworkStream that owns the socket.
			using (NetworkStream networkStream = new NetworkStream(socket, ownsSocket: true))
			{
				// Read data from the client
				StreamReader reader = new StreamReader(networkStream);
				string requestString = reader.ReadToEnd(); // This is a simple way to read the request. For real applications, you should parse HTTP headers and content properly.

				// Process the request and get a response (this is where you'd put your HTTP handling logic)
				string responseString = ProcessRequest(requestString);

				// Write the response back to the client
				byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
				networkStream.Write(responseBytes, 0, responseBytes.Length);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Exception: " + e.Message);
		}
	}

	private string ProcessRequest(string request)
	{
		// Here, implement your logic to handle the HTTP request
		// This is a placeholder response
		return "HTTP/1.1 200 OK\r\nContent-Length: 11\r\nContent-Type: text/plain\r\n\r\nHello World";
	}
}