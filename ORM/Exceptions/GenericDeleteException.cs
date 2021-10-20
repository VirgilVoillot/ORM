namespace ORM {

    public class GenericDeleteException : ORMException
    {

        private const string ERROR_MESSAGE = "DELETE has not condition : {0}";

        public GenericDeleteException(string SQLrequest):base(string.Format(ERROR_MESSAGE,SQLrequest)) {
         }

    }
}