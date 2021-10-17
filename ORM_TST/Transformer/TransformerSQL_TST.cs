using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class TransformerSQL_TST
    {
        
        private const string REQUEST_SELECT_WITHOUT_CONDITION = ConstantsSQL.KEYWORD_SELECT + Constants.COLUMN_CLIENT_ID_NAME + ", " + 
        Constants.COLUMN_CLIENT_FIRSTNAME_NAME + ", " + Constants.COLUMN_CLIENT_LASTNAME_NAME + ConstantsSQL.KEYWORD_FROM + Constants.TABLE_NAME;

        private TransformerSQL transformerSQL;
        private FakeEntity entity;

        private void Initialize(){
            this.transformerSQL = new TransformerSQL();
        }

        [TestMethod()]
        [ExpectedException(typeof(MissingTableAttributException))]
        public void GivenAClassWithoutTableAttribute_WhenCreateSelectRequest_ThenThrowMissingTableAttributeException(){
            Initialize();

            SQLconstruction result = transformerSQL.createSelectRequest<object>();
        }





        private void InitializeEntityWithoutValueAsPropriety(){
            Initialize();
            entity = new FakeEntity();
        }        

        [TestMethod()]
        public void GivenFakeEntityClass_WhenGetSQLSelect_ThenReturnConstructionWithRequest(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<FakeEntity>();

            Assert.AreEqual(REQUEST_SELECT_WITHOUT_CONDITION, result.SQLrequest);
        }

        [TestMethod()]
        public void GivenFakeEntityClass_WhenGetSQLSelect_ThenReturnConstructionWithEmptyArrayOfParameter(){
            InitializeEntityWithoutValueAsPropriety();

            SQLconstruction result = transformerSQL.createSelectRequest<FakeEntity>();

            Assert.IsTrue(result.Params.Length == 0);
        }

    }
}