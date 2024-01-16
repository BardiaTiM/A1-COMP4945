using System.IO;

namespace Assignment1
{

    public class HttpServletRequest
    {
        private Stream inputStream;

        public HttpServletRequest(Stream inputStream)
        {
            this.inputStream = inputStream;
        }

        public Stream GetInputStream()
        {
            return inputStream;
        }
    }

}