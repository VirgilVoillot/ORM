using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class ColumnAttribute_TST
    {
        private ColumnAttribute column;

        private void Initialize(){
            this.column = new ColumnAttribute(Constants.COLUMN_CLIENT_ID_NAME);
        }

        [TestMethod]
        public void GivenNameInColumn_WhenWeAskNameProperty_ThenReturnConstColumnName()
        {
            Initialize();

            string result = column.Name;

            Assert.AreEqual(Constants.COLUMN_CLIENT_ID_NAME, result);
        }
    }
}