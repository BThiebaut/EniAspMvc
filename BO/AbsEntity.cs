using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public abstract class AbsEntity<T> : IIdentifiable
    {
        public int Id { get; set; }

        public virtual void duplicate<T>(T obj)
        {
            var props = this.GetObjectProperties<T>(obj);
            foreach (var prop in props)
            {
                if (prop != null && prop.CanWrite && prop.Name != "Id")
                {
                    try
                    {
                        prop.SetValue(this, prop.GetValue(obj));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }

        }

        public List<PropertyInfo> GetObjectProperties<T>(T obj)
        {
            Type myType = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            List<PropertyInfo> result = new List<PropertyInfo>();

            foreach (PropertyInfo prop in props)
            {
                result.Add(prop);
            }
            return result;
        }

        


    }
}
