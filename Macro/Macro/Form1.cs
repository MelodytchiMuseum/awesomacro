using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using WindowsInput;
using System.Threading;

namespace Macro
{
    public partial class Form1 : Form
    {
        // List.
        List<string> respList = new List<string>();
        List<string> usedList = new List<string>();

        // Randomizer.
        Random ranGen = new Random();
        public int ranGet = -1;

        // Constructor.
        public Form1()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            LoadResponses();
            HotKeyManager.RegisterHotKey(Keys.Insert, KeyModifiers.NoRepeat);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HitMacro);
        }

        // Reload button.
        private void button1_Click(object sender, EventArgs e)
        {
            respList.Clear();
            LoadResponses();
        }

        // Notification tray icon.
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        // Loading responses.
        private void LoadResponses()
        {
            // File doesn't exist.
            if (!File.Exists(@"Responses.txt"))
            {
                MessageBox.Show("Responses.txt could not be found.", "Awesomacro Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // File exists.
            else
            {
                System.Collections.Generic.IEnumerable<String> tempRead = File.ReadLines(@"Responses.txt");
                for (int i = 0; i < tempRead.Count(); i++)
                {
                    if (tempRead.ElementAt(i).Length > 0) respList.Add(tempRead.ElementAt(i));
                }
                if (respList.Count == 0) MessageBox.Show("Responses.txt is empty.", "Awesomacro Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textResp.Text = respList.Count + " responses loaded.";
                ranGet = -1;
                usedList.Clear();
            }
        }

        // Using the macro.
        private void HitMacro(object sender, HotKeyEventArgs e)
        {
            if (respList.Count > 0) SimType();
        }

        // Simulating typing.
        private void SimType()
        {
            // Getting response.
            ranGet = ranGen.Next(0, respList.Count - 1);

            // Letting go of directional keys.
            InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_W);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_A);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_S);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.VK_D);

            // Opening chat.
            InputSimulator.SimulateKeyDown(VirtualKeyCode.SHIFT);
            InputSimulator.SimulateKeyDown(VirtualKeyCode.RETURN);
            Thread.Sleep(10);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.SHIFT);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.RETURN);

            // Typing.
            Char[] tempChar = respList.ElementAt(ranGet).ToCharArray();
            for (int i = 0; i < tempChar.Length; i++) {
                TypeChar(tempChar[i]);
                //if (i > 0 && (i / 25) == Math.Round(i / 25)) Thread.Sleep(10);
            }

            // Sending.
            InputSimulator.SimulateKeyDown(VirtualKeyCode.RETURN);
            Thread.Sleep(10);
            InputSimulator.SimulateKeyUp(VirtualKeyCode.RETURN);

            // List management.
            if (respList.Count == 1 && usedList.Count > 0)
            {
                List<string> tempList = respList;
                respList = usedList;
                usedList = tempList;
            }
            else if (respList.Count > 1) {
                usedList.Add(respList.ElementAt(ranGet));
                respList.RemoveAt(ranGet);
            }
        }

        // Getting character to type.
        private void TypeChar(Char inChar) {
            // Converting char to string.
            String outStr = inChar.ToString();

            // String not correctly handled by SendKeys.
            if (outStr.Equals("(")) TypeShift(VirtualKeyCode.VK_9);
            else if (outStr.Equals(")")) TypeShift(VirtualKeyCode.VK_0);
            else if (outStr.Equals("^")) TypeShift(VirtualKeyCode.VK_6);
            else if (outStr.Equals("%")) TypeShift(VirtualKeyCode.VK_5);
            else if (outStr.Equals("{")) TypeShift(VirtualKeyCode.OEM_4);
            else if (outStr.Equals("}")) TypeShift(VirtualKeyCode.OEM_6);
            else if (outStr.Equals("~")) TypeShift(VirtualKeyCode.OEM_3);
            else if (outStr.Equals("+")) TypeLittle(VirtualKeyCode.ADD);

            // Characters (specified for quicker typing).
            else if (outStr.Equals("A")) TypeShift(VirtualKeyCode.VK_A);
            else if (outStr.Equals("B")) TypeShift(VirtualKeyCode.VK_B);
            else if (outStr.Equals("C")) TypeShift(VirtualKeyCode.VK_C);
            else if (outStr.Equals("D")) TypeShift(VirtualKeyCode.VK_D);
            else if (outStr.Equals("E")) TypeShift(VirtualKeyCode.VK_E);
            else if (outStr.Equals("F")) TypeShift(VirtualKeyCode.VK_F);
            else if (outStr.Equals("G")) TypeShift(VirtualKeyCode.VK_G);
            else if (outStr.Equals("H")) TypeShift(VirtualKeyCode.VK_H);
            else if (outStr.Equals("I")) TypeShift(VirtualKeyCode.VK_I);
            else if (outStr.Equals("J")) TypeShift(VirtualKeyCode.VK_J);
            else if (outStr.Equals("K")) TypeShift(VirtualKeyCode.VK_K);
            else if (outStr.Equals("L")) TypeShift(VirtualKeyCode.VK_L);
            else if (outStr.Equals("M")) TypeShift(VirtualKeyCode.VK_M);
            else if (outStr.Equals("N")) TypeShift(VirtualKeyCode.VK_N);
            else if (outStr.Equals("O")) TypeShift(VirtualKeyCode.VK_O);
            else if (outStr.Equals("P")) TypeShift(VirtualKeyCode.VK_P);
            else if (outStr.Equals("Q")) TypeShift(VirtualKeyCode.VK_Q);
            else if (outStr.Equals("R")) TypeShift(VirtualKeyCode.VK_R);
            else if (outStr.Equals("S")) TypeShift(VirtualKeyCode.VK_S);
            else if (outStr.Equals("T")) TypeShift(VirtualKeyCode.VK_T);
            else if (outStr.Equals("U")) TypeShift(VirtualKeyCode.VK_U);
            else if (outStr.Equals("V")) TypeShift(VirtualKeyCode.VK_V);
            else if (outStr.Equals("W")) TypeShift(VirtualKeyCode.VK_W);
            else if (outStr.Equals("X")) TypeShift(VirtualKeyCode.VK_X);
            else if (outStr.Equals("Y")) TypeShift(VirtualKeyCode.VK_Y);
            else if (outStr.Equals("Z")) TypeShift(VirtualKeyCode.VK_Z);
            else if (outStr.Equals("a")) TypeLittle(VirtualKeyCode.VK_A);
            else if (outStr.Equals("b")) TypeLittle(VirtualKeyCode.VK_B);
            else if (outStr.Equals("c")) TypeLittle(VirtualKeyCode.VK_C);
            else if (outStr.Equals("d")) TypeLittle(VirtualKeyCode.VK_D);
            else if (outStr.Equals("e")) TypeLittle(VirtualKeyCode.VK_E);
            else if (outStr.Equals("f")) TypeLittle(VirtualKeyCode.VK_F);
            else if (outStr.Equals("g")) TypeLittle(VirtualKeyCode.VK_G);
            else if (outStr.Equals("h")) TypeLittle(VirtualKeyCode.VK_H);
            else if (outStr.Equals("i")) TypeLittle(VirtualKeyCode.VK_I);
            else if (outStr.Equals("j")) TypeLittle(VirtualKeyCode.VK_J);
            else if (outStr.Equals("k")) TypeLittle(VirtualKeyCode.VK_K);
            else if (outStr.Equals("l")) TypeLittle(VirtualKeyCode.VK_L);
            else if (outStr.Equals("m")) TypeLittle(VirtualKeyCode.VK_M);
            else if (outStr.Equals("n")) TypeLittle(VirtualKeyCode.VK_N);
            else if (outStr.Equals("o")) TypeLittle(VirtualKeyCode.VK_O);
            else if (outStr.Equals("p")) TypeLittle(VirtualKeyCode.VK_P);
            else if (outStr.Equals("q")) TypeLittle(VirtualKeyCode.VK_Q);
            else if (outStr.Equals("r")) TypeLittle(VirtualKeyCode.VK_R);
            else if (outStr.Equals("s")) TypeLittle(VirtualKeyCode.VK_S);
            else if (outStr.Equals("t")) TypeLittle(VirtualKeyCode.VK_T);
            else if (outStr.Equals("u")) TypeLittle(VirtualKeyCode.VK_U);
            else if (outStr.Equals("v")) TypeLittle(VirtualKeyCode.VK_V);
            else if (outStr.Equals("w")) TypeLittle(VirtualKeyCode.VK_W);
            else if (outStr.Equals("x")) TypeLittle(VirtualKeyCode.VK_X);
            else if (outStr.Equals("y")) TypeLittle(VirtualKeyCode.VK_Y);
            else if (outStr.Equals("z")) TypeLittle(VirtualKeyCode.VK_Z);
            else if (outStr.Equals("0")) TypeLittle(VirtualKeyCode.VK_0);
            else if (outStr.Equals("1")) TypeLittle(VirtualKeyCode.VK_1);
            else if (outStr.Equals("2")) TypeLittle(VirtualKeyCode.VK_2);
            else if (outStr.Equals("3")) TypeLittle(VirtualKeyCode.VK_3);
            else if (outStr.Equals("4")) TypeLittle(VirtualKeyCode.VK_4);
            else if (outStr.Equals("5")) TypeLittle(VirtualKeyCode.VK_5);
            else if (outStr.Equals("6")) TypeLittle(VirtualKeyCode.VK_6);
            else if (outStr.Equals("7")) TypeLittle(VirtualKeyCode.VK_7);
            else if (outStr.Equals("8")) TypeLittle(VirtualKeyCode.VK_8);
            else if (outStr.Equals("9")) TypeLittle(VirtualKeyCode.VK_9);
            else if (outStr.Equals("!")) TypeShift(VirtualKeyCode.VK_1);
            else if (outStr.Equals("@")) TypeShift(VirtualKeyCode.VK_2);
            else if (outStr.Equals("#")) TypeShift(VirtualKeyCode.VK_3);
            else if (outStr.Equals("$")) TypeShift(VirtualKeyCode.VK_4);
            else if (outStr.Equals("&")) TypeShift(VirtualKeyCode.VK_7);
            else if (outStr.Equals("*")) TypeShift(VirtualKeyCode.VK_8);

            // Normally type it.
            else SendKeys.Send(outStr);
        }

        // Shifted character.
        private void TypeShift(VirtualKeyCode keyIn) {
            InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LSHIFT, keyIn);
        }

        // Normal character.
        private void TypeLittle(VirtualKeyCode keyIn) {
            InputSimulator.SimulateKeyPress(keyIn);
        }

        // End.
    }
}