using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAttDataADO
{
    //Sample batch program using SQL SP/ADO

    class Program
    {
        //public static string ws_start = DateTime.Now.ToString();
        //public static string ws_end;
        public static string _database = ConfigurationSettings.AppSettings["FCCDATA"];
        static int line_cnt = 0;
        static int err_cnt = 0;

        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            //Setup input
            string ws_path = ConfigurationSettings.AppSettings["INPUT_PATH"];
            string ws_input_file = ConfigurationSettings.AppSettings["INPUT_FILE"];
            string ws_full_input = ws_path + ws_input_file;
            string inputData = File.ReadAllText(ws_path + ws_input_file);
            
            //Setup DB connection..
            SqlConnection _dest = new SqlConnection(_database);
            SqlCommand _sqlCommand;
            


            if (!File.Exists(ws_full_input))
            {
                Console.WriteLine("Error INPUT File Not found!");
            }
            else
            {
                try
                {
                    string[] allFields;
                    //SqlCommand sqlc;
                    _dest.Open();
                    _sqlCommand = new SqlCommand("usp_add_am_ant_sys", _dest);
                    //Add SP Parameters
                    _sqlCommand = SetupSqlCmd(_sqlCommand);
                    



                    foreach (string row in inputData.Split('\n'))
                    {
                        line_cnt++;
                        allFields = row.Split('|');

                        //Assign value to SP-Parameters
                        _sqlCommand =  AssignValueSqlCmd(_sqlCommand, allFields);

                        _sqlCommand.ExecuteNonQuery();

                        Console.WriteLine(row);


                    }
                }
                catch(Exception ex)
                {
                    //Log your error here
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    _dest.Close();
                }
            }
            
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine("FIN..");
            Console.WriteLine("FIN..");
        }

        public static SqlCommand SetupSqlCmd(SqlCommand sqlcom)
        {
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.Add("@am_dom_status", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@ant_dir_ind", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@ant_mode", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@ant_sys_id", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@application_id", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@aug_count", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@augmented_ind", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@bad_data_switch", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@biased_lat", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@domestic_pattern", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@dummy_data_switch", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@efficiency_restricted", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@efficiency_theoretical", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@eng_record_type", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@feed_circ_other", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@feed_circ_type", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@grandfathered_ind", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@hours_operation", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lat_deg", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lat_dir", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lat_min", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lat_sec", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lat_whole_secs", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lon_deg", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lon_dir", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lon_min", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lon_sec", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@lon_whole_secs", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@mainkey", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@power", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@q_factor", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@q_factor_custom_ind", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@rms_augmented", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@rms_standard", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@rms_theoretical", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@specified_hrs_range", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@tower_count", SqlDbType.NVarChar, 50);
            sqlcom.Parameters.Add("@last_update_date", SqlDbType.NVarChar, 50);
            return sqlcom;

        }

        public static SqlCommand AssignValueSqlCmd(SqlCommand _sqlCommand, string[] allFields)
        {
            _sqlCommand.Parameters["@am_dom_status"].Value = allFields[0].ToString().Trim();
            _sqlCommand.Parameters["@ant_dir_ind"].Value = allFields[1].ToString().Trim();
            _sqlCommand.Parameters["@ant_mode"].Value = allFields[2].ToString().Trim();
            _sqlCommand.Parameters["@ant_sys_id"].Value = allFields[3].ToString().Trim();
            _sqlCommand.Parameters["@application_id"].Value = allFields[4].ToString().Trim();
            _sqlCommand.Parameters["@aug_count"].Value = allFields[5].ToString().Trim();
            _sqlCommand.Parameters["@augmented_ind"].Value = allFields[6].ToString().Trim();
            _sqlCommand.Parameters["@bad_data_switch"].Value = allFields[7].ToString().Trim();
            _sqlCommand.Parameters["@biased_lat"].Value = allFields[8].ToString().Trim();
            _sqlCommand.Parameters["@domestic_pattern"].Value = allFields[9].ToString().Trim();
            _sqlCommand.Parameters["@dummy_data_switch"].Value = allFields[10].ToString().Trim();
            _sqlCommand.Parameters["@efficiency_restricted"].Value = allFields[11].ToString().Trim();
            _sqlCommand.Parameters["@efficiency_theoretical"].Value = allFields[12].ToString().Trim();
            _sqlCommand.Parameters["@eng_record_type"].Value = allFields[13].ToString().Trim();
            _sqlCommand.Parameters["@feed_circ_other"].Value = allFields[14].ToString().Trim();
            _sqlCommand.Parameters["@feed_circ_type"].Value = allFields[15].ToString().Trim();
            _sqlCommand.Parameters["@grandfathered_ind"].Value = allFields[16].ToString().Trim();
            _sqlCommand.Parameters["@hours_operation"].Value = allFields[17].ToString().Trim();
            _sqlCommand.Parameters["@lat_deg"].Value = allFields[18].ToString().Trim();
            _sqlCommand.Parameters["@lat_dir"].Value = allFields[19].ToString().Trim();
            _sqlCommand.Parameters["@lat_min"].Value = allFields[20].ToString().Trim();
            _sqlCommand.Parameters["@lat_sec"].Value = allFields[21].ToString().Trim();
            _sqlCommand.Parameters["@lat_whole_secs"].Value = allFields[22].ToString().Trim();
            _sqlCommand.Parameters["@lon_deg"].Value = allFields[23].ToString().Trim();
            _sqlCommand.Parameters["@lon_dir"].Value = allFields[24].ToString().Trim();
            _sqlCommand.Parameters["@lon_min"].Value = allFields[25].ToString().Trim();
            _sqlCommand.Parameters["@lon_sec"].Value = allFields[26].ToString().Trim();
            _sqlCommand.Parameters["@lon_whole_secs"].Value = allFields[27].ToString().Trim();
            _sqlCommand.Parameters["@mainkey"].Value = allFields[28].ToString().Trim();
            _sqlCommand.Parameters["@power"].Value = allFields[29].ToString().Trim();
            _sqlCommand.Parameters["@q_factor"].Value = allFields[30].ToString().Trim();
            _sqlCommand.Parameters["@q_factor_custom_ind"].Value = allFields[31].ToString().Trim();
            _sqlCommand.Parameters["@rms_augmented"].Value = allFields[32].ToString().Trim();
            _sqlCommand.Parameters["@rms_standard"].Value = allFields[33].ToString().Trim();
            _sqlCommand.Parameters["@rms_theoretical"].Value = allFields[34].ToString().Trim();
            _sqlCommand.Parameters["@specified_hrs_range"].Value = allFields[35].ToString().Trim();
            _sqlCommand.Parameters["@tower_count"].Value = allFields[36].ToString().Trim();
            _sqlCommand.Parameters["@last_update_date"].Value = allFields[37].ToString().Trim();


            return _sqlCommand;
        }
    }
}
