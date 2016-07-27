﻿/*
Conversation.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linphone.Model
{
    /// <summary>
    /// Represents a chat conversation between the user and someone else.
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// Display name of the remote contact.
        /// </summary>
        public string DisplayedName { get; set; }

        /// <summary>
        /// SIP address of the remote contact.
        /// </summary>
        public string SipAddress { get; set; }

        /// <summary>
        /// Latest message (can be troncated) received or sent.
        /// </summary>
        public string LatestMessage 
        {
            get
            {
                string lastText = Messages.Last().Text;
                if (lastText == null || lastText.Length <= 0)
                {
                    return Resources.AppResources.ImageMessageReceived;
                }
                return lastText;
            }
        }

        /// <summary>
        /// Date of the latest message received or sent.
        /// </summary>
        public string LatestMessageDate 
        {
            get
            {
                return FormatDate(Messages.Last().Time);
            }
        }

        /// <summary>
        /// Returns true if the last message hasn't been read yet by the user, otherwise returns false
        /// </summary>
        public bool IsLastMessageUnread
        {
            get
            {
                return !Messages.Last().IsRead;
            }
        }

        /// <summary>
        /// List of messages sent and received.
        /// </summary>
        public IList<LinphoneChatMessage> Messages { get; set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        public Conversation(string sipAddress, string displayName, IList<Object> messages)
        {
            SipAddress = sipAddress;
            DisplayedName = displayName;
            Messages = new List<LinphoneChatMessage>();
            foreach (LinphoneChatMessage msg in messages)
            {
                Messages.Add(msg);
            }
        }

        private string FormatDate(long timestamp)
        {
            DateTime date = new DateTime(timestamp * TimeSpan.TicksPerSecond, DateTimeKind.Utc).AddYears(1969).ToLocalTime();
            DateTime now = DateTime.Now;
            if (now.Year == date.Year && now.Month == date.Month && now.Day == date.Day)
                return String.Format("{0:HH:mm}", date);
            else if (now.Year == date.Year && now.DayOfYear - date.DayOfYear <= 6)
                return String.Format("{0:ddd}", date);
            else if (now.Year == date.Year)
                return String.Format("{0:MM/dd}", date);
            else
                return String.Format("{0:MM/dd/yy}", date);
        }
    }
}
