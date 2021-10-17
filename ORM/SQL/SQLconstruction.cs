using System;

namespace ORM {

    public class SQLconstruction{

        private string _sqlRequest;
        public string SQLrequest{
            get=> _sqlRequest;
            set=> _sqlRequest = value;
        }

        private object[] _params;
        public object[] Params{
            get=> _params;
            set=> _params = value;
        }

        public SQLconstruction(){
            _sqlRequest = String.Empty;
            _params = new object[0];
        }
    }
}