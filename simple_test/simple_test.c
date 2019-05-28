/** \file
 * \brief Example code for Simple Open EtherCAT master
 *
 * Usage : simple_test [ifname1]
 * ifname is NIC interface, f.e. eth0
 *
 * This is a minimal test.
 *
 * (c)Arthur Ketels 2010 - 2011
 */

#include <stdio.h>
#include <string.h>
#include "ethercat.h"

#define EC_TIMEOUTMON 500

char IOmap[4096];//
OSAL_THREAD_HANDLE thread1;
int expectedWKC;
boolean needlf;
volatile int wkc;
boolean inOP;
uint8 currentgroup = 0;

void simpletest(char *ifname)
{
    int i, j, oloop, iloop, chk;
    needlf = FALSE;
    inOP = FALSE;

   printf("Starting simple test\n");

   /* initialise SOEM, bind socket to ifname */
   if (ec_init(ifname))
   {
      printf("ec_init on %s succeeded.\n",ifname);
      /* find and auto-config slaves */


       if ( ec_config_init(FALSE) > 0 )//配置初始化，返回值表示是否配置成功
      {
		 //配置成功后所有从站都会被请求进入PR_OP 状态
         printf("%d slaves found and configured.\n",ec_slavecount);//ec_slavecount表示从站总个数

         ec_config_map(&IOmap);		//物理地址与逻辑地址映射

         ec_configdc();		//DC分布式时钟

         printf("Slaves mapped, state to SAFE_OP.\n");
         /* wait for all slaves to reach SAFE_OP state */
         ec_statecheck(0, EC_STATE_SAFE_OP,  EC_TIMEOUTSTATE * 4);

         oloop = ec_slave[0].Obytes;
         if ((oloop == 0) && (ec_slave[0].Obits > 0)) oloop = 1;
         if (oloop > 8) oloop = 8;
         iloop = ec_slave[0].Ibytes;
         if ((iloop == 0) && (ec_slave[0].Ibits > 0)) iloop = 1;
         if (iloop > 8) iloop = 8;

         printf("segments : %d : %d %d %d %d\n",ec_group[0].nsegments ,ec_group[0].IOsegment[0],ec_group[0].IOsegment[1],ec_group[0].IOsegment[2],ec_group[0].IOsegment[3]);

         printf("Request operational state for all slaves\n");
         expectedWKC = (ec_group[0].outputsWKC * 2) + ec_group[0].inputsWKC;
         printf("Calculated workcounter %d\n", expectedWKC);
         ec_slave[0].state = EC_STATE_OPERATIONAL;
         /* send one valid process data to make outputs in slaves happy*/
         ec_send_processdata();
         ec_receive_processdata(EC_TIMEOUTRET);
         /* request OP state for all slaves */
         ec_writestate(0);
         chk = 40;
         /* wait for all slaves to reach OP state */
         do
         {
            ec_send_processdata();
            ec_receive_processdata(EC_TIMEOUTRET);
            ec_statecheck(0, EC_STATE_OPERATIONAL, 50000);
         }
         while (chk-- && (ec_slave[0].state != EC_STATE_OPERATIONAL));

		 //现在所有从站都变为OP状态，就是operational可操作状态
         if (ec_slave[0].state == EC_STATE_OPERATIONAL )
         {
            printf("Operational state reached for all slaves.\n");
            inOP = TRUE;
                /* cyclic loop */
			for (i = 1; i <= 10000; i++)
			{
				ec_send_processdata();
				wkc = ec_receive_processdata(EC_TIMEOUTRET);

				if (wkc >= expectedWKC)
				{
					printf("Processdata cycle %4d, WKC %d , O:", i, wkc);

					for (j = 0; j < oloop; j++)
					{
						printf(" %2.2x", *(ec_slave[0].outputs + j));
					}

					printf(" I:");
					for (j = 0; j < iloop; j++)
					{
						printf(" %2.2x", *(ec_slave[0].inputs + j));
					}
					printf(" T:%"PRId64"\r", ec_DCtime);
					needlf = TRUE;
				}
				osal_usleep(5000);

			}
			inOP = FALSE;
		 }
		 else
		 {
			 printf("Not all slaves reached operational state.\n");
			 ec_readstate();
			 for (i = 1; i <= ec_slavecount; i++)
			 {
				 if (ec_slave[i].state != EC_STATE_OPERATIONAL)
				 {
					 printf("Slave %d State=0x%2.2x StatusCode=0x%4.4x : %s\n",
						 i, ec_slave[i].state, ec_slave[i].ALstatuscode, ec_ALstatuscode2string(ec_slave[i].ALstatuscode));
				 }
			 }
		 }
		 printf("\nRequest init state for all slaves\n");
		 ec_slave[0].state = EC_STATE_INIT;
		 /* request INIT state for all slaves */
		 ec_writestate(0);
	   }
	   else
	   {
		   printf("No slaves found!\n");
	   }
	   printf("End simple test, close socket\n");
	   /* stop SOEM, close socket */
	   ec_close();
    }
    else
    {
        printf("No socket connection on %s\nExcecute as root\n",ifname);
    }
}

OSAL_THREAD_FUNC ecatcheck( void *ptr )
{
    int slave;
    (void)ptr;                  /* Not used */

    while(1)
    {
		if (inOP && ((wkc < expectedWKC) || ec_group[currentgroup].docheckstate))/*还没进入OP状态前不检查*/
        {
            if (needlf)
            {
               needlf = FALSE;
               printf("\n");
            }
            /* one or more slaves are not responding */
            ec_group[currentgroup].docheckstate = FALSE;
            ec_readstate();
            for (slave = 1; slave <= ec_slavecount; slave++)
            {
               if ((ec_slave[slave].group == currentgroup) && (ec_slave[slave].state != EC_STATE_OPERATIONAL))
               {
                  ec_group[currentgroup].docheckstate = TRUE;
				  //从站状态是 SAFE_OP + ERROR
                  if (ec_slave[slave].state == (EC_STATE_SAFE_OP + EC_STATE_ERROR))
                  {
                     printf("ERROR : slave %d is in SAFE_OP + ERROR, attempting ack.\n", slave);
                     ec_slave[slave].state = (EC_STATE_SAFE_OP + EC_STATE_ACK);
                     ec_writestate(slave);
                  }
				  //从站状态是：SAFE_OP
                  else if(ec_slave[slave].state == EC_STATE_SAFE_OP)
                  {
                     printf("WARNING : slave %d is in SAFE_OP, change to OPERATIONAL.\n", slave);
                     ec_slave[slave].state = EC_STATE_OPERATIONAL;
                     ec_writestate(slave);
                  }
				  /*接下来需要注意，如果既不是SAFE_OP或是SAFE_OP+ERROR，那么根据EtherCAT状态机，
					要想使从站进入OP状态，不能跳变，只能进行重新配置，就是下面的reconfig函数。
					这个函数把slave从头开始转换为safe_op状态*/
				  //从站状态非空，即有状态
                  else if(ec_slave[slave].state > EC_STATE_NONE)
                  {
                     if (ec_reconfig_slave(slave, EC_TIMEOUTMON))
                     {
                        ec_slave[slave].islost = FALSE;
                        printf("MESSAGE : slave %d reconfigured\n",slave);
                     }
                  }

                  else if(!ec_slave[slave].islost)
                  {
                     /* re-check state */
                     ec_statecheck(slave, EC_STATE_OPERATIONAL, EC_TIMEOUTRET);
                     if (ec_slave[slave].state == EC_STATE_NONE)
                     {
                        ec_slave[slave].islost = TRUE;
                        printf("ERROR : slave %d lost\n",slave);
                     }
                  }
               }
               if (ec_slave[slave].islost)
               {
                  if(ec_slave[slave].state == EC_STATE_NONE)
                  {
                     if (ec_recover_slave(slave, EC_TIMEOUTMON))
                     {
                        ec_slave[slave].islost = FALSE;
                        printf("MESSAGE : slave %d recovered\n",slave);
                     }
                  }
                  else
                  {
                     ec_slave[slave].islost = FALSE;
                     printf("MESSAGE : slave %d found\n",slave);
                  }
               }
            }
            if(!ec_group[currentgroup].docheckstate)
               printf("OK : all slaves resumed OPERATIONAL.\n");
        }
        osal_usleep(10000);//10ms
    }
}

int main(int argc, char *argv[])
{
	// 输入：	\Device\NPF_{9A10D941-1301-4A51-A856-024B1399EA32}
	//			\Device\NPF_{790C6A05-E499-4760-BDCF-A202689914E4}
	//\Device\NPF_{4D36E972-E325-11CE-BFC1-08002BE10318}
	/*Description : Microsoft, Device to use for wpcap: {4AE8C889-9F47-4F5B-955B-B1180584E073}
	  Description : Microsoft, Device to use for wpcap: {00009ED3-3197-432A-B12C-5977779656D9}
	  Description : MS NDIS 6.0 LoopBack Driver, Device to use for wpcap: {019A4524-3843-4A70-A410-87C68BD79DD4}
	  Description : Oracle, Device to use for wpcap: {ECA611D5-9775-4696-AC54-78FB26304C83}
	  Description : Supereal network, Device to use for wpcap: {790C6A05-E499-4760-BDCF-A202689914E4}
	  Description : Microsoft, Device to use for wpcap: {AF607617-B9DF-408E-B22D-964A181979A9}
	  Description : Sangfor SSL VPN CS Support System VNIC, Device to use for wpcap: {9153561B-04B5-4540-B3B0-8042A30F9899}*/
	
   printf("SOEM (Simple Open EtherCAT Master)\nSimple test\n");

   if (argc > 1)
   {
      /* create thread to handle slave error handling in OP */
      //pthread_create( &thread1, NULL, (void *) &ecatcheck, (void*) &ctime);
      int k = osal_thread_create(&thread1, 128000, &ecatcheck, (void*) &ctime);
	  
      /* start cyclic part */
      simpletest(argv[1]);

   }
   else
   {
      printf("Usage: simple_test ifname1\nifname = eth0 for example\n");
   }

   printf("End program\n");
   return (0);
}
