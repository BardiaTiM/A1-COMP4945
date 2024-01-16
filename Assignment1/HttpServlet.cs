namespace Assignment1
{
    public abstract class HttpServlet
    {
        public abstract void doGet(HttpServletRequest request, HttpServletResponse response);

        public abstract void doPost(HttpServletRequest request, HttpServletResponse response);

    }
}