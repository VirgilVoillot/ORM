using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;

namespace ORM_TST
{
    [TestClass]
    public class Entity_TST
    {

        private const string TABLE_NAME = "CUSTOMER";
        private const string COLUMN_NAME = "CLIENT_ID";
        private const string PROPERTY_NAME = "ClientID";

        private class FakeEntity : Entity {
            
            public override string Table(){
                return TABLE_NAME;
            }

            private int _clientID;
            [ColumnAttribute(COLUMN_NAME)]
            public int ClientID{
                get => _clientID;
                set => _clientID = value;
            }

        }

        private FakeEntity entity;

        private void Initialize(){

            entity = new FakeEntity();

        }


        [TestMethod]
        public void GivenFakeEntity_WhenWeAskTableName_ThenReturnConstTableName()
        {
            Initialize();

            string result = entity.Table();

            Assert.AreEqual(TABLE_NAME, result);
        }

        [TestMethod]
        public void GivenColumnNameAsAttributColumn_WhenWeLookForThisAttribut_ThenWeFinddIt()
        {
            Initialize();
            bool hasAttribute = false;
            
            foreach(System.Reflection.PropertyInfo property in entity.GetType().GetProperties()){
                
                if(property.Name == PROPERTY_NAME){

                    var attr = (ColumnAttribute)System.Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));

                    if(attr != null){
                        hasAttribute = attr.Name == COLUMN_NAME;
                    }
                }
                                
            }

            Assert.IsTrue(hasAttribute);
        }
              
    }
}
