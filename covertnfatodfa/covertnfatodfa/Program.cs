using System;
using System.Collections.Generic;

namespace covertnfatodfa
{
    class Program
    {

        static string nextstep(string[,] rules,string state,string alpha,int n)
        {
            string[] x = state.Split(',');
            string y = "";
            List<string> a = new List<string>();
            for(int i = 0; i < x.Length; i++)
            {
                    for (int j = 0; j < n; j++)
                    {
                        if (rules[j, 0] == x[i] && rules[j, 1] == alpha)
                        {
                            if (checkrepeat(a, rules[j, 2]) == false)
                            {
                                a.Add(rules[j, 2]);
                                y = y + rules[j, 2] + ",";
                            }
                        }
                    }               
            }
            if (y.Contains(","))
            {
                y = y.Substring(0, y.Length - 1);
            }
           return y;
        }

        static bool checkrepeat(List<string> a,string x)
        {
            string[] y2 = x.Split(',');
            for (int i = 0; i < a.Count; i++)
            {
                string[]y1=a[i].Split(',');
                if(y1.Length==y2.Length)
                {
                    if (checkequal(y2, y1, y1.Length) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool checkequal(string[]a,string[]b,int n)
        {
            bool t = false;
            for(int i = 0; i < n; i++)
            {
                t = false;
                for(int j = 0; j < n; j++)
                {
                    if (a[i] == b[j])
                    {
                        t = true;
                    }
                }
                if (t == false)
                {
                    return false;
                }
            }
            return true;
        }


        static void findclousur(string x, string[,] rules,int n)
        {
            string m =x;
            for(int i = 0; i < n; i++)
            {
                if(rules[i,0]==x && rules[i, 1] == "$")
                {
                    m = m + "," + rules[i, 2];
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (rules[i, 2] == x)
                {
                    rules[i, 2] = m;
                }
            }
        }
        
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            s1 = s1.Substring(1, s1.Length - 2);
            string[] state = s1.Split(',');

            string s2 = Console.ReadLine();
            s2 = s2.Substring(1, s2.Length - 2);
            string[] alphabet = s2.Split(',');

            string s3 = Console.ReadLine();
            s3 = s3.Substring(1, s3.Length - 2);
            string[] finalstate = s3.Split(',');

            int n = int.Parse(Console.ReadLine());
            string[,] rules = new string[n, 3];
            for (int i = 0; i < n; i++)
            {
                string[] s4 = Console.ReadLine().Split(',');
                rules[i, 0] = s4[0];
                rules[i, 1] = s4[1];
                rules[i, 2] = s4[2];
            }


            //step1
            for(int i = 0; i < state.Length; i++)
            {
                findclousur(state[i], rules, n);
            }

            string d = state[0];
           for(int i = 0; i < n; i++)
           {
                if(rules[i,0]==state[0] && rules[i, 1] == "$")
                {
                    d = d + "," + rules[i, 2];
                }
                
           }
            state[0] = d;

            //for (int i = 0; i < n; i++)
            //{
            //    Console.WriteLine(rules[i, 0] + " " + rules[i, 1] + " " + rules[i, 2]);
            //}




            //step2
            List<string> a = new List<string>();
            List<string> b = new List<string>();
            List<string> c = new List<string>();
            a.Add(state[0]);
            b.Add(state[0]);
            //Console.WriteLine(nextstep(rules, "q3,q1","1" , n));
            bool p = false;
            while (true)
            {
                bool t = false;
                for (int i = 0; i < b.Count; i++)
                {
                    for (int j = 0; j < alphabet.Length; j++)
                    {
                        string h=nextstep(rules, b[i], alphabet[j], n);
                        if (checkrepeat(a, h) == false && h!="")
                        {
                            a.Add(h);
                            c.Add(h);
                            t = true;
                        }
                        if(h == "")
                        {
                            p = true;
                        }
                    }
                }
                if (t == false)
                {
                    break;
                }

                for(int i = 0; i < b.Count; i++)
                {
                    b.RemoveAt(i);
                }
                for(int i = 0; i < c.Count; i++)
                {
                    b.Add(c[i]);
                }
                for (int i = 0; i < c.Count; i++)
                {
                    c.RemoveAt(i);
                }
            }

            //Console.WriteLine("**************");
            //for (int i = 0; i < a.Count; i++)
            //{
            //    Console.WriteLine(a[i]);
            //}
            if (p == true)
            {
                Console.WriteLine(a.Count + 1);
            }
            else
            {
                Console.WriteLine(a.Count);
            }
        }
    }
}
