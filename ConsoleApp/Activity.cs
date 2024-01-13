namespace Assignment1ConsoleApp;

using System;
using System.IO;
using System.Threading.Tasks;

class Activity
{
    // static async Task Main(string[] args)
    // {
    //     await new Activity().OnCreate();
    // }

    public Activity()
    {
    }

    public async Task OnCreate()
    {
        AsyncTask uploadAsyncTask = new UploadAsyncTask();
        uploadAsyncTask.Execute();
        
        Console.WriteLine("Waiting for Callback");
        try
        {
            Console.ReadLine();
        }
        catch (FileUploadException e)
        {
            Console.WriteLine("Error. Exception: " + e.Message);
        }
    }
}

