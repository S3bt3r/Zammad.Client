using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Zammad.Client.IntegrationTests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }

    public class TestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            return testCases
                .OrderBy(t => t.GetOrder());
        }
    }

    public static class ITestCaseExtensions
    {
        public static int GetOrder(this ITestCase testCase)
        {
            var order = testCase
                .TestMethod
                .Method
                .GetCustomAttributes(typeof(OrderAttribute).AssemblyQualifiedName)
                .LastOrDefault();
            return order != null ? order.GetNamedArgument<int>(nameof(OrderAttribute.Order)) : int.MaxValue;
        }

    }

    public static class IDictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result) == false)
            {
                result = new TValue();
                dictionary.Add(key, result);
            }
            
            return result;
        }
    }
}
