namespace ORM {

    public class NoColumnToChangeException : ORMException
    {

        private const string ERROR_MESSAGE = "UPDATE request has not column to change.";

        public NoColumnToChangeException():base(ERROR_MESSAGE) {
         }

    }
}