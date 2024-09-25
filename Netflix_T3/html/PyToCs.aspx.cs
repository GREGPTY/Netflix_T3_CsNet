using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Routing;
using static System.Net.Mime.MediaTypeNames;
using Netflix_T3.C_;
using System.Security.AccessControl;
using System.Linq.Expressions;

namespace Netflix_T3.html
{
    public partial class PyToCs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                SelectMathModuleItems();
                SelectRandomModuleItems();
                txtMathA.Enabled = false; txtMathA.Visible = false;txtMathB.Enabled = false; txtMathB.Visible = false;
                txtRandomA.Enabled = false; txtRandomA.Visible = false; txtRandomB.Enabled = false; txtRandomB.Visible = false;
                //SeeTypeOptions(typeof(Random));                
            }
            else
            {
                //ddlPlatformModule.Visible=false;
            }
        }
        private void SelectMathModuleItems()
        {
            ddlMathOptions.Items.Clear();
            string[] items = { "Ceiling", "Floor", "Round", "Truncate", "IEEERemainder", "Abs", "Max", "Min", "Log", "LogWithBase", "Sign", "BigMul", "DivRem", "DivRem", "Acos", "Asin", "Atan", "Atan2", "Cosh", "Sinh", "Tanh", "Sqrt", "Log10", "Ceiling", "Cos", "Floor", "Sin", "Tan", "Round", "Log", "Exp", "Pow", "Equals", "GetHashCode", "GetType", "ToString" };
            ddlMathOptions.Items.Add(new ListItem { Text="Select Options", Value="watchoptions_math", Selected=true});
            foreach (string item in items) {
                ddlMathOptions.Items.Add(new ListItem {Text=item, Value=item});
            }            
        }
        private void SelectRandomModuleItems()
        {
            ddlRandomOptions.Items.Clear();
            string[] RandomModulesItems = { "Next", "Next-MaxValue", "Next-Start-Limit", "NextDouble", "NextBytes", "Equals", "GetHashCode", "GetType", "ToString" };
            ddlRandomOptions.Items.Add(new ListItem { Text = "Select Option", Value = "watchoptions_random", Selected = true });
            foreach (string item in RandomModulesItems)
            {
                ddlRandomOptions.Items.Add(new ListItem { Text = item, Value = item });
            }
        }
        protected void ddlMathOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlMathOptions.SelectedValue;            
            //txtMathB.Visible = false;txtMathB.Enabled = false;*/
            MathModule mathModule = new MathModule();
            StringBuilder sba = new StringBuilder();
            StringBuilder sbb = new StringBuilder();
         if(selectedValue != "watchoptions_math")
            {
                txtMathA.Visible = true;txtMathA.Enabled = true;
                txtMathB.Visible = false;txtMathB.Enabled = false;
                if (mathModule.OneVariableForMath(ddlMathOptions.SelectedValue))
                {
                    sba.Clear();sbb.Clear();
                    sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor para la seleccion escogida [" + selectedValue + "] </p></div>");
                 
                }
                else
                {
                    sba.Clear(); sbb.Clear();
                    sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Seleccion escogida [" + selectedValue + "]</p></div>");
                    sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor 'a' </p></div>");
                    sbb.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor 'b' </p></div>");

                    txtMathB.Enabled = true; txtMathB.Visible = true;
                }                
          }else{
                sba.Clear(); sbb.Clear();
                
                txtMathA.Enabled = false; txtMathA.Visible = false;
                txtMathB.Enabled = false; txtMathB.Visible = false;
            }
            literalMathA.Text = sba.ToString();
            literalMathB.Text = sbb.ToString();
        }

        protected void ddlRandomOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectRandomValue = ddlRandomOptions.SelectedValue;
            RandomModule randomModule = new RandomModule();
            StringBuilder sba = new StringBuilder();
            StringBuilder sbb = new StringBuilder();
            txtRandomA.Enabled = false;txtRandomA.Visible = false;
            txtRandomB.Enabled = false;txtRandomB.Visible = false;
            if (randomModule.OneOrTwoVariableForRandom(selectRandomValue) == 1)
            {
                txtRandomA.Enabled = true; txtRandomA.Visible = true;
                txtRandomB.Text = null; txtRandomB.Enabled = false; txtRandomB.Visible = false;
                sba.Clear(); sbb.Clear();
                sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Seleccion escogida [" + selectRandomValue + "]</p></div>");
                sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor 'a' </p></div>");

            } else if (randomModule.OneOrTwoVariableForRandom(selectRandomValue) == 2) 
            {
                txtRandomA.Enabled= true; txtRandomA.Visible = true;
                txtRandomB.Enabled= true; txtRandomB.Visible = true;
                sba.Clear(); sbb.Clear();
                sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Seleccion escogida [" + selectRandomValue + "]</p></div>");
                sba.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor 'a' </p></div>");
                sbb.AppendLine("<div class='div-ddClass'><p='p-title-module1'>Introduzca el valor 'b' </p></div>");
            }
            else
            {
                sba.Clear(); sbb.Clear();
                txtRandomA.Text= null; txtRandomA.Enabled= false; txtRandomA.Visible= false;
                txtRandomB.Text= null; txtRandomB.Enabled= false; txtRandomB.Visible= false;
            }
            RandomLiteralA.Text= sba.ToString();
            RandomLiteralB.Text= sbb.ToString();
            }
        
        private void SeeTypeOptions(Type type)//lo utilizo para saber que contien las clases o modulos
        {            
            MethodInfo[] methods = type.GetMethods();
            StringBuilder sbL = new StringBuilder();
            string temporalMethod = "";
            foreach (MethodInfo method in methods)
            {
                if (!(temporalMethod==method.Name.ToString())) {
                    sbL.AppendLine("<div class=''>" + method.Name + "</div>");
                    temporalMethod= method.Name.ToString();
                }
            }
            PlatformLiteral.Text = sbL.ToString();
        }
        
        private void mod_dec_all()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Métodos del tipo:");

            //switch () {
            //case 1:
            phMathData.Controls.Add(new Literal { Text="<div class='div-math-data-4label'>"});
            Type type = typeof(Math);
            MethodInfo[] methods = type.GetMethods();
            //int i = 0;
            //List<Label> labelMathTxt = new List<Label>();
            foreach (MethodInfo method in methods)
                {
                                
                //Label label = new Label { CssClass = "label-math-all", ID = "ID-math-all-"+i, Text=method.Name };
                //Console.WriteLine(method.Name);
                sb.AppendLine("<label class='label-math-all'>"+method.Name+"</label>");
                /*phMathData.Controls.Add(new Literal { Text = "<div>" });                
                labelMathTxt.Add(label); //Agrega a la lista de labels
                phMathData.Controls.Add(label); //agrega a la lista de placeholders
                phMathData.Controls.Add(new Literal { Text = "</div>" });
                i++;*/
                }
                liMessage.Text = sb.ToString();
            //phMathData.Controls.Add(new Literal { Text = "</div>" });
            // break;
            //}
        }
        protected void ClcButtonMathCalculate(object sender, EventArgs e)
        {
            string respuesta = null;
            double a;
            MathModule mathModule = new MathModule();
            literalRespuesta.Text = null;
            try
            {                
                 if (ddlMathOptions.SelectedValue != "watchoptions_math")
                {                    
                    if (string.IsNullOrEmpty(txtMathA.Text))
                    {
                        throw new Exception("El campo 'A' no puede estar vacío.");
                    }
                    a = Convert.ToDouble(txtMathA.Text);
                    if (mathModule.OneVariableForMath(ddlMathOptions.SelectedValue.ToString()))
                    {
                        respuesta = mathModule.SelectedItem(ddlMathOptions.SelectedValue,a,0.0);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtMathB.Text))
                        {
                            throw new Exception("El campo 'B' no puede estar vacío.");
                        }

                        double b = Convert.ToDouble(txtMathB.Text);
                        respuesta = mathModule.SelectedItem(ddlMathOptions.SelectedValue, a, b);
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "<div class='div-ddClass'><p class='p-title-module1'>Error: " + ex.Message + "</p></div>";
            }
            literalRespuesta.Text = respuesta;
        }
        protected void ClcButtonRandomGenerate(object sender, EventArgs e) {
            string answerRandom = null;
            double a;
            RandomModule randomModule = new RandomModule();
            RandomLiteralAnswer.Text = null;
            try
            {
                if (ddlRandomOptions.SelectedValue != "watchoptions_random") //si la opcion escogida es esta no ejecutara nada aunque se presione el boton
                {                                   
                    if (randomModule.OneOrTwoVariableForRandom(ddlRandomOptions.SelectedValue) == 1) //si es un modulo que requiere de 1 solo valor entra aqui
                    {
                        if (string.IsNullOrEmpty(txtRandomA.Text))
                        {
                            throw new Exception("El campo 'A' no puede estar vacío.");
                        }
                        a = Convert.ToDouble(txtRandomA.Text);
                        answerRandom = randomModule.SelectedItem(ddlRandomOptions.SelectedValue, a, 0.0);
                    }
                    else if(randomModule.OneOrTwoVariableForRandom(ddlRandomOptions.SelectedValue)==2) //si es un modulo que requiere de 2 valores, entra aqui
                    {
                        if (string.IsNullOrEmpty(txtRandomA.Text))
                        {
                            throw new Exception("El campo 'A' no puede estar vacío.");
                        }
                        a = Convert.ToDouble(txtRandomA.Text);
                        if (string.IsNullOrEmpty(txtRandomB.Text))
                        {
                            throw new Exception("El campo 'B' no puede estar vacío.");
                        }

                        double b = Convert.ToDouble(txtRandomB.Text);
                        answerRandom = randomModule.SelectedItem(ddlRandomOptions.SelectedValue, a, b);
                    }
                    else //si no requiere de ningun valor entra aqui
                    {
                        answerRandom = randomModule.SelectedItem(ddlRandomOptions.SelectedValue, 0.0, 0.0);
                    }
                }
            }
            catch (Exception ex)
            {
                answerRandom = "<div class='div-ddClass'><p class='p-title-module1'>Error: " + ex.Message + "</p></div>";
            }
            RandomLiteralAnswer.Text = answerRandom;
        }
        //End Math Module
    }
}