using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ORM;


namespace ORM_TST
{
    [TestClass]
    public class TransformerSQL_UPDATE_TST
    {
        
        private const string UPDATE_REQUEST = "UPDATE " + Constants.TABLE_NAME + " SET " + Constants.COLUMN_CLIENT_FIRSTNAME_NAME + " = :param0, " +
        Constants.COLUMN_CLIENT_LASTNAME_NAME + " = :param1" + ConstantsSQL.KEYWORD_WHERE + Constants.COLUMN_CLIENT_ID_NAME + " = :param2";

        private TransformerSQL transformerSQL;
        private CustomerEntity entity;

        private void Initialize(){
            this.transformerSQL = new TransformerSQL();
        }



        [TestMethod()]
        [ExpectedException(typeof(MissingTableAttributException))]
        public void GivenAClassWithoutTableAttribute_WhenCreateUpdateRequest_ThenThrowMissingTableAttributeException(){
            Initialize();
            var obj = new EntityWithoutAttribute();

            transformerSQL.createUpdateRequest<EntityWithoutAttribute>(obj);
        }

        [TestMethod()]
        [ExpectedException(typeof(NoColumnToChangeException))]
        public void GivenAEntityWhereAllAttributArePreventUpdating_WhenCreateUpdateRequest_ThenThrowNoColumnToChangeException(){
            Initialize();
            var obj = new NoUpdateEntity();
            obj.ClientFirstName = Constants.FIRST_NAME;
            obj.ClientID = Constants.CLIENT_ID;
            
            transformerSQL.createUpdateRequest<NoUpdateEntity>(obj);
        }





        private void InitializeEntityWithoutValueAsPropriety(){
            Initialize();
            entity = new CustomerEntity();
            entity.ClientID = Constants.CLIENT_ID;
            entity.ClientFirstName = Constants.FIRST_NAME;
            entity.ClientLastName = Constants.LAST_NAME;
        }

        [TestMethod()]
        public void GivenEntityWithPrimaryKey_WhenCreateSqlRequest_ThenRequestNotModifyPrimaryKey(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createUpdateRequest<CustomerEntity>(entity);

            Assert.AreEqual(UPDATE_REQUEST, result.SQLrequest);
        }

        [TestMethod()]
        public void GivenEntity_WhenCreateSqlRequest_ThenParameterContainsAllAttributes(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createUpdateRequest<CustomerEntity>(entity);

            Assert.AreEqual(entity.ClientFirstName, result.Params[0].Value);
            Assert.AreEqual(entity.ClientLastName, result.Params[1].Value);
            Assert.AreEqual(entity.ClientID, result.Params[2].Value);
        }
    }
}