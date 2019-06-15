using Parcial_Tidele.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Services.Repository
{
    public class HouseRepository : Repository
    {
        public HouseRepository ()
        {
            this.Table = "CASAS";
        }

        public House FindById(int Id)
        {
            try
            {
                String Query = String.Format("SELECT TOP 1 * FROM {0} WHERE Id = {1}" , this.Table, Id);
                this.ExecSelect(Query);
                this.SqlDataReader.Read();

                return this.GetRowCasted();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.SqlConnection.Close();
            }
        }

        public List<House> FindAll()
        {
            try
            {
                String Query = String.Format("SELECT * FROM {0}" , this.Table);
                this.ExecSelect(Query);
                List<House> houses = new List<House>();

                while (this.SqlDataReader.Read())
                    houses.Add(this.GetRowCasted());

                return houses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.SqlConnection.Close();
            }
        }

        private House GetRowCasted()
        {
            return new House
            {
                Id = int.Parse(this.SqlDataReader["Id"].ToString()),
                Description = this.SqlDataReader["Descripcion"].ToString()
            };
        }
    }
}
