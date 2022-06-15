using Jitbit.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RunNameHeader.Text = "Данные из БД соответствующие RunName";
            
        }


        public Db DataBase { set; get; } = new Db();

        FileDialog dialog = new FileDialog();


        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            dialog.FilePath = "";
            DataBase.ToFluid.Clear();
            DataBase.Fluids.Clear();
            DataBase.Physical.Clear();
            DataBase.Run.Clear();
            DataBase.ToPhysical.Clear();


            dialog.OpenFileDialog();

            if (dialog.FilePath != "") {
                string sqlExpression = "";
                using (var connection = new SqliteConnection("Data Source=" + dialog.FilePath))
                {
                    connection.Open();


                    sqlExpression = "SELECT * FROM PipingFluid";
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                DataBase.Fluids.Add(new PipingFluid
                                {
                                    OID = reader.GetValue(0).ToString(),
                                    FluidCode = reader.GetValue(1).ToString(),
                                    PressureRating = (double)reader.GetValue(2),
                                    Temp = (double)reader.GetValue(3)
                                });

                            }
                        }
                    }
                    sqlExpression = "SELECT * FROM PipingPhysical";
                    command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                int incr = 0;
                                DataBase.Physical.Add(new PipingPhysical
                                {
                                    OID = reader.GetValue(incr++).ToString(),
                                    RunLength = (double)reader.GetValue(incr++),
                                    LineWeight = (double)reader.GetValue(incr++),
                                    RunDiam = (double)reader.GetValue(incr++),
                                    WallThickness = (double)reader.GetValue(incr)
                                });

                            }
                        }
                    }
                    sqlExpression = "SELECT * FROM PipingRun";
                    command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                int incr = 0;
                                DataBase.Run.Add(new PipingRun
                                {
                                    OID = reader.GetValue(incr++).ToString(),
                                    RunName = reader.GetValue(incr++).ToString(),
                                    ItemTag = reader.GetValue(incr++).ToString(),
                                    NPD = reader.GetValue(incr++).ToString()
                                });

                            }
                        }
                    }
                    sqlExpression = "SELECT * FROM RunToFluid";
                    command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                int incr = 0;
                                DataBase.ToFluid.Add(new RunToFluid
                                {
                                    OIDFrom = reader.GetValue(incr++).ToString(),
                                    OIDTo = reader.GetValue(incr++).ToString(),
                                });

                            }
                        }
                    }
                    sqlExpression = "SELECT * FROM RunToPhysical";
                    command = new SqliteCommand(sqlExpression, connection);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read())   // построчно считываем данные
                            {
                                int incr = 0;
                                DataBase.ToPhysical.Add(new RunToPhysical
                                {
                                    OIDFrom = reader.GetValue(incr++).ToString(),
                                    OIDTo = reader.GetValue(incr++).ToString(),
                                });

                            }
                        }
                    }

                    DB.ItemsSource = DataBase.Run;
                    DB2.ItemsSource = DataBase.Run;
                    DB3.ItemsSource = DataBase.Physical;
                    DB4.ItemsSource = DataBase.Fluids;

                }


                }
            }

            private void DB_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
            {
                if (DB.SelectedIndex >= 0)
                {
                    List<RunToPhysical> res_1 = DataBase.ToPhysical.Where(x => x.OIDFrom == DataBase.Run[DB.SelectedIndex].OID).ToList();
                    List<RunToFluid> res_2 = DataBase.ToFluid.Where(x => x.OIDFrom == DataBase.Run[DB.SelectedIndex].OID).ToList();
                    List<PipingFluid> PipFluid = new List<PipingFluid>();
                    List<PipingPhysical> PipPhysical = new List<PipingPhysical>();

                    for (int i = 0; i < res_1.Count; i++)
                    {
                        PipFluid.AddRange(DataBase.Fluids.Where(x => x.OID == res_2[i].OIDTo).ToList());

                    }
                    for (int i = 0; i < res_2.Count; i++)
                    {
                        PipPhysical.AddRange(DataBase.Physical.Where(x => x.OID == res_1[i].OIDTo).ToList());
                    }

                }
            }
        
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CsvExport exportCSV = new CsvExport();
            

            FileDialog dialog2 = new FileDialog();
            dialog2.SaveFileDialog();
            
                int k = 0;
                int b = 0;
                int m = 0;

            while (k < DataBase.Run.Count() || b < DataBase.Physical.Count() || m < DataBase.Fluids.Count())
            {
                exportCSV.AddRow();

                if (DataBase.Run[k].RunName.Any())
                {
                    exportCSV["RunName"] = DataBase.Run[k].RunName;
                    exportCSV["ItemTag"] = DataBase.Run[k].ItemTag;
                    exportCSV["NPD"] = DataBase.Run[k].NPD;
                    k++;
                }
                if (DataBase.Physical[b].OID.Any())
                {
                    exportCSV["RunLength"] = DataBase.Physical[b].RunLength;
                    exportCSV["LineWeight"] = DataBase.Physical[b].LineWeight;
                    exportCSV["RunDiam"] = DataBase.Physical[b].RunDiam;
                    b++;
                }
                if (m < DataBase.Fluids.Count())
                    if (DataBase.Fluids[m].OID.Any())
                {

                     exportCSV["PressureRating"] = DataBase.Fluids[m].PressureRating;
                     exportCSV["FluidCode"] = DataBase.Fluids[m].FluidCode;
                     exportCSV["Temp"] = DataBase.Fluids[m].Temp;
                     m++;
                 }

            }


            exportCSV.ExportToFile(dialog2.FilePath + ".csv");
           
        }
    }
    }

