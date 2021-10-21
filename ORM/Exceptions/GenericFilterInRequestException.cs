namespace ORM {

    public class GenericFilterInRequestException : ORMException
    {

        private const string ERROR_MESSAGE = "The request has not filter.";

        public GenericFilterInRequestException():base(ERROR_MESSAGE) {
         }

    }
}