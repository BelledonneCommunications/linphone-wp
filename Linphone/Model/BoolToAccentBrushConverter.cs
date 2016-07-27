﻿/*
BoolToAccentBrushConverter.cs
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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Linphone.Model
{
    /// <summary>
    /// Converter returning the AccentColorBrush if the boolean is true, else returning a title color.
    /// </summary>
    public class BoolToAccentBrushConverter : IValueConverter
    {
        /// <returns>A SolidColorBrush (PhoneAccentBrush or PhoneSubtleBrush).</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (SolidColorBrush)(Application.Current.Resources["PhoneAccentBrush"]);
            }
            else
            {
                return (SolidColorBrush)(Application.Current.Resources["PhoneSubtleBrush"]);
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
