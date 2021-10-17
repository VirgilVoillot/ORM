namespace ORM {

    public abstract class ORMException : System.Exception
    {
        public ORMException(string message) : base(message) { }

    }
}