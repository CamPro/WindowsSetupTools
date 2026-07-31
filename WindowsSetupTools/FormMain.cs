using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsSetupTools
{
    public partial class FormMain : Form
    {
        readonly string startupShortcut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + $"\\{Application.ProductName}.lnk";

        readonly string winrar = @"C:\Program Files\WinRAR\WinRAR.exe";

        string chromeApp = string.Empty;
        string edgeApp = string.Empty;

        bool isRealProtection = false;

        public FormMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text += " " + Environment.MachineName;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            SetupUIGlobal();
            SetupUITabCaiDatPhanMem(); // cai dat phan mem
            SetupUITabCaiDatNhanh(); // cai dat nhanh
            SetupUITabTienIch(); // cong cu tien ich
            SetupUITabChuyenDung(); // cong cu chuyen dung
            SetupUITabHopCongCu(); // hop cong cu

            string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
            RegistryKey keyReg = Registry.LocalMachine.OpenSubKey(keyPath) ?? Registry.CurrentUser.OpenSubKey(keyPath);
            chromeApp = keyReg?.GetValue("")?.ToString();

            keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe";
            keyReg = Registry.LocalMachine.OpenSubKey(keyPath) ?? Registry.CurrentUser.OpenSubKey(keyPath);
            edgeApp = keyReg?.GetValue("")?.ToString();

            textChangeUser.Text = Environment.UserName;
            textChangePort.Text = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP-Tcp").GetValue("PortNumber").ToString();
            buttonChangePass.Enabled = false;
            buttonChangeUser.Enabled = false;
            buttonChangePort.Enabled = false;

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void SetupUIGlobal()
        {
            buttonOpenFolder.Image = Properties.Resources.icon_explorer;
            buttonOpenFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOpenFolder.ImageAlign = ContentAlignment.MiddleLeft;
            buttonOpenFolder.TextAlign = ContentAlignment.MiddleCenter;

            buttonOpenControlPanel.Image = Properties.Resources.icon_control_panel;
            buttonOpenControlPanel.Text = "";

            buttonOpenSettings.Image = Properties.Resources.icon_settings;
            buttonOpenSettings.Text = "";

            buttonOpenProgramsAndFeatures.Image = Properties.Resources.icon_programs_and_features;
            buttonOpenProgramsAndFeatures.Text = "";

            buttonOpenAppsAndFeatures.Image = Properties.Resources.icon_apps_and_features;
            buttonOpenAppsAndFeatures.Text = "";

            buttonOpenCommandPrompt.Image = Properties.Resources.icon_command_prompt;
            buttonOpenCommandPrompt.Text = "";

            buttonOpenPowerShell.Image = Properties.Resources.icon_powershell;
            buttonOpenPowerShell.Text = "";

            buttonOpenWindowsSecurity.Image = Properties.Resources.icon_windows_defender;
            buttonOpenWindowsSecurity.Text = "";

            buttonOpenWindowsFirewall.Image = Properties.Resources.icon_windows_firewall;
            buttonOpenWindowsFirewall.Text = "";

            buttonOpenNetworkConnections.Image = Properties.Resources.icon_network_connections;
            buttonOpenNetworkConnections.Text = "";

            buttonOpenTaskScheduler.Image = Properties.Resources.icon_task_scheduler;
            buttonOpenTaskScheduler.Text = "";

            buttonOpenTaskManager.Image = Properties.Resources.icon_task_manager;
            buttonOpenTaskManager.Text = "";

            buttonDiskManagement.Image = Properties.Resources.icon_diskmgmt;
            buttonDiskManagement.Text = "";

            buttonDeviceManager.Image = Properties.Resources.icon_devmgmt;
            buttonDeviceManager.Text = "";

            buttonPowerOptions.Image = Properties.Resources.icon_powercfg;
            buttonPowerOptions.Text = "";

            buttonDateAndTime.Image = Properties.Resources.icon_time;
            buttonDateAndTime.Text = "";

            buttonRegistryEditor.Image = Properties.Resources.icon_regedit;
            buttonRegistryEditor.Text = "";

            buttonMicrosoftDirectXDiagnosticTool.Image = Properties.Resources.icon_dxdiag;
            buttonMicrosoftDirectXDiagnosticTool.Text = "";

            buttonSystemInformation.Image = Properties.Resources.icon_msinfo32;
            buttonSystemInformation.Text = "";

            buttonAdvancedUserAccounts.Image = Properties.Resources.icon_netplwiz;
            buttonAdvancedUserAccounts.Text = "";

            buttonSystemProperties.Image = Properties.Resources.icon_sysdm;
            buttonSystemProperties.Text = "";

            buttonTrustedPlatformModule.Image = Properties.Resources.icon_tpm;
            buttonTrustedPlatformModule.Text = "";

            buttonIPConfigurationUtility.Image = Properties.Resources.icon_ipconfig;
            buttonIPConfigurationUtility.Text = "";

            buttonSystemProtection.Image = Properties.Resources.icon_sysdm;
            buttonSystemProtection.Text = "";

            buttonCheckActiveWindows.Image = Properties.Resources.icon_check;
            buttonCheckActiveWindows.Text = "";

            buttonAboutWindows.Image = Properties.Resources.icon_winver;
            buttonAboutWindows.Text = "";

            buttonOpenAppDataLocalFolder.Image = Properties.Resources.icon_folder_local;
            buttonOpenAppDataLocalFolder.Text = "";

            buttonOpenAppDataRoamingFolder.Image = Properties.Resources.icon_folder_roaming;
            buttonOpenAppDataRoamingFolder.Text = "";

            buttonOpenStartupFolder.Image = Properties.Resources.icon_folder_startup;
            buttonOpenStartupFolder.Text = "";

            buttonOpenCommonStartupFolder.Image = Properties.Resources.icon_folder_startup;
            buttonOpenCommonStartupFolder.Text = "";

            buttonOpenTempFolder.Image = Properties.Resources.icon_folder_temp;
            buttonOpenTempFolder.Text = "";

            buttonAutoSelfDelete.Image = Properties.Resources.icon_trash;
            buttonAutoSelfDelete.Text = "";

            buttonShutdown.Image = Properties.Resources.icon_shutdown;
            buttonShutdown.Text = "";

            buttonRestart.Image = Properties.Resources.icon_restart;
            buttonRestart.Text = "";
        }

        public void SetupUITabCaiDatPhanMem()
        {
            buttonSetupWinRAR.Image = Properties.Resources.icon_winrar;
            buttonSetupWinRAR.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupWinRAR.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupWinRAR.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupWinRARVi.Image = Properties.Resources.icon_winrar;
            buttonSetupWinRARVi.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupWinRARVi.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupWinRARVi.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupSevenZip.Image = Properties.Resources.icon_7z;
            buttonSetupSevenZip.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupSevenZip.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupSevenZip.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupChrome.Image = Properties.Resources.icon_chrome;
            buttonSetupChrome.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupChrome.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupChrome.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupChromeEnglish.Image = Properties.Resources.icon_chrome;
            buttonSetupChromeEnglish.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupChromeEnglish.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupChromeEnglish.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupCocCoc.Image = Properties.Resources.icon_coccoc;
            buttonSetupCocCoc.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupCocCoc.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupCocCoc.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupUnikey.Image = Properties.Resources.icon_unikey;
            buttonSetupUnikey.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupUnikey.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupUnikey.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupEvkey.Image = Properties.Resources.icon_evkey;
            buttonSetupEvkey.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupEvkey.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupEvkey.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupTeamViewer.Image = Properties.Resources.icon_teamviewer;
            buttonSetupTeamViewer.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupTeamViewer.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupTeamViewer.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupAnyDesk.Image = Properties.Resources.icon_anydesk;
            buttonSetupAnyDesk.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupAnyDesk.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupAnyDesk.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupUltraViewer.Image = Properties.Resources.icon_ultraviewer;
            buttonSetupUltraViewer.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupUltraViewer.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupUltraViewer.TextAlign = ContentAlignment.MiddleCenter;

            buttonMicrosoftOfficeOne.Image = Properties.Resources.icon_office;
            buttonMicrosoftOfficeOne.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMicrosoftOfficeOne.ImageAlign = ContentAlignment.MiddleLeft;
            buttonMicrosoftOfficeOne.TextAlign = ContentAlignment.MiddleCenter;

            buttonMicrosoftOfficeTwo.Image = Properties.Resources.icon_office1;
            buttonMicrosoftOfficeTwo.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMicrosoftOfficeTwo.ImageAlign = ContentAlignment.MiddleLeft;
            buttonMicrosoftOfficeTwo.TextAlign = ContentAlignment.MiddleCenter;

            buttonMicrosoftOfficeThree.Image = Properties.Resources.icon_office2;
            buttonMicrosoftOfficeThree.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMicrosoftOfficeThree.ImageAlign = ContentAlignment.MiddleLeft;
            buttonMicrosoftOfficeThree.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupFoxitReader.Image = Properties.Resources.icon_foxit_reader;
            buttonSetupFoxitReader.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupFoxitReader.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupFoxitReader.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupSumatraPDF.Image = Properties.Resources.icon_sumatra_pdf;
            buttonSetupSumatraPDF.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupSumatraPDF.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupSumatraPDF.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupPotPlayer.Image = Properties.Resources.icon_pot_player;
            buttonSetupPotPlayer.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupPotPlayer.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupPotPlayer.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupVLC.Image = Properties.Resources.icon_vlc;
            buttonSetupVLC.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupVLC.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupVLC.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupImageGlass.Image = Properties.Resources.icon_image_glass;
            buttonSetupImageGlass.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupImageGlass.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupImageGlass.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupIDM.Image = Properties.Resources.icon_idman;
            buttonSetupIDM.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupIDM.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupIDM.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupEverything.Image = Properties.Resources.icon_everything;
            buttonSetupEverything.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupEverything.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupEverything.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupCCleaner.Image = Properties.Resources.icon_ccleaner;
            buttonSetupCCleaner.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupCCleaner.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupCCleaner.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupVisualStudioCommunity.Image = Properties.Resources.icon_visual_studio;
            buttonSetupVisualStudioCommunity.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupVisualStudioCommunity.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupVisualStudioCommunity.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupSublimeText.Image = Properties.Resources.icon_sublime_text;
            buttonSetupSublimeText.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupSublimeText.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupSublimeText.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupOBSStudio.Image = Properties.Resources.icon_obs_studio;
            buttonSetupOBSStudio.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupOBSStudio.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupOBSStudio.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupShareX.Image = Properties.Resources.icon_share_x;
            buttonSetupShareX.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupShareX.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupShareX.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupLightshot.Image = Properties.Resources.icon_lightshot;
            buttonSetupLightshot.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupLightshot.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupLightshot.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupFastStoneCapture.Image = Properties.Resources.icon_faststone_capture;
            buttonSetupFastStoneCapture.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupFastStoneCapture.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupFastStoneCapture.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupTelegram.Image = Properties.Resources.icon_telegram;
            buttonSetupTelegram.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupTelegram.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupTelegram.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupZalo.Image = Properties.Resources.icon_zalo;
            buttonSetupZalo.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupZalo.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupZalo.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupWandriver.Image = Properties.Resources.icon_wan_driver;
            buttonSetupWandriver.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupWandriver.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupWandriver.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupGoogleDrive.Image = Properties.Resources.icon_google_drive;
            buttonSetupGoogleDrive.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupGoogleDrive.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupGoogleDrive.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupVMwareWorkstation.Image = Properties.Resources.icon_vmware;
            buttonSetupVMwareWorkstation.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupVMwareWorkstation.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupVMwareWorkstation.TextAlign = ContentAlignment.MiddleCenter;

        }

        public void SetupUITabCaiDatNhanh()
        {
            buttonOffRealTimeProtection.Image = Properties.Resources.icon_windows_security_off;
            buttonOffRealTimeProtection.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOffRealTimeProtection.ImageAlign = ContentAlignment.MiddleLeft;
            buttonOffRealTimeProtection.TextAlign = ContentAlignment.MiddleCenter;

            buttonAddCmdRightMouse.Image = Properties.Resources.icon_terminal;
            buttonAddCmdRightMouse.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAddCmdRightMouse.ImageAlign = ContentAlignment.MiddleLeft;
            buttonAddCmdRightMouse.TextAlign = ContentAlignment.MiddleCenter;

            buttonSetupFramework35Iso.Image = Properties.Resources.icon_terminal;
            buttonSetupFramework35Iso.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSetupFramework35Iso.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSetupFramework35Iso.TextAlign = ContentAlignment.MiddleCenter;

            buttonSearchWifiDriver.Image = Properties.Resources.icon_wifi_hexagonal;
            buttonSearchWifiDriver.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSearchWifiDriver.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSearchWifiDriver.TextAlign = ContentAlignment.MiddleCenter;

        }

        public void SetupUITabTienIch()
        {
            buttonWindowsUpdateBlocker.Image = Properties.Resources.icon_sordum_windows_update_blocker;
            buttonWindowsUpdateBlocker.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonWindowsUpdateBlocker.ImageAlign = ContentAlignment.MiddleLeft;
            buttonWindowsUpdateBlocker.TextAlign = ContentAlignment.MiddleCenter;

            buttonDefenderControl.Image = Properties.Resources.icon_sordum_defender_control;
            buttonDefenderControl.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDefenderControl.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDefenderControl.TextAlign = ContentAlignment.MiddleCenter;

            buttonDnsJumper.Image = Properties.Resources.icon_sordum_dns_jumper;
            buttonDnsJumper.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDnsJumper.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDnsJumper.TextAlign = ContentAlignment.MiddleCenter;

            buttonUpdateTime.Image = Properties.Resources.icon_sordum_update_time;
            buttonUpdateTime.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonUpdateTime.ImageAlign = ContentAlignment.MiddleLeft;
            buttonUpdateTime.TextAlign = ContentAlignment.MiddleCenter;

            buttonBlueLifeHostsEditor.Image = Properties.Resources.icon_sordum_bluelife_host_editor;
            buttonBlueLifeHostsEditor.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonBlueLifeHostsEditor.ImageAlign = ContentAlignment.MiddleLeft;
            buttonBlueLifeHostsEditor.TextAlign = ContentAlignment.MiddleCenter;

            buttonEasyContextMenu.Image = Properties.Resources.icon_sordum_easy_context_menu;
            buttonEasyContextMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEasyContextMenu.ImageAlign = ContentAlignment.MiddleLeft;
            buttonEasyContextMenu.TextAlign = ContentAlignment.MiddleCenter;

            buttonEasyServiceOptimizer.Image = Properties.Resources.icon_sordum_easy_service_optimizer;
            buttonEasyServiceOptimizer.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEasyServiceOptimizer.ImageAlign = ContentAlignment.MiddleLeft;
            buttonEasyServiceOptimizer.TextAlign = ContentAlignment.MiddleCenter;

            buttonStoreAppsTool.Image = Properties.Resources.icon_sordum_store_apps_tool;
            buttonStoreAppsTool.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonStoreAppsTool.ImageAlign = ContentAlignment.MiddleLeft;
            buttonStoreAppsTool.TextAlign = ContentAlignment.MiddleCenter;

            buttonTempCleaner.Image = Properties.Resources.icon_sordum_temp_cleaner;
            buttonTempCleaner.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonTempCleaner.ImageAlign = ContentAlignment.MiddleLeft;
            buttonTempCleaner.TextAlign = ContentAlignment.MiddleCenter;

            buttonRestartExplorer.Image = Properties.Resources.icon_sordum_restart_explorer;
            buttonRestartExplorer.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRestartExplorer.ImageAlign = ContentAlignment.MiddleLeft;
            buttonRestartExplorer.TextAlign = ContentAlignment.MiddleCenter;

            buttonWin11ClassicContextMenu.Image = Properties.Resources.icon_sordum_win11_classic_context_menu;
            buttonWin11ClassicContextMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonWin11ClassicContextMenu.ImageAlign = ContentAlignment.MiddleLeft;
            buttonWin11ClassicContextMenu.TextAlign = ContentAlignment.MiddleCenter;

            buttonRegistryWorkshop.Image = Properties.Resources.icon_registry_workshop;
            buttonRegistryWorkshop.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRegistryWorkshop.ImageAlign = ContentAlignment.MiddleLeft;
            buttonRegistryWorkshop.TextAlign = ContentAlignment.MiddleCenter;

            buttonBkavShowHiddenFiles.Image = Properties.Resources.icon_bkav_show_hidden_files;
            buttonBkavShowHiddenFiles.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonBkavShowHiddenFiles.ImageAlign = ContentAlignment.MiddleLeft;
            buttonBkavShowHiddenFiles.TextAlign = ContentAlignment.MiddleCenter;

            buttonRecuva.Image = Properties.Resources.icon_recuva;
            buttonRecuva.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRecuva.ImageAlign = ContentAlignment.MiddleLeft;
            buttonRecuva.TextAlign = ContentAlignment.MiddleCenter;

            buttonFidoScript.Image = Properties.Resources.icon_terminal;
            buttonFidoScript.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonFidoScript.ImageAlign = ContentAlignment.MiddleLeft;
            buttonFidoScript.TextAlign = ContentAlignment.MiddleCenter;

            buttonRufus.Image = Properties.Resources.icon_rufus;
            buttonRufus.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRufus.ImageAlign = ContentAlignment.MiddleLeft;
            buttonRufus.TextAlign = ContentAlignment.MiddleCenter;

            buttonOfficeToolPlus.Image = Properties.Resources.icon_office_tool_plus;
            buttonOfficeToolPlus.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOfficeToolPlus.ImageAlign = ContentAlignment.MiddleLeft;
            buttonOfficeToolPlus.TextAlign = ContentAlignment.MiddleCenter;

        }

        public void SetupUITabChuyenDung()
        {
            buttonCpuZ.Image = Properties.Resources.icon_cpu_z;
            buttonCpuZ.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCpuZ.ImageAlign = ContentAlignment.MiddleLeft;
            buttonCpuZ.TextAlign = ContentAlignment.MiddleCenter;

            buttonGpuZ.Image = Properties.Resources.icon_gpu_z;
            buttonGpuZ.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonGpuZ.ImageAlign = ContentAlignment.MiddleLeft;
            buttonGpuZ.TextAlign = ContentAlignment.MiddleCenter;

            buttonHWiNFO.Image = Properties.Resources.icon_hwinfo;
            buttonHWiNFO.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonHWiNFO.ImageAlign = ContentAlignment.MiddleLeft;
            buttonHWiNFO.TextAlign = ContentAlignment.MiddleCenter;

            buttonSpeccy.Image = Properties.Resources.icon_speccy;
            buttonSpeccy.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonSpeccy.ImageAlign = ContentAlignment.MiddleLeft;
            buttonSpeccy.TextAlign = ContentAlignment.MiddleCenter;

            buttonCrystalDiskInfo.Image = Properties.Resources.icon_crystal_disk_info;
            buttonCrystalDiskInfo.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCrystalDiskInfo.ImageAlign = ContentAlignment.MiddleLeft;
            buttonCrystalDiskInfo.TextAlign = ContentAlignment.MiddleCenter;

            buttonKeyboardTest.Image = Properties.Resources.icon_keyboard_test;
            buttonKeyboardTest.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonKeyboardTest.ImageAlign = ContentAlignment.MiddleLeft;
            buttonKeyboardTest.TextAlign = ContentAlignment.MiddleCenter;

            buttonIsMyLcdOK.Image = Properties.Resources.icon_ismylcdok;
            buttonIsMyLcdOK.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonIsMyLcdOK.ImageAlign = ContentAlignment.MiddleLeft;
            buttonIsMyLcdOK.TextAlign = ContentAlignment.MiddleCenter;

            buttontLCDtest.Image = Properties.Resources.icon_tlcdtest;
            buttontLCDtest.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttontLCDtest.ImageAlign = ContentAlignment.MiddleLeft;
            buttontLCDtest.TextAlign = ContentAlignment.MiddleCenter;


            buttonMicrosoftActivationScriptsOnline.Image = Properties.Resources.icon_microsoft_activation_scripts;
            buttonMicrosoftActivationScriptsOnline.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMicrosoftActivationScriptsOnline.ImageAlign = ContentAlignment.MiddleLeft;
            buttonMicrosoftActivationScriptsOnline.TextAlign = ContentAlignment.MiddleCenter;

            buttonMicrosoftActivationScriptsOffline.Image = Properties.Resources.icon_microsoft_activation_scripts;
            buttonMicrosoftActivationScriptsOffline.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonMicrosoftActivationScriptsOffline.ImageAlign = ContentAlignment.MiddleLeft;
            buttonMicrosoftActivationScriptsOffline.TextAlign = ContentAlignment.MiddleCenter;

            buttonActivateAIOTools.Image = Properties.Resources.icon_terminal;
            buttonActivateAIOTools.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonActivateAIOTools.ImageAlign = ContentAlignment.MiddleLeft;
            buttonActivateAIOTools.TextAlign = ContentAlignment.MiddleCenter;

            buttonWindowsActivateForVPS.Image = Properties.Resources.icon_terminal;
            buttonWindowsActivateForVPS.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonWindowsActivateForVPS.ImageAlign = ContentAlignment.MiddleLeft;
            buttonWindowsActivateForVPS.TextAlign = ContentAlignment.MiddleCenter;


            buttonNirsoftChromePassword.Image = Properties.Resources.icon_nirsoft_chrome_password;
            buttonNirsoftChromePassword.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftChromePassword.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftChromePassword.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftIEPasswords.Image = Properties.Resources.icon_nirsoft_ie_passwords;
            buttonNirsoftIEPasswords.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftIEPasswords.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftIEPasswords.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftPasswordFirefox.Image = Properties.Resources.icon_nirsoft_password_firefox;
            buttonNirsoftPasswordFirefox.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftPasswordFirefox.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftPasswordFirefox.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftPasswordRemoteDesktop.Image = Properties.Resources.icon_nirsoft_password_remote_desktop;
            buttonNirsoftPasswordRemoteDesktop.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftPasswordRemoteDesktop.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftPasswordRemoteDesktop.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftWebBrowserPassword.Image = Properties.Resources.icon_nirsoft_web_browser_password;
            buttonNirsoftWebBrowserPassword.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftWebBrowserPassword.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftWebBrowserPassword.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftWirelessKeyView.Image = Properties.Resources.icon_nirsoft_wireless_key_view;
            buttonNirsoftWirelessKeyView.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftWirelessKeyView.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftWirelessKeyView.TextAlign = ContentAlignment.MiddleCenter;

            buttonNirsoftWirelessNetworkWatcher.Image = Properties.Resources.icon_nirsoft_wireless_network_watcher;
            buttonNirsoftWirelessNetworkWatcher.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonNirsoftWirelessNetworkWatcher.ImageAlign = ContentAlignment.MiddleLeft;
            buttonNirsoftWirelessNetworkWatcher.TextAlign = ContentAlignment.MiddleCenter;

        }

        public void SetupUITabHopCongCu()
        {
            linkIObitClonedFilesScanner.Image = Properties.Resources.icon_iobit_cloned_files_scanner;
            linkIObitClonedFilesScanner.Text = "     " + linkIObitClonedFilesScanner.Text;
            linkIObitClonedFilesScanner.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitContextMenuManager.Image = Properties.Resources.icon_iobit_context_menu_manager;
            linkIObitContextMenuManager.Text = "     " + linkIObitContextMenuManager.Text;
            linkIObitContextMenuManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDefaultProgram.Image = Properties.Resources.icon_iobit_default_program;
            linkIObitDefaultProgram.Text = "     " + linkIObitDefaultProgram.Text;
            linkIObitDefaultProgram.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDiskCleaner.Image = Properties.Resources.icon_iobit_disk_cleaner;
            linkIObitDiskCleaner.Text = "     " + linkIObitDiskCleaner.Text;
            linkIObitDiskCleaner.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDiskDoctor.Image = Properties.Resources.icon_iobit_disk_doctor;
            linkIObitDiskDoctor.Text = "     " + linkIObitDiskDoctor.Text;
            linkIObitDiskDoctor.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDiskExplorer.Image = Properties.Resources.icon_iobit_disk_explorer;
            linkIObitDiskExplorer.Text = "     " + linkIObitDiskExplorer.Text;
            linkIObitDiskExplorer.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDriverManager.Image = Properties.Resources.icon_iobit_driver_manager;
            linkIObitDriverManager.Text = "     " + linkIObitDriverManager.Text;
            linkIObitDriverManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitDuplicateFileFinder.Image = Properties.Resources.icon_iobit_duplicate_file_finder;
            linkIObitDuplicateFileFinder.Text = "     " + linkIObitDuplicateFileFinder.Text;
            linkIObitDuplicateFileFinder.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitEmptyFolderScanner.Image = Properties.Resources.icon_iobit_empty_folder_scanner;
            linkIObitEmptyFolderScanner.Text = "     " + linkIObitEmptyFolderScanner.Text;
            linkIObitEmptyFolderScanner.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitFileShredder.Image = Properties.Resources.icon_iobit_file_shredder;
            linkIObitFileShredder.Text = "     " + linkIObitFileShredder.Text;
            linkIObitFileShredder.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitIEHelper.Image = Properties.Resources.icon_iobit_ie_helper;
            linkIObitIEHelper.Text = "     " + linkIObitIEHelper.Text;
            linkIObitIEHelper.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitInternetBooster.Image = Properties.Resources.icon_iobit_internet_booster;
            linkIObitInternetBooster.Text = "     " + linkIObitInternetBooster.Text;
            linkIObitInternetBooster.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitLargeFileFinder.Image = Properties.Resources.icon_iobit_large_file_finder;
            linkIObitLargeFileFinder.Text = "     " + linkIObitLargeFileFinder.Text;
            linkIObitLargeFileFinder.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitMonitor8.Image = Properties.Resources.icon_iobit_monitor;
            linkIObitMonitor8.Text = "     " + linkIObitMonitor8.Text;
            linkIObitMonitor8.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitMonitor19.Image = Properties.Resources.icon_iobit_monitor;
            linkIObitMonitor19.Text = "     " + linkIObitMonitor19.Text;
            linkIObitMonitor19.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitMyWin10.Image = Properties.Resources.icon_iobit_my_win10;
            linkIObitMyWin10.Text = "     " + linkIObitMyWin10.Text;
            linkIObitMyWin10.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitProcessManager.Image = Properties.Resources.icon_iobit_process_manager;
            linkIObitProcessManager.Text = "     " + linkIObitProcessManager.Text;
            linkIObitProcessManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitProgramDeactivator.Image = Properties.Resources.icon_iobit_program_deactivator;
            linkIObitProgramDeactivator.Text = "     " + linkIObitProgramDeactivator.Text;
            linkIObitProgramDeactivator.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitRegistryCleaner.Image = Properties.Resources.icon_iobit_registry_cleaner;
            linkIObitRegistryCleaner.Text = "     " + linkIObitRegistryCleaner.Text;
            linkIObitRegistryCleaner.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitRegistryDefrag.Image = Properties.Resources.icon_iobit_registry_defrag;
            linkIObitRegistryDefrag.Text = "     " + linkIObitRegistryDefrag.Text;
            linkIObitRegistryDefrag.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitReinforce.Image = Properties.Resources.icon_iobit_reinforce;
            linkIObitReinforce.Text = "     " + linkIObitReinforce.Text;
            linkIObitReinforce.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitRescueCenter.Image = Properties.Resources.icon_iobit_rescue_center;
            linkIObitRescueCenter.Text = "     " + linkIObitRescueCenter.Text;
            linkIObitRescueCenter.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitScreenShot.Image = Properties.Resources.icon_iobit_screen_shot;
            linkIObitScreenShot.Text = "     " + linkIObitScreenShot.Text;
            linkIObitScreenShot.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitShortcutFixer.Image = Properties.Resources.icon_iobit_shortcut_fixer;
            linkIObitShortcutFixer.Text = "     " + linkIObitShortcutFixer.Text;
            linkIObitShortcutFixer.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitSmartRAM.Image = Properties.Resources.icon_iobit_smart_ram;
            linkIObitSmartRAM.Text = "     " + linkIObitSmartRAM.Text;
            linkIObitSmartRAM.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitStartupManager.Image = Properties.Resources.icon_iobit_startup_manager;
            linkIObitStartupManager.Text = "     " + linkIObitStartupManager.Text;
            linkIObitStartupManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitSystemControl.Image = Properties.Resources.icon_iobit_system_control;
            linkIObitSystemControl.Text = "     " + linkIObitSystemControl.Text;
            linkIObitSystemControl.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitSystemInformation.Image = Properties.Resources.icon_iobit_system_information;
            linkIObitSystemInformation.Text = "     " + linkIObitSystemInformation.Text;
            linkIObitSystemInformation.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitUndelete.Image = Properties.Resources.icon_iobit_undelete;
            linkIObitUndelete.Text = "     " + linkIObitUndelete.Text;
            linkIObitUndelete.ImageAlign = ContentAlignment.MiddleLeft;

            linkIObitWinFix.Image = Properties.Resources.icon_iobit_win_fix;
            linkIObitWinFix.Text = "     " + linkIObitWinFix.Text;
            linkIObitWinFix.ImageAlign = ContentAlignment.MiddleLeft;





            linkGlaryCheckDisk.Image = Properties.Resources.icon_glary_check_disk;
            linkGlaryCheckDisk.Text = "     " + linkGlaryCheckDisk.Text;
            linkGlaryCheckDisk.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryContextMenuManager.Image = Properties.Resources.icon_glary_cmm;
            linkGlaryContextMenuManager.Text = "     " + linkGlaryContextMenuManager.Text;
            linkGlaryContextMenuManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryDiskAnalysis.Image = Properties.Resources.icon_glary_disk_analysis;
            linkGlaryDiskAnalysis.Text = "     " + linkGlaryDiskAnalysis.Text;
            linkGlaryDiskAnalysis.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryDiskCleaner.Image = Properties.Resources.icon_glary_disk_cleaner;
            linkGlaryDiskCleaner.Text = "     " + linkGlaryDiskCleaner.Text;
            linkGlaryDiskCleaner.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryDiskDefrag.Image = Properties.Resources.icon_glary_disk_defrag;
            linkGlaryDiskDefrag.Text = "     " + linkGlaryDiskDefrag.Text;
            linkGlaryDiskDefrag.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryDriverBackup.Image = Properties.Resources.icon_glary_driver_backup;
            linkGlaryDriverBackup.Text = "     " + linkGlaryDriverBackup.Text;
            linkGlaryDriverBackup.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryDuplicateFileFinder.Image = Properties.Resources.icon_glary_dupefinder;
            linkGlaryDuplicateFileFinder.Text = "     " + linkGlaryDuplicateFileFinder.Text;
            linkGlaryDuplicateFileFinder.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryEmptyFolderFinder.Image = Properties.Resources.icon_glary_empty_folder_finder;
            linkGlaryEmptyFolderFinder.Text = "     " + linkGlaryEmptyFolderFinder.Text;
            linkGlaryEmptyFolderFinder.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryEncryptExe.Image = Properties.Resources.icon_glary_encrypt_exe;
            linkGlaryEncryptExe.Text = "     " + linkGlaryEncryptExe.Text;
            linkGlaryEncryptExe.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryFileEncrypt.Image = Properties.Resources.icon_glary_file_encrypt;
            linkGlaryFileEncrypt.Text = "     " + linkGlaryFileEncrypt.Text;
            linkGlaryFileEncrypt.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryFileSplitter.Image = Properties.Resources.icon_glary_file_splitter;
            linkGlaryFileSplitter.Text = "     " + linkGlaryFileSplitter.Text;
            linkGlaryFileSplitter.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryFileUndelete.Image = Properties.Resources.icon_glary_file_undelete;
            linkGlaryFileUndelete.Text = "     " + linkGlaryFileUndelete.Text;
            linkGlaryFileUndelete.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryIEHelper.Image = Properties.Resources.icon_glary_ie_helper;
            linkGlaryIEHelper.Text = "     " + linkGlaryIEHelper.Text;
            linkGlaryIEHelper.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryJoinExe.Image = Properties.Resources.icon_glary_join_exe;
            linkGlaryJoinExe.Text = "     " + linkGlaryJoinExe.Text;
            linkGlaryJoinExe.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryMemoryDefrag.Image = Properties.Resources.icon_glary_mem_defrag;
            linkGlaryMemoryDefrag.Text = "     " + linkGlaryMemoryDefrag.Text;
            linkGlaryMemoryDefrag.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryProcessManager.Image = Properties.Resources.icon_glary_proc_mgr;
            linkGlaryProcessManager.Text = "     " + linkGlaryProcessManager.Text;
            linkGlaryProcessManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryQuickSearch.Image = Properties.Resources.icon_glary_quick_search;
            linkGlaryQuickSearch.Text = "     " + linkGlaryQuickSearch.Text;
            linkGlaryQuickSearch.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryRegistryDefrag.Image = Properties.Resources.icon_glary_reg_defrag;
            linkGlaryRegistryDefrag.Text = "     " + linkGlaryRegistryDefrag.Text;
            linkGlaryRegistryDefrag.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryRegistryCleaner.Image = Properties.Resources.icon_glary_registry_cleaner;
            linkGlaryRegistryCleaner.Text = "     " + linkGlaryRegistryCleaner.Text;
            linkGlaryRegistryCleaner.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryRestoreCenter.Image = Properties.Resources.icon_glary_restore_center;
            linkGlaryRestoreCenter.Text = "     " + linkGlaryRestoreCenter.Text;
            linkGlaryRestoreCenter.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryShortcutFixer.Image = Properties.Resources.icon_glary_shortcut_fixer;
            linkGlaryShortcutFixer.Text = "     " + linkGlaryShortcutFixer.Text;
            linkGlaryShortcutFixer.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryFileShredder.Image = Properties.Resources.icon_glary_shredder;
            linkGlaryFileShredder.Text = "     " + linkGlaryFileShredder.Text;
            linkGlaryFileShredder.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlarySoftwareUpdate.Image = Properties.Resources.icon_glary_software_update;
            linkGlarySoftwareUpdate.Text = "     " + linkGlarySoftwareUpdate.Text;
            linkGlarySoftwareUpdate.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryStartupManager.Image = Properties.Resources.icon_glary_startup_manager;
            linkGlaryStartupManager.Text = "     " + linkGlaryStartupManager.Text;
            linkGlaryStartupManager.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlarySystemInformation.Image = Properties.Resources.icon_glary_sysinfo;
            linkGlarySystemInformation.Text = "     " + linkGlarySystemInformation.Text;
            linkGlarySystemInformation.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryTracksEraser.Image = Properties.Resources.icon_glary_tracks_eraser;
            linkGlaryTracksEraser.Text = "     " + linkGlaryTracksEraser.Text;
            linkGlaryTracksEraser.ImageAlign = ContentAlignment.MiddleLeft;

            linkGlaryUninstaler.Image = Properties.Resources.icon_glary_uninstaler;
            linkGlaryUninstaler.Text = "     " + linkGlaryUninstaler.Text;
            linkGlaryUninstaler.ImageAlign = ContentAlignment.MiddleLeft;
        }

        private void checkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkTopMost.Checked;
        }

        private void checkAutoStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (File.Exists(startupShortcut))
            {
                File.Delete(startupShortcut);
            }
            if (checkAutoStartup.Checked)
            {
                string script = Application.StartupPath + "\\startup.vbs";
                File.WriteAllText(script, "Set oWS = WScript.CreateObject(\"WScript.Shell\") \nSet oLink = oWS.CreateShortcut(\"" + startupShortcut + "\") \noLink.TargetPath = \"" + Application.ExecutablePath + "\" \noLink.Save");
                Process process = Process.Start(new ProcessStartInfo() { FileName = script, WindowStyle = ProcessWindowStyle.Hidden });
                process.WaitForExit();
                Task.Delay(10);
                File.Delete(script);
            }
        }

        private void buttonAutoSelfDelete_Click(object sender, EventArgs e)
        {
            string batchFile = Path.Combine(Path.GetTempPath(), "self-delete.bat");
            string script = $"TIMEOUT /T 3 /NOBREAK & DEL /F /S /Q {Application.ExecutablePath} & DEL /F /S /Q {batchFile}";
            File.WriteAllText(batchFile, script);
            Process process = new Process();
            process.StartInfo.FileName = batchFile;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            Application.Exit();
        }

        private void tabControlGlobal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!File.Exists(winrar))
            {
                WriteMessage("WinRAR is not installed");
            }
        }

        public async Task DownloadFileWithProgressAsync(string url, string destinationPath, IProgress<double> progress)
        {
            var client = new HttpClient();

            // Gọi API nhưng chỉ đọc Header trước để lấy dung lượng file (Content-Length)
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1;
            var contentStream = await response.Content.ReadAsStreamAsync();
            var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 131072, true);

            var buffer = new byte[131072];
            long totalReadBytes = 0;
            int readBytes;

            while ((readBytes = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, readBytes);
                totalReadBytes += readBytes;

                // Tính toán và báo cáo phần trăm
                if (totalBytes != -1)
                {
                    double percentage = (double)totalReadBytes / totalBytes * 100;
                    progress?.Report(percentage);
                }
            }

            fileStream.Close();
            progress?.Report(100);
        }

        public async void DownGitlabZipStartButton(Button buttonClick, string zipName, string exeRun32, string exeRun64 = "", string regfile = "")
        {
            string buttext = buttonClick.Text;
            string fileUrl = $"https://gitlab.com/wintools/software/-/raw/main/{zipName}.zip";
            string localPath = Path.Combine(Path.GetTempPath(), $"{zipName}.zip");
            string outputFolder = Path.Combine(Path.GetTempPath(), zipName);
            string execute = Path.Combine(outputFolder, (Environment.Is64BitOperatingSystem && !string.IsNullOrEmpty(exeRun64)) ? exeRun64 : exeRun32);
            string regkey = Path.Combine(outputFolder, regfile);

            if (!File.Exists(execute))
            {
                if (!File.Exists(localPath))
                {
                    var progress = new Progress<double>(percent =>
                    {
                        if (percent < 100)
                        {
                            buttonClick.Text = $"{percent:F2}%";
                        }
                        else
                        {
                            buttonClick.Text = buttext;
                        }
                    });
                    await DownloadFileWithProgressAsync(fileUrl, localPath, progress);
                }

                if (!File.Exists(winrar)) WriteMessage("WinRAR is not installed");

                if (File.Exists(winrar) && File.Exists(localPath))
                {
                    if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                    Process extract = new Process();
                    extract.StartInfo.FileName = winrar;
                    extract.StartInfo.Arguments = $"x -o+ \"{localPath}\" \"{outputFolder}";
                    extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.StartInfo.CreateNoWindow = true;
                    extract.Start();
                    extract.WaitForExit();
                    File.Delete(localPath);
                }
            }
            if (!string.IsNullOrEmpty(regfile) && File.Exists(regkey))
            {
                Process process = new Process();
                process.StartInfo.FileName = "reg.exe";
                process.StartInfo.Arguments = $"IMPORT \"{regkey}\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
            }
            if (File.Exists(execute)) Process.Start(execute);
        }

        public async void DownGitlabZipStartLabel(Label buttonLabel, string zipname, string exename, string regname = "")
        {
            string buttext = buttonLabel.Text;
            string fileUrl = $"https://gitlab.com/wintools/software/-/raw/main/{zipname}.zip";
            string localPath = Path.Combine(Path.GetTempPath(), $"{zipname}.zip");
            string outputFolder = Path.Combine(Path.GetTempPath(), $"{zipname}");
            string execute = Path.Combine(outputFolder, exename.EndsWith(".exe") ? exename : $"{exename}.exe");
            string regkey = Path.Combine(outputFolder, regname);

            if (!File.Exists(execute))
            {
                if (!File.Exists(localPath))
                {
                    var progress = new Progress<double>(percent =>
                    {
                        if (percent < 100)
                        {
                            buttonLabel.Text = $"{percent:F2}%";
                        }
                        else
                        {
                            buttonLabel.Text = buttext;
                        }
                    });
                    await DownloadFileWithProgressAsync(fileUrl, localPath, progress);
                }

                if (!File.Exists(winrar)) WriteMessage("WinRAR is not installed");

                if (File.Exists(winrar) && File.Exists(localPath))
                {
                    if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                    Process extract = new Process();
                    extract.StartInfo.FileName = winrar;
                    extract.StartInfo.Arguments = $"x -o+ \"{localPath}\" \"{outputFolder}";
                    extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.StartInfo.CreateNoWindow = true;
                    extract.Start();
                    extract.WaitForExit();
                    File.Delete(localPath);
                }
            }
            if (!string.IsNullOrEmpty(regname) && File.Exists(regkey))
            {
                Process process = new Process();
                process.StartInfo.FileName = "reg.exe";
                process.StartInfo.Arguments = $"IMPORT \"{regkey}\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
            }
            if (File.Exists(execute)) Process.Start(execute);
        }

        public async void DownLinkCompressionStartButton(Button buttonClick, string linkUrl, string exename)
        {
            string buttext = buttonClick.Text;
            string zipName = linkUrl.Split('/').Last();
            if (zipName.ToLower().EndsWith(".zip") || zipName.ToLower().EndsWith(".rar"))
            {
                zipName = zipName.Substring(0, zipName.Length - 4);
            }
            if (zipName.ToLower().EndsWith(".7z"))
            {
                zipName = zipName.Substring(0, zipName.Length - 3);
            }
            string localPath = Path.Combine(Path.GetTempPath(), $"{zipName}.zip");
            string outputFolder = Path.Combine(Path.GetTempPath(), zipName);
            string execute = Path.Combine(outputFolder, exename);

            if (!File.Exists(execute))
            {
                if (!File.Exists(localPath))
                {
                    var progress = new Progress<double>(percent =>
                    {
                        if (percent < 100)
                        {
                            buttonClick.Text = $"{percent:F2}%";
                        }
                        else
                        {
                            buttonClick.Text = buttext;
                        }
                    });
                    await DownloadFileWithProgressAsync(linkUrl, localPath, progress);
                }

                if (!File.Exists(winrar)) WriteMessage("WinRAR is not installed");

                if (File.Exists(winrar) && File.Exists(localPath))
                {
                    if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                    Process extract = new Process();
                    extract.StartInfo.FileName = winrar;
                    extract.StartInfo.Arguments = $"x -o+ \"{localPath}\" \"{outputFolder}";
                    extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.StartInfo.CreateNoWindow = true;
                    extract.Start();
                    extract.WaitForExit();
                    File.Delete(localPath);
                }
            }
            if (File.Exists(execute)) Process.Start(execute);
        }

        public async void DownLinkEXEStartButton(Button buttonClick, string linkUrl, string exename, string arguments = "")
        {
            string buttext = buttonClick.Text;
            string localPath = Path.Combine(Path.GetTempPath(), exename);

            if (!File.Exists(localPath))
            {
                var progress = new Progress<double>(percent =>
                {
                    if (percent < 100)
                    {
                        buttonClick.Text = $"{percent:F2}%";
                    }
                    else
                    {
                        buttonClick.Text = buttext;
                    }
                });
                await DownloadFileWithProgressAsync(linkUrl, localPath, progress);
            }
            if (File.Exists(localPath)) Process.Start(localPath, arguments);
        }

        public void WriteMessage(string message)
        {
            labelMsg.ForeColor = Color.Red;
            labelMsg.Text = message;
            Task onetask = new Task(() =>
            {
                Thread.Sleep(3000);

                this.Invoke(new Action(() =>
                {
                    labelMsg.Text = "";
                }));
            });
            onetask.Start();
        }

        public void OpenBrowserUrl(string linkUrl)
        {
            if (File.Exists(chromeApp))
            {
                Process.Start(chromeApp, linkUrl);
            }
            else if (File.Exists(edgeApp))
            {
                Process.Start(edgeApp, linkUrl);
            }
            else
            {
                Process.Start(linkUrl);
            }
        }

        #region GLOBAL

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Application.StartupPath); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenControlPanel_Click(object sender, EventArgs e)
        {
            try { Process.Start("control.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenSettings_Click(object sender, EventArgs e)
        {
            try { Process.Start("ms-settings:"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenProgramsAndFeatures_Click(object sender, EventArgs e)
        {
            try { Process.Start("control.exe", "appwiz.cpl"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenAppsAndFeatures_Click(object sender, EventArgs e)
        {
            try { Process.Start("ms-settings:appsfeatures"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenCommandPrompt_Click(object sender, EventArgs e)
        {
            try { Process.Start("cmd.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenPowerShell_Click(object sender, EventArgs e)
        {
            try { Process.Start("powershell.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenWindowsSecurity_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", "windowsdefender:"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenWindowsFirewall_Click(object sender, EventArgs e)
        {
            try { Process.Start("WF.msc"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
            //Process.Start("control.exe", "firewall.cpl");
        }

        private void buttonOpenNetworkConnections_Click(object sender, EventArgs e)
        {
            try { Process.Start("control.exe", "ncpa.cpl"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenTaskScheduler_Click(object sender, EventArgs e)
        {
            try { Process.Start("taskschd.msc", "/s"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenTaskManager_Click(object sender, EventArgs e)
        {
            try { Process.Start("taskmgr.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenAppDataLocalFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenAppDataRoamingFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenStartupFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.Startup)); }
            catch (Exception ex) { WriteMessage(ex.Message); }
            //Process.Start("shell:startup");
        }

        private void buttonOpenCommonStartupFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup)); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonOpenTempFolder_Click(object sender, EventArgs e)
        {
            try { Process.Start("explorer.exe", Path.GetTempPath()); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonPowerOptions_Click(object sender, EventArgs e)
        {
            try { Process.Start("ms-settings:powersleep"); }
            catch (Exception ex) { WriteMessage(ex.Message); } // powercfg.cpl
        }

        private void buttonDateAndTime_Click(object sender, EventArgs e)
        {
            Process.Start("ms-settings:dateandtime");
        }

        private void buttonDiskManagement_Click(object sender, EventArgs e)
        {
            try { Process.Start("diskmgmt.msc"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonDeviceManager_Click(object sender, EventArgs e)
        {
            try { Process.Start("devmgmt.msc"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonRegistryEditor_Click(object sender, EventArgs e)
        {
            try { Process.Start("regedit.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonSystemProtection_Click(object sender, EventArgs e)
        {
            try { Process.Start(@"C:\Windows\Sysnative\SystemPropertiesProtection.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonMicrosoftDirectXDiagnosticTool_Click(object sender, EventArgs e)
        {
            try { Process.Start("dxdiag.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonSystemInformation_Click(object sender, EventArgs e)
        {
            try { Process.Start("msinfo32.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonAdvancedUserAccounts_Click(object sender, EventArgs e)
        {
            try { Process.Start("netplwiz.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonSystemProperties_Click(object sender, EventArgs e)
        {
            try { Process.Start("sysdm.cpl"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonTrustedPlatformModule_Click(object sender, EventArgs e)
        {
            try { Process.Start("tpm.msc"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonIPConfigurationUtility_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", "/k ipconfig");
        }

        private void buttonCheckActiveWindows_Click(object sender, EventArgs e)
        {
            try { Process.Start("slmgr.vbs", "/xpr"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonAboutWindows_Click(object sender, EventArgs e)
        {
            try { Process.Start("winver.exe"); }
            catch (Exception ex) { WriteMessage(ex.Message); }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            shutdown("/r /t 0");
            Application.Exit();
        }

        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            shutdown("/s /t 0");
            Application.Exit();
        }

        public void net(string arg)
        {
            var psi = new ProcessStartInfo("net.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void wmic(string arg)
        {
            var psi = new ProcessStartInfo("wmic.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void reg(string arg)
        {
            var psi = new ProcessStartInfo("reg.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void netsh(string arg)
        {
            var psi = new ProcessStartInfo("netsh.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void shutdown(string arg)
        {
            var psi = new ProcessStartInfo("shutdown.exe", "-a") { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(1000);
            psi = new ProcessStartInfo("shutdown.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void taskkill(string exe)
        {
            var psi = new ProcessStartInfo("taskkill.exe", "/IM " + exe + " /F") { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void tzutil(string zonename)
        {
            var psi = new ProcessStartInfo("tzutil.exe", "/s \"" + zonename + "\"") { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void schtasks(string arg)
        {
            var psi = new ProcessStartInfo("schtasks.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void cacls(string arg)
        {
            var psi = new ProcessStartInfo("cacls.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        public void takeown(string arg)
        {
            var psi = new ProcessStartInfo("takeown.exe", arg) { CreateNoWindow = true, UseShellExecute = false };
            var process = Process.Start(psi);
            process.WaitForExit(5000);
        }

        #endregion GLOBAL


        #region CAI DAT PHAN MEM

        public async void SetupWinRAR(Button buttonClick, string lang = "")
        {
            string fileUrl = string.Empty;
            string localPath = string.Empty;
            string buttext = buttonClick.Text;

            string rar32 = $"https://www.rarlab.com/rar/winrar-x32-701{lang}.exe";
            string rar64 = $"https://www.rarlab.com/rar/winrar-x64-723{lang}.exe";
            string filename32 = Path.Combine(Path.GetTempPath(), "winrar-x32-701.exe");
            string filename64 = Path.Combine(Path.GetTempPath(), "winrar-x64-723.exe");

            if (Environment.Is64BitOperatingSystem)
            {
                fileUrl = rar64;
                localPath = filename64;
            }
            else
            {
                fileUrl = rar32;
                localPath = filename32;
            }
            var progress = new Progress<double>(percent =>
            {
                if (percent < 100)
                {
                    buttonClick.Text = $"{percent:F2}%";
                }
                else
                {
                    buttonClick.Text = buttext;
                }
            });

            await DownloadFileWithProgressAsync(fileUrl, localPath, progress);

            Process process = Process.Start(localPath, "/S");
            process.WaitForExit();
            File.Delete(localPath);

            // rarreg.key
            string rarreg = @"C:\Program Files\WinRAR\rarreg.key";
            string regkey = "RAR registration data\r\nWinRAR\r\nUnlimited Company License\r\nUID=4b914fb772c8376bf571\r\n6412212250f5711ad072cf351cfa39e2851192daf8a362681bbb1d\r\ncd48da1d14d995f0bbf960fce6cb5ffde62890079861be57638717\r\n7131ced835ed65cc743d9777f2ea71a8e32c7e593cf66794343565\r\nb41bcf56929486b8bcdac33d50ecf773996052598f1f556defffbd\r\n982fbe71e93df6b6346c37a3890f3c7edc65d7f5455470d13d1190\r\n6e6fb824bcf25f155547b5fc41901ad58c0992f570be1cf5608ba9\r\naef69d48c864bcd72d15163897773d314187f6a9af350808719796";
            File.WriteAllText(rarreg, regkey);
        }

        private void buttonSetupWinRAR_Click(object sender, EventArgs e)
        {
            SetupWinRAR((Button)sender);
        }

        private void buttonSetupWinRARVi_Click(object sender, EventArgs e)
        {
            SetupWinRAR((Button)sender, "vn");
        }

        private async void buttonSetupSevenZip_Click(object sender, EventArgs e)
        {
            string fileUrl = string.Empty;
            string localPath = string.Empty;
            string buttext = buttonSetupSevenZip.Text;

            string zip32 = "https://github.com/ip7z/7zip/releases/download/26.02/7z2602.exe";
            string zip64 = "https://github.com/ip7z/7zip/releases/download/26.02/7z2602-x64.exe";
            string filename32 = Path.Combine(Path.GetTempPath(), "7z2602.exe");
            string filename64 = Path.Combine(Path.GetTempPath(), "7z2602-x64.exe");
            string sevenzip = "C:\\Program Files\\7-Zip\\7z.exe";

            if (Environment.Is64BitOperatingSystem)
            {
                fileUrl = zip64;
                localPath = filename64;
            }
            else
            {
                fileUrl = zip32;
                localPath = filename32;
            }

            var progress = new Progress<double>(percent =>
            {
                if (percent < 100)
                {
                    buttonSetupSevenZip.Text = $"{percent:F2}%";
                }
                else
                {
                    buttonSetupSevenZip.Text = buttext;
                }
            });

            await DownloadFileWithProgressAsync(fileUrl, localPath, progress);

            Process process = Process.Start(localPath, "/S");
            process.WaitForExit();
            File.Delete(localPath);
        }

        private void buttonSetupChrome_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "chrome-setup-vi", "ChromeSetup.exe");
        }

        private void buttonSetupChromeEnglish_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "chrome-setup-en", "ChromeSetup.exe");
        }

        private void buttonSetupCocCoc_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "coc-coc", "CocCocSetup.exe");
        }

        private void buttonSetupUnikey_Click(object sender, EventArgs e)
        {
            string fileUrl = string.Empty;
            string localPath = string.Empty;
            string outputFolder = string.Empty;
            string execute = "UniKeyNT.exe";
            string setupFolder = Path.Combine(@"C:\Program Files", "Unikey");
            string setupExecute = Path.Combine(setupFolder, "UniKeyNT.exe");

            if (Environment.Is64BitOperatingSystem)
            {
                fileUrl = "https://gitlab.com/wintools/software/-/raw/main/unikey46RC2-230919-win64.zip";
                localPath = Path.Combine(Path.GetTempPath(), "unikey46RC2-230919-win64.zip");
                outputFolder = Path.Combine(Path.GetTempPath(), "Unikey");
                execute = Path.Combine(outputFolder, "UniKeyNT.exe");
            }
            else
            {
                fileUrl = "https://gitlab.com/wintools/software/-/raw/main/unikey46RC2-230919-win32.zip";
                localPath = Path.Combine(Path.GetTempPath(), "unikey46RC2-230919-win32.zip");
                outputFolder = Path.Combine(Path.GetTempPath(), "Unikey");
                execute = Path.Combine(outputFolder, "UniKeyNT.exe");
            }
            if (!File.Exists(execute))
            {
                if (!File.Exists(localPath))
                {
                    new System.Net.WebClient().DownloadFile(fileUrl, localPath);
                }

                if (!File.Exists(winrar)) WriteMessage("WinRAR is not installed");

                if (File.Exists(winrar) && File.Exists(localPath))
                {
                    if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                    Process extract = new Process();
                    extract.StartInfo.FileName = winrar;
                    extract.StartInfo.Arguments = $"x -o+ \"{localPath}\" \"{outputFolder}";
                    extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.StartInfo.CreateNoWindow = true;
                    extract.Start();
                    extract.WaitForExit();
                    File.Delete(localPath);
                }
            }
            // setup C:\Program Files
            Directory.Move(outputFolder, setupFolder);
            // create shortcut to desktop
            if (File.Exists(setupExecute))
            {
                string deskShortcut = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\Unikey.lnk";
                string script = Application.StartupPath + "\\shortcut.vbs";

                File.WriteAllText(script, "Set oWS = WScript.CreateObject(\"WScript.Shell\") \nSet oLink = oWS.CreateShortcut(\"" + deskShortcut + "\") \noLink.TargetPath = \"" + setupExecute + "\" \noLink.Save");
                Process process = Process.Start(new ProcessStartInfo() { FileName = script, WindowStyle = ProcessWindowStyle.Hidden });
                process.WaitForExit();
                Task.Delay(10);
                File.Delete(script);
            }
            // start
            if (File.Exists(setupExecute)) Process.Start(setupExecute);
        }

        private void buttonSetupEvkey_Click(object sender, EventArgs e)
        {
            string fileUrl = string.Empty;
            string localPath = string.Empty;
            string outputFolder = string.Empty;
            string execute = string.Empty;
            string setupFolder = Path.Combine(@"C:\Program Files", "EVKey");
            string setupExecute = string.Empty;

            if (Environment.Is64BitOperatingSystem)
            {
                fileUrl = "https://gitlab.com/wintools/software/-/raw/main/evkey64.zip";
                localPath = Path.Combine(Path.GetTempPath(), "evkey64.zip");
                outputFolder = Path.Combine(Path.GetTempPath(), "evkey64");
                execute = Path.Combine(outputFolder, "EVKey64.exe");
                setupExecute = Path.Combine(setupFolder, "EVKey64.exe");
            }
            else
            {
                fileUrl = "https://gitlab.com/wintools/software/-/raw/main/evkey32.zip";
                localPath = Path.Combine(Path.GetTempPath(), "evkey32.zip");
                outputFolder = Path.Combine(Path.GetTempPath(), "evkey32");
                execute = Path.Combine(outputFolder, "EVKey32.exe");
                setupExecute = Path.Combine(setupFolder, "EVKey32.exe");
            }
            if (!File.Exists(execute))
            {
                if (!File.Exists(localPath))
                {
                    new System.Net.WebClient().DownloadFile(fileUrl, localPath);
                }

                if (!File.Exists(winrar)) WriteMessage("WinRAR is not installed");

                if (File.Exists(winrar) && File.Exists(localPath))
                {
                    if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                    Process extract = new Process();
                    extract.StartInfo.FileName = winrar;
                    extract.StartInfo.Arguments = $"x -o+ \"{localPath}\" \"{outputFolder}";
                    extract.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    extract.StartInfo.CreateNoWindow = true;
                    extract.Start();
                    extract.WaitForExit();
                    File.Delete(localPath);
                }
            }
            // setup C:\Program Files
            Directory.Move(outputFolder, setupFolder);
            // create shortcut to desktop
            if (File.Exists(setupExecute))
            {
                string deskShortcut = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\Unikey.lnk";
                string script = Application.StartupPath + "\\shortcut.vbs";

                File.WriteAllText(script, "Set oWS = WScript.CreateObject(\"WScript.Shell\") \nSet oLink = oWS.CreateShortcut(\"" + deskShortcut + "\") \noLink.TargetPath = \"" + setupExecute + "\" \noLink.Save");
                Process process = Process.Start(new ProcessStartInfo() { FileName = script, WindowStyle = ProcessWindowStyle.Hidden });
                process.WaitForExit();
                Task.Delay(10);
                File.Delete(script);
            }
            // start
            if (File.Exists(setupExecute)) Process.Start(setupExecute);
        }

        private void buttonSetupTeamViewer_Click(object sender, EventArgs e)
        {
            string linkDown = "https://dl.teamviewer.com/download/version_15x/TeamViewer_Setup_x64.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "TeamViewer_Setup_x64.exe");
        }

        private void buttonSetupAnyDesk_Click(object sender, EventArgs e)
        {
            string linkDown = "https://download.anydesk.com/AnyDesk.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "AnyDesk.exe");
        }

        private void buttonSetupUltraViewer_Click(object sender, EventArgs e)
        {
            string linkDown = "https://dl2.ultraviewer.net/UltraViewer_setup_6.6.133_vi.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "UltraViewer_setup_6.6.133_vi.exe", "/SILENT /NORESTART");
        }

        private void buttonMicrosoftOfficeOne_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://drive.google.com/drive/folders/1fjcESTisxsreaw8CYUn4TEK2EknCrIw4";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonMicrosoftOfficeTwo_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://drive.google.com/drive/folders/1sZMQ1ZX-gTARPPgNpgrD8j0OggFA7xck";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonMicrosoftOfficeThree_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://drive.google.com/drive/folders/1sBYSobgOaVvH6SkCiNd-8hAboeEMrSXa";
            OpenBrowserUrl(linkUrl);
        }

        private void linkSetupOfficeToolPlus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControlGlobal.SelectedTab = tabPageTienIch;
        }

        private void buttonSetupFoxitReader_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://www.foxit.com/pdf-reader/";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonSetupSumatraPDF_Click(object sender, EventArgs e)
        {
            string linkDown = "https://www.sumatrapdfreader.org/dl/rel/3.6.1/SumatraPDF-3.6.1-64-install.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "SumatraPDF-3.6.1-64-install.exe", "-install -all-users -with-preview -silent");
            Task.Delay(500);
            string testPdf = Path.Combine(Path.GetTempPath(), "test.pdf");
            string blankPdf = "%PDF-1.7\r\n1 0 obj\r\n<</CreationDate(D:20260723114651+07'00')/Title(PDF)/Creator()/Producer()>>\r\nendobj\r\n2 0 obj\r\n<</Type/Catalog/Pages 3 0 R/Metadata 5 0 R>>\r\nendobj\r\n3 0 obj\r\n<</Type/Pages/Count 1/Kids[4 0 R]>>\r\nendobj\r\n4 0 obj\r\n<</Type/Page/MediaBox[0 0 612 792]/Parent 3 0 R/Group<</CS/DeviceRGB/S/Transparency>>>>\r\nendobj\r\n5 0 obj\r\n<</Type/Metadata/Subtype/XML/Length 1405>>\r\nstream\r\nendstream\r\nendobj\r\nxref\r\n0 6\r\n0000000000 65535 f \r\n0000000015 00000 n \r\n0000000160 00000 n \r\n0000000220 00000 n \r\n0000000271 00000 n \r\n0000000374 00000 n \r\ntrailer\r\n<</ID[<8F6B813D76415D4FA6758A585608028C><8F6B813D76415D4FA6758A585608028C>]/Info 1 0 R/Root 2 0 R/Size 6>>\r\nstartxref\r\n1855\r\n%%EOF";
            File.WriteAllText(testPdf, blankPdf);
            Process.Start("explorer.exe", testPdf);
        }

        private void buttonSetupPotPlayer_Click(object sender, EventArgs e)
        {
            string linkDown = "https://t1.kakaocdn.net/potplayer/PotPlayer/Version/Latest/PotPlayerSetup64.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "PotPlayerSetup64.exe");
        }

        private void buttonSetupVLC_Click(object sender, EventArgs e)
        {
            string linkDown = "https://get.videolan.org/vlc/3.0.23/win64/vlc-3.0.23-win64.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "vlc-3.0.23-win64.exe");
        }

        private void buttonSetupImageGlass_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/d2phap/ImageGlass/releases/download/9.5.0.515/ImageGlass_9.5.0.515_x64.msi";
            DownLinkEXEStartButton((Button)sender, linkDown, "ImageGlass_9.5.0.515_x64.msi");
        }

        private void buttonSetupIDM_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://www.internetdownloadmanager.com/download.html";
            string linkDown = string.Empty;

            string htmlCode = new System.Net.WebClient().DownloadString(linkUrl);

            foreach (var item in htmlCode.Split('\n'))
            {
                if (item.Contains("https://download.internetdownloadmanager.com"))
                {
                    linkDown = item.Replace("href=\"", "|").Replace("\">", "|").Split('|')[1];
                    break;
                }
            }
            if (string.IsNullOrEmpty(linkDown))
            {
                OpenBrowserUrl(linkUrl);
            }
            else
            {
                DownLinkEXEStartButton((Button)sender, linkDown, "idm-setup.exe");
            }
        }

        private void buttonSetupEverything_Click(object sender, EventArgs e)
        {
            string linkDown = "https://www.voidtools.com/Everything-1.4.1.1032.x64-Setup.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "Everything-1.4.1.1032.x64-Setup.exe");
        }

        private void buttonSetupCCleaner_Click(object sender, EventArgs e)
        {
            string ccFolder = @"C:\Program Files\CCleaner";
            if (!Directory.Exists(ccFolder)) Directory.CreateDirectory(ccFolder);

            string fileConfig = @"C:\Program Files\CCleaner\ccleaner.ini";
            string ccConfig = "[Options]\nFirstInstallDate=20260101\nRunICS=0\nBrandover=0\nSkipUAC=1\nLanguage=1066\nDAST=01/01/2026 01:00:00\nHomeScreen=2\nDefaultDetailedView=2\nAlphaLSLUT=1785221007\nAcqSrc=mmm_ccl_oth_007_745_m\nUpdateBackground=0\nDUGuid=f1a28e18-9bce-48dd-ab9e-51e47e4cfd59\nFTU=01/01/2026|4|1\nCookiesToSave=*.avast.com|*.ccleaner.com|*.ccleanercloud.com\nLatestICS=6.41.11567\nLLSR=01/01/2026 01:00:00\nCountryCode=VN\nLastCheckCountry=01/01/2026 01:00:00 PM\nSystemMonitoring=0\nBrowserMonitoring=0\nLNR=01/01/2026 01:00:00\nUpdateKey=01/01/2026 01:00:00 PM\nLastSUScan=01/01/2026 01:00:00\nNumOfOutdatedSoftware=12\nNumOfTotalSoftware=28\nOutdatedSoftwareCache=[]\nUpToDateSoftwareCache=[]\nWipeMFTFreeSpace=0\nAutoClose=1\nLastDriverScan=01/01/2026 01:00:00\nNumOfTotalDrivers=68\nNumOfOutdatedDrivers=17\nOutdatedDriversCache=[]\nUpToDateDriversCache=[]\nBCD=0,2,\nMonitoring=0\nLastPOScan=01/01/2026 01:00:00\nNumOfAwakeSoftware=14\nActiveProgramsToSleepCache=[]\nNumOfTotalPrograms=14\nWINDOW_LEFT=288\nWINDOW_TOP=76\nWINDOW_WIDTH=1024\nWINDOW_HEIGHT=708\nWINDOW_MAX=0\nWipeFreeSpaceDrives=\nUpdateCheck=0\nPrefsPrivacyShowOffers1stParty=0\nHelpImproveCCleaner=0\n(App)Google Chrome - Cookies=False\n(App)Mozilla - Cookies=False\nShowCleanWarning=False\nLastCleaned=01/01/2026 01:00:00\nShowGoogleChromeCleanWarning=False\nSTS=MP3ZI2MWQPQS4CUDPTUYC5UIPF3ZI55URE8VIDIKJTBWUTB7GAGSWVCDKS8YG7BUBWFE2S4WKN8VCP32GW3DENJSHA5A4CUWI3JV4NJSGA3C6QBXBWFFIVUDIN8VIDIKKTHEUS37HE3DEN3VG63DAN3YHA4VIP3ZGW6DAN2PBJKFITUVHW2A4CS\n(App)Windows Event Trace Logs=True\n(App)Windows Event Logs=True\n(App)Edge Chromium - Cookies=False\nDelayTemp=0\nHideWarnings=0\n";
            File.WriteAllText(fileConfig, ccConfig);

            string linkDown = "https://download.ccleaner.com/ccsetup641.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "ccsetup641.exe", "/S /L=1066"); // 1066 = Vietnamese
        }

        private void buttonSetupVisualStudioCommunity_Click(object sender, EventArgs e)
        {
            string linkDown = "https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Stable&version=VS18&source=VSLandingPage&passive=false&cid=2500";
            DownLinkEXEStartButton((Button)sender, linkDown, "VisualStudio.exe");
        }

        private void buttonSetupSublimeText_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://www.sublimetext.com/download_thanks?target=win-x64";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonSetupOBSStudio_Click(object sender, EventArgs e)
        {
            string linkDown = "https://cdn-fastly.obsproject.com/downloads/OBS-Studio-32.1.2-Windows-x64-Installer.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "OBS-Studio-32.1.2-Windows-x64-Installer.exe");
        }

        private void buttonSetupShareX_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/ShareX/ShareX/releases/download/v21.0.0/ShareX-21.0.0-setup-x64.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "ShareX-21.0.0-setup-x64.exe");
        }

        private void buttonSetupLightshot_Click(object sender, EventArgs e)
        {
            string linkDown = "https://app.prntscr.com/build/setup-lightshot.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "setup-lightshot.exe");
        }

        private void buttonSetupFastStoneCapture_Click(object sender, EventArgs e)
        {
            string linkDown = "https://www.faststonesoft.net/DN/FSCaptureSetup112.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "FSCaptureSetup112.exe");
        }

        private void buttonSetupTelegram_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://telegram.org/dl/desktop/win64";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonSetupZalo_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://zalo.me/download/zalo-pc?utm=90000";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonSetupWandriver_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://drive.google.com/drive/folders/1VhuGEa47zGyl1vv3Rn9uFeOS-rtC5p0d";
            linkUrl = "https://drive.google.com/drive/folders/0B4O7k-ah6irsX2xKc1FRT0ZfV0k?resourcekey=0-ayexa2DTAX_rr9QvcRMVgg";
            OpenBrowserUrl(linkUrl);
        }

        private void buttonSetupGoogleDrive_Click(object sender, EventArgs e)
        {
            string linkDown = "https://dl.google.com/drive-file-stream/GoogleDriveSetup.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "GoogleDriveSetup.exe");
        }

        private void buttonSetupVMwareWorkstation_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://drive.usercontent.google.com/download?id=1dF-dgOM1q0zSr099V2DIOrrVPL6lsHlB&export=download";
            OpenBrowserUrl(linkUrl);
        }

        #endregion CAI DAT PHAN MEM


        #region CAI DAT NHANH

        private void buttonOffRealTimeProtection_Click(object sender, EventArgs e)
        {
            try
            {
                Process startsoft = new Process();
                startsoft.StartInfo.FileName = "powershell.exe";
                startsoft.StartInfo.Arguments = "Set-MpPreference -DisableRealtimeMonitoring $true";
                startsoft.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                startsoft.Start();
                isRealProtection = true;
            }
            catch (Exception ex)
            {
                WriteMessage(ex.Message);
            }
        }

        private void buttonAddCmdRightMouse_Click(object sender, EventArgs e)
        {
            string regfile = Path.Combine(Path.GetTempPath(), "open-command-prompt-here.reg");
            string regText = "Windows Registry Editor Version 5.00\\n\\n[HKEY_CLASSES_ROOT\\Directory\\Background\\shell\\CommandPrompt]\\n@=\"Open Command Prompt Here\"\\n\"Icon\"=\"cmd.exe\"\\n\\n[HKEY_CLASSES_ROOT\\Directory\\Background\\shell\\CommandPrompt\\command]\\n@=\"cmd.exe /s /k pushd \\\"%V\\\"\"\\n\\n[HKEY_CLASSES_ROOT\\Directory\\shell\\CommandPrompt]\\n@=\"Open Command Prompt Here\"\\n\"Icon\"=\"cmd.exe\"\\n\\n[HKEY_CLASSES_ROOT\\Directory\\shell\\CommandPrompt\\command]\\n@=\"cmd.exe /s /k pushd \\\"%1\\\"\"";

            File.WriteAllText(regfile, regText);

            Process process = new Process();
            process.StartInfo.FileName = "reg.exe";
            process.StartInfo.Arguments = $"IMPORT \"{regfile}\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        private void buttonSetupFramework35Iso_Click(object sender, EventArgs e)
        {
            string[] disks = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string diskIso = string.Empty;

            foreach (string disk in disks)
            {
                string folderIso = $@"{disk}:\sources\sxs";
                if (Directory.Exists(folderIso))
                {
                    diskIso = disk;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(diskIso))
            {
                Process.Start("dism.exe", $@"/online /enable-feature /featurename:NetFX3 /All /Source:{diskIso}:\sources\sxs /LimitAccess");
            }
        }

        private void buttonSearchWifiDriver_Click(object sender, EventArgs e)
        {
            string wifiAdapter = string.Empty;
            // Lấy tất cả các giao diện mạng trên máy tính
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                // Kiểm tra nếu là card mạng không dây (Wi-Fi)
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (adapter.Name == "Wi-Fi")
                    {
                        wifiAdapter = adapter.Description;
                        break;
                    }
                }
            }
            Process.Start("chrome.exe", $"https://www.google.com/search?q={wifiAdapter.Replace(" ", "+")}+driver+download");
        }

        private void buttonApplyFavoriteSettings_Click(object sender, EventArgs e)
        {
            if (checkShowIconInDesktop.Checked)
            {
                // show ThisPC and Control panel in desktop
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 0 /f");
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}\" /t REG_DWORD /d 0 /f");
                // show ThisPC and Control panel in desktop
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\ClassicStartMenu\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 0 /f");
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\ClassicStartMenu\" /v \"{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}\" /t REG_DWORD /d 0 /f");
            }
            if (checkConfigExplorerAndQuickAccess.Checked)
            {
                // explorer mở mặc định vào This PC
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v LaunchTo /t REG_DWORD /d 1 /f");
                // tắt Recent files và Frequent folders trong Quick access
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\" /v ShowRecent /t REG_DWORD /d 0 /f");
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\" /v ShowFrequent /t REG_DWORD /d 0 /f");
                // hiển thị các file hệ thống
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ShowSuperHidden /t REG_DWORD /d 1 /f");
            }
            if (checkNeverCombineTaskbar.Checked)
            {
                // taskbar never combine
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v TaskbarGlomLevel /t REG_DWORD /d 2 /f");
            }
            if (checkConfigSearchAndCotana.Checked)
            {
                // turn off cotana
                reg("ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search\" /v AllowCortana /t REG_DWORD /d 0 /f");
                // show search icon
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Search\" /v SearchboxTaskbarMode /t REG_DWORD /d 1 /f");
                // Windows Search chỉ tìm trên máy tính, không tìm trên web (Bing)
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Windows\\Explorer\" /v DisableSearchBoxSuggestions /t REG_DWORD /d 1 /f");
            }
            if (checkShowAllIconOnTheTray.Checked)
            {
                // show all icon on the tray
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\" /v EnableAutoTray /t REG_DWORD /d 0 /f");
            }
            if (checkOffShowSuggestionsInStart.Checked)
            {
                // turn off Show suggestions occasionally in start
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v SystemPaneSuggestionsEnabled /t REG_DWORD /d 0 /f");
                reg("ADD \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager\" /v SubscribedContent-338388Enabled /t REG_DWORD /d 0 /f");
            }
            if (checkActivePowerHighPerformance.Checked)
            {
                // active power plan High Performance
                Process.Start("powercfg.exe", "/setactive SCHEME_MIN");
            }
            if (checkDisableSleepWhilePluggedIn.Checked)
            {
                // never sleep while plugged in
                Process.Start("powercfg.exe", "/change standby-timeout-ac 0");
            }
            if (checkDisableOneDrive.Checked)
            {
                // kill onedrive
                taskkill("/f /im OneDrive.exe");
                // remove startup onedrive
                reg("DELETE \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\" /v \"OneDrive\" /f");
                // disable onedrive
                reg("ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\OneDrive\" /v DisableFileSyncNGSC /t REG_DWORD /d 1 /f");
            }
            if (checkDisableUserAccountControl.Checked)
            {
                // disable UAC
                reg("ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v EnableLUA /t REG_DWORD /d 0 /f");
            }

            taskkill("/f /im explorer.exe");
            Process.Start("explorer.exe");
        }

        private void textChangePass_TextChanged(object sender, EventArgs e)
        {
            buttonChangePass.Enabled = true;
        }

        private void textChangeUser_TextChanged(object sender, EventArgs e)
        {
            buttonChangeUser.Enabled = true;
        }

        private void textChangePort_TextChanged(object sender, EventArgs e)
        {
            buttonChangePort.Enabled = true;
        }

        private void buttonChangePass_Click(object sender, EventArgs e)
        {
            string pass = textChangePass.Text;
            if (string.IsNullOrEmpty(pass))
            {
                return;
            }
            string user = Environment.UserName;
            net("user \"" + user + "\" \"" + pass + "\"");
            buttonChangePass.ForeColor = Color.Blue;
            Clipboard.SetText(pass);
            shutdown("/r /t 1800");
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            string newUser = textChangeUser.Text;
            if (string.IsNullOrEmpty(newUser))
            {
                return;
            }
            string curUser = Environment.UserName;
            wmic("useraccount where name='" + curUser + "' rename '" + newUser + "'");
            buttonChangeUser.ForeColor = Color.Blue;
            buttonChangePass.Enabled = false;
            textChangePass.Enabled = false;
            shutdown("/r /t 1800");
        }

        private void buttonChangePort_Click(object sender, EventArgs e)
        {
            int port = Convert.ToInt32(textChangePort.Text);
            reg("ADD \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP-Tcp\" /v PortNumber /t REG_DWORD /d " + port + " /f");
            netsh("advfirewall firewall add rule name=\"Remote-Port\" dir=in action=allow protocol=TCP localport=" + port);
            buttonChangePort.ForeColor = Color.Blue;
            shutdown("/r /t 1800");
        }

        private void buttonChangeTimezone_Click(object sender, EventArgs e)
        {
            string country = "";
            string state = "";
            string city = "";
            string ip = "";
            string timezone = "";
            string dataJson = new System.Net.WebClient().DownloadString("http://ip-api.com/json");
            string[] array = dataJson.Replace("{", "").Replace("}", "").Replace(",", "|").Split('|');
            foreach (var item in array)
            {
                string[] values = item.Trim().Replace("\"", "").Split(':');
                if (values[0] == "country")
                {
                    country = values[1];
                }
                if (values[0] == "regionName")
                {
                    state = values[1];
                }
                if (values[0] == "city")
                {
                    city = values[1];
                }
                if (values[0] == "timezone")
                {
                    timezone = values[1];
                }
                if (values[0] == "query")
                {
                    ip = values[1];
                }
            }
            foreach (var localize in timeZones)
            {
                if (localize[2] == timezone)
                {
                    tzutil(localize[0]); // change timezone
                    break;
                }
            }
            buttonChangeTimezone.ForeColor = Color.Blue;
            buttonChangeTimezone.Text = timezone;
        }

        private void checkActivePowerHighPerformance_CheckedChanged(object sender, EventArgs e)
        {
            if (checkActivePowerHighPerformance.Checked)
            {
                checkDisableSleepWhilePluggedIn.Checked = true;
            }
        }

        #endregion CAI DAT NHANH


        #region CONG CU TIEN ICH

        private void buttonWindowsUpdateBlocker_ClickAsync(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "windows-update-blocker", "Wub.exe", "Wub_x64.exe");
        }

        private void buttonDefenderControl_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "defender-control", "dControl.exe");
        }

        private void buttonDnsJumper_ClickAsync(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "dns-jumper", "DnsJumper.exe");
        }

        private void buttonUpdateTime_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "update-time", "UpdateTime.exe", "UpdateTime_x64.exe");
        }

        private void buttonBlueLifeHostsEditor_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "bluelife-host-editor", "hEdit.exe", "hEdit_x64.exe");
        }

        private void buttonEasyContextMenu_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "easy-context-menu", "EcMenu.exe", "EcMenu_x64.exe");
        }

        private void buttonEasyServiceOptimizer_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "easy-service-optimizer", "eso.exe");
        }

        private void buttonStoreAppsTool_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "store-apps-tool", "StoreAT.exe", "StoreAT_x64.exe");
        }

        private void buttonTempCleaner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "temp-cleaner", "TempCleaner.exe", "TempCleaner_x64.exe");
        }

        private void buttonRestartExplorer_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "restart-explorer", "Rexplorer.exe", "Rexplorer_x64.exe");
        }

        private void buttonWin11ClassicContextMenu_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "win11-classic-context-menu", "W11ClassicMenu.exe");
        }

        private void buttonRegistryWorkshop_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "registry-workshop", "RegWorkshop.exe", "RegWorkshopX64.exe");
        }

        private void buttonBkavShowHiddenFiles_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "bkav-show-hidden-files", "FixAttrb.exe");
        }

        private void buttonRecuva_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "ccleaner-recuva", "recuva.exe", "recuva64.exe");
        }

        private void buttonFidoScript_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://github.com/pbatard/Fido/raw/refs/heads/master/Fido.ps1";
            string scriptText = new System.Net.WebClient().DownloadString(linkUrl);

            string shellFile = Path.Combine(Path.GetTempPath(), "Fido.ps1");
            File.WriteAllText(shellFile, scriptText);

            Process.Start("powerShell.exe", $"-ExecutionPolicy Bypass -File {shellFile}");
        }

        private void buttonRufus_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/pbatard/rufus/releases/download/v4.15/rufus-4.15p.exe";
            DownLinkEXEStartButton((Button)sender, linkDown, "rufus-4.15p.exe");
        }

        private void buttonOfficeToolPlus_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/YerongAI/Office-Tool/releases/download/v11.5.7.0/Office_Tool_with_runtime_v11.5.7.0_x64.7z";
            DownLinkCompressionStartButton((Button)sender, linkDown, @"Office Tool\Office Tool Plus.exe");
        }

        #endregion CONG CU TIEN ICH


        #region CONG CU CHUYEN DUNG

        private void buttonCpuZ_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "cpu-z", "cpuz_x32.exe", "cpuz_x64.exe");
        }

        private void buttonGpuZ_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "gpu-z", "GPU-Z.exe", "", "gpuz.reg");
        }

        private void buttonHWiNFO_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "hwinfo", "HWiNFO32.exe", "HWiNFO64.exe");
        }

        private void buttonSpeccy_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "speccy", "Speccy.exe", "Speccy64.exe");
        }

        private void buttonCrystalDiskInfo_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "crystal-disk-info", "DiskInfo32.exe", "DiskInfo64.exe");
        }

        private void buttonKeyboardTest_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "keyboard-test", "KeyboardTest.exe");
        }

        private void buttonIsMyLcdOK_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "is-my-lcd-ok", "IsMyLcdOK.exe");
        }

        private void buttontLCDtest_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "t-lcd-test", "tLCDtest.exe");
        }

        private void buttonMicrosoftActivationScriptsOnline_Click(object sender, EventArgs e)
        {
            Process.Start("powerShell.exe", "irm https://get.activated.win | iex");
        }

        private void buttonMicrosoftActivationScriptsOffline_Click(object sender, EventArgs e)
        {
            string linkUrl = "https://github.com/massgravel/Microsoft-Activation-Scripts/raw/refs/heads/master/MAS/All-In-One-Version-KL/MAS_AIO.cmd";
            string scriptText = new System.Net.WebClient().DownloadString(linkUrl);

            string batchFile = Path.Combine(Path.GetTempPath(), "MAS.bat");
            File.WriteAllText(batchFile, scriptText);

            Process.Start(batchFile);
        }

        private void buttonNirsoftChromePassword_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-chrome-password", "ChromePass.exe");
            timerNirsoftStart.Enabled = true;
        }

        private void buttonNirsoftIEPasswords_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-ie-passwords", "iepv.exe");
            timerNirsoftStart.Enabled = true;
        }

        private void buttonNirsoftPasswordFirefox_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-password-firefox", "PasswordFox.exe");
            timerNirsoftStart.Enabled = true;
        }

        private void buttonNirsoftWebBrowserPassword_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-web-browser-password", "WebBrowserPassView.exe");
            timerNirsoftStart.Enabled = true;
        }

        private void buttonNirsoftPasswordRemoteDesktop_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-password-remote-desktop", "rdpv.exe");
        }

        private void buttonNirsoftWirelessKeyView_Click(object sender, EventArgs e)
        {
            if (!isRealProtection)
            {
                WriteMessage("Real-time Protection");
                return;
            }
            DownGitlabZipStartButton((Button)sender, "nirsoft-wireless-key-view", "WirelessKeyView.exe");
        }

        private void buttonNirsoftWirelessNetworkWatcher_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartButton((Button)sender, "nirsoft-wireless-network-watcher", "WNetWatcher.exe");
        }

        private void buttonActivateAIOTools_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/disksave/software/raw/refs/heads/main/activate-aio-tools-by-savio.7z";
            DownLinkCompressionStartButton((Button)sender, linkDown, "Activate-AIO-Tools-v3.1.3-by-Savio.bat");
        }

        private void buttonWindowsActivateForVPS_Click(object sender, EventArgs e)
        {
            string linkDown = "https://github.com/disksave/software/raw/refs/heads/main/windows-activate.7z";
            DownLinkCompressionStartButton((Button)sender, linkDown, "WindowsActivate.bat");
        }

        private void timerNirsoftStart_Tick(object sender, EventArgs e)
        {
            string chromeFile = Path.Combine(Path.GetTempPath(), "nirsoft-chrome-password", "report.html");
            string ieFile = Path.Combine(Path.GetTempPath(), "nirsoft-ie-passwords", "report.html");
            string firefoxFile = Path.Combine(Path.GetTempPath(), "nirsoft-password-firefox", "report.html");
            string browserFile = Path.Combine(Path.GetTempPath(), "nirsoft-web-browser-password", "report.html");
            string timeFolder = Path.Combine(Application.StartupPath, $"{Environment.MachineName}-{DateTime.Now.ToString("yyyy-MM-dd")}");
            if (!Directory.Exists(timeFolder))
            {
                Directory.CreateDirectory(timeFolder);
            }
            if (File.Exists(chromeFile))
            {
                string htmlFile = Path.Combine(timeFolder, "chrome.html");
                File.Copy(chromeFile, htmlFile, true);
                buttonNirsoftChromePassword.ForeColor = Color.DarkBlue;
                timerNirsoftStart.Enabled = false;
            }
            if (File.Exists(ieFile))
            {
                string htmlFile = Path.Combine(timeFolder, "ie.html");
                File.Copy(ieFile, htmlFile, true);
                buttonNirsoftIEPasswords.ForeColor = Color.DarkBlue;
                timerNirsoftStart.Enabled = false;
            }
            if (File.Exists(firefoxFile))
            {
                string htmlFile = Path.Combine(timeFolder, "firefox.html");
                File.Copy(firefoxFile, htmlFile, true);
                buttonNirsoftPasswordFirefox.ForeColor = Color.DarkBlue;
                timerNirsoftStart.Enabled = false;
            }
            if (File.Exists(browserFile))
            {
                string htmlFile = Path.Combine(timeFolder, "browser.html");
                File.Copy(browserFile, htmlFile, true);
                buttonNirsoftWebBrowserPassword.ForeColor = Color.DarkBlue;
                timerNirsoftStart.Enabled = false;
            }
        }

        #endregion CONG CU CHUYEN DUNG


        #region HOP CONG CU

        private void linkIObitClonedFilesScanner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-cloned-files-scanner", "ClonedFilesScanner.exe");
        }

        private void linkIObitContextMenuManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-context-menu-manager", "ContextMenuManager.exe");
        }

        private void linkIObitDefaultProgram_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-default-program", "DefaultProgram.exe");
        }

        private void linkIObitDiskCleaner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-disk-cleaner", "DiskCleaner.exe");
        }

        private void linkIObitDiskDoctor_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-disk-doctor", "DiskDoctor.exe");
        }

        private void linkIObitDiskExplorer_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-disk-explorer", "DiskExplorer.exe");
        }

        private void linkIObitDriverManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-driver-manager", "DriverManager.exe");
        }

        private void linkIObitDuplicateFileFinder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-duplicate-file-finder", "DuplicateFileFinder.exe");
        }

        private void linkIObitEmptyFolderScanner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-empty-folder-scanner", "EmptyFolderScanner.exe");
        }

        private void linkIObitFileShredder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-file-shredder", "FileShredder.exe");
        }

        private void linkIObitIEHelper_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-ie-helper", "IEHelper.exe");
        }

        private void linkIObitInternetBooster_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-internet-booster", "InternetBooster.exe");
        }

        private void linkIObitLargeFileFinder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-large-file-finder", "LargeFileFinder.exe");
        }

        private void linkIObitMonitor8_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-monitor-8", "Monitor.exe");
        }

        private void linkIObitMonitor19_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-monitor-19", "Monitor.exe");
        }

        private void linkIObitMyWin10_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-my-win10", "MyWin10.exe");
        }

        private void linkIObitProcessManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-process-manager", "ProcessManager.exe");
        }

        private void linkIObitProgramDeactivator_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-program-deactivator", "ProgramDeactivator.exe");
        }

        private void linkIObitRegistryCleaner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-registry-cleaner", "RegistryCleaner.exe");
        }

        private void linkIObitRegistryDefrag_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-registry-defrag", "RegistryDefrag.exe");
        }

        private void linkIObitReinforce_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-reinforce", "Reinforce.exe");
        }

        private void linkIObitRescueCenter_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-rescue-center", "RescueCenter.exe");
        }

        private void linkIObitScreenShot_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-screen-shot", "ScreenShot.exe");
        }

        private void linkIObitShortcutFixer_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-shortcut-fixer", "ShortcutFixer.exe");
        }

        private void linkIObitSmartRAM_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-smart-ram", "SmartRAM.exe");
        }

        private void linkIObitStartupManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-startup-manager", "StartupManager.exe");
        }

        private void linkIObitSystemControl_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-system-control", "SystemControl.exe");
        }

        private void linkIObitSystemInformation_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-system-information", "SystemInformation.exe");
        }

        private void linkIObitUndelete_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-undelete", "Undelete.exe");
        }

        private void linkIObitWinFix_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "iobit-win-fix", "WinFix.exe");
        }

        private void linkGlaryCheckDisk_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "CheckDisk.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryContextMenuManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "cmm.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryDiskAnalysis_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "DiskAnalysis.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryDiskCleaner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "DiskCleaner.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryDiskDefrag_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "DiskDefrag.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryDriverBackup_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "DriverBackup.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryDuplicateFileFinder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "dupefinder.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryEmptyFolderFinder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "EmptyFolderFinder.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryEncryptExe_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "EncryptExe.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryFileEncrypt_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "fileencrypt.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryFileSplitter_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "filesplitter.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryFileUndelete_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "FileUndelete.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryIEHelper_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "iehelper.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryJoinExe_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "joinExe.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryMemoryDefrag_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "memdefrag.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryProcessManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "procmgr.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryQuickSearch_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "QuickSearch.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryRegistryDefrag_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "regdefrag.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryRegistryCleaner_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "RegistryCleaner.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryRestoreCenter_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "RestoreCenter.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryShortcutFixer_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "ShortcutFixer.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryFileShredder_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "shredder.exe", "RegkeyLifetime.reg");
        }

        private void linkGlarySoftwareUpdate_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "SoftwareUpdate.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryStartupManager_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "StartupManager.exe", "RegkeyLifetime.reg");
        }

        private void linkGlarySystemInformation_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "sysinfo.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryTracksEraser_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "TracksEraser.exe", "RegkeyLifetime.reg");
        }

        private void linkGlaryUninstaler_Click(object sender, EventArgs e)
        {
            DownGitlabZipStartLabel((Label)sender, "glary-utilities", "Uninstaler.exe", "RegkeyLifetime.reg");
        }

        #endregion HOP CONG CU

        private List<string[]> timeZones = new List<string[]>()
        {
            new string[] {"Azores Standard Time", "(GMT-01:00) Azores", "Atlantic/Azores"},
            new string[] {"Cape Verde Standard Time", "(GMT-01:00) Cape Verde Islands", "Atlantic/Cape_Verde"},
            new string[] {"Mid-Atlantic Standard Time", "(GMT-02:00) Mid-Atlantic", "Atlantic/South_Georgia"},
            new string[] {"E. South America Standard Time", "(GMT-03:00) Brasilia", "America/Sao_Paulo"},
            new string[] {"SA Eastern Standard Time", "(GMT-03:00) Buenos Aires, Georgetown", "America/Argentina/Buenos_Aires"},
            new string[] {"Greenland Standard Time", "(GMT-03:00) Greenland", "America/Godthab"},
            new string[] {"Newfoundland Standard Time", "(GMT-03:30) Newfoundland and Labrador", "America/St_Johns"},
            new string[] {"Atlantic Standard Time", "(GMT-04:00) Atlantic Time (Canada)", "America/Halifax"},
            new string[] {"SA Western Standard Time", "(GMT-04:00) Caracas, La Paz", "America/La_Paz"},
            new string[] {"Central Brazilian Standard Time", "(GMT-04:00) Manaus", "America/Cuiaba"},
            new string[] {"Pacific SA Standard Time", "(GMT-04:00) Santiago", "America/Santiago"},
            new string[] {"SA Pacific Standard Time", "(GMT-05:00) Bogota, Lima, Quito", "America/Bogota"},
            new string[] {"Eastern Standard Time", "(GMT-05:00) Eastern Time (US and Canada)", "America/New_York"},
            new string[] {"US Eastern Standard Time", "(GMT-05:00) Indiana (East)", "America/Indiana/Indianapolis"},
            new string[] {"Central America Standard Time", "(GMT-06:00) Central America", "America/Costa_Rica"},
            new string[] {"Central Standard Time", "(GMT-06:00) Central Time (US and Canada)", "America/Chicago"},
            new string[] {"Central Standard Time (Mexico)", "(GMT-06:00) Guadalajara, Mexico City, Monterrey", "America/Monterrey"},
            new string[] {"Canada Central Standard Time", "(GMT-06:00) Saskatchewan", "America/Edmonton"},
            new string[] {"US Mountain Standard Time", "(GMT-07:00) Arizona", "America/Phoenix"},
            new string[] {"Mountain Standard Time (Mexico)", "(GMT-07:00) Chihuahua, La Paz, Mazatlan", "America/Chihuahua"},
            new string[] {"Mountain Standard Time", "(GMT-07:00) Mountain Time (US and Canada)", "America/Denver"},
            new string[] {"Pacific Standard Time", "(GMT-08:00) Pacific Time (US and Canada); Tijuana", "America/Tijuana"},
            new string[] {"Alaskan Standard Time", "(GMT-09:00) Alaska", "America/Anchorage"},
            new string[] {"Hawaiian Standard Time", "(GMT-10:00) Hawaii", "Pacific/Honolulu"},
            new string[] {"Samoa Standard Time", "(GMT-11:00) Midway Island, Samoa", "Pacific/Apia"},
            new string[] {"Greenwich Standard Time", "(GMT) Casablanca, Monrovia", "Africa/Monrovia"},
            new string[] {"GMT Standard Time", "(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London", "Europe/London"},
            new string[] {"W. Europe Standard Time", "(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "Europe/Berlin"},
            new string[] {"Central Europe Standard Time", "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "Europe/Belgrade"},
            new string[] {"Romance Standard Time", "(GMT+01:00) Brussels, Copenhagen, Madrid, Paris", "Europe/Paris"},
            new string[] {"Central European Standard Time", "(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb", "Europe/Belgrade"},
            new string[] {"W. Central Africa Standard Time", "(GMT+01:00) West Central Africa", "Africa/Lagos"},
            new string[] {"GTB Standard Time", "(GMT+02:00) Athens, Bucharest, Istanbul", "Europe/Istanbul"},
            new string[] {"Egypt Standard Time", "(GMT+02:00) Cairo", "Africa/Cairo"},
            new string[] {"South Africa Standard Time", "(GMT+02:00) Harare, Pretoria", "Africa/Harare"},
            new string[] {"FLE Standard Time", "(GMT+02:00) Helsinki, Kiev, Riga, Sofia, Tallinn, Vilnius", "Europe/Riga"},
            new string[] {"Israel Standard Time", "(GMT+02:00) Jerusalem", "Asia/Jerusalem"},
            new string[] {"E. Europe Standard Time", "(GMT+02:00) Minsk", "Europe/Minsk"},
            new string[] {"Namibia Standard Time", "(GMT+02:00) Windhoek", "Africa/Windhoek"},
            new string[] {"Arabic Standard Time", "(GMT+03:00) Baghdad", "Asia/Baghdad"},
            new string[] {"Arab Standard Time", "(GMT+03:00) Kuwait, Riyadh", "Asia/Kuwait"},
            new string[] {"Russian Standard Time", "(GMT+03:00) Moscow, St. Petersburg, Volgograd", "Europe/Moscow"},
            new string[] {"E. Africa Standard Time", "(GMT+03:00) Nairobi", "Africa/Nairobi"},
            new string[] {"Iran Standard Time", "(GMT+03:30) Tehran", "Asia/Tehran"},
            new string[] {"Arabian Standard Time", "(GMT+04:00) Abu Dhabi, Muscat", "Asia/Muscat"},
            new string[] {"Azerbaijan Standard Time", "(GMT+04:00) Baku", "Asia/Baku"},
            new string[] {"Georgian Standard Time", "(GMT+04:00) Tblisi", "Asia/Tbilisi"},
            new string[] {"Caucasus Standard Time", "(GMT+04:00) Yerevan", "Asia/Yerevan"},
            new string[] {"Afghanistan Standard Time", "(GMT+04:30) Kabul", "Asia/Kabul"},
            new string[] {"Ekaterinburg Standard Time", "(GMT+05:00) Ekaterinburg", "Asia/Yekaterinburg"},
            new string[] {"West Asia Standard Time", "(GMT+05:00) Islamabad, Karachi, Tashkent", "Asia/Tashkent"},
            new string[] {"India Standard Time", "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi", "Asia/Calcutta"},
            new string[] {"Nepal Standard Time", "(GMT+05:45) Kathmandu", "Asia/Kathmandu"},
            new string[] {"N. Central Asia Standard Time", "(GMT+06:00) Almaty, Novosibirsk", "Asia/Novosibirsk"},
            new string[] {"Central Asia Standard Time", "(GMT+06:00) Astana, Dhaka", "Asia/Almaty"},
            new string[] {"Sri Lanka Standard Time", "(GMT+06:00) Sri Jayawardenepura", "Asia/Colombo"},
            new string[] {"Myanmar Standard Time", "(GMT+06:30) Yangon (Rangoon)", "Asia/Rangoon"},
            new string[] {"SE Asia Standard Time", "(GMT+07:00) Bangkok, Hanoi, Jakarta", "Asia/Bangkok"},
            new string[] {"North Asia Standard Time", "(GMT+07:00) Krasnoyarsk", "Asia/Krasnoyarsk"},
            new string[] {"China Standard Time", "(GMT+08:00) Beijing, Chongqing, Hong Kong SAR, Urumqi", "Asia/Shanghai"},
            new string[] {"North Asia East Standard Time", "(GMT+08:00) Irkutsk, Ulaanbaatar", "Asia/Irkutsk"},
            new string[] {"Singapore Standard Time", "(GMT+08:00) Kuala Lumpur, Singapore", "Asia/Singapore"},
            new string[] {"W. Australia Standard Time", "(GMT+08:00) Perth", "Australia/Perth"},
            new string[] {"Taipei Standard Time", "(GMT+08:00) Taipei", "Asia/Taipei"},
            new string[] {"Tokyo Standard Time", "(GMT+09:00) Osaka, Sapporo, Tokyo", "Asia/Tokyo"},
            new string[] {"Korea Standard Time", "(GMT+09:00) Seoul", "Asia/Seoul"},
            new string[] {"Yakutsk Standard Time", "(GMT+09:00) Yakutsk", "Asia/Yakutsk"},
            new string[] {"Cen. Australia Standard Time", "(GMT+09:30) Adelaide", "Australia/Adelaide"},
            new string[] {"AUS Central Standard Time", "(GMT+09:30) Darwin", "Australia/Darwin"},
            new string[] {"E. Australia Standard Time", "(GMT+10:00) Brisbane", "Australia/Brisbane"},
            new string[] {"AUS Eastern Standard Time", "(GMT+10:00) Canberra, Melbourne, Sydney", "Australia/Sydney"},
            new string[] {"West Pacific Standard Time", "(GMT+10:00) Guam, Port Moresby", "Pacific/Guam"},
            new string[] {"Tasmania Standard Time", "(GMT+10:00) Hobart", "Australia/Hobart"},
            new string[] {"Vladivostok Standard Time", "(GMT+10:00) Vladivostok", "Asia/Vladivostok"},
            new string[] {"Central Pacific Standard Time", "(GMT+11:00) Magadan, Solomon Islands, New Caledonia", "Pacific/Guadalcanal"},
            new string[] {"New Zealand Standard Time", "(GMT+12:00) Auckland, Wellington", "Pacific/Auckland"},
            new string[] {"Fiji Standard Time", "(GMT+12:00) Fiji Islands, Kamchatka, Marshall Islands", "Pacific/Fiji"},
            new string[] {"Tonga Standard Time", "(GMT+13:00) Nuku'alofa", "Pacific/Tongatapu"}
        };

    }
}
