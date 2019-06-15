using Parcial_Tidele.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_Tidele.Services.Repository
{
    public class SpellRepository : Repository
    {
        public SpellRepository()
        {
            this.Table = "HECHIZOS";
        }

        public List<Spell> FindSpellsByMagicianId(int magicianId)
        {
            try
            {
                String Query = String.Format("SELECT HECHIZOS.Id AS HechizoId, " +
                                             "HECHIZOS.Nombre AS HechizoNombre, " +
                                             "HECHIZOS.Descripcion AS HechizoDescripcion, " +
                                             "HECHIZOVENCEDOR.Id AS IdVencedor, " +
                                             "HECHIZOVENCEDOR.Nombre AS NombreVencedor, " +
                                             "HECHIZOVENCEDOR.Descripcion AS DescripcionVencedor " +
                                             "FROM HECHIZOSMAGOS " +
                                             "INNER JOIN HECHIZOS ON HECHIZOS.Id = HECHIZOSMAGOS.IdHechizo " +
                                             "LEFT JOIN HECHIZOS AS HECHIZOVENCEDOR ON HECHIZOVENCEDOR.Id = HECHIZOS.IdHechizoQueLoVence " +
                                             "WHERE HECHIZOSMAGOS.Id = {0}", magicianId);
                this.ExecSelect(Query);
                List<Spell> spells = new List<Spell>();

                while (this.SqlDataReader.Read())
                    spells.Add(this.GetRowCasted());

                return spells;
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

        public List<Spell> FindAll()
        {
            try
            {
                String Query = String.Format("SELECT HECHIZOS.Id AS HechizoId, " +
                                             "HECHIZOS.Nombre AS HechizoNombre, " +
                                             "HECHIZOS.Descripcion AS HechizoDescripcion, " +
                                             "HECHIZOVENCEDOR.Id AS IdVencedor, " +
                                             "HECHIZOVENCEDOR.Nombre AS NombreVencedor, " +
                                             "HECHIZOVENCEDOR.Descripcion AS DescripcionVencedor " +
                                             "FROM HECHIZOS " +
                                             "LEFT JOIN HECHIZOS AS HECHIZOVENCEDOR ON HECHIZOVENCEDOR.Id = HECHIZOS.IdHechizoQueLoVence " +
                                             "WHERE LEN(HECHIZOS.Descripcion) > 0");
                this.ExecSelect(Query);
                List<Spell> spells = new List<Spell>();

                while (this.SqlDataReader.Read())
                    spells.Add(this.GetRowCasted());

                return spells;
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

        private Spell GetRowCasted()
        {
            return new Spell
            {
                Id = Convert.ToInt32(this.SqlDataReader["HechizoId"].ToString()),
                Name = Convert.ToString(this.SqlDataReader["HechizoNombre"].ToString()),
                Description = Convert.ToString(this.SqlDataReader["HechizoDescripcion"].ToString()),
                SpellDefeat = new Spell
                {
                    Id = Convert.ToInt32(this.SqlDataReader["IdVencedor"].ToString()),
                    Name = Convert.ToString(this.SqlDataReader["NombreVencedor"].ToString()),
                    Description = Convert.ToString(this.SqlDataReader["DescripcionVencedor"].ToString())
                }
            };
        }
    }
}
