﻿/*
AudioSettings.xaml.cs
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

using Linphone.Agents;
using Linphone.Core;
using Linphone.Model;
using Linphone.Resources;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Navigation;

namespace Linphone.Views
{
    /// <summary>
    /// Page displaying the audio settings and the audio codecs to let the user enable/disable them.
    /// </summary>
    public partial class AudioSettings : BasePage, EchoCalibratorListener
    {
        private CodecsSettingsManager _settings = new CodecsSettingsManager();
        private bool saveSettingsOnLeave = true;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AudioSettings()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            _settings.Load();
            AMRNB.IsChecked = _settings.AMRNB;
            AMRWB.IsChecked = _settings.AMRWB;
            Speex16.IsChecked = _settings.Speex16;
            Speex8.IsChecked = _settings.Speex8;
            PCMU.IsChecked = _settings.PCMU;
            PCMA.IsChecked = _settings.PCMA;
            G722.IsChecked = _settings.G722;
            G729.IsChecked = _settings.G729 && Customs.EnableG729;
            G729.IsEnabled = Customs.EnableG729;
            ILBC.IsChecked = _settings.ILBC;
            SILK16.IsChecked = _settings.SILK16;
            GSM.IsChecked = _settings.GSM;
            OPUS.IsChecked = _settings.OPUS;
            ISAC.IsChecked = _settings.Isac;
        }

        /// <summary>
        /// Method called when the page is displayed.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LinphoneManager.Instance.ECListener = this;
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

        private void Save()
        {
            _settings.AMRNB = ToBool(AMRNB.IsChecked);
            _settings.AMRWB = ToBool(AMRWB.IsChecked);
            _settings.Speex16 = ToBool(Speex16.IsChecked);
            _settings.Speex8 = ToBool(Speex8.IsChecked);
            _settings.PCMU = ToBool(PCMU.IsChecked);
            _settings.PCMA = ToBool(PCMA.IsChecked);
            _settings.G722 = ToBool(G722.IsChecked);
            _settings.G729 = ToBool(G729.IsChecked) && Customs.EnableG729;
            _settings.ILBC = ToBool(ILBC.IsChecked);
            _settings.SILK16 = ToBool(SILK16.IsChecked);
            _settings.GSM = ToBool(GSM.IsChecked);
            _settings.OPUS = ToBool(OPUS.IsChecked);
            _settings.Isac = ToBool(ISAC.IsChecked);
            _settings.Save();
        }

        private void cancel_Click_1(object sender, EventArgs e)
        {
            saveSettingsOnLeave = false;
            NavigationService.GoBack();
        }

        private bool ToBool(bool? enabled)
        {
            if (!enabled.HasValue) enabled = false;
            return (bool)enabled;
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ECCalibratorButton_Click_1(object sender, EventArgs e)
        {
            ECCalibratorButton.IsEnabled = false;
            ECCalibratorStatusButton.Content = AppResources.ECCalibrationInProgress;
            LinphoneManager.Instance.LinphoneCore.StartEchoCalibration();
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
        }

        /// <summary>
        /// Actualises the echo calibrator listener when the pages changes.
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs nea)
        {
            LinphoneManager.Instance.ECListener = null;
        }

        /// <summary>
        /// Called when the echo calibrator returns its status.
        /// </summary>
        /// <param name="status">The status of the echo canceller calibrator</param>
        /// <param name="delayMs">The echo delay in milliseconds if the status is EcCalibratorStatus.Done</param>
        public void ECStatusNotified(EcCalibratorStatus status, int delayMs)
        {
            BaseModel.UIDispatcher.BeginInvoke(() =>
            {
                if (status == EcCalibratorStatus.Done)
                {
                    ECCalibratorStatusButton.Content = String.Format(AppResources.ECCalibrationDone, delayMs);
                }
                else if (status == EcCalibratorStatus.DoneNoEcho)
                {
                    ECCalibratorStatusButton.Content = AppResources.ECCalibrationDoneNoEcho;
                }
                else if (status == EcCalibratorStatus.Failed)
                {
                    ECCalibratorStatusButton.Content = AppResources.ECCalibrationFailed;
                }
                else if (status == EcCalibratorStatus.InProgress)
                {
                    ECCalibratorStatusButton.Content = AppResources.ECCalibrationInProgress;
                }
                ECCalibratorButton.IsEnabled = true;
            });
        }
    }
}