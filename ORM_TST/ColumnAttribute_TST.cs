using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class ColumnAttribute_TST
    {
        private const string COLUMN_NAME = "CLIENT_ID";
        private ColumnAttribute column;

        private void Initialize(){
            this.column = new ColumnAttribute(COLUMN_NAME);
        }

        [TestMethod]
        public void GivenNameInColumn_WhenWeAskNameProperty_ThenReturnConstColumnName()
        {
            Initialize();

            string result = column.Name;

            Assert.AreEqual(COLUMN_NAME, result);
        }
    }
}