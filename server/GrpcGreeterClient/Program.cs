using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;
using System.Security.Cryptography.X509Certificates;
// The port number must match the port of the gRPC server.
// using var channel = GrpcChannel.ForAddress("https://localhost:5143");

var cert = new X509Certificate2("https.crt", "66sTE079");
var handler = new HttpClientHandler();
handler.ClientCertificates.Add(cert);
var httpClient = new HttpClient(handler);

using var channel = GrpcChannel.ForAddress("https://localhost:5143/", new GrpcChannelOptions
{
	HttpClient = httpClient
});

var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
					  new HelloRequest { Name = "GreeterClient" }
				  );

Console.WriteLine("Greereeg: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
