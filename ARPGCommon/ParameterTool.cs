using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace ARPGCommon
{
    public class ParameterTool
    {
        public static T GetParameter<T>(Dictionary<byte, object> parameters, ParameterCode parameterCode, bool isObject = true)
        {
            return isObject
                ? JsonMapper.ToObject<T>(((object)parameters[(byte)parameterCode]).ToString())
                : (T)parameters[(byte)parameterCode];
        }

        public static void AddParameter(Dictionary<byte, object> parameters, ParameterCode parameterCode, object value,
            bool isObject = true)
        {
            parameters.Add((byte)parameterCode, isObject ? JsonMapper.ToJson(value) : value);
        }
    }
}