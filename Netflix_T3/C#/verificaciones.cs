using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netflix_T3.C_
{
    public class verificaciones
    {
        public string NoSpaceSrting(string String = null)
        {
            string answer = "";
            String = String.Replace(" ", "");
            answer = String;
            return answer;
        }
        public string Lower_Username(string String = null)
        {
            string answer = "";
            String = String.ToLower();
            answer = String;
            return answer;
        }
    }
}