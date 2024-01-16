using System.IO;

namespace Assignment1
{

    public class HttpServletResponse
    {
        private Stream outputStream;

        public HttpServletResponse(Stream outputStream)
        {
            this.outputStream = outputStream;
        }

        public Stream GetOutputStream()
        {
            return outputStream;
        }
    }
}