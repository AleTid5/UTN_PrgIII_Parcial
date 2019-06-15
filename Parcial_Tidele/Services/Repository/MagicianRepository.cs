using Parcial_Tidele.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Services.Repository
{
    public class MagicianRepository : Repository
    {
        public MagicianRepository()
        {
            this.Table = "MAGOS";
        }

        public List<Magician> FindAll()
        {
            try
            {
                String Query = String.Format("SELECT MAGOS.Id AS MagoId, " +
                                             "MAGOS.Nombre AS MagoNombre, " +
                                             "MAGOS.IdCasa AS MagoCasaId " +                                             
                                             "FROM {0} " +
                                             "INNER JOIN {1} ON {1}.Id = {0}.IdMago" ,
                                             "HECHIZOSMAGOS",
                                             this.Table);
                this.ExecSelect(Query);
                List<Magician> magicians = new List<Magician>();

                while (this.SqlDataReader.Read())
                    magicians.Add(this.GetRowCasted());

                return magicians;
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

        private Magician GetRowCasted()
        {
            return new Magician
            {
                Id = int.Parse(this.SqlDataReader["MagoId"].ToString()),
                Name = this.SqlDataReader["MagoNombre"].ToString(),
                House = new HouseRepository().FindById(Convert.ToInt32(this.SqlDataReader["MagoCasaId"].ToString())),
                Spells = new SpellRepository().FindSpellsByMagicianId(Convert.ToInt32(this.SqlDataReader["MagoId"].ToString()))
            };
        }
    }
}
