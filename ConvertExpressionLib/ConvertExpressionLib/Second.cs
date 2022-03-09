using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertExpressionLib
{
    public class Second
    {
        public static string ReplacerV(string inString, Dictionary<string, decimal> variableDictionary)
        {
            try
            {
                foreach (var variable in variableDictionary)
                {
                    int ind = inString.IndexOf(" " + variable.Key + " ");
                    Trace.WriteLine("index of = " + ind);
                    Trace.WriteLine(inString);

                    if (ind >= 0)
                    {
                        inString = inString.Remove(ind, variable.Key.Length + 2);
                        Trace.WriteLine(inString);
                        inString = inString.Insert(ind, variable.Value.ToString());
                        Trace.WriteLine(inString);
                    }
                }

                return inString;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return inString;
            }
        }

        public static int ReplacerF(string inString, out decimal outputD)
        {
            outputD = 0;
            try
            {
                while (inString.Contains('('))
                {
                    int ind1 = inString.LastIndexOf("(");
                    Trace.WriteLine("LastIndexOf(\"(\") = " + ind1);
                    Trace.WriteLine(inString[ind1]);

                    int ind2 = inString.IndexOf(")", ind1);
                    Trace.WriteLine("IndexOf(\")\") = " + ind2);
                    Trace.WriteLine(inString[ind2]);

                    string str = null;

                    decimal dec1 = 0;
                    decimal dec2 = 0;

                    for (int i = ind1 + 1; i < ind2; i++)
                    {
                        Trace.Write(inString[i]);
                        if (Char.IsNumber(inString[i]))
                        {
                            str = str + inString[i];
                        }
                        else if (inString[i] == ',')
                        {
                            dec1 = Convert.ToDecimal(str);
                            str = null;
                        }
                    }
                    dec2 = Convert.ToDecimal(str);
                    str = null;
                    Trace.WriteLine("");

                    Trace.WriteLine("--------------------");
                    Trace.WriteLine(dec1);
                    Trace.WriteLine(dec2);
                    Trace.WriteLine("--------------------");

                    //Индекс с которого будем искать начало ключевого слова (sum м других)
                    // -4 нужно для что бы отступить от открывающей скобки к началу ключевого слова
                    int sInd = ind1 - 4;
                    if (sInd < 0)
                        sInd = 0;

                    //TODO Замени на switch Убери этот страх божий
                    int sumInd = inString.IndexOf("sum", sInd, 4);
                    if (sumInd >= 0)
                    {
                        decimal sumRes = dec1 + dec2;
                        Trace.WriteLine("sumRes = " + sumRes);
                        inString = inString.Remove(sumInd, ind2 - sumInd + 1);
                        inString = inString.Insert(sumInd, sumRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }

                    int subInd = inString.IndexOf("sub", sInd, 4);
                    if (subInd >= 0)
                    {
                        decimal subRes = dec1 - dec2;
                        Trace.WriteLine("sumRes = " + subRes);
                        inString = inString.Remove(subInd, ind2 - subInd + 1);
                        inString = inString.Insert(subInd, subRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }

                    int multInd = inString.IndexOf("mult", sInd, 4);
                    if (multInd >= 0)
                    {
                        decimal multRes = dec1 * dec2;
                        Trace.WriteLine("multRes = " + multRes);
                        inString = inString.Remove(multInd, ind2 - multInd + 1);
                        inString = inString.Insert(multInd, multRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }

                    int divInd = inString.IndexOf("div", sInd, 4);
                    if (divInd >= 0)
                    {
                        decimal divRes = dec1 / dec2;
                        Trace.WriteLine("divRes = " + divRes);
                        inString = inString.Remove(divInd, ind2 - divInd + 1);
                        inString = inString.Insert(divInd, divRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }

                    int modInd = inString.IndexOf("mod", sInd, 4);
                    if (modInd >= 0)
                    {
                        decimal modRes = dec1 % dec2;
                        Trace.WriteLine("modRes = " + modRes);
                        inString = inString.Remove(modInd, ind2 - modInd + 1);
                        inString = inString.Insert(modInd, modRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }

                    int ceilInd = inString.IndexOf("ceil", sInd, 4);
                    if (ceilInd >= 0)
                    {
                        decimal ceilRes = Math.Ceiling(dec1 / dec2);
                        Trace.WriteLine("ceilRes = " + ceilRes);
                        inString = inString.Remove(ceilInd, ind2 - ceilInd + 1);
                        inString = inString.Insert(ceilInd, ceilRes.ToString());
                        Trace.WriteLine(inString);
                        Trace.WriteLine("--------------------");
                        continue;
                    }
                }

                outputD = Convert.ToDecimal(inString);
                return 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public static decimal MainFun(string formulaStr, Dictionary<string, decimal> variableDictionary)
        {
            decimal outputD = 0;
            try
            {
                formulaStr = formulaStr.ToLower();
                formulaStr = formulaStr.Trim();
                //Trace.WriteLine(formulaStr);

                formulaStr = ReplacerV(formulaStr, variableDictionary);
                ReplacerF(formulaStr, out outputD);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return outputD;
        }
    }
}
