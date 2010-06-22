using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionAlumno
{
    interface IOrderSerchStruct<T>
    {
        int Size { get; }
        void Add(T item);
        void Clear();
        bool Contains(T item);
        T FindInStruct(T item);
        bool Remove(T item);
        T getMinValue();
        T getMinimoAndRemove();

    }
}

