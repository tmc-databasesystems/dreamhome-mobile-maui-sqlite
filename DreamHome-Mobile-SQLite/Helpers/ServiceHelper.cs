using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHome_Mobile_SQLite.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceProvider Services { get; private set; } = default!;
        public static void Initialize(IServiceProvider services) => Services = services;
        public static T GetRequiredService<T>() where T : notnull => Services.GetRequiredService<T>();
    }

}
