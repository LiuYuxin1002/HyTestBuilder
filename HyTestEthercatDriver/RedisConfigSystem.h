#pragma once

/**
 * where diffrent from hiredisHelper.h is this file
 * is some setting for redis other than operation.
 */

//setting the size of redis buffer.
int setBufferSize(int bits);
//setting the time of expire time.
int setExpireTime(int second);
int setExpireTime_mini(int minisecond);
