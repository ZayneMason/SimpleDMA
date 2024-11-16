using System;
using System.Numerics;
using KMBox.NET;
using KMBox.NET.Structures;
using System.Text.Json;
using System.Net;
using System.Runtime.Versioning;
using System.ComponentModel;


namespace SimpleDMA.SimpleDMA
{
    internal class ConnectionInfo
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Uuid { get; set; }
    }

    internal class SimpleInput
    {
        private KmBoxClient KmBoxClient { get; set; }
        public SimpleInput()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\KMBoxSettings.json";

            try
            {
                string jsonString = File.ReadAllText(filePath);

                ConnectionInfo connection = JsonSerializer.Deserialize<ConnectionInfo>(jsonString);

                this.KmBoxClient = new KmBoxClient(IPAddress.Parse(connection.Ip), connection.Port, connection.Uuid);
                this.KmBoxClient.Connect();
                Console.WriteLine("Connected to KMBox.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file {filePath} was not found.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: The JSON format is invalid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public Task<bool> MoveMouse(Vector2 position)
        {
            return KmBoxClient.MouseMoveSimple((short)position.X, (short)position.Y);
        }

        public Task<bool> Click(MouseButton button)
        {
            return KmBoxClient.MouseClick(button);
        }

        public Task<bool> LeftClick()
        {
            return KmBoxClient.MouseClick();
        }

        public Task<bool> RightClick()
        {
            return KmBoxClient.MouseClick();
        }

        public Task<bool> MiddleClick()
        {
            return KmBoxClient.MouseMiddleClick();
        }

        public Task<bool> Scroll(int amount)
        {
            return KmBoxClient.MouseWheel(amount);
        }

        public Task<bool> MoveMouseSmoothly(Vector2 position, uint speed, Vector2 curvePoint, Vector2 end)
        {
            return KmBoxClient.MouseMoveBezier((short)position.X, (short)position.Y, speed, (short)curvePoint.X, (short)curvePoint.Y, (short)end.X, (short)end.Y);
        }

        public Task<bool> MoveMouseSmoothlyRandomSpeed(Vector2 position, uint min, uint max, Vector2 curvePoint, Vector2 end)
        {
            Random random = new Random();
            return KmBoxClient.MouseMoveBezier((short)position.X, (short)position.Y, (uint)random.NextInt64(min, max), (short)curvePoint.X, (short)curvePoint.Y, (short)end.X, (short)end.Y);
        }

        public Task<bool> KeyPress(KeyboardButton button, KeyboardModifiers modifiers = 0)
        {
            return KmBoxClient.KeyboardButtonDown(button, modifiers);
        }

        public Task<bool> KeysUp()
        {
            return KmBoxClient.AllKeyboardButtonsUp();
        }

        public async Task<bool> TypeText(string text, int delay = 80)
        {
            return await KmBoxClient.TypeText(text, delay);
        }

        public ReportListener CreateReportListener()
        {
            return KmBoxClient.CreateReportListener();
        }

        public async Task<bool> MaskKey(KeyboardButton button)
        {
            return await KmBoxClient.MaskKeyboardButton(button);
        }

        public async Task<bool> MaskMouseButton(MouseMasks button)
        {
            return await KmBoxClient.MaskMouseInput(button);
        }

        public async Task<bool> UnmaskInputs()
        {
            return await KmBoxClient.UnmaskAllInput();
        }
    }
}
