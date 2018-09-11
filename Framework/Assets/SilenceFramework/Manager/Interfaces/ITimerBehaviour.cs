namespace BlankFramework
{
    public interface ITimerBehaviour
    {
        /// <summary>
        /// 时间更新器
        /// </summary>
        //  void TimerUpdate();
        /// <summary>
        /// 时间更新器带参数
        /// </summary>
        /// <param name="timerInfo"></param>
        void TimerUpdate(TimerInfo timerInfo);
    }
}