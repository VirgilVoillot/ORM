using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class TransformerSQL_DELETE_TST
    {
        private const string REQUEST_SELECT_WITH_CLIENT_ID_CONDITION = ConstantsSQL.KEYWORD_DELETE + ConstantsSQL.KEYWORD_FROM + Constants.TABLE_NAME + 
        ConstantsSQL.KEYWORD_WHERE + Constants.COLUMN_CLIENT_ID_NAME + " = :param0";

        private TransformerSQL transformerSQL;
        private CustomerEntity entity;

        private void Initialize(){
            this.transformerSQL = new TransformerSQL();
            this.entity = new CustomerEntity();
        }

        [TestMethod()]
        [ExpectedException(typeof(GenericDeleteException))]
        public void GivenEntityWithoutPrimaryKeyWithValue_WhenCreateDeleteRequest_ThenThrowGenericDeleteException(){
            Initialize();

            transformerSQL.createDeleteRequest<CustomerEntity>(entity);
        }

        private void InitializeWithOnePrimaryKey(){
            Initialize();
            entity.ClientID = Constants.CLIENT_ID;
        }


        [TestMethod()]
        public void GivenEntityWithPrimaryKey_WhenCreateDeleteRequest_ThenVerifySQLRequestWhyDatabinding(){
            InitializeWithOnePrimaryKey();

            SQLconstruction result = transformerSQL.createDeleteRequest<CustomerEntity>(entity);

            Assert.AreEqual(REQUEST_SELECT_WITH_CLIENT_ID_CONDITION, result.SQLrequest);
        }

        
        [TestMethod()]
        public void GivenEntityWithPrimaryKey_WhenCreateDeleteRequest_ThenVerifyParameterIsSamePrimaryKey(){
            InitializeWithOnePrimaryKey();

            SQLconstruction result = transformerSQL.createDeleteRequest<CustomerEntity>(entity);

            Assert.AreEqual(entity.ClientID, result.Params[0].Value);
        }
    }
}