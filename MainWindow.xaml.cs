﻿using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
/* ############################################
 * ### Dalton Christopher                   ###
 * ### Desktop-Frens - Windows - .NET8.0    ###
 * ### 05/2024                              ###
 * ############################################*/

namespace Desktop_Frens
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")] // Mouse shit
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008; //flags
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        readonly NotifyIcon TaskIcon = new();
        // Fren Objects
        public FrenObject? _Slug_Fren;
        public FrenObject? _Dog_Fren;
        public FrenObject? _Spooky_Fren;
        public FrenObject? _Frog_Fren;
        public FrenObject? _Frog_B_Fren;


        public MainWindow() // Main
        {
            try
            {
                // ToolBar settings
                this.ShowInTaskbar = false;
                this.Topmost = true;

                // init main window
                InitializeComponent();

                // Init Frens
                LoadFrenObjects();

                // Initialize NotifyIcon
                if (TaskIcon != null)
                {
                    //Icon and toolbar menu/display options
                    TaskIcon.Icon = (Icon)ImageManager.GetImage("slug_icon", typeof(Icon));
                    TaskIcon.Visible = true;
                    TaskIcon.Text = "Desktop Fren";
                    // Create context menu for NotifyIcon
                    var settingsMenu = new SettingsMenu(this);
                    // Assign the context menu to TaskIcon
                    TaskIcon.ContextMenuStrip = settingsMenu._menuStrip;
                    settingsMenu.AllEnabled();

                    TaskIcon.Click += (s, e) => MenuClick();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error loading image: {ex.Message}");
            }
        }

        void LoadFrenObjects()
        {
            _Slug_Fren = new("Slug", 6, this, 3.95, 88, this._AnimatedImg_1, 60, 60, -1); // -1
            _Dog_Fren = new("Dog", 7, this, 7.4, 75, _AnimatedImg_2, 85, 95, -5); // -5
            _Spooky_Fren = new("Spooky", 8, this, 5.9, 85, _AnimatedImg_3, 110, 110, -52); // -52
            _Frog_Fren = new("Frog", 7, this, 0.3, 135, _AnimatedImg_4, 75, 100, 10); //10  move-1.3
            _Frog_B_Fren = new("Frog_B", 7, this, 0.3, 135, _AnimatedImg_5, 95, 115, -5); // -5 move-2
        }

        /// <summary>
        /// Set Fren Object On/Off via passed Fren
        /// </summary>
        /// <param name="fren"> The Fren to Update On/Off </param>
        public static void SetFrenActive(FrenObject fren)
        {
            if (fren == null) return;
            if (fren.IsActive())
                fren.Disable();
            else
                fren.SetActive();
        }

        public void FlipFrens()
        {
            _Slug_Fren.PublicFlip();
            _Dog_Fren.PublicFlip();
            _Slug_Fren.PublicFlip();
            _Frog_B_Fren.PublicFlip();
            _Frog_Fren.PublicFlip();
            _Spooky_Fren.PublicFlip();
        }

        static void MenuClick()
        {
            // Simulate right mouse button down event
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            // Simulate right mouse button up event
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }


    }

}
