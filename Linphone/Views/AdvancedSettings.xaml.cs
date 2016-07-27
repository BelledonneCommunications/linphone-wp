﻿/*
AdvancedSettings.xaml.cs
Copyright (C) 2015  Belledonne Communications, Grenoble, France
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using Linphone.Model;
using Linphone.Resources;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Navigation;
using Linphone.Agents;
using Linphone.Core;

namespace Linphone.Views
{
    /// <summary>
    ///  Page displaying advanced settings (such as Tunnel, DTMFs, ...)
    /// </summary>
    public partial class AdvancedSettings : BasePage
    {
        private CallSettingsManager _callSettings = new CallSettingsManager();
        private NetworkSettingsManager _networkSettings = new NetworkSettingsManager();
        private ChatSettingsManager _chatSettings = new ChatSettingsManager();
        private ApplicationSettingsManager _settings = new ApplicationSettingsManager();
        private bool saveSettingsOnLeave = true;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AdvancedSettings()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            _callSettings.Load();
            _networkSettings.Load();
            _chatSettings.Load();
            _settings.Load();

            rfc2833.IsChecked = _callSettings.SendDTFMsRFC2833;
            sipInfo.IsChecked = _callSettings.SendDTFMsSIPInfo;
            vibrator.IsChecked = _chatSettings.VibrateOnIncomingMessage;
            resizeDown.IsChecked = _chatSettings.ScaleDownSentPictures;

            List<string> mediaEncryptions = new List<string>
            {
                AppResources.MediaEncryptionNone,
                AppResources.MediaEncryptionSRTP,
            };
            MediaEncryption.ItemsSource = mediaEncryptions;
            MediaEncryption.SelectedItem = _networkSettings.MEncryption;

            List<string> firewallPolicies = new List<string>
            {
                AppResources.FirewallPolicyNone,
                AppResources.FirewallPolicyStun,
                AppResources.FirewallPolicyIce
            };
            FirewallPolicy.ItemsSource = firewallPolicies;
            FirewallPolicy.SelectedItem = _networkSettings.FWPolicy;

            Stun.Text = _networkSettings.StunServer;

            List<string> tunnelModes = new List<string>
            {
                AppResources.TunnelModeDisabled,
                AppResources.TunnelMode3GOnly,
                AppResources.TunnelModeAlways,
                AppResources.TunnelModeAuto
            };
            tunnelMode.ItemsSource = tunnelModes;
            tunnelMode.SelectedItem = _networkSettings.TunnelMode;
            tunnelPort.Text = _networkSettings.TunnelPort;
            tunnelServer.Text = _networkSettings.TunnelServer;

            TunnelPanel.Visibility = LinphoneManager.Instance.LinphoneCore.TunnelAvailable && Customs.IsTunnelEnabled ? Visibility.Visible : Visibility.Collapsed; //Hidden properties for now

            Debug.IsChecked = _settings.DebugEnabled;
            SendLogs.IsEnabled = _settings.DebugEnabled;
            ResetLogs.IsEnabled = _settings.DebugEnabled;
        }

        private void Save()
        {
            _callSettings.SendDTFMsRFC2833 = rfc2833.IsChecked;
            _callSettings.SendDTFMsSIPInfo = sipInfo.IsChecked;
            _callSettings.Save();

            _networkSettings.MEncryption = MediaEncryption.SelectedItem.ToString();
            _networkSettings.FWPolicy = FirewallPolicy.SelectedItem.ToString();
            _networkSettings.StunServer = Stun.Text;
            _networkSettings.TunnelMode = tunnelMode.SelectedItem.ToString();
            _networkSettings.TunnelServer = tunnelServer.Text;
            _networkSettings.TunnelPort = tunnelPort.Text;
            _networkSettings.Save();

            _chatSettings.VibrateOnIncomingMessage = vibrator.IsChecked;
            _chatSettings.ScaleDownSentPictures = resizeDown.IsChecked;
            _chatSettings.Save();

            _settings.DebugEnabled = (bool)Debug.IsChecked;
            _settings.Save();

            LinphoneManager.Instance.ConfigureLogger();
        }

        /// <summary>
        /// Method called when the page is displayed.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            saveSettingsOnLeave = true;
        }

        /// <summary>
        /// Method called when the page is hidden.
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs nee)
        {
            base.OnNavigatedFrom(nee);
            LinphoneManager.Instance.LogUploadProgressIndicationEH -= LogUploadProgressIndication;
            BugReportUploadPopup.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Method called when the user is navigation away from this page
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (saveSettingsOnLeave)
            {
                Save();
            }
            base.OnNavigatingFrom(e);
        }

        private void cancel_Click_1(object sender, EventArgs e)
        {
            saveSettingsOnLeave = false;
            NavigationService.GoBack();
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void linphone_Click_1(object sender, EventArgs e)
        {
            MediaEncryption.SelectedItem = AppResources.MediaEncryptionSRTP;
            FirewallPolicy.SelectedItem = AppResources.FirewallPolicyIce;
            Stun.Text = "stun.linphone.org";
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton appBarSave = new ApplicationBarIconButton(new Uri("/Assets/AppBar/save.png", UriKind.Relative));
            appBarSave.Text = AppResources.SaveSettings;
            ApplicationBar.Buttons.Add(appBarSave);
            appBarSave.Click += save_Click_1;

            ApplicationBarIconButton appBarCancel = new ApplicationBarIconButton(new Uri("/Assets/AppBar/cancel.png", UriKind.Relative));
            appBarCancel.Text = AppResources.CancelChanges;
            ApplicationBar.Buttons.Add(appBarCancel);
            appBarCancel.Click += cancel_Click_1;

            ApplicationBarIconButton appBarLinphoneValues = new ApplicationBarIconButton(new Uri("/Assets/AppBar/linphone.png", UriKind.Relative));
            appBarLinphoneValues.Text = AppResources.LinphoneValues;
            ApplicationBar.Buttons.Add(appBarLinphoneValues);
            appBarLinphoneValues.Click += linphone_Click_1;
        }

        private void LogUploadProgressIndication(int offset, int total)
        {
            BaseModel.UIDispatcher.BeginInvoke(() =>
            {
                BugReportUploadProgressBar.Maximum = total;
                if (offset <= total)
                {
                    BugReportUploadProgressBar.Value = offset;
                }
            });
        }
        
        private void SendLogs_Click(object sender, RoutedEventArgs e)
        {
            saveSettingsOnLeave = false;
            BugReportUploadPopup.Visibility = Visibility.Visible;
            LinphoneManager.Instance.LogUploadProgressIndicationEH += LogUploadProgressIndication;
            LinphoneManager.Instance.LinphoneCore.UploadLogCollection();
        }

        private void Debug_Checked(object sender, RoutedEventArgs e)
        {
            _settings.LogLevel = OutputTraceLevel.Message;
            SendLogs.IsEnabled = true;
            ResetLogs.IsEnabled = true;
        }

        private void Debug_Unchecked(object sender, RoutedEventArgs e)
        {
            _settings.LogLevel = OutputTraceLevel.None;
            SendLogs.IsEnabled = false;
            ResetLogs.IsEnabled = false;
        }

        private void ResetLogs_Click(object sender, RoutedEventArgs e)
        {
            LinphoneManager.Instance.LinphoneCoreFactory.ResetLogCollection();
        }
    }
}