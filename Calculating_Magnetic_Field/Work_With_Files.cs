using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Diagnostics;
using Calculating_Magnetic_Field.Figures;
using Calculating_Magnetic_Field.Models;
using Calculating_Magnetic_Field.ModelFactories;
using Calculating_Magnetic_Field.Sources;

namespace Calculating_Magnetic_Field
{
    public static class Work_With_Files
    {

        public static float SizeScale;

        public static string path_file = "\\path file.txt";

        public static string path_to_directory_of_power_lines_data;//директория с файлами для построения изолиний

        public static string path_to_directory_of_gmsh;

        public static string path_to_data_files; //Директория с файлами задачи


        /// <summary>
        /// Определяет папку, в которой хранятся данные для построения графиков
        /// </summary>
        public static void Set_Paths_To_Problem_Files()
        {
            DirectoryInfo directory = new DirectoryInfo(".");
            //FileInfo file = new FileInfo(directory.FullName + path_file);

            // если файл, хранящий имя папки, существует, то название папки читается из него
            /*if (file.Exists)
            {
                using (StreamReader streamReader = new StreamReader(directory.FullName + path_file))
                {
                    path_to_directory_of_power_lines_data = streamReader.ReadLine();
                    path_to_data_files = streamReader.ReadLine();
                }
            }*/

            // иначе по умолчанию указывается папка "Построение силовых линий" в папке решения
            //else
            {
                /*DirectoryInfo directory2;
                directory2 = directory.Parent.Parent.Parent;*/
                path_to_directory_of_power_lines_data = directory.FullName + "\\Building_power_lines";
                path_to_data_files = directory.FullName + "\\Problem_files";
                using (StreamWriter streamWriter = new StreamWriter(directory.FullName + path_file))
                {
                    streamWriter.WriteLine(path_to_directory_of_power_lines_data);
                    streamWriter.WriteLine(path_to_data_files);
                }
            }
        }

        public static void RefreshPaths()
        {
            DirectoryInfo directory = new DirectoryInfo(".");
            /*DirectoryInfo directory2;
            directory2 = directory.Parent.Parent.Parent;*/
            path_to_directory_of_power_lines_data = directory.FullName + "\\Building_power_lines";
            path_to_data_files = directory.FullName + "\\Problem_files";
            using (StreamWriter streamWriter = new StreamWriter(directory.FullName + path_file))
            {
                streamWriter.WriteLine(path_to_directory_of_power_lines_data);
                streamWriter.WriteLine(path_to_data_files);
            }

        }


        /// <summary>
        /// Записывает в файлы узлы и элементы триангуляции, полученной в Gmsh
        /// </summary>
        /// <param name="Path_To_msh_File">Путь к файлу msh version 1, хранящим узлы и элементы триангуляции и т.д.</param>
        /// <param name="Path_To_Triangles_File">Путь к файлу с элементами триангуляции</param>
        /// <param name="Path_To_XY_File">Путь к файлу с узлами триангуляции</param>
        public static void WriteNodesAndElementsToFile(string Path_To_msh_File, string Path_To_Triangles_File, string Path_To_XY_File)
        {
            PointD[] PointsForCulc;
            List<string> elements, points;
            elements = new List<string>();
            points = new List<string>();
            int n = 0, k = 0;
            string str;
            string[] str1;
            string[,] str2, str3;

            //StreamReader stream = new StreamReader(path);
            try
            {
                using (StreamReader stream = new StreamReader(Path_To_msh_File))
                {
                    do
                    {
                        str = stream.ReadLine();
                    } while (str != "$NOD");
                    k = int.Parse(stream.ReadLine());
                    while (str != "$ENDNOD")
                    {
                        str = stream.ReadLine();
                        points.Add(str);
                    }

                    str = stream.ReadLine();
                    do
                    { str = stream.ReadLine(); } while (str.Split(' ').Length != 8);
                    while (str != "$ENDELM")
                    {
                        n++;
                        elements.Add(str);
                        str = stream.ReadLine();

                    }

                }

                PointsForCulc = new PointD[k];
                str3 = new string[k, 2];
                str2 = new string[n, 3];
                str1 = new string[8];
                for (int j = 0; j < n; j++)
                {
                    str1 = elements[j].Split(' ');
                    str2[j, 0] = str1[5];
                    str2[j, 1] = str1[6];
                    str2[j, 2] = str1[7];
                }
                str1 = new string[4];
                for (int j = 0; j < k; j++)
                {
                    str1 = points[j].Split(' ');
                    str3[j, 0] = str1[1];
                    str3[j, 1] = str1[2];
                    PointsForCulc[j] = new PointD(SizeScale * double.Parse(str1[1].Replace('.', ',')), SizeScale * double.Parse(str1[2].Replace('.', ',')));
                }
                using (StreamWriter stream = new StreamWriter(path_to_directory_of_power_lines_data + Path_To_Triangles_File))
                {
                    stream.WriteLine(n);
                    for (int j = 0; j < n; j++)
                    {
                        str = String.Format("{0} {1} {2}", str2[j, 0], str2[j, 1], str2[j, 2]);
                        stream.WriteLine(str);
                    }
                }
                using (StreamWriter stream = new StreamWriter(path_to_directory_of_power_lines_data + Path_To_XY_File))
                {
                    stream.WriteLine(k);
                    for (int j = 0; j < k; j++)
                    {
                        str = String.Format("{0} {1}", double.Parse(str3[j, 0].Replace('.', ',')), double.Parse(str3[j, 1].Replace('.', ',')));
                        stream.WriteLine(str.Replace(',', '.'));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения/записи", "Ошибка!");
            }
        }

        /// <summary>
        /// Записывает в файлы узлы и элементы триангуляции, полученной в Gmsh
        /// </summary>
        /// <param name="Path_To_msh_File">Путь к файлу msh version 1, хранящим узлы и элементы триангуляции и т.д.</param>
        /// <param name="Path_To_Triangles_File">Путь к файлу с элементами триангуляции</param>
        /// <param name="Path_To_XY_File">Путь к файлу с узлами триангуляции</param>
        public static PointD[] GetNodesFromFile(string Path_To_XY_File)
        {
            PointD[] PointsForCulc;
            int k = 0;
            string str;
            string[] str1;
            try
            {
                using (StreamReader stream = new StreamReader(Path_To_XY_File))
                {
                    k = Convert.ToInt32(stream.ReadLine());
                    PointsForCulc = new PointD[k];
                    for (int j = 0; j < k; j++)
                    {
                        str = stream.ReadLine();
                        str1 = str.Split(' ');
                        PointsForCulc[j].X = Convert.ToDouble(str1[0].Replace('.', ','));
                        PointsForCulc[j].Y = Convert.ToDouble(str1[1].Replace('.', ','));
                    }
                }

                return PointsForCulc;
            }
            catch
            {
                throw new IOException();
                //MessageBox.Show("Ошибка чтения/записи", "Ошибка!");
            }

            
        }

        /// <summary>
        /// Записывает в файл исходные данные задачи
        /// </summary>
        /// <param name="model">Ссылка на модель задачи</param>
        /// <param name="format">Формат файла (в долгом ящике) </param>
        /// <param name="file">Имя файла</param>
        public static void WritePhisicalObjectsInformationToFile(IModel model, string format, string file)
        {
            Bound_Rectangle rectangle;
            Bound_Circle circle;
            Bound_Stadium bound_Stadium;
            int FerrCount = model.Bounds.Count;
            int SourcesCount = model.Sources.Count;
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine("$Model");
                writer.WriteLine($"{model.GetPotencialDimensionType()}");
                writer.WriteLine($"{model.Potencial.TypeOFPotencial}");
                writer.WriteLine($"{model.Potencial}");
                writer.WriteLine($"{model.Depth}");
                writer.WriteLine($"{model.PhysicalField}");
                writer.WriteLine("$EndModel");
                writer.WriteLine("$Fields");
                writer.WriteLine(FerrCount);
                for(int i = 0; i < FerrCount; i++)
                {
                    switch (model.Bounds[i].ThisFigure.GetFigureType())
                    {
                        case FigureType.Rectangle:
                            {
                                rectangle = (Bound_Rectangle)model.Bounds[i].ThisFigure;
                                writer.WriteLine("Rectangle");
                                writer.WriteLine($"{rectangle.Location.X} " +
                                                 $"{rectangle.Location.Y} " +
                                                 $"{rectangle.Lenth} " +
                                                 $"{rectangle.Width} " +
                                                 $"{model.Bounds[i].Bound_Ribs.Count} " +
                                                 $"{model.Bounds[i].Right_Mu} " +
                                                 $"{model.Bounds[i].Left_Mu}");
                                break;
                            }
                        case FigureType.Circle:
                            {
                                circle = (Bound_Circle)model.Bounds[i].ThisFigure;
                                writer.WriteLine("Circle");
                                writer.WriteLine($"{circle.Location.X} " +
                                                 $"{circle.Location.Y} " +
                                                 $"{circle.Radius} " +
                                                 $"{model.Bounds[i].Bound_Ribs.Count} " +
                                                 $"{model.Bounds[i].Right_Mu} " +
                                                 $"{model.Bounds[i].Left_Mu}");
                                break;
                            }
                        case FigureType.Stadium:
                            {
                                bound_Stadium = (Bound_Stadium)model.Bounds[i].ThisFigure;
                                writer.WriteLine("Stadium");
                                writer.WriteLine($"{bound_Stadium.Location.X} " +
                                                 $"{bound_Stadium.Location.Y} " +
                                                 $"{bound_Stadium.Length} " +
                                                 $"{bound_Stadium.Width} " +
                                                 $"{bound_Stadium.Orientation} " +
                                                 $"{model.Bounds[i].Bound_Ribs.Count} " +
                                                 $"{model.Bounds[i].Right_Mu} " +
                                                 $"{model.Bounds[i].Left_Mu}");
                                break;
                            }
                    }
                    
                }
                writer.WriteLine("$EndFields");
                writer.WriteLine("$Sources");
                writer.WriteLine(SourcesCount);
                for(int i = 0; i < SourcesCount; i++)
                    switch (model.Sources[i].GetSourceType())
                    {
                        case SourceTypes.VolumeSource:
                            {
                                switch (model.Sources[i].GetFigureType())
                                {
                                    case FigureType.Rectangle:
                                        {
                                            rectangle = (Bound_Rectangle)model.Sources[i].GetFigure();
                                            writer.WriteLine("Rectangle VolumeSource");
                                            writer.WriteLine($"{rectangle.Location.X} " +
                                                             $"{rectangle.Location.Y} " +
                                                             $"{rectangle.Lenth} " +
                                                             $"{rectangle.Width} " +
                                                             $"{((IVolumeSource)model.Sources[i]).SourcePower} " +
                                                             $"{((IVolumeSource)model.Sources[i]).N} " +
                                                             $"{((IVolumeSource)model.Sources[i]).M}");
                                            break;
                                        }
                                }
                                
                                break;
                            }

                        case SourceTypes.ResidualIntensitySource:
                            {
                                switch (model.Sources[i].GetFigureType())
                                {
                                    case FigureType.Rectangle:
                                        {
                                            rectangle = (Bound_Rectangle)model.Sources[i].GetFigure();
                                            writer.WriteLine("Rectangle ResidualIntensitySource");
                                            writer.WriteLine($"{rectangle.Location.X} " +
                                                             $"{rectangle.Location.Y} " +
                                                             $"{rectangle.Lenth} {rectangle.Width} " +
                                                             $"{((IResidualIntensitySource)model.Sources[i]).Direction} " +
                                                             $"{((IResidualIntensitySource)model.Sources[i]).ResidualIntensity} " +
                                                             $"{((IResidualIntensitySource)model.Sources[i]).N}");
                                            break;
                                        }
                                }
                                break;
                            }
                        case SourceTypes.LinearSource:
                            {
                                writer.WriteLine("Line LinearSource");
                                writer.WriteLine($"{((ILinearSource)model.Sources[i]).Rib.Point1.X} " +
                                                 $"{((ILinearSource)model.Sources[i]).Rib.Point1.Y} " +
                                                 $"{((ILinearSource)model.Sources[i]).Rib.Point2.X} " +
                                                 $"{((ILinearSource)model.Sources[i]).Rib.Point2.Y} " +
                                                 $"{((ILinearSource)model.Sources[i]).Density}" +
                                                 $"{((ILinearSource)model.Sources[i]).N}");
                                break;
                            }
                        case SourceTypes.PointSource:
                            {
                                writer.WriteLine("Point PointSource");
                                writer.WriteLine($"{((IPointSource)model.Sources[i]).Location.X}" +
                                                 $"{((IPointSource)model.Sources[i]).Location.Y}" +
                                                 $"{((IPointSource)model.Sources[i]).SourcePower}");
                                break;
                            }
                    }
                writer.WriteLine("$EndSources");
            }
        }

        /// <summary>
        /// Чтение из файла исходных данных задачи
        /// </summary>
        /// <param name="model">Ссылка на модель задачи</param>
        /// <param name="file">Имя файла</param>
        public static void ReadPhisicalObjectsInformationFromFile(out IModel model, out ModelFactory modelFactory, string file)
        {
            PhysicalField physicalField = PhysicalField.Magnetic;
            modelFactory = null;
            model = null;
            int FerrCount;
            int SourcesCount;
            string str;
            string[] data;
            string name_of_figure;
            string type_of_source;
            using (StreamReader reader = new StreamReader(file))
            {
                while ((str = reader.ReadLine()) != "$Model") { }
                str = reader.ReadLine();
                switch (str)
                {
                    case "Electric":
                        {
                            physicalField = PhysicalField.Electric;
                            break;
                        }
                    case "Magnetic":
                        {
                            physicalField = PhysicalField.Magnetic;
                            break;
                        }
                    case "Current":
                        {
                            physicalField = PhysicalField.Current;
                            break;
                        }
                }
                str = reader.ReadLine();
                switch (str)
                {
                    case "Vector":
                        {
                            modelFactory = new VectorModelFactory(physicalField);
                            break;
                        }
                    case "Scalar":
                        {
                            modelFactory = new ScalarModelFactory(physicalField);
                            break;
                        }
                }
                str = reader.ReadLine();
                model = modelFactory.CreateModel(Convert.ToDouble(str));

                str = reader.ReadLine();
                switch (str)
                {
                    case "PSL":
                        {
                            model.PotencialFactoryMethod = new PSL_Factory_Method();
                            break;
                        }
                    case "PDL":
                        {
                            model.PotencialFactoryMethod = new PDL_Factory_Method();
                            break;
                        }
                }
                model.Potencial = model.PotencialFactoryMethod.CreatePotencial(model.GetPotencialDimensionType());

                while ((str = reader.ReadLine()) != "$Fields") { }
                FerrCount = Convert.ToInt32(reader.ReadLine());
                while ((str = reader.ReadLine())!= "$EndFields")
                {
                    name_of_figure = str;
                    switch (name_of_figure)
                    {
                        case "Rectangle":
                            {
                                data = reader.ReadLine().Split(' ');
                                model.AddBorderOfEnvironments(new Bound_Rectangle(new System.Drawing.PointF(
                                                                                                Convert.ToSingle(data[0]),
                                                                                                Convert.ToSingle(data[1])),
                                                                                                Convert.ToSingle(data[2]),
                                                                                                Convert.ToSingle(data[3])),
                                                              Convert.ToInt32(data[4]),
                                                              Convert.ToDouble(data[5]),
                                                              Convert.ToDouble(data[6]));
                                break;            
                            }                     
                        case "Circle":            
                            {                     
                                data = reader.ReadLine().Split(' ');
                                model.AddBorderOfEnvironments(new Bound_Circle(new System.Drawing.PointF(
                                                                                                Convert.ToSingle(data[0]),
                                                                                                Convert.ToSingle(data[1])),
                                                                                                Convert.ToSingle(data[2])),
                                                              Convert.ToInt32(data[3]),
                                                              Convert.ToDouble(data[4]),
                                                              Convert.ToDouble(data[5]));
                                break;            
                            }
                        case "Stadium":
                            {
                                data = reader.ReadLine().Split(' ');
                                model.AddBorderOfEnvironments(new Bound_Stadium(new System.Drawing.PointF(
                                                                                                Convert.ToSingle(data[0]),
                                                                                                Convert.ToSingle(data[1])),
                                                                                                Convert.ToSingle(data[2]),
                                                                                                Convert.ToSingle(data[3]),
                                                                                                (StadiumOrientation)Enum.Parse(typeof(StadiumOrientation), data[4])),
                                                              Convert.ToInt32(data[5]),
                                                              Convert.ToDouble(data[6]),
                                                              Convert.ToDouble(data[7]));
                                break;
                            }
                    }

                }
                while ((str = reader.ReadLine()) != "$Sources") { }
                SourcesCount = Convert.ToInt32(reader.ReadLine());
                while ((str = reader.ReadLine()) != "$EndSources")
                {
                    data = str.Split(' ');

                    name_of_figure = data[0];
                    type_of_source = data[1];
                    data = reader.ReadLine().Split(' ');
                    switch (type_of_source)
                    {
                        case "VolumeSource":
                            {
                                switch (name_of_figure)
                                {
                                    case "Rectangle":
                                        {
                                            model.AddSource(modelFactory.CreateVolumeSource(new Bound_Rectangle(new System.Drawing.PointF(
                                                                                                Convert.ToSingle(data[0]),
                                                                                                Convert.ToSingle(data[1])),
                                                                                                Convert.ToSingle(data[2]),
                                                                                                Convert.ToSingle(data[3])),
                                                                                            Convert.ToDouble(data[4]),
                                                                                            Convert.ToInt32(data[5]),
                                                                                            Convert.ToInt32(data[6])));
                                            break;
                                        }
                                }

                                break;
                            }
                        case "ResidualIntensitySource":
                            {
                                
                                SimpleDirections direction = 0;
                                switch (data[4])
                                {
                                    case "FromBottomToTop":
                                        {
                                            direction = SimpleDirections.FromBottomToTop;
                                            break;
                                        }
                                    case "FromTopToBottom":
                                        {
                                            direction = SimpleDirections.FromTopToBottom;
                                            break;
                                        }
                                    case "FromLeftToRight":
                                        {
                                            direction = SimpleDirections.FromLeftToRight;
                                            break;
                                        }
                                    case "FromRightToLeft":
                                        {
                                            direction = SimpleDirections.FromRightToLeft;
                                            break;
                                        }
                                }
                                model.AddSource(modelFactory.CreateResidualIntensitySource(new Bound_Rectangle(new System.Drawing.PointF(
                                                                                                               Convert.ToSingle(data[0]),
                                                                                                               Convert.ToSingle(data[1])),
                                                                                                               Convert.ToSingle(data[2]),
                                                                                                               Convert.ToSingle(data[3])),
                                                                                           direction,
                                                                                           Convert.ToDouble(data[5]),
                                                                                           Convert.ToInt32(data[6])));
                                break; 
                            }
                        case "LinearSource":
                            {
                                model.AddSource(modelFactory.CreateLinearSource(new PointD(
                                                                                    Convert.ToSingle(data[0]),
                                                                                    Convert.ToSingle(data[1])),
                                                                                new PointD(
                                                                                    Convert.ToSingle(data[2]),
                                                                                    Convert.ToSingle(data[3])),
                                                                                Convert.ToDouble(data[4]),
                                                                                Convert.ToInt32(data[5])));
                                break;
                            }
                        case "PointSource":
                            {
                                model.AddSource(modelFactory.CreatePointSource(new PointD(
                                                                                    Convert.ToSingle(data[0]),
                                                                                    Convert.ToSingle(data[1])),
                                                                                Convert.ToDouble(data[2]), model));
                                break;
                            }
                    }
                }
            }
        }

        public static void CreateGeoFile(PointD p0, PointD p1, PointD p2, PointD p3, double size, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("SetFactory(\"OpenCASCADE\");");
                writer.WriteLine("Point(1) = {" + $"{Convert.ToString(p0.X).Replace(',', '.')}, {Convert.ToString(p0.Y).Replace(',', '.')}, 0, {Convert.ToString(size).Replace(',', '.')}" + "};");
                writer.WriteLine("Point(2) = {" + $"{Convert.ToString(p1.X).Replace(',', '.')}, {Convert.ToString(p1.Y).Replace(',', '.')}, 0, {Convert.ToString(size).Replace(',', '.')}" + "};");
                writer.WriteLine("Point(3) = {" + $"{Convert.ToString(p2.X).Replace(',', '.')}, {Convert.ToString(p2.Y).Replace(',', '.')}, 0, {Convert.ToString(size).Replace(',', '.')}" + "};");
                writer.WriteLine("Point(4) = {" + $"{Convert.ToString(p3.X).Replace(',', '.')}, {Convert.ToString(p3.Y).Replace(',', '.')}, 0, {Convert.ToString(size).Replace(',', '.')}" + "};");

                writer.WriteLine("Line(1) = {1,2};");
                writer.WriteLine("Line(2) = {2,3};");
                writer.WriteLine("Line(3) = {3,4};");
                writer.WriteLine("Line(4) = {4,1};");

                writer.WriteLine("Line Loop(5)={1,2,3,4};");

                writer.WriteLine("Plane Surface(6) = {5};");

                writer.WriteLine("Show \"*\"");
            }
        }


        public static void CreateMshFile(string file_geo, string file_msh, string format)
        {
            Set_path_to_Gmsh();
            Process process = new Process();
            process.StartInfo.FileName = path_to_directory_of_gmsh + "\\gmsh-4.6.0-Windows64" + "\\gmsh.exe";
            process.StartInfo.Arguments = $@"{file_geo} -format {format} -2";
            process.Start();
        }

        private static void Set_path_to_Gmsh()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog()== DialogResult.OK)
            {
                path_to_directory_of_gmsh = folderBrowserDialog.SelectedPath;
            }
            
        }

        public static void SaveGraphicData(List<double> X, List<double> func)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".dat";
            saveFileDialog.Filter = "Data files (*.dat)|*.dat|All files|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    for (int i = 0; i < X.Count; i++)
                    {
                        writer.WriteLine(X[i] + "\t" + func[i]);
                    }
                }
            }
        }

    }
}
