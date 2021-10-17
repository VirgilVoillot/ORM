using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class TableAttribute_TST
    {
        private TableAttribute table;

        private void Initialize(){
            this.table = new TableAttribute(Constants.TABLE_NAME);
        }

        [TestMethod]
        public void GivenNameInTable_WhenWeAskNameTable_ThenReturnConstTableName()
        {
            Initialize();

            string result = table.Name;

            Assert.AreEqual(Constants.TABLE_NAME, result);
        }
    }
}