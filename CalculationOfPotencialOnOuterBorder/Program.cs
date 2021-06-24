using Calculating_Magnetic_Field;
using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.ModelFactories;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System;

namespace CalculationOfPotencialOnOuterBorder
{
    public class Program
    {
        const int countOfElectrodes = 16;
        static IModel model;
        static ModelFactory modelFactory;
        static GraphicsCalculation graphicsCalculation;

        static List<Rib> ribs;
        static List<PointD> points;
        static List<double> function;

        static string folder = "C:\\Users\\Димка\\Desktop\\Сложная область\\Расчет потенциала в точках\\";
        static string fileName = "PotencialInPoints 3.dat";
        static string folderOfFunctionsFile = "C:\\Users\\Димка\\Desktop\\Сложная область\\Расчет потенциала в точках\\";
        static string CalibratedfunctionsFileName = "Calibrated Potencial In Points.dat";
        static string CalibratedfunctionsFileName2 = "Calibrated Potencial In Points (второй способ).dat";

        static string path = "F:\\Programming\\Диссертация\\Calculating_Magnetic_Field\\Calculating_Magnetic_Field\\bin\\Debug\\Problem_files\\Поле тока\\" +
                              "Поле тока. условие Неймана 2 источника вкл много всего источники внутри пластины ППС круги да стадионы 2.dat";
        static void Main(string[] args)
        {
            CalculatePotencialOnOuterBorder();
            //CalibratePotencial2();
            //CalculateErrors();
            Console.WriteLine("Расчет окончен");
            Console.ReadLine();
        }

        static private void CalculatePotencialOnOuterBorder()
        {
            Work_With_Files.ReadPhisicalObjectsInformationFromFile(out model, out modelFactory, path);
            model.SolveProblem();
            ribs = model.Bounds[0].ThisFigure.SplitBorder(countOfElectrodes);
            points = new List<PointD>(countOfElectrodes);
            PointD p1 = new PointD(ribs[0].Point1.X, ribs[0].Point1.Y - ribs[0].LengthOfElement / 10000);
            PointD p9 = new PointD(ribs[8].Point1.X, ribs[8].Point1.Y + ribs[8].LengthOfElement / 10000);
            for (int i = 0; i < countOfElectrodes; i++)
            {
                if (i == 0)
                {
                    points.Add(p1);
                    continue;
                }
                if (i == 8)
                {
                    points.Add(p9);
                    continue;
                }
                points.Add(ribs[i].Point1);
            }

            graphicsCalculation = new GraphicsCalculation(model);
            function = new List<double>(countOfElectrodes);

            for (int i = 0; i < countOfElectrodes; i++)
            {
                function.Add(model.CalculatePotencial(points[i]));
            }



            if (!Directory.Exists(folder + $"countOfElectrodes_{countOfElectrodes}"))
            {
                Directory.CreateDirectory(folder + $"countOfElectrodes_{countOfElectrodes}");
            }
            using (StreamWriter writer = new StreamWriter(folder + $"countOfElectrodes_{countOfElectrodes}\\" + fileName))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{points[i].X}\t{points[i].Y}\t{function[i]}");
                }

            }

            List<double> len = new List<double>(countOfElectrodes);
            len.Add(0);
            for(int i = 0; i < countOfElectrodes; i++)
            {
                len.Add(len[i] + ribs[i].LengthOfElement);
            }
            using (StreamWriter writer = new StreamWriter(folder + $"countOfElectrodes_{countOfElectrodes}\\" + "Для графика.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{len[i]}\t{function[i]}");
                }

            }


        }

        static private void CalibratePotencial()
        {
            List<PointD> points = new List<PointD>(countOfElectrodes);
            List<double> femmFun = new List<double>(countOfElectrodes);
            double femmMiddleValue;
            double femmSum = 0;
            List<double> MBEfun = new List<double>(countOfElectrodes);
            double MBEMiddleValue;
            double MBEsum = 0;
            using (StreamReader streamReader = new StreamReader(folder + fileName))
            {
                string str1;
                string[] str2;

                while (!streamReader.EndOfStream)
                {
                    str1 = streamReader.ReadLine();
                    str2 = str1.Split('\t');
                    points.Add(new PointD(double.Parse(str2[0]), double.Parse(str2[1])));
                    femmFun.Add(double.Parse(str2[3]));
                    MBEfun.Add(double.Parse(str2[2]));
                }
            }
            for(int i = 0; i < countOfElectrodes; i++)
            {
                femmSum += femmFun[i];
                MBEsum += MBEfun[i];
            }
            femmMiddleValue = femmSum / countOfElectrodes;
            MBEMiddleValue = MBEsum / countOfElectrodes;

            for(int i = 0; i < countOfElectrodes; i++)
            {
                femmFun[i] -= femmMiddleValue;
                MBEfun[i] -= MBEMiddleValue;
            }

            using (StreamWriter streamWriter = new StreamWriter(folderOfFunctionsFile + CalibratedfunctionsFileName))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    streamWriter.WriteLine($"{points[i].X}\t{points[i].Y}\t{MBEfun[i]}\t{femmFun[i]}");
                }
            }

        }
        static private void CalibratePotencial2()
        {
            List<PointD> points = new List<PointD>(countOfElectrodes);
            List<double> femmFun = new List<double>(countOfElectrodes);
            double delta;
            double femmSum = 0;
            List<double> deltas = new List<double>(countOfElectrodes);
            List<double> MBEfun = new List<double>(countOfElectrodes);

            using (StreamReader streamReader = new StreamReader(folder + fileName))
            {
                string str1;
                string[] str2;


                while (!streamReader.EndOfStream)
                {
                    str1 = streamReader.ReadLine();
                    str2 = str1.Split('\t');
                    points.Add(new PointD(double.Parse(str2[0]), double.Parse(str2[1])));
                    femmFun.Add(double.Parse(str2[3]));
                    MBEfun.Add(double.Parse(str2[2]));
                }
            }


            using (StreamWriter streamWriter = new StreamWriter(folderOfFunctionsFile + "FemmWithoutCalibration.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    streamWriter.WriteLine($"{i}\t{femmFun[i]}");
                }
            }
            delta = femmFun[0] - MBEfun[0];
            for (int i = 0; i < countOfElectrodes; i++)
            {
                femmFun[i] -= delta;
            }

            using (StreamWriter streamWriter = new StreamWriter(folderOfFunctionsFile + CalibratedfunctionsFileName2))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    streamWriter.WriteLine($"{points[i].X}\t{points[i].Y}\t{MBEfun[i]}\t{femmFun[i]}");
                }
            }

            using (StreamWriter streamWriter = new StreamWriter(folderOfFunctionsFile + "Femm.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    streamWriter.WriteLine($"{i}\t{femmFun[i]}");
                }
            }
            using (StreamWriter streamWriter = new StreamWriter(folderOfFunctionsFile + "MBE.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    streamWriter.WriteLine($"{i}\t{MBEfun[i]}");
                }
            }

        }

        static void CalculateErrors()
        {

            List<PointD> points = new List<PointD>(countOfElectrodes);
            List<double> femmFun = new List<double>(countOfElectrodes);
            double delta;
            double femmSum = 0;
            List<double> deltas = new List<double>(countOfElectrodes);
            List<double> MBEfun = new List<double>(countOfElectrodes);
            List<double> errors = new List<double>(countOfElectrodes);
            List<double> errorsTo100 = new List<double>(countOfElectrodes);
            List<double> errorsToFemmMiddle = new List<double>(countOfElectrodes);

            using (StreamReader reader = new StreamReader(folder + "countOfElectrodes_16PotencialInPoints 3.dat"))
            {
                string str1;
                string[] str2;


                while (!reader.EndOfStream)
                {
                    str1 = reader.ReadLine();
                    str2 = str1.Split('\t');
                    points.Add(new PointD(double.Parse(str2[0]), double.Parse(str2[1])));
                    femmFun.Add(double.Parse(str2[3]));
                    MBEfun.Add(double.Parse(str2[2]));
                }
            }


            for (int i = 0; i < countOfElectrodes; i++)
            {
                errors.Add(Math.Abs((femmFun[i] - MBEfun[i]) / femmFun[i]));
            }
            for (int i = 0; i < countOfElectrodes; i++)
            {
                errorsTo100.Add(Math.Abs((femmFun[i] - MBEfun[i]) / 100e-6));
            }
            double femmMax = femmFun.Max();
            double femmMin = femmFun.Min();
            double femmMiddle = (femmMax - femmMin) / 2;

            for (int i = 0; i < countOfElectrodes; i++)
            {
                errorsToFemmMiddle.Add(Math.Abs((femmFun[i] - MBEfun[i]) / femmMiddle));
            }

            using (StreamWriter writer = new StreamWriter(folder + $"Данные для графиков и погрешности\\{countOfElectrodes}" + "Данные и погрешности.dat"))
            {
                writer.WriteLine($"№\tМГЭ\tFemm\tПогрешность");
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{i}\t{MBEfun[i]}\t{femmFun[i]}\t{errors[i] * 100}%");
                }
            }

            using (StreamWriter writer = new StreamWriter(folder + $"Данные для графиков и погрешности\\{countOfElectrodes}" + "Данные и погрешности(Приведены к 100).dat"))
            {
                writer.WriteLine($"№\tМГЭ\tFemm\tПогрешность");
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{i}\t{MBEfun[i]}\t{femmFun[i]}\t{errorsTo100[i] * 100}%");
                }
            }

            

            using (StreamWriter writer = new StreamWriter(folder + $"Данные для графиков и погрешности\\{countOfElectrodes}" + "Данные и погрешности (Приведены к среднему FEMMa).dat"))
            {
                writer.WriteLine($"№\tМГЭ\tFemm\tПогрешность");
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{i}\t{MBEfun[i]}\t{femmFun[i]}\t{errorsToFemmMiddle[i] * 100}%");
                }
            }

            using (StreamWriter writer = new StreamWriter(folder + $"Данные для графиков и погрешности\\{countOfElectrodes}" + "График Femm to MBE format.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{i}\t{femmFun[i]}");
                }
            }
            using (StreamWriter writer = new StreamWriter(folder + $"Данные для графиков и погрешности\\{countOfElectrodes}" + "График МГЭ.dat"))
            {
                for (int i = 0; i < countOfElectrodes; i++)
                {
                    writer.WriteLine($"{i}\t{MBEfun[i]}");
                }
            }

            
        }



    }
}


