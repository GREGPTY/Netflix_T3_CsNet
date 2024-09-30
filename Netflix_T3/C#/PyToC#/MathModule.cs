using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netflix_T3.C_
{
    public class MathModule
    {
        void seno(float a, float b)
        {
            Math.Sin(Math.PI/2);//hola
        }
        public bool OneVariableForMath(string data)
        {
            bool answerbool = false;
            if ((data=="Ceiling")||(data=="Floor")||(data=="Round")||(data=="Log")||(data == "Truncate")||(data=="Abs")||(data == "Sign")||
                (data=="Sign")
                 )            {
                answerbool = true;
            }
            //not onevariable: IEEE reminder, Max, LogWithBase

        return answerbool; 
        }
        public string MayorMenorCero(int data) {
            string answer=null;
            if (data < 0){
                answer = "el valor es menor que 0";
            }else if (data>0)
            {
                answer = "el valor es mayor que 0";
            }
            else
            {
                answer = "el valor es 0";
            }

            return answer;
        }
        public string SelectedItem(string value, double a, double b)
        {
            int c=0, d=0;
            string answer=null;
            switch (value)
            {
                case "Ceiling":
                    // Código para Math.Ceiling
                    answer= "<div class='div-ddClass'><p='p-title-module1'> Redondea al numero siguiente sin importar que sea .001, tu numero es: " + a+" y redondeado es: " + Math.Ceiling(a)+ "</p></div>";
                    break;
                case "Floor":                    
                    answer= "<div class='div-ddClass'><p='p-title-module1'> Redondea al numero actual sin importar que sea .999, tu numero es: "+a + " y redondeado es: " + Math.Floor(a)+"</p></div>";
                    break;
                case "Round":
                    answer = "<div class='div-ddClass'><p='p-title-module1'> Redondea al numero actual, tu numero es: " + a + " y redondeado es: " + Math.Round(a) + "</p></div>";
                    break;
                case "Truncate":
                    answer = "<div class='div-ddClass'><p='p-title-module1'> Redondea al numero actual, tu numero es: " + a + " y redondeado es: " + Math.Truncate(a) + "</p></div>";
                    break;
                case "IEEERemainder":
                    answer = "<div class='div-ddClass'><p='p-title-module1'> Es resultado de a-(n*b) donde el cociente redondeado 'n' que sale de a/b " +
                        ", tu numero es [a]: " + a + ", [b]: "+b+", [n]: "+ Math.Round(a/b)+
                        " y el resultado es: " + Math.IEEERemainder(a,b) + "</p></div>";
                    break;
                case "Abs":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "El numero introducido es |"+a+"| y el absoluto es: "+Math.Abs(a)+
                        "</p></div>";
                    break;
                case "Max":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "Los numeros introducidos son [" + a + "], ["+b+"] y el mayor es: " + Math.Max(a,b) +
                        "</p></div>";
                    break;
                case "Min":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "Los numeros introducidos son [" + a + "], [" + b + "] y el menor es: " + Math.Min(a, b) +
                        "</p></div>";
                    break;
                case "Log":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "El Logaritmo de [" + a + "] natural es: " + Math.Log(a) +
                        "</p></div>";
                    break;
                case "LogWithBase":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "El Logaritmo de [" + a + "], con base ["+b+"] es: " + Math.Log(a,b) +
                        "</p></div>";
                    break;
                case "Sign":
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                        "El numero introducido es [" + a + "] y el valor es: " + Math.Sign(a) +
                        ", Lo cual indica que: "+ MayorMenorCero(Math.Sign(a))+
                        "</p></div>";
                    break;
                case "BigMul":                    
                    c= Convert.ToInt32(a);
                    d= Convert.ToInt32(b);
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                       "Los numero introducidos son [" + a + "], [" + b + "] y solo se aceptan enteros  " +
                       "asi que debemos redondearlos, estos son ["+c+"] * ["+d+"] su multiplicacion="+ Math.BigMul(c,d)+
                       "</p></div>";
                    break;
                case "DivRem":
                    c = Convert.ToInt32(a);
                    d = Convert.ToInt32(b);
                    answer = "<div class='div-ddClass'><p='p-title-module1'>" +
                       "Los numero introducidos son [" + a + "], [" + b + "] y solo se aceptan enteros  " +
                       "asi que debemos redondearlos, estos son [" + c + "] / [" + d + "] su operacion es " +
                       "calcular el cociente =" + Math.DivRem(c, d,out int e) +
                       "</p></div>";
                    break;
                //versiones inversas (arco...)
                case "Acos":
                    
                    break;
                case "Asin":
                    
                    break;
                case "Atan":
                    
                    break;
                case "Atan2":
                    
                    break;
                //análogos hiperbólicos
                case "Cosh":
                    
                    break;
                case "Sinh":
                    
                    break;
                case "Tanh":
                    
                    break;
                case "Sqrt":
                    
                    break;
                case "Log10":
                    
                    break;
                case "Cos":
                    
                    break;
                case "Sin":
                    
                    break;
                case "Tan":
                    
                    break;
                case "Exp":
                    
                    break;
                case "Pow": //x^y o a^b

                    break;
                case "Equals":
                    
                    break;
                case "GetHashCode":
                    
                    break;
                case "GetType":
                    
                    break;
                case "ToString":
                 
                    break;                
                default :
                    break;
                    //No existe la conversion directa a radianes ni degrees, factorial, hypot
                    //dato de hypor: devuelve la longitud de la hipotenusa de un triángulo rectángulo con las longitudes de los catetos iguales a (x) y (y) (lo mismo que sqrt(pow(x, 2) + pow(y, 2)) pero más preciso).
            }
            return answer;
        }
    }
}