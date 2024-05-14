using System.Data;
using System.Net.Sockets;

namespace HttpServer;

public class Handler
{
    private readonly TcpClient client;
    private readonly string filename;
    private readonly string projectDirectory;

    public Handler(TcpClient client, String filename, String projectDirectory)
    {
        this.client = client;
        this.filename = filename;
        this.projectDirectory = projectDirectory;
    }
    
    public void Do()
    {
        try
        {
            StreamReader sr = new StreamReader(this.client.GetStream());
            StreamWriter sw = new StreamWriter(this.client.GetStream());
            
            Console.WriteLine("Connected to " + client.Client.RemoteEndPoint);
            string request = sr.ReadLine();
            Console.WriteLine("Request: {0}", request);

            //check if request ist GET else abort
            if (request == null || !request.Contains("GET")) return;
            
            //check route
            string route = GetRouteFromRequest(request);
            switch (route)
            {
                case "/file":
                    ServeFile(sw);
                    break;
                case "/":
                    ServeIndexHtml(sw);
                    break;
                case "/download":
                    ServeFileBlob(sw);
                    break;
                default:
                    SendNotFoundResponse(sw);
                    break;
            }

        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            client.Close();
        }
    }

    private string GetRouteFromRequest(string request)
    {
        string[] requestParts = request.Split(' ');
        string route = requestParts[1];
        return route;
    }

    private void ServeFile(StreamWriter sw)
    {
        // Read the file
        string theData;
        using (StreamReader file = new StreamReader(Path.Combine(this.projectDirectory, this.filename)))
        {
            theData = file.ReadToEnd();
        }

        // Send the response
        sw.WriteLine("HTTP/1.0 200 OK");
        sw.WriteLine("Date: {0}", DateTime.Now.ToString());
        sw.WriteLine("Server: Fileserver");
        sw.WriteLine("Content-length: " + theData.Length);
        sw.WriteLine("Content-type: text/plain");
        sw.WriteLine(); // Empty line
        sw.WriteLine(theData);
        //flusch benötigt das alles beim empfänger ankommt
        sw.Flush();

        Console.WriteLine("File gesendet");
    }

    private void ServeFileBlob(StreamWriter sw)
    {
        byte[] fileBytes;
        using (FileStream file = new FileStream(Path.Combine(this.projectDirectory, this.filename), FileMode.Open, FileAccess.Read))
        {
            fileBytes = new byte[file.Length];
            file.Read(fileBytes, 0, fileBytes.Length);
        }

        // Send the response
        sw.WriteLine("HTTP/1.0 200 OK");
        sw.WriteLine("Date: " + DateTime.Now.ToString());
        sw.WriteLine("Server: Fileserver");
        sw.WriteLine("Content-length: " + fileBytes.Length);
        sw.WriteLine("Content-type: application/octet-stream");
        sw.WriteLine("Content-disposition: attachment; filename=\"" + Path.GetFileName(this.filename) + "\"");
        sw.WriteLine(); // Empty line
        sw.Flush();

        // Write the file bytes to the response stream
        client.GetStream().Write(fileBytes, 0, fileBytes.Length);

        Console.WriteLine("File blob gesendet");
    }

    private void SendNotFoundResponse(StreamWriter sw)
    {
        sw.WriteLine("HTTP/1.0 404 Not Found");
        sw.WriteLine("Date: " + DateTime.Now.ToString());
        sw.WriteLine("Server: Fileserver");
        sw.WriteLine(); // Empty line
        sw.Flush();

        Console.WriteLine("404 response gesendet");
    }

    private void ServeIndexHtml(StreamWriter sw)
    {
        string indexHtml = @"
            <html>
            <head>
                <title>Game Log</title>
            </head>
            <body>
                <h1>Joystick log of Battleships</h1>
                <iframe src=""/file"" width=""800"" height=""600""></iframe>
                <br>
                <a href=""/download"">Download File</a>
            </body>
            </html>";

        // Send the response
        sw.WriteLine("HTTP/1.0 200 OK");
        sw.WriteLine("Date: " + DateTime.Now.ToString());
        sw.WriteLine("Server: TestFileServer 1.0");
        sw.WriteLine("Content-length: " + indexHtml.Length);
        sw.WriteLine("Content-type: text/html");
        sw.WriteLine(); // Empty line
        sw.WriteLine(indexHtml);
        sw.Flush();

        Console.WriteLine("Index HTML gesendet");
    }
}