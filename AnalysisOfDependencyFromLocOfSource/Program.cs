using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Calculating_Magnetic_Field;
using Calculating_Magnetic_Field.ModelFactories;
using Calculating_Magnetic_Field.Sources;
using Calculating_Magnetic_Field.Sources.PotencialSources;
using Calculating_Magnetic_Field.Models;
using System.Collections.Generic;
using Calculating_Magnetic_Field.Figures;


namespace AnalysisOfDependencyFromLocOfSource
{
    class Program
    {
        static string path_to_dir = "C:\\Users\\Димка\\Desktop\\Анализ\\Данные графиков";
        static PointD Point1 = new PointD(0.0016, -0.02);
        static PointD Point2 = new PointD(0.0016, 0.02);
        static GraphicsCalculating graphicsCalculating;
        static int countOfVariants = 6;
        static ModelFactory modelFactory;
        static IModel model;
        static string path = "F:\\Programming\\Диссертация\\Calculating_Magnetic_Field\\Calculating_Magnetic_Field\\bin\\Debug\\Problem_files\\Поле тока\\" +
                              "Поле тока. условие Неймана 2 источника вкл средн стадион зар на границе внутр на полукольцах.dat";

        static Bound_Rib[] ribs = new Bound_Rib[4];
        static PointD[] points = new PointD[4];
        static List<ChargedThread> sources = new List<ChargedThread>(4);

        

        static List<PointD> centresOfArcs = new List<PointD>(4);

        static List<double> anglesOfCentresOfArcs = new List<double>(4);
        static List<double> anglesOfSecondPoints = new List<double>(4);

        static List<List<Bound_Rib>> new_ribs_Owning_Sources = new List<List<Bound_Rib>>(4);
        static List<int> indexesOfRibsWithSources = new List<int>(4);

        static List<List<double>> anglesArrays = new List<List<double>>(4);
        static List<List<PointD>> pointsOnArcs = new List<List<PointD>>(4);

        static List<double> function;
        static List<double> X;

        static void Main(string[] args)
        {
            Calculate_Variants();
            //Solve_With_PlittingChords();

            Console.WriteLine("Расчет окончен");
            Console.ReadKey();
        }

        private static void Calculate_Variants()
        {
            for (int i = 0; i < 4; i++)
                anglesArrays.Add(new List<double>(countOfVariants));
            for (int i = 0; i < 4; i++)
                pointsOnArcs.Add(new List<PointD>(countOfVariants));
            for (int i = 0; i < 4; i++)
                new_ribs_Owning_Sources.Add(new List<Bound_Rib>(countOfVariants));


            //Work_With_Files.ReadPhisicalObjectsInformationFromFile(out model, out modelFactory, path);

            //graphicsCalculating = new GraphicsCalculating(model);

            //for (int i = 0; i < 4; i++)
            //{
            //    sources.Add((ChargedThread)model.Sources[i]);
            //}

            //SetLocationsOfSourcesOnBound();


            for (int k = 0; k < countOfVariants; k++)
            {
                Work_With_Files.ReadPhisicalObjectsInformationFromFile(out model, out modelFactory, path);

                graphicsCalculating = new GraphicsCalculating(model);

                sources = new List<ChargedThread>();
                for (int i = 0; i < 4; i++)
                {
                    sources.Add((ChargedThread)model.Sources[i]);
                }
                List<Bound_Rib> ribs = model.Bounds[1].Bound_Ribs;
                SetLocationsOfSourcesOnBound();
                for (int i = 0; i < 4; i++)
                {
                    int index = indexesOfRibsWithSources[i];
                    Bound_Rib rib = new_ribs_Owning_Sources[i][k];
                    ribs[index] = rib;
                    ribs[index + 1].Point1 = rib.Point2;
                    ribs[index - 1].Point2 = rib.Point1;
                    //((ChargedThread)model.Sources[i]).Location = pointsOnArcs[i][k];
                }
                model.SolveProblem();
                function = graphicsCalculating.Calculate(Point1, Point2, 101, GraphicTypes.Current_Y_component);
                X = graphicsCalculating.GetLenth();

                SaveGraphicData(k.ToString(), $"Variation of elements\\ППС\\{model.Bounds[1].Bound_Ribs.Count} elements\\{countOfVariants} variants");
            }
        }


        static void Solve_With_PlittingChords()
        {
            Work_With_Files.ReadPhisicalObjectsInformationFromFile(out model, out modelFactory, path);

            graphicsCalculating = new GraphicsCalculating(model);

            SplitChords();
            model.SolveProblem();
            function = graphicsCalculating.Calculate(Point1, Point2, 101, GraphicTypes.Current_Y_component);
            X = graphicsCalculating.GetLenth();

            SaveGraphicData("Измельченная у источников сетка(D.y)", countOfVariants.ToString());
        }
        static void SplitChords()
        {
            sources = new List<ChargedThread>();
            for (int i = 0; i < 4; i++)
            {
                sources.Add((ChargedThread)model.Sources[i]);
            }
            
            Bound bound = model.Bounds[1];
            List<Bound_Rib> ribs = bound.Bound_Ribs;
            Bound_Stadium bound_Stadium = (Bound_Stadium)bound.ThisFigure;

            PointD centre1 = new PointD(bound_Stadium.Location.X + bound_Stadium.Length / 2, bound_Stadium.Location.Y);
            PointD centre2 = new PointD(bound_Stadium.Location.X + bound_Stadium.Length / 2, bound_Stadium.Location.Y - bound_Stadium.Width);
            PointD centre;
            Bound_Rib rib;
            Bound_Rib splitting_Rib;
            double length;
            double r_c;
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                {
                    centre = centre1;
                }
                else
                {
                    centre = centre2;
                }

                for (int j = 0; j < bound.Bound_Ribs.Count; j++)
                {
                    rib = bound.Bound_Ribs[j];
                    if(rib.Point2.X == sources[i].Location.X 
                            && 
                       rib.Point2.Y == sources[i].Location.Y)
                    {
                        for (int k = 3; k >= 0; k--)
                        {
                            splitting_Rib = ribs[j + 1 + k];
                            ribs.RemoveAt(j + 1 + k);
                            ribs.InsertRange(j + 1 + k, SplitChord(splitting_Rib, centre, bound_Stadium.Radius, 180 - k * 30));
                        }
                        for (int k = 3; k >= 0; k--)
                        {
                            splitting_Rib = ribs[j - k];
                            ribs.RemoveAt(j - k);
                            ribs.InsertRange(j - k, SplitChord(splitting_Rib, centre, bound_Stadium.Radius, 180 - k * 30));
                        }
                        break;
                    }
                    
                }
            }
        }

        static List<Bound_Rib> SplitChord(Bound_Rib rib, PointD centre, double radius, int n)
        {
            double angle_1 = Trigonometry.Angle(centre, rib.Point1);
            double angle_2 = Trigonometry.Angle(centre, rib.Point2);
            double d_a = angle_2 - angle_1;
            double d_a_n = d_a / n;
            List<Bound_Rib> inserting_Ribs = new List<Bound_Rib>(90);
            inserting_Ribs.Add(new Bound_Rib(rib.Point1, new PointD(centre.X + radius * Math.Cos(angle_1 + d_a_n), centre.Y + radius * Math.Sin(angle_1 + d_a_n))));
            PointD p1, p2;
            for (int i = 1; i < n - 1; i++)
            {
                p1 = new PointD(centre.X + radius * Math.Cos(angle_1 + i * d_a_n), centre.Y + radius * Math.Sin(angle_1 + i * d_a_n));
                p2 = new PointD(centre.X + radius * Math.Cos(angle_1 + (i + 1) * d_a_n), centre.Y + radius * Math.Sin(angle_1 + (i + 1) * d_a_n));
                inserting_Ribs.Add(new Bound_Rib(p1, p2));
            }
            inserting_Ribs.Add(new Bound_Rib(new PointD(centre.X + radius * Math.Cos(angle_1 + (n - 1) * d_a_n), centre.Y + radius * Math.Sin(angle_1 + (n - 1) * d_a_n)), rib.Point2));
            return inserting_Ribs;
        }


        static private void SetLocationsOfSourcesOnBound()
        {
            Bound bound = model.Bounds[1];
            Bound_Stadium bound_Stadium = (Bound_Stadium)bound.ThisFigure;
            double r_c;
            double a11;
            double a22;
            double a_c_2;
            double d_a;
            PointD p_c;
            PointD p11;
            PointD p22;

            PointF centre1 = new PointF(bound_Stadium.Location.X + bound_Stadium.Length / 2, bound_Stadium.Location.Y);
            PointF centre2 = new PointF(bound_Stadium.Location.X + bound_Stadium.Length / 2, bound_Stadium.Location.Y - bound_Stadium.Width);

            int count = bound.Bound_Ribs.Count;
            for (int k = 0; k < sources.Count / 2; k++)
            {
                for (int i = 0; i < bound.Bound_Ribs.Count; i++)
                {
                    Bound_Rib rib = bound.Bound_Ribs[i];
                    double length = rib.LengthElement;
                    r_c = rib.GetMiddleOfRib().DistanceToOtherPoint(sources[k].Location);
                    double a_source = Trigonometry.Angle(centre1.X, centre1.Y, sources[k].Location.X, sources[k].Location.Y);
                    sources[k].Location = new PointD(centre1.X + bound_Stadium.Radius * Math.Cos(a_source), centre1.Y + bound_Stadium.Radius * Math.Sin(a_source));
                    if (r_c < bound.Bound_Ribs[i].LengthElement / 2)
                    {
                        ribs[k] = rib;

                        a11 = GetAngle(i, bound_Stadium);
                        a22 = GetAngle(i + 1, bound_Stadium);
                        a_c_2 = (a11 + a22) / 2;
                        d_a = (a22 - a_c_2) / (countOfVariants - 1);
                        anglesArrays[k].Add(a_c_2);
                        for(int j = 1; j < countOfVariants - 1; j++)
                        {
                            anglesArrays[k].Add(a_c_2 + d_a * j);
                        }

                        anglesOfCentresOfArcs.Add(a_c_2);
                        anglesArrays[k].Add(a22);
                        for(int j = 0; j < countOfVariants; j++)
                        {
                            pointsOnArcs[k].Add(new PointD(centre1.X + bound_Stadium.Radius * Math.Cos(anglesArrays[k][j]), centre1.Y + bound_Stadium.Radius * Math.Sin(anglesArrays[k][j])));
                        }

                        p_c = new PointD(centre1.X + bound_Stadium.Radius * Math.Cos(a_c_2), centre1.Y + bound_Stadium.Radius * Math.Sin(a_c_2));
                        centresOfArcs.Add(p_c);


                        indexesOfRibsWithSources.Add(i);
                        double d_a_p1_p2 = a22 - a11;

                        double a_p1 = a_source - d_a_p1_p2 / 2;
                        double a_p2 = a_source + d_a_p1_p2 / 2;

                        double d_alpha = d_a_p1_p2 / (2 * (countOfVariants - 1));
                        for (int j = 0; j < countOfVariants; j++)
                        {
                            p11 = new PointD(centre1.X + bound_Stadium.Radius * Math.Cos(a_p1 - d_alpha * j), centre1.Y + bound_Stadium.Radius * Math.Sin(a_p1 - d_alpha * j));
                            p22 = new PointD(centre1.X + bound_Stadium.Radius * Math.Cos(a_p2 - d_alpha * j), centre1.Y + bound_Stadium.Radius * Math.Sin(a_p2 - d_alpha * j));
                            new_ribs_Owning_Sources[k].Add(new Bound_Rib(p11, p22));
                        }
                        break;
                    }
                }
            }
            for (int k = sources.Count / 2; k < sources.Count; k++)
            {
                for (int i = 0; i < bound.Bound_Ribs.Count; i++)
                {
                    Bound_Rib rib = bound.Bound_Ribs[i];
                    double length = rib.LengthElement;
                    r_c = rib.GetMiddleOfRib().DistanceToOtherPoint(sources[k].Location);

                    double a_source = Trigonometry.Angle(centre2.X, centre2.Y, sources[k].Location.X, sources[k].Location.Y);
                    sources[k].Location = new PointD(centre2.X + bound_Stadium.Radius * Math.Cos(a_source), centre2.Y + bound_Stadium.Radius * Math.Sin(a_source));
                    if (r_c < bound.Bound_Ribs[i].LengthElement / 2)
                    {
                        ribs[k] = rib;


                        a11 = GetAngle(i, bound_Stadium);
                        a22 = GetAngle(i + 1, bound_Stadium);
                        a_c_2 = (a11 + a22) / 2;
                        d_a = (a22 - a_c_2) / (countOfVariants - 1);
                        anglesArrays[k].Add(a_c_2);
                        for (int j = 1; j < countOfVariants - 1; j++)
                        {
                            anglesArrays[k].Add(a_c_2 + d_a * j);
                        }
                        anglesOfCentresOfArcs.Add(a_c_2);
                        anglesArrays[k].Add(a22);
                        for (int j = 0; j < countOfVariants; j++)
                        {
                            pointsOnArcs[k].Add(new PointD(centre2.X + bound_Stadium.Radius * Math.Cos(anglesArrays[k][j]), centre2.Y + bound_Stadium.Radius * Math.Sin(anglesArrays[k][j])));
                        }

                        indexesOfRibsWithSources.Add(i);
                        double d_a_p1_p2 = a22 - a11;

                        double a_p1 = a_source - d_a_p1_p2 / 2;
                        double a_p2 = a_source + d_a_p1_p2 / 2;

                        double d_alpha = d_a_p1_p2 / (2 * (countOfVariants - 1));
                        for (int j = 0; j < countOfVariants; j++)
                        {
                            p11 = new PointD(centre2.X + bound_Stadium.Radius * Math.Cos(a_p1 - d_alpha * j), centre2.Y + bound_Stadium.Radius * Math.Sin(a_p1 - d_alpha * j));
                            p22 = new PointD(centre2.X + bound_Stadium.Radius * Math.Cos(a_p2 - d_alpha * j), centre2.Y + bound_Stadium.Radius * Math.Sin(a_p2 - d_alpha * j));
                            new_ribs_Owning_Sources[k].Add(new Bound_Rib(p11, p22));
                        }
                        break;
                    }
                }
            }
        }



        static private double GetAngle(int ribNumber, Bound_Stadium bound_stadium)
        {
            int n = model.Bounds[1].Bound_Ribs.Count;
            int n1, n2, n3, n4;
            double dAlpha, Alpha = 0;

            double perimeter = bound_stadium.GetPerimeter();
            double r = bound_stadium.Radius;
            n1 = Convert.ToInt32(n * bound_stadium.Width / perimeter);

            n2 = Convert.ToInt32(n * Math.PI * bound_stadium.Radius / perimeter);
            dAlpha = Math.PI / n2;
            n3 = n1;
            n4 = n - n1 - n2 - n3;



            if (ribNumber >= n1 && ribNumber <= n1 + n2)
            {
                Alpha = Math.PI;
                for (int i = 0; i < ribNumber - n1; i++)
                    Alpha += dAlpha;
            }

            if (ribNumber >= n1 + n2 + n3)
            {
                Alpha = 0;
                for (int i = 0; i < ribNumber - (n1 + n2 + n3); i++)
                    Alpha += dAlpha;
            }
            return Alpha;
        }


        static private void SaveGraphicData(string fileName, string subdirName = "Default")
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path_to_dir);
            directoryInfo.CreateSubdirectory(subdirName);
            using (StreamWriter writer = new StreamWriter(path_to_dir + $"\\{subdirName}\\{fileName}.dat"))
            {
                for (int i = 0; i < X.Count; i++)
                {
                    writer.WriteLine(X[i] + "\t" + function[i]);
                }
            }
        }


    }
}
