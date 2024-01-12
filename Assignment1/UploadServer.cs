using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Assignment1 {
	public class UploadServer {
		public static void Main(string[] args) {
			IPAddress localAddr = IPAddress.Parse("127.0.0.1");
			int port = 8082;
			TcpListener server = null;

			try {
				server = new TcpListener(localAddr, port);
				server.Start();
				Console.WriteLine("Server started");
			}
			catch (Exception e) {
				Console.WriteLine("Failed to create server: " + e.Message);
				return;
			}

			while (true) {
				try {
					TcpClient client = server.AcceptTcpClient();
					Console.WriteLine("Client connected.");

					// Create a new instance of UploadServerThread
					UploadServerThread serverThread = new UploadServerThread();

					// Start the thread
					Thread clientThread = new Thread(serverThread.Run);
					clientThread.Start();
				}
				catch (Exception e) {
					Console.WriteLine("Error occurred: " + e.Message);
					server.Stop();
					break;
				}
			}

			// Optionally, you may want to gracefully stop the server outside the loop.
		}
	}
}