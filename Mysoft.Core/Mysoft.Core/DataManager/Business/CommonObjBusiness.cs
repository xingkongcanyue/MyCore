using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager.Business
{
    public class CommonObjBusiness
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public DateTime CreateOn { get; set; }

        public int OrderNum { get; set; }

        public DynamicProperties DynamicProperties { get; set; }
        
        

    }
    public class DynamicProperties
    {
        Dictionary<string, DynamicProperty> _properties = new Dictionary<string, DynamicProperty>();

        public object this[string name]
        {
            get{return _properties[name].Value;}
            set { _properties[name].Value = value; }
        }

        public Dictionary<string, DynamicProperty> All { get { return _properties; } }
    
    }
    public class DynamicProperty
    {
        public MetaPropertyDefine MetaDefine{get;set;}

        public object Value{get;set;}
    }
}
