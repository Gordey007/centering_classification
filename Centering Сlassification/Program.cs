using System;
using System.Collections.Generic;
using System.Linq;
namespace Centering_Сlassification
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> list = new List<List<string>>();
            List<List<string>> data = new List<List<string>>();
            List<string> triangle = new List<string>();

            List<List<string>> distance = new List<List<string>>();

            List<List<string>> classList2 = new List<List<string>>();
            List<string> classList = new List<string>();
            List<List<string>> knnList = new List<List<string>>();

            List<string> classIf = new List<string>();

            List<List<string>> countClassElements = new List<List<string>>();
            List<List<string>> average2 = new List<List<string>>();
            List<string> average = new List<string>();

            int countClass, min = 0, options = 0;

            kNNn();

            for (int i = 0; i < list.Count - 2; i++)
            {
                sorting(distance[i][0], min);
            }

            Console.WriteLine($"{triangle[triangle.Count-1]} - {distance[distance.Count-1][1]}");


            void kNNn()
            {
                int counter = 0;
                string line;

                System.IO.StreamReader file = new System.IO.StreamReader(@"F:\kNNn.txt");
                while ((line = file.ReadLine()) != null)
                {
                    list.Add(new List<string> { line });
                    pars(counter, line);
                    counter++;
                }
                file.Close();

                Console.WriteLine("Данные");
                foreach (List<string> subList in list)
                {
                    foreach (string item in subList)
                    {
                        Console.Write(" " + item);
                    }
                    Console.WriteLine();
                }

                for (int i = 0; i < data[data.Count - 1].Count - 1; i++)
                {
                    if (i == 0)
                    {
                        classList.Add(data[data.Count - 1][i]);
                    }
                    if (data[data.Count - 1][i] != data[data.Count - 1][i + 1])
                    {
                        classList.Add(data[data.Count - 1][i + 1]);
                    }
                }

                centeringСlassification();

                distanceСalculation();

                countClass = classList.Count;

                for (int i = 0; i < countClass; i++)
                {
                    classIf.Add(0.ToString());
                }
            }


            void pars(int counter, string str)
            {
                string str1 = "";
                int param = 0;
                int paramStart = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        if (counter == 0)
                        {
                            data.Add(new List<string> { });
                        }
                        param++;
                    }
                }

                options = param;

                if (counter == 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (param != 0)
                        {
                            for (int j = i; j < str.Length; j++)
                            {
                                if (str[j] != ' ')
                                {
                                    str1 += str[j];
                                }
                                if (str[j] == ' ')
                                {
                                    triangle.Add(str1);
                                    str1 = null;
                                    paramStart++;
                                    i = j;
                                    break;
                                }
                            }
                            param--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (counter > 0)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (param != 0)
                        {
                            for (int j = i; j < str.Length; j++)
                            {
                                if (str[j] != ' ')
                                {
                                    str1 += str[j];
                                }
                                if (str[j] == ' ')
                                {
                                    data[paramStart].Add(str1);
                                    str1 = null;
                                    paramStart++;
                                    i = j;
                                    break;
                                }
                            }
                            param--;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            void centeringСlassification()
            {
                double sumA = 0;
                int check = 0;
                int count = -1;

                for (int i = 0; i < classList.Count; i++)
                {
                    countClassElements.Add(new List<string> {  });
                    countClassElements[i].Add("0");
                    countClassElements[i].Add(classList[i]);
                    

                    for (int j = 0; j < data[data.Count - 1].Count; j++)
                    {
                        if (classList[i] == data[data.Count - 1][j])
                        {
                            countClassElements[i][0] = (Convert.ToInt32(countClassElements[i][0])+1).ToString();
                        }
                    }
                }

                for (int a = 0; a < countClassElements.Count; a++)
                {
                    for (int j = 0; j < options - 1; j++)
                    {
                        average2.Add(new List<string> { });
                        
                        for (int i = 0; i < Convert.ToInt32(countClassElements[a][0]); i++)
                        {
                            sumA += Convert.ToDouble(data[j][i+check]);                          
                        }
                        count++;
                        average2[count].Add(Convert.ToDouble(sumA / Convert.ToDouble(countClassElements[a][0])).ToString());
                        average2[count].Add(countClassElements[a][1]);

                        sumA = 0;                      
                    }
                    check += Convert.ToInt32(countClassElements[a][0]);
                }                     
            }


            void distanceСalculation()
            {
                for (int i = 0; i < average2.Count - 1; i++)
                {
                    double distanceFormula = 0;
                    double sum = 0;
                    for (int j = 0; j < options - 1; j++)
                    {
                        distanceFormula = Math.Pow((Convert.ToDouble(triangle[j]) - Convert.ToDouble(average2[i][0])), 2);
                        sum = sum + distanceFormula;
                    }

                    distanceFormula = Math.Sqrt(sum);

                    distance.Add(new List<string> { });
                    distance[i].Add(distanceFormula.ToString());
                    distance[i].Add(average2[i][1].ToString());
                }
            }


            void sorting(string kolon, int min1)
            {
                List<List<string>> temp = new List<List<string>>();
                for (int i = 0; i < distance.Count - 1; i++)
                {
                    bool f = false;
                    for (int j = 0; j < distance.Count - i - 1; j++)
                    {
                        if (Convert.ToDouble(distance[j + 1][0]) > Convert.ToDouble(distance[j][0]))
                        {
                            f = true;
                            temp.Add(distance[j + 1]);
                            distance[j + 1] = distance[j];
                            distance[j] = temp[temp.Count - 1];
                        }
                    }
                    if (!f) break;
                }
            }
        }
    }
}
