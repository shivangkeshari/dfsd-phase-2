using System;
using System.Data.Common;

namespace API.Common.Models
{
    public static class DatabaseModelFactory
    {
        public static IDatabaseModel Create(string databaseModelAssemblyQualifiedName, DbDataReader dr)
        {
            var handlerType = Type.GetType(databaseModelAssemblyQualifiedName, true);
            return (IDatabaseModel)Activator.CreateInstance(
                handlerType, dr);
        }
    }
}