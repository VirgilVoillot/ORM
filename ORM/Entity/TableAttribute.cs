using System;

namespace ORM
{
    [System.AttributeUsage(System.AttributeTargets.Class,
                       AllowMultiple = false)
                       ]
    public class TableAttribute : Attribute
    {
        
        public string Name;

        public TableAttribute(string _name){
            this.Name = _name;
        }

    }
}
