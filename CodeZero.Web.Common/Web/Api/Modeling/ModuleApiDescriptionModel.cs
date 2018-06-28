//  <copyright file="ModuleApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Collections.Generic;
using CodeZero.Collections.Extensions;

namespace CodeZero.Web.Api.Modeling
{
    [Serializable]
    public class ModuleApiDescriptionModel
    {
        public string Name { get; set; }

        public IDictionary<string, ControllerApiDescriptionModel> Controllers { get; }

        private ModuleApiDescriptionModel()
        {
            
        }

        public ModuleApiDescriptionModel(string name)
        {
            Name = name;

            Controllers = new Dictionary<string, ControllerApiDescriptionModel>();
        }

        public ControllerApiDescriptionModel AddController(ControllerApiDescriptionModel controller)
        {
            if (Controllers.ContainsKey(controller.Name))
            {
                throw new CodeZeroException($"There is already a controller with name: {controller.Name} in module: {Name}");
            }

            return Controllers[controller.Name] = controller;
        }

        public ControllerApiDescriptionModel GetOrAddController(string name)
        {
            return Controllers.GetOrAdd(name, () => new ControllerApiDescriptionModel(name));
        }
        
        public ModuleApiDescriptionModel CreateSubModel(string[] controllers, string[] actions)
        {
            var subModel = new ModuleApiDescriptionModel(Name);

            foreach (var controller in Controllers.Values)
            {
                if (controllers == null || controllers.Contains(controller.Name))
                {
                    subModel.AddController(controller.CreateSubModel(actions));
                }
            }

            return subModel;
        }
    }
}