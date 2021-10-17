using System;

namespace ORM {

    public class SQLconstruction{

        private string _sqlRequest;
        public string SQLrequest{
            get=> _sqlRequest;
            set=> _sqlRequest = value;
        }

        private SQLparameter[] _params;
        public SQLparameter[] Params{
            get=> _params;
            set=> _params = value;
        }

        public SQLconstruction(){
            _sqlRequest = String.Empty;
            _params = new SQLparameter[0];
        }
    }
}