namespace Assignment1ConsoleApp;

    public class UploadAsyncTask : AsyncTask
    {
        protected internal override async Task<string> DoInBackground()
        {
            try
            {
                return new UploadClient().UploadFile();
            }
            catch (FileUploadException e)
            {
                Console.Error.WriteLine(e.Message);
                return "Error during file upload.";
            }
        }

        protected internal override void OnPostExecute(string result)
        {
            Console.WriteLine(result);
        }
    }
    
