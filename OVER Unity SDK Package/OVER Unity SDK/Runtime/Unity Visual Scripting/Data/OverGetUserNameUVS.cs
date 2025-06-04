using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace OverSDK.VisualScripting
{
    [UnitTitle("Get User Name")]
    [UnitCategory("Over")]
    [TypeIcon(typeof(OverBaseType))]
    public class OverGetUserNameUVS : Unit
    {
        [DoNotSerialize]
        [PortLabel("Name")]
        public ValueOutput Name;

        public static Func<string> GetUserNameExp = null;

        protected override void Definition()
        {
            Name = ValueOutput<String>("Name", GetUserName);
        }

        private string GetUserName(Flow flow)
        {
            string username = "test";

#if APP_MAIN
        if (GetUserNameExp != null)
        {
            username = GetUserNameExp();
        }
#endif

            return username;
        }
    }
}
