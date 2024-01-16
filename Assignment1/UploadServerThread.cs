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
				// Creating instances of request and response
				HttpServletRequest request = new HttpServletRequest(networkStream);
				HttpServletResponse response = new HttpServletResponse(networkStream);

				StreamReader reader = new StreamReader(networkStream);
				string requestLine = reader.ReadLine();
				Console.WriteLine(requestLine);

				if (requestLine.StartsWith("GET / HTTP/1.1")) {
					Console.WriteLine("GET");
					UploadServlet uploadServlet = new UploadServlet();
					uploadServlet.doGet(request, response);
				} else if (requestLine.StartsWith("POST")) {
					Console.WriteLine("POST");
					UploadServlet uploadServlet = new UploadServlet();
					uploadServlet.doPost(request, response);
				}

				socket.Close();
			}
		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}
	}

}