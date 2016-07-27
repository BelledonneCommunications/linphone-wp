/*
VideoPolicy.h
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

#pragma once

#include "Utils.h"

namespace Linphone
{
    namespace Core
	{
		ref class LinphoneCoreFactory;
		ref class LinphoneCore;

		/// <summary>
		/// Class describing policy regarding video streams establishments.
		/// </summary>
		public ref class VideoPolicy sealed
		{
		public:
			/// <summary>
			/// Whether video shall be automatically proposed for outgoing calls.
			/// </summary>
			property bool AutomaticallyInitiate
			{
				bool get();
				void set(bool value);
			}

			/// <summary>
			/// Whether video shall be automatically accepted for incoming calls.
			/// </summary>
			property bool AutomaticallyAccept
			{
				bool get();
				void set(bool value);
			}

		private:
			friend class Linphone::Core::Utils;
			friend ref class Linphone::Core::LinphoneCoreFactory;
			friend ref class Linphone::Core::LinphoneCore;

			VideoPolicy();
			VideoPolicy(bool automaticallyInitiate, bool automaticallyAccept);

			bool automaticallyInitiate;
			bool automaticallyAccept;
		};
	}
}