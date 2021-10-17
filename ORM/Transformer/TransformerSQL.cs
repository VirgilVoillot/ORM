using System;
using System.Collections.Generic;

namespace ORM {

    public class TransformerSQL : ITransformerSQL{

        public SQLconstruction createSelectRequest<T>(T entity){

            SQLconstruction sql = new SQLconstruction();
            sql.SQLrequest = GetSelectPartOfRequest<T>();
            AdjustSQLcontructionWithEntityForConditions(sql,entity);
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

        private void AdjustSQLcontructionWithEntityForConditions(SQLconstruction sql,object entity){

            List<KeyValuePair<string,object>> listOfParameter = new List<KeyValuePair<string,object>>();

            foreach(System.Reflection.PropertyInfo property in entity.GetType().GetProperties()){
                
                var attr = (ColumnAttribute)System.Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));
                if(attr == null)
                    continue;
                var value = property.GetValue(entity);
                if(value == null)
                    continue;
                if(!attr.IncludeDefaultValueInResearch){
                    if(object.Equals(value,default(int)))
                        continue;
                    if(object.Equals(value,default(bool)))
                        continue;
                }
                listOfParameter.Add(new KeyValuePair<string, object>(attr.Name,value));
            }

            SQLparameter[] parameters = new SQLparameter[listOfParameter.Count];
            List<string> listOfSearchCondition = new List<string>();

            for(int index = 0; index < listOfParameter.Count; index++){

                SQLparameter param = new SQLparameter();
                param.Name = ConstantsSQL.PARAM_NAME+index.ToString();
                param.Value = listOfParameter[index].Value;
                parameters[index] = param;

                listOfSearchCondition.Add(listOfParameter[index].Key + ConstantsSQL.EQUALITY + ConstantsSQL.BINDVARIABLE+param.Name);
            }

            if(listOfSearchCondition.Count != 0){
                sql.SQLrequest += ConstantsSQL.KEYWORD_WHERE + String.Join(ConstantsSQL.KEYWORD_AND, listOfSearchCondition.ToArray());
                sql.Params = parameters;
                listOfSearchCondition.Clear();
            }
            listOfParameter.Clear();
            listOfParameter = null;
            listOfSearchCondition = null;
        }
    }
}