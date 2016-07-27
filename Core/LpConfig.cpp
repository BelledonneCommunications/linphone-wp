/*
LpConfig.cpp
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

#include "ApiLock.h"
#include "LpConfig.h"
#include "Server.h"

bool Linphone::Core::LpConfig::GetBool(Platform::String^ section, Platform::String^ key, bool defaultValue)
{
	return (GetInt(section, key, defaultValue) == TRUE);
}

void Linphone::Core::LpConfig::SetBool(Platform::String^ section, Platform::String^ key, bool value)
{
	SetInt(section, key, (int)value);
}

int Linphone::Core::LpConfig::GetInt(Platform::String^ section, Platform::String^ key, int defaultValue)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	int value = lp_config_get_int(this->config, ccSection, ccKey, defaultValue);
	delete(ccKey);
	delete(ccSection);
	return value;
}

void Linphone::Core::LpConfig::SetInt(Platform::String^ section, Platform::String^ key, int value)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	lp_config_set_int(this->config, ccSection, ccKey, value);
	delete(ccKey);
	delete(ccSection);
}

int64 Linphone::Core::LpConfig::GetInt64(Platform::String^ section, Platform::String^ key, int64 defaultValue)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	int64 value = lp_config_get_int64(this->config, ccSection, ccKey, defaultValue);
	delete(ccKey);
	delete(ccSection);
	return value;
}

void Linphone::Core::LpConfig::SetInt64(Platform::String^ section, Platform::String^ key, int64 value)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	lp_config_set_int64(this->config, ccSection, ccKey, value);
	delete(ccKey);
	delete(ccSection);
}

float Linphone::Core::LpConfig::GetFloat(Platform::String^ section, Platform::String^ key, float defaultValue)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	float value = lp_config_get_float(this->config, ccSection, ccKey, defaultValue);
	delete(ccKey);
	delete(ccSection);
	return value;
}

void Linphone::Core::LpConfig::SetFloat(Platform::String^ section, Platform::String^ key, float value)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	lp_config_set_float(this->config, ccSection, ccKey, value);
	delete(ccKey);
	delete(ccSection);
}

Platform::String^ Linphone::Core::LpConfig::GetString(Platform::String^ section, Platform::String^ key, Platform::String^ defaultValue)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	const char *ccDefaultValue = Linphone::Core::Utils::pstoccs(defaultValue);
	const char *ccvalue = lp_config_get_string(this->config, ccSection, ccKey, ccDefaultValue);
	Platform::String^ value = Linphone::Core::Utils::cctops(ccvalue);
	delete(ccDefaultValue);
	delete(ccKey);
	delete(ccSection);
	return value;
}

void Linphone::Core::LpConfig::SetString(Platform::String^ section, Platform::String^ key, Platform::String^ value)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	const char *ccValue = Linphone::Core::Utils::pstoccs(value);
	lp_config_set_string(this->config, ccSection, ccKey, ccValue);
	delete(ccValue);
	delete(ccKey);
	delete(ccSection);
}

Platform::Array<int>^ Linphone::Core::LpConfig::GetRange(Platform::String^ section, Platform::String^ key, const Platform::Array<int>^ defaultValue)
{
	API_LOCK;
	Platform::Array<int>^ range = ref new Platform::Array<int>(2);
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	lp_config_get_range(this->config, ccSection, ccKey, &range[0], &range[1], defaultValue[0], defaultValue[1]);
	delete(ccKey);
	delete(ccSection);
	return range;
}

void Linphone::Core::LpConfig::SetRange(Platform::String^ section, Platform::String^ key, const Platform::Array<int>^ value)
{
	API_LOCK;
	const char *ccSection = Linphone::Core::Utils::pstoccs(section);
	const char *ccKey = Linphone::Core::Utils::pstoccs(key);
	lp_config_set_range(this->config, ccSection, ccKey, value[0], value[1]);
	delete(ccKey);
	delete(ccSection);
}

Linphone::Core::LpConfig::LpConfig(::LpConfig *config) :
	config(config)
{
}

Linphone::Core::LpConfig::LpConfig(Platform::String^ configPath, Platform::String^ factoryConfigPath)
{
	API_LOCK;
	const char *ccConfigPath = Linphone::Core::Utils::pstoccs(configPath);
	const char *ccFactoryConfigPath = Linphone::Core::Utils::pstoccs(factoryConfigPath);
	this->config = lp_config_new_with_factory(ccConfigPath, ccFactoryConfigPath);
	delete(ccFactoryConfigPath);
	delete(ccConfigPath);
}

Linphone::Core::LpConfig::~LpConfig()
{
}