//  <copyright file="ArrayMatcher.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.AspNetCore.Mvc.Proxying.Utils
{
    internal static class ArrayMatcher
    {
        public static T[] Match<T>(T[] sourceArray, T[] destinationArray)
        {
            var result = new List<T>();

            var currentMethodParamIndex = 0;
            var parentItem = default(T);

            foreach (var sourceItem in sourceArray)
            {
                if (currentMethodParamIndex < destinationArray.Length)
                {
                    var destinationItem = destinationArray[currentMethodParamIndex];
                    
                    if (EqualityComparer<T>.Default.Equals(sourceItem, destinationItem))
                    {
                        parentItem = default(T);
                        currentMethodParamIndex++;
                    }
                    else
                    {
                        if (parentItem == null)
                        {
                            parentItem = destinationItem;
                            currentMethodParamIndex++;
                        }
                    }
                }

                var resultItem = EqualityComparer<T>.Default.Equals(parentItem, default(T)) ? sourceItem : parentItem;
                result.Add(resultItem);
            }

            return result.ToArray();
        }
    }
}
