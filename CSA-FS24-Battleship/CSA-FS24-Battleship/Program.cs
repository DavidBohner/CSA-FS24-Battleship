﻿using CSA_FS24_Battleship.DataModel;
using CSA_FS24_Battleship.DataModel.Ships;
using CSA_FS24_Battleship.GameLogic;
using Explorer700Library;
using HttpServer;

namespace CSA_FS24_Battleship;
class Program
{
    private static Explorer700 _explorer700 = new();
    static void Main(string[] args)
    {
        if (!File.Exists("logs/data.txt"))
        {
            Directory.CreateDirectory("logs");
            File.WriteAllText("logs/data.txt", "// Logs from Battleships - Team 01\n");
        }

        Thread HttpThread = new Thread(HttpServer.HttpServer.StartServer);
        HttpThread.Start();
        while (true)
        {
            Console.WriteLine("Battleship game started...");

            GameManager gm = new();
            gm.GameSession();

            bool goAgain = false;
            while (!goAgain)
            {
                switch (_explorer700.Joystick.Keys)
                {
                    case Keys.NoKey:
                        continue;
                    case Keys.Center:
                        goAgain = true;
                        Thread.Sleep(500); // To avoid instantly firing at 0,0.
                        break;
                    default:
                        return;
                }
            }
        }
    }
}