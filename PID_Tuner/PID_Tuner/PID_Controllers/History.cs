using System.Collections.Generic;

namespace PID_Tuner
{
    public class History<T>
    {
        private int maxCapacity { get; set; }
        List<T> list;

        public History(int length)
        {
            list = new List<T>(new T[length]);
            maxCapacity = length;
        }

        public T this[int i]
        {
            get { return list[i]; }
        }

        public void Add(T itm)
        {
            list.Insert(0, itm);

            int trim = list.Count - maxCapacity;
            if (trim > 0)
                list.RemoveRange(maxCapacity, trim);
        }
    }

    

}
