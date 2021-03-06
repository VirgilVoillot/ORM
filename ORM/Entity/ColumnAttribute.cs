using System;

namespace ORM
{
    [System.AttributeUsage(System.AttributeTargets.Property,
                       AllowMultiple = false)
                       ]
    public class ColumnAttribute : Attribute
    {
        
        public string Name;

        public ColumnAttribute(string _name){
            this.Name = _name;
        }

    }
}
