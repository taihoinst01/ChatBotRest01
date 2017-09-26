namespace BasicMultiDialogBot.Dialogs
{
    using System;
    using System.Diagnostics;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Threading.Tasks;

    [Serializable]
    public class TimeThread
    {
        //private static TimeThread _instance;
        DateTime now;   // 현재날짜
        int currentSecond;  //현재 초(분+초)
        int afterSecond;    //이후 초(분+초)
        int delaySecond;    //지연 시간
        private bool isStop;
        public IDialogContext context;
        private String beforeActivityId;


        public TimeThread(IDialogContext context)
        {
            this.context = context;
        }
        /* SingleTon
        protected TimeThread()
        {
        }

        public static TimeThread getInstance()
        {
            if (_instance == null)
            {
              lock(_instance)
              {
                 _instance = new TimeThread();
              }
            }
            return _instance;
        }
        */
        public void setIsStop(bool isStop)
        {
            this.isStop = isStop;
        }
        
        public void init()
        {
            now = DateTime.Now;
            currentSecond = (now.Minute * 60) + now.Second;
            afterSecond = currentSecond;
            delaySecond = 2;
        }
        public async void run()
        {
            String currentActivityId = context.Activity.Id;

            init();
            Debug.WriteLine("분 : "+now.Minute);
            Debug.WriteLine("초 : "+now.Second);
            Debug.WriteLine("분+초 : " + currentSecond);
            Debug.WriteLine("시간 계산 start...");
            while (currentSecond + delaySecond > afterSecond)
            {
                DateTime after = DateTime.Now;
                afterSecond = (after.Minute * 60) + after.Second;
            }
            if (beforeActivityId == null || beforeActivityId == currentActivityId)
            {
                Debug.WriteLine("시간 계산 END..." + delaySecond + "초가 지났습니다.");
            }else
            {
                Debug.WriteLine("지연시간이 되기 전에 입력값이 발생했습니다.");
                beforeActivityId = currentActivityId;
            }

            await result(context);
        }
        public async Task result(IDialogContext context)
        {
            await context.PostAsync("10초 지났습니다.");
            context.Done(0);

        }
    }
}