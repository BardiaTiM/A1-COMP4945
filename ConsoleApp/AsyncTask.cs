namespace Assignment1ConsoleApp
{
    using System.Threading.Tasks;
    using System.Threading;

    public abstract class AsyncTask
    {
        public AsyncTask Execute()
        {
            OnPreExecute();
            new Thread(() => { WorkerThread(this).Wait(); }).Start();
            return this;
        }

        protected internal abstract Task<string> DoInBackground();

        protected virtual void OnPreExecute()
        {
        }

        protected internal virtual void OnPostExecute(string result)
        {
        }

        protected virtual void OnProgressUpdate(string progress)
        {
        }

        protected void PublishProgress(string progress)
        {
            OnProgressUpdate(progress);
        }

        protected async Task WorkerThread(AsyncTask asyncTask)
        {
            string result = await asyncTask.DoInBackground();
            asyncTask.OnPostExecute(result);
        }
    }
}