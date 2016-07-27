﻿/*
Numpad.xaml.cs
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

namespace Linphone.Controls
{
    /// <summary>
    /// Control to display numpad during a call
    /// </summary>
    public partial class Numpad : UserControl
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        public Numpad()
        {
            InitializeComponent();
        }

        private void Numpad_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String tag = button.Tag as String;
            LinphoneManager.Instance.LinphoneCore.SendDTMF(Convert.ToChar(tag));
        }
    }
}
