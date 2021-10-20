using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM {

    public class TransformerSQL : ITransformerSQL{

        public SQLconstruction createSelectRequest<T>(T entity){

            SQLconstruction sql = new SQLconstruction();
            sql.SQLrequest = GetSelectPartOfRequest<T>();
            AdjustSQLcontructionWithEntityForConditions(sql,entity, false);
            return sql;
        }

        public SQLconstruction createDeleteRequest<T>(T entity){
            SQLconstruction sql = new SQLconstruction();
            TableAttribute attr = GetTableAttribute<T>();
            sql.SQLrequest = ConstantsSQL.KEYWORD_DELETE + ConstantsSQL.KEYWORD_FROM + attr.Name;
            AdjustSQLcontructionWithEntityForConditions(sql,entity,true);
            if(!sql.Params.Any())
                throw new GenericDeleteException(sql.SQLrequest);
            return sql;
        }

        private string GetSelectPartOfRequest<T>(){
            string request = ConstantsSQL.KEYWORD_SELECT;

            var tableAttribute = GetTableAttribute<T>();

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

        private TableAttribute GetTableAttribute<T>(){
            TableAttribute tableAttribute = (TableAttribute)System.Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            if(tableAttribute == null)
                throw new MissingTableAttributException(typeof(T).Name);
            return tableAttribute;
        }

        private void AdjustSQLcontructionWithEntityForConditions(SQLconstruction sql,object entity, bool useOnlyPrimaryKey){

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
                if(useOnlyPrimaryKey && !attr.IsPrimaryKey)
                    continue;

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