/*
Enums.h
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

namespace Linphone
{
	namespace Core
	{
		/// <summary>
		/// Presence statuses.
		/// </summary>
		public enum class OnlineStatus : int
		{
			Offline = 0,
			Online = 1,
			Busy = 2,
			BeRightBack = 3,
			Away = 4,
			OnThePhone = 5,
			OutToLunch = 6,
			DoNotDisturb = 7,
			StatusMoved = 8,
			StatusAltService = 9,
			Pending = 10
		};

		/// <summary>
		/// Direction of a call, either Outgoing or Incoming.
		/// </summary>
		public enum class CallDirection : int
		{
			Outgoing, 
			Incoming
		};
		
		/// <summary>
		/// Linphone core states
		/// </summary>
		public enum class GlobalState : int
		{
			GlobalOff = 0,
			GlobalStartup = 1,
			GlobalOn = 2,
			GlobalShutdown = 3
		};

		/// <summary>
		/// Proxy registration states
		/// </summary>
		public enum class RegistrationState : int
		{
			RegistrationNone = 0,
			RegistrationInProgress = 1,
			RegistrationOk = 2,
			RegistrationCleared = 3,
			RegistrationFailed = 4
		};

		/// <summary>
		/// Types of media encryption.
		/// </summary>
		public enum class MediaEncryption : int
		{
			None = 0,
			SRTP = 1,
			ZRTP = 2
		};

		/// <summary>
		/// Policy to use to work around the issues caused by NAT (Network Address Translation).
		/// </summary>
		public enum class FirewallPolicy : int
		{
			NoFirewall = 0,
			UseNatAddress = 1,
			UseStun = 2,
			UseIce = 3
		};

		/// <summary>
		/// Statuses of the echo canceller calibration process.
		/// </summary>
		public enum class EcCalibratorStatus : int
		{
			InProgress = 0,
			Done = 1,
			Failed = 2,
			DoneNoEcho = 3
		};

		/// <summary>
		/// Linphone call states
		/// </summary>
		public enum class LinphoneCallState : int
		{
			Idle = 0,
			IncomingReceived = 1,
			OutgoingInit = 2,
			OutgoingProgress = 3,
			OutgoingRinging = 4,
			OutgoingEarlyMedia = 5,
			Connected = 6,
			StreamsRunning = 7,
			Pausing = 8,
			Paused = 9,
			Resuming = 10,
			Refered = 11,
			Error = 12,
			CallEnd = 13,
			PausedByRemote = 14,
			UpdatedByRemote = 15,
			IncomingEarlyMedia = 16,
			Updating = 17,
			Released = 18
		};

		/// <summary>
		/// Represents a call status
		/// </summary>
		public enum class LinphoneCallStatus : int
		{
			Success = 0,
			Aborted = 1,
			Missed = 2,
			Declined = 3
		};

		/// <summary>
		/// Levels for trace output
		/// </summary>
		public enum class OutputTraceLevel : int
		{
			Debug = 0,
			Message = 1,
			Warning = 2,
			Error = 3,
			None = 255
		};

		/// <summary>
		/// The output destinations for logging
		/// </summary>
		public enum class OutputTraceDest : int
		{
			None = 0,
			Debugger = 1,
			File = 2,
			TCPRemote = 3
		};

		/// <summary>
		/// Linphone chat message states
		/// </summary>
		public enum class LinphoneChatMessageState : int
		{
			Idle = 0,
			InProgress = 1,
			Delivered = 2,
			NotDelivered = 3,
			FileTransferError = 4,
			FileTransferDone = 5
		};

		public enum class Reason : int
		{
			LinphoneReasonNone = 0,
			LinphoneReasonNoResponse = 1, /**<No response received from remote*/
			LinphoneReasonForbidden = 2, /**<Authentication failed due to bad credentials or resource forbidden*/
			LinphoneReasonDeclined = 3, /**<The call has been declined*/
			LinphoneReasonNotFound = 4, /**<Destination of the call was not found.*/
			LinphoneReasonNotAnswered = 5, /**<The call was not answered in time (request timeout)*/
			LinphoneReasonBusy = 6, /**<Phone line was busy */
			LinphoneReasonUnsupportedContent = 7, /**<Unsupported content */
			LinphoneReasonIOError = 8, /**<Transport error: connection failures, disconnections etc...*/
			LinphoneReasonDoNotDisturb = 9, /**<Do not disturb reason*/
			LinphoneReasonUnauthorized = 10, /**<Operation is unauthorized because missing credential*/
			LinphoneReasonNotAcceptable = 11, /**<Operation like call update rejected by peer*/
			LinphoneReasonNoMatch = 12, /**<Operation could not be executed by server or remote client because it didn't have any context for it*/
			LinphoneReasonMovedPermanently = 13, /**<Resource moved permanently*/
			LinphoneReasonGone = 14, /**<Resource no longer exists*/
			LinphoneReasonTemporarilyUnavailable = 15, /**<Temporarily unavailable*/
			LinphoneReasonAddressIncomplete = 16, /**<Address incomplete*/
			LinphoneReasonNotImplemented = 17, /**<Not implemented*/
			LinphoneReasonBadGateway = 18, /**<Bad gateway*/
			LinphoneReasonServerTimeout = 19, /**<Server timeout*/
			LinphoneReasonUnknown = 20 /**Unknown reason*/
		};

		public enum class LinphoneTransport : int
		{
			LinphoneTransportUDP = 0,
			LinphoneTransportTCP = 1,
			LinphoneTransportTLS = 2,
			LinphoneTransportDTLS = 3
		};

		public enum class LinphoneCoreLogCollectionUploadState : int
		{
			LinphoneCoreLogCollectionUploadStateInProgress = 0, /**< Delivery in progress */
			LinphoneCoreLogCollectionUploadStateDelivered = 1, /**< Log collection upload successfully delivered and acknowledged by remote end point */
			LinphoneCoreLogCollectionUploadStateNotDelivered = 2
		};

		/// <summary>
		/// Media type of the statistics (audio or video).
		/// </summary>
		public enum class MediaType : int
		{
			Audio = 0,
			Video = 1
		};

		/// <summary>
		/// State of the ICE processing.
		/// </summary>
		public enum class IceState : int
		{
			NotActivated = 0,
			Failed = 1,
			InProgress = 2,
			HostConnection = 3,
			ReflexiveConnection = 4,
			RelayConnection = 5
		};

		/// <summary>
		/// Indicates for a given media the stream direction.
		/// </summary>
		public enum class MediaDirection : int
		{
			Inactive = 0,
			SendOnly = 1,
			RecvOnly = 2,
			SendRecv = 3
		};
	}
}