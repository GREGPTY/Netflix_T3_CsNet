using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services.Description;

namespace Netflix_T3.C_
{
    public class RandomModule
    {
        void seno(float a, float b)
        {
            Math.Sin(Math.PI/2);
        }
        public int OneOrTwoVariableForRandom(string data)
        {
            int answerint = 0;
            if ((data == "Next-MaxValue")||(data == "NextBytes")) 
            {
                answerint = 1;
            } else if ((data == "Next-Start-Limit"))
            {
                answerint = 2;
            }
            else if((data == "watchoptions_random") || (data == "Next"))
            {
                answerint=0;
            }

            // "Next", "NextWithControl", "NextDouble", "NextBytes",
            // "Equals", "GetHashCode", "GetType", "ToString"
            /*Requiero saber como implementar Sample(Python)
             * (lista,cantidad de numeros a elegir), Choice (elige 1 de una lista)*/
            return answerint; 
        }
        
        public string SelectedItem(string value, double a, double b)
        {
            string answer=null;
            //Random rnd = new Random(a);// here 'a' is a seed for random module
            Random random = new Random();// en math no es necesario 
            switch (value)
            {
                case "Next":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>Generador de numeros aleatoreos, tu numero es: "+ random.Next() + "</p></div>";
                    break;
                case "Next-MaxValue":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>Generador de numeros aleatoreos, tu numero limite es: " + a + ", redondeado es: " + Convert.ToInt32(a) +" y el numero random es: "+
                        + random.Next(Convert.ToInt32(a)) + "</p></div>";
                    break;                    
                case "Next-Start-Limit":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>Generador de numeros aleatoreos, tu numero inicial es: " + a + ", redondeado es: " + Convert.ToInt32(a) + "," +
                        " tu numero limite es: " + b + ", redondeado es: " + Convert.ToInt32(b) + " y el numero random es: " +
                        +random.Next(Convert.ToInt32(a), Convert.ToInt32(b)) + "</p></div>";
                    break;
                case "NextDouble":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>Generador de numeros aleatoreos pero flotante, tu numero esta en un rango entre 0.0 y 1.0 es: " + random.NextDouble() + "</p></div>";
                    break;
                case "NextBytes":
                    /*Byte[] byt = new Byte [Convert.ToInt32(a)];
                    random.NextBytes(byt);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class='div-ddClass'><p='p-title-module1'>Cantidad de numeros aleatorios para generar: " + a + ", redondeado es: " + Convert.ToInt32(a)+ "</p></div>");
                    for(int z=0;z<a;b+=1)
                    {
                        sb.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Numero Random de la posicion {"+z+"} es: " + byt[z] +" </p></div>");
                    }
                    answer = sb.ToString();*///Error: Insufficient memory to continue the execution of the program.
                    answer = "<div class='div-ddClass'><p='p-title-module1'>No disponible</div></p>";
                    break;
                case "Equals":
                    random.Equals(a);
                    break;
                case "GetHashCode":
                    
                    break;
                case "GetType":
                    
                    break;
                case "ToString":
                    
                    break;
                default:
                    //answer = "<div class='div-ddClass'><p='p-title-module1'>Operacion no reconocida</p></div>";
                    break ;
            }
            /*Reminder: aqui se enlistaran las funciones que no existen en random
            - randrange(inicio, fin, incremento) no existe con el 'incremento'

            */
            return answer;
        }
    }
}