using static System.Math;

namespace TeleprompterConsole
{
    internal class TeleprompterConfig {
        public bool Done => done;
        
        public int DelayInMilliseconds { get; set; }

        public void UpdateDelay(int increment) // negative to speed up
        {
            var newDelay = Min(DelayInMilliseconds + increment, 1000);
            newDelay = Max(newDelay, 20);
            lock (lockHandle) {
                DelayInMilliseconds = newDelay;
            }
        }

        public void SetDone() {
            done = true;
        }

        private bool done;
        private object lockHandle = new object();
    }
}