using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using AspNet.System.Extensions;


/// <summary>
/// 一些扩展方法，用于反射操作，它们都可以优化反射性能。
/// </summary>
public static class ReflectionExtensions
{
    private static readonly Hashtable GetterDic = Hashtable.Synchronized(new Hashtable(10240));
    private static readonly Hashtable SetterDic = Hashtable.Synchronized(new Hashtable(10240));
    private static readonly Hashtable MethodDic = Hashtable.Synchronized(new Hashtable(10240));


    public static object FastGetValue(this FieldInfo fieldInfo, object obj)
    {
        if (fieldInfo == null)
            throw new ArgumentNullException(nameof(fieldInfo));

        GetValueDelegate getter = (GetValueDelegate) GetterDic[fieldInfo];
        if (getter == null)
        {
            getter = DynamicMethodFactory.CreateFieldGetter(fieldInfo);
            GetterDic[fieldInfo] = getter;
        }

        return getter(obj);
    }

    public static void FastSetField(this FieldInfo fieldInfo, object obj, object value)
    {
        if (fieldInfo == null)
            throw new ArgumentNullException(nameof(fieldInfo));

        SetValueDelegate setter = (SetValueDelegate) SetterDic[fieldInfo];
        if (setter == null)
        {
            setter = DynamicMethodFactory.CreateFieldSetter(fieldInfo);
            SetterDic[fieldInfo] = setter;
        }

        setter(obj, value);
    }


    public static object FastNew(this Type instanceType)
    {
        if (instanceType == null)
            throw new ArgumentNullException(nameof(instanceType));

        CtorDelegate ctor = (CtorDelegate) MethodDic[instanceType];
        if (ctor == null)
        {
            ConstructorInfo ctorInfo = instanceType.GetConstructor(Type.EmptyTypes);
            ctor = DynamicMethodFactory.CreateConstructor(ctorInfo);
            MethodDic[instanceType] = ctor;
        }

        return ctor();
    }


    public static object FastGetValue(this PropertyInfo propertyInfo, object obj)
    {
        if (propertyInfo == null)
            throw new ArgumentNullException(nameof(propertyInfo));

        GetValueDelegate getter = (GetValueDelegate) GetterDic[propertyInfo];
        if (getter == null)
        {
            getter = DynamicMethodFactory.CreatePropertyGetter(propertyInfo);
            GetterDic[propertyInfo] = getter;
        }

        return getter(obj);
    }

    public static void FastSetValue(this PropertyInfo propertyInfo, object obj, object value)
    {
        if (propertyInfo == null)
            throw new ArgumentNullException(nameof(propertyInfo));

        SetValueDelegate setter = (SetValueDelegate) SetterDic[propertyInfo];
        if (setter == null)
        {
            setter = DynamicMethodFactory.CreatePropertySetter(propertyInfo);
            SetterDic[propertyInfo] = setter;
        }

        setter(obj, value);
    }


    public static object FastInvoke(this MethodInfo methodInfo, object obj, params object[] parameters)
    {
        if (methodInfo == null)
            throw new ArgumentNullException(nameof(methodInfo));

        MethodDelegate invoker = (MethodDelegate) MethodDic[methodInfo];
        if (invoker == null)
        {
            invoker = DynamicMethodFactory.CreateMethod(methodInfo);
            MethodDic[methodInfo] = invoker;
        }

        return invoker(obj, parameters);
    }
}

