using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using KMBox.NET;
using KMBox.NET.Structures;


namespace SimpleDMA.SimpleDMA
{
    internal class SimpleInput
    {
        KmBoxClient KmBoxClient { get; set; }

        public SimpleInput(KmBoxClient kmBoxClient)
        {
            this.KmBoxClient = kmBoxClient;
        }

        public void MoveMouse(Vector2 position)
        {
            KmBoxClient.MouseMoveSimple((short)position.X, (short)position.Y);
        }

        public void Click(MouseButton button)
        {
            KmBoxClient.MouseClick(button);
        }

        public void LeftClick()
        {
            KmBoxClient.MouseClick();
        }

        public void RightClick()
        {
            KmBoxClient.MouseClick();
        }

        public void MiddleClick()
        {
            KmBoxClient.MouseMiddleClick();
        }

        public void Scroll(int amount)
        {
            KmBoxClient.MouseWheel(amount);
        }

        public void MoveMouseSmoothly(Vector2 position, uint speed, Vector2 start, Vector2 end)
        {
            KmBoxClient.MouseMoveBezier((short)position.X, (short)position.Y, speed, (short)start.X, (short)start.Y, (short)end.X, (short)end.Y);
        }

        public void KeyPress(KeyboardButton button)
        {
            KmBoxClient.KeyboardButtonDown(button);
        }

        public void KeysUp()
        {
            KmBoxClient.AllKeyboardButtonsUp();
        }
    }
}
