using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Assignment1;

public class UploadServerThread {
	private Socket socket;

	public UploadServerThread(Socket socket) {
		this.socket = socket;
	}

	public void Run() {
		try {
			using (NetworkStream networkStream = new NetworkStream(socket, ownsSocket: true)) {
				StreamReader reader = new StreamReader(networkStream);
				string requestLine = reader.ReadLine();
				Console.WriteLine(requestLine);

				if (requestLine.StartsWith("GET / HTTP/1.1")) {
					Console.WriteLine("GET");
					UploadServlet uploadServlet = new UploadServlet();
					uploadServlet.doGet(networkStream);
				} else if (requestLine.StartsWith("POST")) {
					Console.WriteLine("POST");
					UploadServlet uploadServlet = new UploadServlet();
					uploadServlet.doPost(networkStream);
				}

				socket.Close();
			}
		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}
	}

	// private string HandleGetRequest() {
	// 	// Implement your logic for handling GET request
	// 	// This is a placeholder response
	// 	return "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 11\r\n\r\nHello World";
	// }

	private string HandlePostRequest() {
		// Implement your logic for handling POST request
		// This is a placeholder response
		return "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 11\r\n\r\nPost Received";
	}
}