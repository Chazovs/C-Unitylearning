using System;
using System.Collections.Generic;

public class ServiceLocator
{
    private static Dictionary<string, object> services = new Dictionary<string, object>();

    public static T GetService<T>()
    {
        if (services.ContainsKey(typeof(T).Name))
        {
            return (T)services[typeof(T).Name];
        }
        else if (typeof(T) != null)
        {
            Type testType = typeof(T);
            System.Reflection.ConstructorInfo ci = testType.GetConstructor(new Type[] { });
            object service = ci.Invoke(new object[] { });

            services.Add(typeof(T).Name, service);

            return (T)services[typeof(T).Name];
        }

        throw new Exception("Сервис не найден");
    }

    internal static void Reset()
    {
        services = new Dictionary<string, object>();
    }
}
