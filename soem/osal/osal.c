/*
 * Licensed under the GNU General Public License version 2 with exceptions. See
 * LICENSE file in the project root for full license information
 */

#include <winsock2.h>
#include <osal.h>
#include "osal_win32.h"

static int64_t sysfrequency;
static double qpc2usec;

#define USECS_PER_SEC     1000000

int osal_gettimeofday (struct timeval *tv, struct timezone *tz)
{
   int64_t wintime, usecs;
   if(!sysfrequency)
   {
      timeBeginPeriod(1);
      QueryPerformanceFrequency((LARGE_INTEGER *)&sysfrequency);
      qpc2usec = 1000000.0 / sysfrequency;
   }
   QueryPerformanceCounter((LARGE_INTEGER *)&wintime);
   usecs = (int64_t)((double)wintime * qpc2usec);
   tv->tv_sec = (long)(usecs / 1000000);
   tv->tv_usec = (long)(usecs - (tv->tv_sec * 1000000));

   return 1;
}

//获取主站时间
ec_timet osal_current_time (void)
{
   struct timeval current_time;
   ec_timet return_value;

   osal_gettimeofday (&current_time, 0);
   return_value.sec = current_time.tv_sec;
   return_value.usec = current_time.tv_usec;
   return return_value;
}

void osal_time_diff(ec_timet *start, ec_timet *end, ec_timet *diff)
{
   if (end->usec < start->usec) {
      diff->sec = end->sec - start->sec - 1;
      diff->usec = end->usec + 1000000 - start->usec;
   }
   else {
      diff->sec = end->sec - start->sec;
      diff->usec = end->usec - start->usec;
   }
}

//************************************
// Method:    osal_timer_start
// FullName:  将self在start_time的基础上增加timeout_usec那么多
// Access:    public 
// Returns:   void
// Qualifier: 
// Parameter: osal_timert * self
// Parameter: uint32 timeout_usec
//************************************
void osal_timer_start (osal_timert *self, uint32 timeout_usec)
{
   struct timeval start_time;
   struct timeval timeout;
   struct timeval stop_time;

   osal_gettimeofday (&start_time, 0);
   timeout.tv_sec = timeout_usec / USECS_PER_SEC;
   timeout.tv_usec = timeout_usec % USECS_PER_SEC;
   timeradd (&start_time, &timeout, &stop_time);//前两个加起来赋值给第三个

   self->stop_time.sec = stop_time.tv_sec;
   self->stop_time.usec = stop_time.tv_usec;
}

boolean osal_timer_is_expired (osal_timert *self)
{
   struct timeval current_time;
   struct timeval stop_time;
   int is_not_yet_expired;

   osal_gettimeofday (&current_time, 0);
   stop_time.tv_sec = self->stop_time.sec;
   stop_time.tv_usec = self->stop_time.usec;
   is_not_yet_expired = timercmp (&current_time, &stop_time, <);

   return is_not_yet_expired == FALSE;
}

/*线程睡眠这么这么多微秒microseconds*/
int osal_usleep(uint32 usec)
{
   osal_timert qtime;
   osal_timer_start(&qtime, usec);
   if(usec >= 1000)
   {
      SleepEx(usec / 1000, FALSE);//SleepEx函数中止当前线程运行直到指定的条件被触发。
	  /*第一个参数是毫秒，第二个参数表示如果该参数为FALSE,函数不会返回直到超时已到。*/
   }
   while (!osal_timer_is_expired(&qtime));/*通过循环，将现在的时间和将到达的时间不断比较，到时立即弹出*/
   return 1;
}
/*包装，同malloc*/
void *osal_malloc(size_t size)
{
   return malloc(size);
}
/*包装，同free*/
void osal_free(void *ptr)
{
   free(ptr);
}

//************************************
// Method:    osal_thread_create
// FullName:  创建SOEM任务线程
// Access:    public 
// Returns:   int				创建成功返回1，否则为0
// Qualifier:
// Parameter: void * * thandle	void类型指针，指向线程
// Parameter: int stacksize		线程堆栈尺寸，表示与此应用的堆栈尺寸相同时为0，这时主线程与创建的线程有
//								一样长度的堆栈。并且其长度会根据需要自动变长
// Parameter: void * func		线程函数所指向的起始地址的函数
// Parameter: void * param		给线程函数传递的参数
//************************************
int osal_thread_create(void **thandle, int stacksize, void *func, void *param)
{
   *thandle = CreateThread(NULL, stacksize, func, param, 0, NULL);
   if(!thandle)
   {
      return 0;
   }
   return 1;
}

int osal_thread_create_rt(void **thandle, int stacksize, void *func, void *param)
{
   int ret;
   ret = osal_thread_create(thandle, stacksize, func, param);
   if (ret)
   {
      ret = SetThreadPriority(*thandle, THREAD_PRIORITY_TIME_CRITICAL);
   }
   return ret;
}
