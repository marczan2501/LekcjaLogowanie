using System.IO;
using System.Data.SQLite;

namespace LogowanieWPFNazwisko
{
    public class SqlCon
    {
        SQLiteConnection m_dbConnection;
        public void IfDatabaseExist(string baza, string tabela)
        {
            if (File.Exists(baza))
            {
                ConnectToDatabase(baza);
            }
            else
            {
                SQLiteConnection.CreateFile(baza);
                ConnectToDatabase(baza);
                CreateTable(tabela);
            }
        }

        public void ConnectToDatabase(string baza)
        {
            m_dbConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", baza));
            m_dbConnection.Open();
        }

        public void CloseConnection()
        {
            m_dbConnection.Close();
        }

        public void CreateTable(string tabela)
        {
            string sql = string.Empty;
            if (tabela == "Users")
            {
                sql = string.Format("create table {0} (Id int, User varchar(90), Password varchar(90))", tabela);
            }
            else
            {
                return;
            }
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public bool CheckUserName(string tabela, string userName)
        {
            string sql = string.Format("Select User From {1}", userName, tabela);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (userName == (string)reader["User"])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int CheckLast(string tabela)
        {
            int id = 0;
            string selectMaxId = string.Format("Select Max(Id) From {0}", tabela);
            SQLiteCommand selectMaxCmd = new SQLiteCommand(selectMaxId, m_dbConnection);
            object val = selectMaxCmd.ExecuteScalar();
            int.TryParse(val.ToString(), out id);
            return id;
        }

        public bool AddUser(string user, string password, string tabela)
        {
            if (!CheckUserName(tabela, user))
            {
                int id = CheckLast(tabela);
                id++;

               
                string sql = string.Format("INSERT INTO {3}(Id, User, Password ) VALUES({0},'{1}','{2}')",
                    id, user, password, tabela);
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int LogOnUser(string user, string password, string tabela)
        {
            if (CheckUserName(tabela, user))
            {
                string sql = string.Format("Select * From {0}", tabela);
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (user == (string)reader["User"])
                        {
                            if (password == (string)reader["Password"])
                            {
                                return 1;
                            }
                            else
                            {
                                return 2;
                            }
                        }
                    }
                }
                return 3;
            }
            else
            {
                return 3;
            }
        }
    }
}