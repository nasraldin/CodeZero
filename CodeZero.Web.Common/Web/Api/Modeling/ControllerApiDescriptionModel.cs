//  <copyright file="ControllerApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
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

namespace CodeZero.Web.Api.Modeling
{
    [Serializable]
    public class ControllerApiDescriptionModel
    {
        public string Name { get; }

        public IDictionary<string,  ActionApiDescriptionModel> Actions { get; }

        private ControllerApiDescriptionModel()
        {

        }

        public ControllerApiDescriptionModel(string name)
        {
            Name = name;

            Actions = new Dictionary<string, ActionApiDescriptionModel>();
        }

        public ActionApiDescriptionModel AddAction(ActionApiDescriptionModel action)
        {
            if (Actions.ContainsKey(action.Name))
            {
                throw new CodeZeroException(
                    $"Can not add more than one action with same name to the same controller. Controller: {Name}, Action: {action.Name}."
                    );
            }

            return Actions[action.Name] = action;
        }

        public ControllerApiDescriptionModel CreateSubModel(string[] actions)
        {
            var subModel = new ControllerApiDescriptionModel(Name);

            foreach (var action in Actions.Values)
            {
                if (actions == null || actions.Contains(action.Name))
                {
                    subModel.AddAction(action);
                }
            }

            return subModel;
        }
    }
}