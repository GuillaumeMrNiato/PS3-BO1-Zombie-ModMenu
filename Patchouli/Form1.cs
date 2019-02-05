using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS3Lib;
using DevExpress.XtraEditors;
using BO2NoHost;
using System.Diagnostics;
using System.Threading;

namespace Patchouli
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        static PS3API PS3 = new PS3API();
        public static void Wait(double time)
        {
            Thread.Sleep((int)time);
        }
        public static void SetText(string txt)
        {
            PS3.SetMemory(FPS.Text, Encoding.ASCII.GetBytes("" + txt + "\0"));
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn)
            {
                PS3.ChangeAPI(SelectAPI.ControlConsole);
            }
            else
            {
                PS3.ChangeAPI(SelectAPI.TargetManager);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PS3.ConnectTarget())
                {
                    label1.Text = "PS3 Connected | Not Attached";
                    label1.ForeColor = Color.Lime;
                    simpleButton4.Enabled = true;
                    simpleButton1.Enabled = false;
                }
                else
                {
                    groupControl2.Enabled = false;
                    XtraMessageBox.Show("Failed to Connect PS3 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Failed to Connect PS3 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.ihax.fr/forums");
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("R3 To Open Menu\nL1 + R1 to navigate\nX to Validate\nR3 back to the main menu\nL3 + R3 To Exit Menu", "Patchouli By MrNiato", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FPS.Enable(true);
            SetText("^5Press ^2R3 ^5To Open The Menu");
            watchButtons.RunWorkerAsync();
        }
        public static string curSub;
        public static bool FloatingBodies;
        public static bool ForceHost;
        public static bool Laser;
        public static int MaxScroll;
        public static bool menuOpen;
        public static bool NoRecoil;
        public static bool RedBoxes;
        public static int Scroll;
        public static bool SteadyAim;
        public static bool Vsat;
        public static bool Wallhack;
        private void watchButtons_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                do
                {
                    if (menuOpen)
                    {
                        if (menuOpen)
                        {
                            if (Buttons.R3() && (curSub == "Main"))
                            {
                                FPS.CloseMenu();
                                menuOpen = false;
                                Wait(300.0);
                            }
                            if (Buttons.Square() && (Scroll == 0))
                            {
                                byte[] laseron = new byte[] { 0x2c, 3, 0, 1 };
                                PS3.SetMemory(Offsets.Laser, laseron);
                                Laser = true;
                                Wait(100.0);
                                if (Buttons.Square() && Laser)
                                {

                                }
                            }
                            if (Buttons.DpadUp() && (Scroll == 0))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\nTarget Finder\nBig Names");
                                Scroll = 10;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 0))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Laser\n^1VSAT\n^7Wallhack\nRedboxes\nForce Host\nFloating Bodies");
                                Scroll = 1;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 1))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^1Laser\n^7VSAT\nWallhack\nRedboxes\nForce Host\nFloating Bodies");
                                Scroll = 0;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 1))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Laser\nVSAT\n^1Wallhack\n^7Redboxes\nForce Host\nFloating Bodies");
                                Scroll = 2;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 2))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Laser\n^1VSAT\n^7Wallhack\nRedboxes\nForce Host\nFloating Bodies");
                                Scroll = 1;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 2))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7VSAT\nWallhack\n^1Redboxes\n^7Force Host\nFloating Bodies\nSteady Aim");
                                Scroll = 3;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 3))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Laser\nVSAT\n^1Wallhack\n^7Redboxes\nForce Host\nFloating Bodies");
                                Scroll = 2;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 3))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Wallhack\nRedboxes\n^1Force Host\n^7Floating Bodies\nSteady Aim\nNo Recoil");
                                Scroll = 4;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 4))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7VSAT\nWallhack\n^1Redboxes\n^7Force Host\nFloating Bodies\nSteady Aim");
                                Scroll = 3;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 4))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Redboxes\nForce Host\n^1Floating Bodies\n^7Steady Aim\nNo Recoil\nTarget Finder");
                                Scroll = 5;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 5))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Wallhack\nRedboxes\n^1Force Host\n^7Floating Bodies\nSteady Aim\nNo Recoil");
                                Scroll = 4;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 5))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Force Host\nFloating Bodies\n^1Steady Aim\n^7No Recoil\nTarget Finder\nBig Names");
                                Scroll = 6;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 6))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Redboxes\nForce Host\n^1Floating Bodies\n^7Steady Aim\nNo Recoil\nTarget Finder");
                                Scroll = 5;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 6))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Floating Bodies\nSteady Aim\n^1No Recoil\n^7Target Finder\nBig Names");
                                Scroll = 7;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 7))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Force Host\nFloating Bodies\n^1Steady Aim\n^7No Recoil\nTarget Finder\nBig Names");
                                Scroll = 6;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 7))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\n^1Target Finder\n^7Big Names");
                                Scroll = 8;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 8))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Floating Bodies\nSteady Aim\n^1No Recoil\n^7Target Finder\nBig Names");
                                Scroll = 7;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 8))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\nTarget Finder\n^1Big Names");
                                Scroll = 9;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 9))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\n^1Target Finder\n^7Big Names");
                                Scroll = 8;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 9))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\nTarget Finder\nBig Names");
                                Scroll = 10;
                                Wait(50.0);
                            }
                            if (Buttons.DpadUp() && (Scroll == 10))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^7Steady Aim\nNo Recoil\nTarget Finder\n^1Big Names");
                                Scroll = 9;
                                Wait(50.0);
                            }
                            if (Buttons.DpadDown() && (Scroll == 10))
                            {
                                SetText("^2Non-Host Menu by Adidas\n^1Laser\n^7VSAT\nWallhack\nRedboxes\nForce Host\nFloating Bodies");
                                Scroll = 0;
                                Wait(50.0);
                            }
                            if (Buttons.Square() && (Scroll == 9))
                            {
                                byte[] fuck = new byte[] { 0x3f, 0xff, 0xff, 0 };
                                PS3.SetMemory(Offsets.BigNames, fuck);
                                Wait(100.0);
                                if (Buttons.Square() && (Scroll == 9))
                                {
                                    byte[] fuckoff = new byte[] { 0x3f, 0x26, 0x66, 0x66 };
                                    PS3.SetMemory(Offsets.BigNames, fuckoff);
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 8))
                            {
                                byte[] kk = new byte[] { 1 };
                                PS3.SetMemory(Offsets.TargetFinder, kk);
                                Wait(100.0);
                                if (Buttons.Square() && (Scroll == 8))
                                {
                                    byte[] kko = new byte[1];
                                    PS3.SetMemory(Offsets.TargetFinder, kko);
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 7))
                            {

                                if (Buttons.Square() && (Scroll == 7))
                                {
                                    byte[] noo = new byte[] { 0x48, 80, 110, 0xf5 };
                                    PS3.SetMemory(Offsets.NoRecoil, noo);
                                    NoRecoil = false;
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 6))
                            {

                                if (Buttons.Square() && SteadyAim)
                                {
                                    byte[] stedo = new byte[] { 0x2c, 4, 0, 2 };
                                    PS3.SetMemory(Offsets.SteadyAim, stedo);
                                    SteadyAim = false;
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 5))
                            {
                                byte[] fbod = new byte[] { 0x43, 0x48 };
                                PS3.SetMemory(Offsets.FloatingBodies, fbod);
                                FloatingBodies = true;
                                Wait(100.0);
                                if (Buttons.Square() && FloatingBodies)
                                {
                                    byte[] foof = new byte[] { 0xc4, 0x48 };
                                    PS3.SetMemory(Offsets.FloatingBodies, foof);
                                    FloatingBodies = false;
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 4))
                            {
                                byte[] fhost = new byte[1];
                                PS3.SetMemory(Offsets.ForceHost, fhost);
                                ForceHost = true;
                                Wait(100.0);
                                if (Buttons.Square() && ForceHost)
                                {
                                    byte[] foff = new byte[] { 1 };
                                    PS3.SetMemory(Offsets.ForceHost, foff);
                                    ForceHost = false;
                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 3))
                            {

                                RedBoxes = true;
                                Wait(100.0);
                                if (Buttons.Square() && RedBoxes)
                                {

                                }
                            }
                            if (Buttons.Square() && (Scroll == 2))
                            {
                                byte[] wallhack = new byte[] { 0x38, 0xc0, 0xff, 0xff };
                                PS3.SetMemory(Offsets.Wallhack, wallhack);
                                Wallhack = true;
                                Wait(100.0);
                                if (Buttons.Square() && Wallhack)
                                {

                                    Wait(100.0);
                                }
                            }
                            if (Buttons.Square() && (Scroll == 1))
                            {

                            }
                        }
                    }
                    continue;
                }

                while (!Buttons.R3());
                menuOpen = true;
                Scroll = 0;
                SetText("^2Non-Host Menu by Adidas\n^1Laser\n^7VSAT\nWallhack\nRedboxes\nForce Host\nFloating Bodies");
                curSub = "Main";
                Wait(300.0);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FPS.DestroyText();
            FPS.Enable(false);
            if (watchButtons.WorkerSupportsCancellation)
            {
                watchButtons.CancelAsync();
            }
        }
    }
}
