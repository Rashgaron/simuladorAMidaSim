using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Cliente : IEntidad
    {
        public int Id { get; set; }
        public int Created_at { get; set; }
        public Cliente(int id, int created_at)
        {
            this.Id = id;
            this.Created_at = created_at;
        }
    }
}