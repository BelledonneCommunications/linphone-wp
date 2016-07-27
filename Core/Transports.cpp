/*
Transports.cpp
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

#include "Transports.h"

Linphone::Core::Transports::Transports() :
udp(5060),
tcp(0),
tls(0)
{
}

Linphone::Core::Transports::Transports(int udp_port, int tcp_port, int tls_port) :
udp(udp_port),
tcp(tcp_port),
tls(tls_port)
{
}

Linphone::Core::Transports::Transports(Linphone::Core::Transports^ t) :
udp(t->UDP),
tcp(t->TCP),
tls(t->TLS)
{
}

int Linphone::Core::Transports::UDP::get()
{
	return udp;
}

void Linphone::Core::Transports::UDP::set(int value)
{
	this->udp = value;
	this->tcp = 0;
	this->tls = 0;
}

int Linphone::Core::Transports::TCP::get()
{
	return tcp;
}

void Linphone::Core::Transports::TCP::set(int value)
{
	this->udp = 0;
	this->tcp = value;
	this->tls = 0;
}

int Linphone::Core::Transports::TLS::get()
{
	return tls;
}

void Linphone::Core::Transports::TLS::set(int value)
{
	this->udp = 0;
	this->tcp = 0;
	this->tls = value;
}

Platform::String^ Linphone::Core::Transports::ToString()
{
	return "udp[" + udp + "] tcp[" + tcp + "] tls[" + tls + "]";
}
