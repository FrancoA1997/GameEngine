using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    public static class Serializer
    {
        public static void ToFile<T>(T instance, string path)
        {
            var attempts = 5;
            while (attempts > 0)
            {
                try
                {
                    using var fs = new FileStream(path, FileMode.Create);
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(fs, instance);
                    attempts = 0; break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    //TODO: log error
                    attempts--;
                }
            }
               
        }
        internal static T FromFile<T>(String path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open);
                var serializer = new DataContractSerializer(typeof(T));
                T instance = (T)serializer.ReadObject(fs);
                return instance;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //TODO: log error
                return default(T);
            }
        }
    }
}
