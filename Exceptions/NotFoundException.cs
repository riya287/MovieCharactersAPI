namespace MovieAPI_Project.Exceptions
{
    public class NotFoundException:Exception
    {
        private readonly string _message;
        public NotFoundException(string message) 
        {
            _message = message;
        }

        public override string Message => _message;
    }
}
