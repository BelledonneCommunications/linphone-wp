/*
LinphoneCallLog.cpp
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

#include "LinphoneCallLog.h"
#include "LinphoneAddress.h"
#include "Enums.h"
#include "Server.h"
#include "Globals.h"
#include "LinphoneCoreFactory.h"

Linphone::Core::LinphoneAddress^ Linphone::Core::LinphoneCallLog::From::get()
{
	API_LOCK;
	return (Linphone::Core::LinphoneAddress^)Linphone::Core::Utils::CreateLinphoneAddress((void*)linphone_call_log_get_from(this->callLog));
}

Linphone::Core::LinphoneAddress^ Linphone::Core::LinphoneCallLog::To::get()
{
	API_LOCK;
	return (Linphone::Core::LinphoneAddress^)Linphone::Core::Utils::CreateLinphoneAddress((void*)linphone_call_log_get_to(this->callLog));
}

Linphone::Core::CallDirection Linphone::Core::LinphoneCallLog::Direction::get()
{
	API_LOCK;
	return (Linphone::Core::CallDirection)linphone_call_log_get_dir(this->callLog);
}

Linphone::Core::LinphoneCallStatus Linphone::Core::LinphoneCallLog::Status::get()
{
	API_LOCK;
	return (Linphone::Core::LinphoneCallStatus)linphone_call_log_get_status(this->callLog);
}

int64 Linphone::Core::LinphoneCallLog::StartDate::get()
{
	API_LOCK;
	return linphone_call_log_get_start_date(this->callLog);
}

int Linphone::Core::LinphoneCallLog::Duration::get()
{
	API_LOCK;
	return linphone_call_log_get_duration(this->callLog);
}

Platform::String^ Linphone::Core::LinphoneCallLog::CallId::get()
{
	API_LOCK;
	return Linphone::Core::Utils::cctops(linphone_call_log_get_call_id(this->callLog));
}

Platform::Boolean Linphone::Core::LinphoneCallLog::VideoEnabled::get()
{
	API_LOCK;
	return (linphone_call_log_video_enabled(this->callLog) == TRUE);
}

Linphone::Core::LinphoneCallLog::LinphoneCallLog(::LinphoneCallLog *cl) :
	callLog(cl)
{
	API_LOCK;
	RefToPtrProxy<LinphoneCallLog^> *log = new RefToPtrProxy<LinphoneCallLog^>(this);
	linphone_call_log_set_user_pointer(this->callLog, log);
}

Linphone::Core::LinphoneCallLog::~LinphoneCallLog()
{
	API_LOCK;
	RefToPtrProxy<LinphoneCallLog^> *log = reinterpret_cast< RefToPtrProxy<LinphoneCallLog^> *>(linphone_call_log_get_user_pointer(this->callLog));
	delete log;
}