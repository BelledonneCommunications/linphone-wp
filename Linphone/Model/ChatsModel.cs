﻿/*
ChatsModel.cs
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Linphone.Views
{
    /// <summary>
    /// Model for the InCall page that handles the display of the various elements of the page.
    /// </summary>
    public class ChatsModel : BaseModel
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        public ChatsModel()
            : base()
        {
        }

        public ObservableCollection<Conversation> Conversations
        {
            get
            {
                return conversations;
            }
            set
            {
                conversations = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Conversation> conversations = new ObservableCollection<Conversation>();
    }
}