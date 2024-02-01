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
				Console.WriteLine("Server started on {0}", port);
			}
			catch (Exception e) {
				Console.WriteLine("Failed to create server: " + e.Message);
				return;
			}

			//Set the number of threads based on CPU Cores
			int maxThreads = Environment.ProcessorCount;
			ThreadPool.SetMaxThreads(maxThreads, maxThreads);

			while (true) {
				try {
					TcpClient client = server.AcceptTcpClient();
					Console.WriteLine("Client connected.");
					
					//Using ThreadPool to manage the client handling
					ThreadPool.QueueUserWorkItem(state =>
					{
						// Extract the socket from the TcpClient
						Socket clientSocket = client.Client;
						
						// Create a new instance of UploadServerThread with the socket
						UploadServerThread serverThread = new UploadServerThread(clientSocket);
						
						// Start the thread
						serverThread.Run();
					});
				}
				catch (Exception e) {
					Console.WriteLine("Error occurred: " + e.Message);
					server.Stop();
					break;
				}
			}
		}
	}
}