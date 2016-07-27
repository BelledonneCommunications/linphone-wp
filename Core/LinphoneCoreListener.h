/*
LinphoneCoreListener.h
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

#include "Enums.h"

namespace Linphone
{
	namespace Core
	{
		ref class LinphoneCall;
		ref class LinphoneProxyConfig;
		ref class LinphoneCallStats;
		ref class LinphoneChatMessage;
		ref class LinphoneChatRoom;

		/// <summary>
		/// Definition of the LinphoneCoreListener interface.
		/// </summary>
		public interface class LinphoneCoreListener
		{
		public:
			/// <summary>
			/// Callback method called when authentication information are requested.
			/// </summary>
			/// <param name="realm">The realm for which authentication information are requested</param>
			/// <param name="username">The username for which authentication information are requested</param>
			void AuthInfoRequested(Platform::String^ realm, Platform::String^ username, Platform::String^ domain);

			/// <summary>
			/// Callback method called when the application state has changed.
			/// </summary>
			/// <param name="state">The new state of the application</param>
			/// <param name="message">A message describing the new state of the application</param>
			void GlobalState(GlobalState state, Platform::String^ message);

			/// <summary>
			/// Callback method called when the state of a call has changed.
			/// </summary>
			/// <param name="call">The call whose state has changed</param>
			/// <param name="state">The new state of the call</param>
			void CallState(LinphoneCall^ call, LinphoneCallState state, Platform::String^ message);

			/// <summary>
			/// Callback method called when the state of the registration of a proxy config has changed.
			/// </summary>
			/// <param name="config">The proxy config for which the registration state has changed</param>
			/// <param name="state">The new registration state for the proxy config</param>
			/// <param name="message">A message describing the new registration state</param>
			void RegistrationState(LinphoneProxyConfig^ config, RegistrationState state, Platform::String^ message);

			/// <summary>
			/// Callback method called when a DTMF is received.
			/// </summary>
			/// <param name="call">The call on which a DTMF has been received</param>
			/// <param name="dtmf">The DTMF that has been received</param>
			void DTMFReceived(LinphoneCall^ call, char16 dtmf);

			/// <summary>
			/// Callback method called when the echo canceller calibration finishes.
			/// </summary>
			/// <param name="status">The status of the echo canceller calibration</param>
			/// <param name="delayMs">The echo delay in milliseconds if the status is EcCalibratorStatus::Done</param>
			void EcCalibrationStatus(EcCalibratorStatus status, int delayMs); 

			/// <summary>
			/// Callback method called when the encryption of a call has changed.
			/// </summary>
			/// <param name="call">The call for which the encryption has changed</param>
			/// <param name="encrypted">A boolean value telling whether the call is encrypted</param>
			/// <param name="authenticationToken">The authentication token for the call if it is encrypted</param>
			void CallEncryptionChanged(LinphoneCall^ call, Platform::Boolean encrypted, Platform::String^ authenticationToken);

			/// <summary>
			/// Callback method called when the statistics of a call have been updated.
			/// </summary>
			/// <param name="call">The call for which the statistics have been updated</param>
			/// <param name="stats">The updated statistics for the call</param>
			void CallStatsUpdated(LinphoneCall^ call, LinphoneCallStats^ stats);

			void MessageReceived(LinphoneChatMessage^ message);

			/// <summary>
			/// Callback method called when the composing status for this room has been updated.
			/// </summary>
			/// <param name="room">The room for which the composing status has been updated</param>
			void ComposingReceived(LinphoneChatRoom^ room);

			/// <summary>
			/// Callback method called to notify the progression of a file transfer.
			/// </summary>
			/// <param name="message">The chat message</param>
			/// <param name="offset">The number of bytes sent/received since the beginning of the file transfer</param>
			/// <param name="total">The total number of bytes to be sent/received</param>
			void FileTransferProgressIndication(LinphoneChatMessage^ message, int offset, int total);

			/// <summary>
			/// Callback method called when the status of the current log upload changes.
			/// </summary>
			/// <param name="state">Tells the state of the upload</param>
			/// <param name="info">An error message if the upload went wrong, the url of the uploaded logs if it went well, null if upload not yet finished</param>
			void LogUploadStatusChanged(LinphoneCoreLogCollectionUploadState state, Platform::String^ info);

			/// <summary>
			/// Callback method called when the progress of the current logs upload has changed.
			/// </summary>
			/// <param name="progress"></param>
			void LogUploadProgressIndication(int offset, int total);
		};
	}
}