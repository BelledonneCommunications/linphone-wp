﻿/*
AccountSettings.xaml.cs
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

using Linphone.Core;
using Linphone.Model;
using Linphone.Resources;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Linphone.Views
{
    /// <summary>
    /// Page used to display SIP account settings
    /// </summary>
    public partial class AccountSettings : BasePage
    {
        private SIPAccountSettingsManager _settings = new SIPAccountSettingsManager();

        private bool saveSettingsOnLeave = true;
        private bool linphoneAccount = false;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AccountSettings()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            _settings.Load();
            Username.Text = _settings.Username;
            UserId.Text = _settings.UserId;
            Password.Password = _settings.Password;
            Domain.Text = _settings.Domain;
            Proxy.Text = _settings.Proxy;
            OutboundProxy.IsChecked = _settings.OutboundProxy;
            DisplayName.Text = _settings.DisplayName;
            Expires.Text = _settings.Expires;

            List<string> transports = new List<string>
            {
                AppResources.TransportUDP,
                AppResources.TransportTCP,
                AppResources.TransportTLS
            };
            Transport.ItemsSource = transports;
            Transport.SelectedItem = _settings.Transport;

            AVPF.IsChecked = _settings.AVPF;
        }

        private void Save()
        {
            if (Domain.Text.Contains(":"))
            {
                if (Proxy.Text.Length == 0)
                {
                    Proxy.Text = Domain.Text;
                }
                Domain.Text = Domain.Text.Split(':')[0];
            }

            _settings.Username = Username.Text;
            _settings.UserId = UserId.Text;
            _settings.Password = Password.Password;
            _settings.Domain = Domain.Text;
            _settings.Proxy = Proxy.Text;
            _settings.OutboundProxy = OutboundProxy.IsChecked;
            _settings.DisplayName = DisplayName.Text;
            _settings.Transport = Transport.SelectedItem.ToString();
            _settings.Expires = Expires.Text;
            _settings.AVPF = AVPF.IsChecked;
            _settings.Save();

            if (linphoneAccount)
            {
                NetworkSettingsManager networkSettings = new NetworkSettingsManager();
                networkSettings.Load();
                networkSettings.MEncryption = networkSettings.EnumToMediaEncryption[MediaEncryption.SRTP];
                networkSettings.FWPolicy = networkSettings.EnumToFirewallPolicy[FirewallPolicy.UseIce];
                networkSettings.StunServer = "stun.linphone.org";
                networkSettings.Save();
            }
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
            Domain.Text = "sip.linphone.org";
            Transport.SelectedItem = AppResources.TransportTLS;
            Proxy.Text = "sip.linphone.org:5223";
            OutboundProxy.IsChecked = true;
            Expires.Text = "28800";
            AVPF.IsChecked = true;
            linphoneAccount = true;
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

        private void Username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UserId.Focus();
        }

        private void UserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Password.Focus();
        }

        private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Domain.Focus();
        }

        private void Expires_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DisplayName.Focus();
        }

        private void Domain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Proxy.Focus();
        }

        private void Proxy_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Expires.Focus();
        }
    }
}