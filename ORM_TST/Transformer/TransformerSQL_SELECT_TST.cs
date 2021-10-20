using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ORM;


namespace ORM_TST
{
    [TestClass]
    public class TransformerSQL_SELECT_TST
    {
        
        private const string REQUEST_SELECT_WITHOUT_CONDITION = ConstantsSQL.KEYWORD_SELECT + Constants.COLUMN_CLIENT_ID_NAME + ", " + 
        Constants.COLUMN_CLIENT_FIRSTNAME_NAME + ", " + Constants.COLUMN_CLIENT_LASTNAME_NAME + ConstantsSQL.KEYWORD_FROM + Constants.TABLE_NAME;

        private const string REQUEST_SELECT_WITH_CLIENT_ID_CONDITION = REQUEST_SELECT_WITHOUT_CONDITION + 
        ConstantsSQL.KEYWORD_WHERE + Constants.COLUMN_CLIENT_ID_NAME + " = :param0";

        private const string REQUEST_SELECT_WITH_REGISTRATION = ConstantsSQL.KEYWORD_SELECT + Constants.COLUMN_CLIENT_ID_NAME + ", " + 
        Constants.COLUMN_IS_REGISTERD_NAME+ ConstantsSQL.KEYWORD_FROM + Constants.TABLE_REGISTRATION_NAME + ConstantsSQL.KEYWORD_WHERE + 
        Constants.COLUMN_IS_REGISTERD_NAME + " = :param0";
        private TransformerSQL transformerSQL;
        private CustomerEntity entity;

        private void Initialize(){
            this.transformerSQL = new TransformerSQL();
        }



        [TestMethod()]
        [ExpectedException(typeof(MissingTableAttributException))]
        public void GivenAClassWithoutTableAttribute_WhenCreateSelectRequest_ThenThrowMissingTableAttributeException(){
            Initialize();
            var obj = new object();

            SQLconstruction result = transformerSQL.createSelectRequest<object>(obj);
        }





        private void InitializeEntityWithoutValueAsPropriety(){
            Initialize();
            entity = new CustomerEntity();
        }        

        [TestMethod()]
        public void GivenCustomerEntityClass_WhenGetSQLSelect_ThenReturnConstructionWithRequest(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<CustomerEntity>(entity);

            Assert.AreEqual(REQUEST_SELECT_WITHOUT_CONDITION, result.SQLrequest);
        }

        [TestMethod()]
        public void GivenCustomerEntityClass_WhenGetSQLSelect_ThenReturnConstructionWithEmptyArrayOfParameter(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<CustomerEntity>(entity);

            Assert.IsFalse(result.Params.Any());
        }


        private void InitializeEntityWithOneValueAsPropriety(){
            InitializeEntityWithoutValueAsPropriety();
            entity.ClientID = Constants.CLIENT_ID;

        } 


        [TestMethod()]
        public void GivenCustomerEntityClassWithOnePropertyWithValue_WhenGetSQLSelect_ThenReturnConstructionWithRequest(){
            InitializeEntityWithOneValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<CustomerEntity>(entity);

            Assert.AreEqual(REQUEST_SELECT_WITH_CLIENT_ID_CONDITION, result.SQLrequest);
        }

        [TestMethod()]
        public void GivenCustomerEntityClassWithOnePropertyWithOneValue_WhenGetSQLSelect_ThenReturnConstructionWithOneParameterInArray(){
            InitializeEntityWithOneValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<CustomerEntity>(entity);

            Assert.IsTrue(result.Params.Length == 1);
        }


        [TestMethod()]
        public void GivenRegistrationEntityClassWithNoPropertyInstanciedOrWithDefaultValue_WhenGetSQLSelect_ThenReturnConstructionWithSelectQithOneCOndition(){
            Initialize();

            SQLconstruction result = transformerSQL.createSelectRequest<RegistrationEntity>(new RegistrationEntity());

            Assert.AreEqual(REQUEST_SELECT_WITH_REGISTRATION,result.SQLrequest);
        }

        [TestMethod()]
        public void GivenRegistrationEntityClassWithNoPropertyInstanciedOrWithDefaultValue_WhenGetSQLSelect_ThenReturnConstructionWithOneParameterInArray(){
            Initialize();

            SQLconstruction result = transformerSQL.createSelectRequest<RegistrationEntity>(new RegistrationEntity());

            Assert.IsFalse((bool)result.Params[0].Value);
        }
    }
}