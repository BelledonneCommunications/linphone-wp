﻿/*
IncomingChatBubble.xaml.cs
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Linphone.Model;
using Linphone.Views;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;
using Linphone.Resources;
using Linphone.Core;

namespace Linphone.Controls
{
    /// <summary>
    /// Control to display received chat messages.
    /// </summary>
    public partial class IncomingChatBubble : ChatBubble
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        public IncomingChatBubble(LinphoneChatMessage message) :
            base (message)
        {
            InitializeComponent();
            Timestamp.Text = HumanFriendlyTimeStamp;

            string fileName = message.FileTransferName;
            string filePath = message.AppData;
            bool isImageMessage = fileName != null && fileName.Length > 0;
            if (isImageMessage)
            {
                Message.Visibility = Visibility.Collapsed;
                Copy.Visibility = Visibility.Collapsed;
                if (filePath != null && filePath.Length > 0)
                {
                    // Image already downloaded
                    Image.Visibility = Visibility.Visible;
                    Save.Visibility = Visibility.Visible;

                    BitmapImage image = Utils.ReadImageFromIsolatedStorage(filePath);
                    Image.Source = image;
                }
                else
                {
                    // Image needs to be downloaded
                    Download.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Message.Visibility = Visibility.Visible;
                Image.Visibility = Visibility.Collapsed;
                Message.Blocks.Add(TextWithLinks);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageDeleted != null)
            {
                MessageDeleted(this, ChatMessage);
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ChatMessage.Text);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool result = Utils.SavePictureInMediaLibrary(ChatMessage.AppData);
            MessageBox.Show(result ? AppResources.FileSavingSuccess : AppResources.FileSavingFailure, AppResources.FileSaving, MessageBoxButton.OK);
        }

        /// <summary>
        /// Delegate for delete event.
        /// </summary>
        public delegate void MessageDeletedEventHandler(object sender, LinphoneChatMessage message);

        /// <summary>
        /// Handler for delete event.
        /// </summary>
        public event MessageDeletedEventHandler MessageDeleted;

        /// <summary>
        /// Delegate for download event.
        /// </summary>
        public delegate void DownloadImageEventHandler(object sender, LinphoneChatMessage message);

        /// <summary>
        /// Handler for download event.
        /// </summary>
        public event DownloadImageEventHandler DownloadImage;

        private void DownloadImage_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadImage != null)
            {
                Download.Visibility = Visibility.Collapsed;
                ProgressBar.Visibility = Visibility.Visible;
                DownloadImage(this, ChatMessage);
            }
        }

        /// <summary>
        /// Displays the image in the bubble
        /// </summary>
        public void RefreshImage()
        {
            string filePath = ChatMessage.AppData;
            ProgressBar.Visibility = Visibility.Collapsed;
            if (filePath != null && filePath.Length > 0)
            {
                Download.Visibility = Visibility.Collapsed;
                Image.Visibility = Visibility.Visible;
                Save.Visibility = Visibility.Visible;

                BitmapImage image = Utils.ReadImageFromIsolatedStorage(filePath);
                Image.Source = image;
            }
            else
            {
                Download.Visibility = Visibility.Visible;
            }
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            BaseModel.CurrentPage.NavigationService.Navigate(new Uri("/Views/FullScreenPicture.xaml?uri=" + ChatMessage.AppData, UriKind.RelativeOrAbsolute));
        }
    }
}
