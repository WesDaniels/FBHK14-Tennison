using System.IO;
using System;
using System.Collections.Generic;

class Program
{
    static string[] lines = new String[6];

    static void Main()
    {
        // Read in every line in the file.
        int rowNumber = 0;

        using (StreamReader reader = new StreamReader("../../input.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Array.Resize(ref lines, rowNumber + 1);
                lines[rowNumber] = line;
                rowNumber++;
            }
        }

        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] vals = lines[i].Split(' ');
            int T = Convert.ToInt32(vals[0]);
            double Ps = Convert.ToDouble(vals[1]); // win when sunny
            double Pr = Convert.ToDouble(vals[2]); // win when raining
            double Pi = Convert.ToDouble(vals[3]); // chance of sun 1st set
            double Pu = Convert.ToDouble(vals[4]); 
            double Pw = Convert.ToDouble(vals[5]); 
            double Pd = Convert.ToDouble(vals[6]);
            double Pl = Convert.ToDouble(vals[7]);

            Console.WriteLine(T+ "," + Ps+ "," + Pr + "," + Pi+ "," + Pu+ "," + Pw+ "," + Pd+ "," + Pl +" : " + Calc(T, Ps, Pr, Pi, Pu, Pw, Pd, Pl, 0, 0));
        }

        Console.ReadLine();
    }

    static public double CalcProb(double Ps,double Pr,double Pi)
    {
        double sunny = (Ps * Pi);
        double rainy = (Pr * (1 - Pi));
        double win = sunny + rainy;
        return Math.Round(win,6);
    }

    static public double Calc(int T, double Ps,double Pr,double Pi,double Pu, double Pw, double Pd, double Pl,int wins, int loss)
    {
        double recursive = CalcProb(Ps, Pr, Pi);

        double winWin = 1;
        double lossWin = 0;
        
        if(wins+1 != T)
        {
            double nPi = Math.Round(Pi + (Pu * Pw),6);
            if (nPi >= 1)
            {
                nPi = 1;
            }
            if (nPi <= 0)
            {
                nPi = 0;
            }
            winWin = Calc(T, Ps, Pr, nPi, Pu, Pw, Pd, Pl, wins + 1, loss);
        }
        double result = Math.Round(recursive * winWin,6);

        if (loss + 1 != T)
        {
            double nPi = Math.Round(Pi - (Pd * Pl),6);
            if (nPi >= 1)
            {
                nPi = 1;
            }
            if (nPi <= 0)
            {
                nPi = 0;
            }
            lossWin = Calc(T, Ps, Pr, nPi, Pu, Pw, Pd, Pl, wins, loss + 1);
            result += Math.Round(((1- recursive) * lossWin),6);
        }

        return  Math.Round(result,6);
    }
}
