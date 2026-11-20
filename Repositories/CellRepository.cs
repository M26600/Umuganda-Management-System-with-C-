using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class CellRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        public List<Cell> GetAllCells()
        {
            List<Cell> cells = new List<Cell>();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT cell_id, cell_number, cell_name FROM cells";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cell cell = new Cell
                        {
                            CellId = Convert.ToInt32(reader["cell_id"]),
                            CellNumber = Convert.ToInt32(reader["cell_number"]),
                            CellName = reader["cell_name"].ToString()
                        };

                        cells.Add(cell);
                    }
                }
            }

            return cells;
        }
    }
}
