using System;
using System.Collections.Generic;

namespace ORM {

    public class TransformerSQL : ITransformerSQL{

        public SQLconstruction createSelectRequest<T>(){

            SQLconstruction sql = new SQLconstruction();
            string request = GetSelectPartOfRequest<T>();
            sql.SQLrequest = request;
            
            return sql;
        }

        private string GetSelectPartOfRequest<T>(){
            string request = ConstantsSQL.KEYWORD_SELECT;

            var tableAttribute = (TableAttribute)System.Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            if(tableAttribute == null)
                throw new MissingTableAttributException(typeof(T).Name);

            List<string> listOfColumns = new List<string>();

            foreach(System.Reflection.PropertyInfo property in typeof(T).GetProperties()){
                
                var attr = (ColumnAttribute)System.Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));
                if(attr == null)
                    continue;
                listOfColumns.Add(attr.Name);
            }
            request += string.Join(ConstantsSQL.COMMON_AND_SPACE, listOfColumns.ToArray()) + ConstantsSQL.KEYWORD_FROM + tableAttribute.Name;

            listOfColumns.Clear();
            listOfColumns = null;
            return request;
        }


    }
}