namespace ORM {

    public class MissingTableAttributException : ORMException
    {

        private const string ERROR_MESSAGE = "TableAttribute is missing on {0} class.";

        public MissingTableAttributException(string className):base(string.Format(ERROR_MESSAGE,className)) {
         }

    }
}