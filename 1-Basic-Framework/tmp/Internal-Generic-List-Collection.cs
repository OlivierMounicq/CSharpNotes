using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalGenericList
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadOnlyCollection
            var collection = new ReadOnlyCollection<string>(new List<string> { "a", "z" });
            Display(collection);

            //Try to add an item by using a backdoor
            try
            {
                var myList2 = collection as IList<string>;
                myList2.Add("e");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine("Bad idea Gallahan but you cannot not add an item! Don't try again!");
                Console.WriteLine(ex);
            }

            //Ok the first one didn't run, but this one will
            var internalList = GetInternalList<string>(collection);
            internalList.Add("e");
            internalList.Add("r");
            internalList.Add("t");
            internalList.Add("y");

            Display(collection);

            Console.WriteLine("OK Callahan, well done: you break the locker");

            //Internal array of a generic list
            var genericList = new List<string>() { "1", "2", "3", "4", "5" } ;
            Display(genericList);

            var internalArray = GetInternalArray<string>(genericList);
            Display(internalArray.AsEnumerable());

            Console.WriteLine("Internal array length : {0}", internalArray.Length);
            internalArray[5] = "6";
            Display(internalArray.AsEnumerable());
            Display(genericList);

            genericList.Add("666");
            Display(genericList);

            var s = Console.ReadLine();
        }

        public static void Display(IEnumerable<string> list)
        {
            Console.WriteLine("****************************************************************");
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }

        //The name of the inner generic list List<T> of the ReadOnlyCollection : list
        public static List<T> GetInternalList<T>(ReadOnlyCollection<T> collection)
        {
            var field = collection.GetType().
                GetField("list", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var internalList = field.GetValue(collection) as List<T>;

            return internalList;
        }

        //The name of the inner array of a generic list List<T>: _items
        public static T[] GetInternalArray<T>(List<T> list)
        {
            var field = list.GetType().
                GetField("_items", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var internalArray = field.GetValue(list) as T[];

            return internalArray;
        }
        
        //Get the version of the list
        public static int GetVersion<T>(List<T> list)
        {
            var field = list.GetType().
                GetField("_version", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var version = field.GetValue(list) as int;

            return version;
        }        
    }
}
