//  <copyright file="ApplicationApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using CodeZero.Collections.Extensions;

namespace CodeZero.Web.Api.Modeling
{
    [Serializable]
    public class ApplicationApiDescriptionModel
    {
        public IDictionary<string, ModuleApiDescriptionModel> Modules { get; }

        public ApplicationApiDescriptionModel()
        {
            Modules = new Dictionary<string, ModuleApiDescriptionModel>();
        }

        public ModuleApiDescriptionModel AddModule(ModuleApiDescriptionModel module)
        {
            if (Modules.ContainsKey(module.Name))
            {
                throw new CodeZeroException("There is already a module with same name: " + module.Name);
            }

            return Modules[module.Name] = module;
        }

        public ModuleApiDescriptionModel GetOrAddModule(string name)
        {
            return Modules.GetOrAdd(name, () => new ModuleApiDescriptionModel(name));
        }

        public ApplicationApiDescriptionModel CreateSubModel(string[] modules = null, string[] controllers = null, string[] actions = null)
        {
            var subModel = new ApplicationApiDescriptionModel();

            foreach (var module in Modules.Values)
            {
                if (modules == null || modules.Contains(module.Name))
                {
                    subModel.AddModule(module.CreateSubModel(controllers, actions));
                }
            }

            return subModel;
        }
    }
}