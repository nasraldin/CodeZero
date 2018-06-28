//  <copyright file="CodeZeroCrossCuttingConcerns.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Services;
using CodeZero.Collections.Extensions;
using JetBrains.Annotations;
using System;

namespace CodeZero.Aspects
{
    public static class CodeZeroCrossCuttingConcerns
    {
        public const string Auditing = "CodeZeroAuditing";
        public const string Validation = "CodeZeroValidation";
        public const string UnitOfWork = "CodeZeroUnitOfWork";
        public const string Authorization = "CodeZeroAuthorization";

        public static void AddApplied(object obj, params string[] concerns)
        {
            if (concerns.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
            }

            (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.AddRange(concerns);
        }

        public static void RemoveApplied(object obj, params string[] concerns)
        {
            if (concerns.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
            }

            //var crossCuttingEnabledObj = obj as IAvoidDuplicateCrossCuttingConcerns;
            if (!(obj is IAvoidDuplicateCrossCuttingConcerns crossCuttingEnabledObj))
            {
                return;
            }

            foreach (var concern in concerns)
            {
                crossCuttingEnabledObj.AppliedCrossCuttingConcerns.RemoveAll(c => c == concern);
            }
        }

        public static bool IsApplied([NotNull] object obj, [NotNull] string concern)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (concern == null)
            {
                throw new ArgumentNullException(nameof(concern));
            }

            return (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.Contains(concern) ?? false;
        }

        public static IDisposable Applying(object obj, params string[] concerns)
        {
            AddApplied(obj, concerns);
            return new DisposeAction(() =>
            {
                RemoveApplied(obj, concerns);
            });
        }

        public static string[] GetApplieds(object obj)
        {
            //var crossCuttingEnabledObj = obj as IAvoidDuplicateCrossCuttingConcerns;
            if (!(obj is IAvoidDuplicateCrossCuttingConcerns crossCuttingEnabledObj))
            {
                return new string[0];
            }

            return crossCuttingEnabledObj.AppliedCrossCuttingConcerns.ToArray();
        }
    }
}