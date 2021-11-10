using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;

namespace TestProject1
{
    /// <summary>
    /// This class is picked up by FakeItEasy automatically and kicks in when we need to fake
    /// an IQueryable&lt;T&gt;. Instead of creating a dynamic proxy for IQueryable that does
    /// not play well with AutoMapper, we make it an empty List&lt;T&gt; that can be treated
    /// as IQueryable&lt;T&gt;
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class DummyQueryableFactory : IDummyFactory
    {
        public bool CanCreate(Type type)
        {
            if (type.IsGenericType)
            {
                var queryableContentType = type.GetGenericArguments()[0];
                var queryableTypeDefinition = typeof(IQueryable<>).MakeGenericType(queryableContentType);
                return queryableTypeDefinition.IsAssignableFrom(type);
            }
            return false;
        }

        public object Create(Type type)
        {
            var queryableContentType = type.GetGenericArguments()[0];
            var listType = typeof(List<>).MakeGenericType(queryableContentType);
            var list = (IEnumerable)Activator.CreateInstance(listType);
            return list.AsQueryable();
        }

        public Priority Priority => Priority.Default;
    }
}