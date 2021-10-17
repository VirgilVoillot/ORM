using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class Entity_TST
    {

        private CustomerEntity entity;

        private void Initialize(){

            entity = new CustomerEntity();

        }


        [TestMethod]
        public void GivenFakeEntity_WhenWeAskTableName_ThenReturnConstTableName()
        {
            Initialize();

            var result = (TableAttribute)System.Attribute.GetCustomAttribute(entity.GetType(), typeof(TableAttribute));

            Assert.AreEqual(Constants.TABLE_NAME, result.Name);
        }

        [TestMethod]
        public void GivenColumnNameAsAttributColumn_WhenWeLookForThisAttribut_ThenWeFindIt()
        {
            Initialize();
            bool hasAttribute = false;
            
            foreach(System.Reflection.PropertyInfo property in entity.GetType().GetProperties()){
                
                if(property.Name == Constants.PROPERTY_NAME){

                    var attr = (ColumnAttribute)System.Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));

                    if(attr != null){
                        hasAttribute = attr.Name == Constants.COLUMN_CLIENT_ID_NAME;
                    }
                }
                                
            }

            Assert.IsTrue(hasAttribute);
        }
              
    }
}
