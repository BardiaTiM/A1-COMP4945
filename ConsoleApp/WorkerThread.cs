namespace Assignment1ConsoleApp;

using System.Threading.Tasks;

public class WorkerThread
{
    private AsyncTask? aTask = null;

    public WorkerThread(AsyncTask asyncTask)
    {
        this.aTask = asyncTask;
    }

    public async Task RunAsync()
    {
        string result = await aTask.DoInBackground();
        aTask.OnPostExecute(result);
    }
}
