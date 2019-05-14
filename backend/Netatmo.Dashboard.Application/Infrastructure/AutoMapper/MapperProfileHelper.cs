using Netatmo.Dashboard.Application.Interfaces.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Netatmo.Dashboard.Application.Infrastructure.AutoMapper
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }

    public static class MapperProfileHelper
    {
        public static IEnumerable<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();
            var mapsFrom =
                (from type in types
                 from instance in type.GetInterfaces()
                 where instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                       !type.IsAbstract && !type.IsInterface
                 select new Map
                 {
                     Source = type.GetInterfaces().First().GetGenericArguments().First(),
                     Destination = type
                 }).ToList();
            return mapsFrom;
        }

        public static IEnumerable<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();
            var mapsFrom =
                (from type in types
                 from instance in type.GetInterfaces()
                 where typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                       !type.IsAbstract && !type.IsInterface
                 select Activator.CreateInstance(type) as IHaveCustomMapping).ToList();
            return mapsFrom;
        }
    }
}
