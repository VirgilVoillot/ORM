using System;
using System.Collections.Generic;
using System.Linq;

namespace ORM {

    public class TransformerSQL : ITransformerSQL{

        public SQLconstruction createSelectRequest<T>(T entity) where T:Entity{

            SQLconstruction sql = new SQLconstruction();
            sql.SQLrequest = GetSelectPartOfRequest<T>();
            AdjustSQLconstructionWithEntityForConditions(sql,entity, false);
            return sql;
        }

        private string GetSelectPartOfRequest<T>() where T:Entity{
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

        public SQLconstruction createDeleteRequest<T>(T entity) where T:Entity{
            SQLconstruction sql = new SQLconstruction();
            TableAttribute attr = GetTableAttribute<T>();
            sql.SQLrequest = ConstantsSQL.KEYWORD_DELETE + ConstantsSQL.KEYWORD_FROM + attr.Name;
            AdjustSQLconstructionWithEntityForConditions(sql,entity,true);
            return sql;
        }

        public SQLconstruction createUpdateRequest<T>(T entity) where T:Entity{
            SQLconstruction sql = new SQLconstruction();
            TableAttribute attr = GetTableAttribute<T>();
            sql.SQLrequest = String.Format(ConstantsSQL.KEYWORD_UPDATE, attr.Name);
            AdjustSQLconstructionWithEntityForChangement(sql,entity);
            AdjustSQLconstructionWithEntityForConditions(sql,entity, true);
            return sql;
        }

        

        private TableAttribute GetTableAttribute<T>() where T:Entity{
            TableAttribute tableAttribute = (TableAttribute)System.Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            if(tableAttribute == null)
                throw new MissingTableAttributException(typeof(T).Name);
            return tableAttribute;
        }

        private void AdjustSQLconstructionWithEntityForChangement(SQLconstruction sql,Entity entity){

            List<KeyValuePair<string,object>> listOfParameter = new List<KeyValuePair<string,object>>();
            foreach(System.Reflection.PropertyInfo property in entity.GetType().GetProperties()){
                
                var attr = (ColumnAttribute)System.Attribute.GetCustomAttribute(property, typeof(ColumnAttribute));
                if(attr == null)
                    continue;
                if(attr.IsPrimaryKey || attr.PreventUpdate)
                    continue;
                var value = property.GetValue(entity);

                listOfParameter.Add(new KeyValuePair<string, object>(attr.Name,value));
            }

            if(!listOfParameter.Any())
                throw new NoColumnToChangeException();

            SQLparameter[] parameters = new SQLparameter[listOfParameter.Count];
            List<string> listOfSearchCondition = new List<string>();

            for(int index = 0; index < listOfParameter.Count; index++){

                SQLparameter param = new SQLparameter();
                param.Name = ConstantsSQL.PARAM_NAME+index.ToString();
                param.Value = listOfParameter[index].Value;
                parameters[index] = param;

                listOfSearchCondition.Add(listOfParameter[index].Key + ConstantsSQL.EQUALITY + ConstantsSQL.BINDVARIABLE+param.Name);
            }

            if(listOfSearchCondition.Any()){
                sql.SQLrequest += String.Join(ConstantsSQL.COMMON_AND_SPACE, listOfSearchCondition.ToArray());
                sql.Params = parameters;
                listOfSearchCondition.Clear();
            }
            listOfParameter.Clear();
            listOfParameter = null;
            listOfSearchCondition = null;
        }

        private void AdjustSQLconstructionWithEntityForConditions(SQLconstruction sql,Entity entity, bool useOnlyPrimaryKey){

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

            if(useOnlyPrimaryKey && !listOfParameter.Any())
                throw new GenericFilterInRequestException();

            int numberOfParameterAlreadyInRequest = sql.Params.Length;
            SQLparameter[] parameters = new SQLparameter[listOfParameter.Count];
            List<string> listOfSearchCondition = new List<string>();

            for(int index = 0; index < listOfParameter.Count; index++){

                SQLparameter param = new SQLparameter();
                param.Name = ConstantsSQL.PARAM_NAME+(numberOfParameterAlreadyInRequest+index).ToString();
                param.Value = listOfParameter[index].Value;
                parameters[index] = param;

                listOfSearchCondition.Add(listOfParameter[index].Key + ConstantsSQL.EQUALITY + ConstantsSQL.BINDVARIABLE+param.Name);
            }

            if(listOfSearchCondition.Any()){
                sql.SQLrequest += ConstantsSQL.KEYWORD_WHERE + String.Join(ConstantsSQL.KEYWORD_AND, listOfSearchCondition.ToArray());
                List<SQLparameter> list = new List<SQLparameter>(sql.Params);
                list.AddRange(parameters);
                sql.Params = list.ToArray();
                list.Clear();
                list = null;
                listOfSearchCondition.Clear();
            }
            listOfParameter.Clear();
            listOfParameter = null;
            listOfSearchCondition = null;
        }
    }
}