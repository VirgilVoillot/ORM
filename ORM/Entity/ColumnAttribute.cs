using System;

namespace ORM
{
    [System.AttributeUsage(System.AttributeTargets.Property,
                       AllowMultiple = false)
                       ]
    public class ColumnAttribute : Attribute
    {
        
        public string Name;
        public bool IncludeDefaultValueInResearch;
        public bool IsPrimaryKey;
        public bool PreventUpdate;

        public ColumnAttribute(string _name){
            this.Name = _name;
            IncludeDefaultValueInResearch = false;
            IsPrimaryKey = false;
            PreventUpdate = false;
        }

    }
}
