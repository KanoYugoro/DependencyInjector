using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace DependencyInjectionContainer.DependencyInjection
{
    public enum lifeStyleType { Singleton, Transient };

    public class DependencyResolver
    {
        private Dictionary<Type, object> registeredSingletons;
        private Dictionary<Type, Type> registeredTransients;

        public Dictionary<Type, Type> getRegisteredTransients()
        {
            return registeredTransients;
        }

        public Dictionary<Type, object> getRegisteredSingletons()
        {
            return registeredSingletons;
        }

        public DependencyResolver()
        {
            registeredSingletons = new Dictionary<Type, object>();
            registeredTransients = new Dictionary<Type, Type>();
        }

        public object Resolve(Type t)
        {
            if (registeredSingletons.ContainsKey(t))
            {
                return registeredSingletons[t];
            }
            else
            if (registeredTransients.ContainsKey(t))
            {
                Type resolvedType = registeredTransients[t];
                ConstructorInfo[] Constructors = resolvedType.GetConstructors();
                ConstructorInfo theConstructorICareAbout = Constructors[0]; //Questionable at best

                ParameterInfo[] ParameterList = theConstructorICareAbout.GetParameters();

                if (ParameterList.Length > 0)
                {
                    Object[] resolvedParams = new Object[ParameterList.Length];

                    for (int i = 0; i < resolvedParams.Length; i++)
                    {
                        Type paramType = ParameterList[i].ParameterType;
                        resolvedParams[i] = this.Resolve(paramType);
                    }

                    return Activator.CreateInstance(resolvedType, resolvedParams);
                }
                else
                {
                    return Activator.CreateInstance(resolvedType);
                }
            }
            else
            {
                throw new InvalidOperationException(String.Format("The type requested ({0}) has not been registered with this dependency resolver.", t.ToString()));
            }
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public void Register<theInterface, theImplementation>(object theSingleton)
        {
            if (!registeredSingletons.ContainsKey(typeof(theInterface)) && !registeredTransients.ContainsKey(typeof(theInterface)))
            {
                registeredSingletons.Add(typeof(theInterface), (theImplementation)theSingleton);
            }
        }

        public void Register<theInterface, theImplementation>(lifeStyleType style = lifeStyleType.Transient)
        {
            if (!registeredSingletons.ContainsKey(typeof(theInterface)) && !registeredTransients.ContainsKey(typeof(theInterface)))
            {
                switch (style)
                {
                    case lifeStyleType.Singleton:
                        Type resolvedType = typeof(theImplementation);
                        ConstructorInfo[] Constructors = resolvedType.GetConstructors();
                        ConstructorInfo theConstructorICareAbout = Constructors[0];
                        ParameterInfo[] ParameterList = theConstructorICareAbout.GetParameters();

                        if (ParameterList.Length == 0)
                        {
                            registeredSingletons.Add(typeof(theInterface), (theImplementation)Activator.CreateInstance(typeof(theImplementation)));
                        }
                        else
                        {
                            Object[] resolvedParams = new Object[ParameterList.Length];

                            for (int i = 0; i < resolvedParams.Length; i++)
                            {
                                Type paramType = ParameterList[i].ParameterType;
                                resolvedParams[i] = this.Resolve(paramType);
                            }
                            registeredSingletons.Add(typeof(theInterface), (theImplementation)Activator.CreateInstance(resolvedType, resolvedParams));
                        }
                        break;

                    case lifeStyleType.Transient:
                        registeredTransients.Add(typeof(theInterface), typeof(theImplementation));
                        break;
                }
            }
            else
            {
                throw new InvalidOperationException(String.Format("The type to be registered ({0}) has already been registered with this dependency resolver.", typeof(theInterface).ToString()));
            }
        }
    }
}