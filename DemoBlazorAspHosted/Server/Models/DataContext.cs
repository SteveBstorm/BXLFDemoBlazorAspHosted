using DemoBlazorAspHosted.Shared;

namespace DemoBlazorAspHosted.Server.Models
{
    public class DataContext
    {
        private List<Stagiaire> Stagiaires { get; set; }
        public DataContext()
        {
            Stagiaires = new List<Stagiaire>();
            Stagiaires.Add(new Stagiaire { Id = 1, LastName = "Morre", FirstName = "Thierry", Email = "tm@form.be", Password = "Test1234=" });
            Stagiaires.Add(new Stagiaire { Id = 2, LastName = "Person", FirstName = "Michael", Email = "mp@form.be", Password = "Test1234=" });
            Stagiaires.Add(new Stagiaire { Id = 3, LastName = "Ly", FirstName = "Khun", Email = "kl@form.be", Password = "Test1234=" });
            Stagiaires.Add(new Stagiaire { Id = 4, LastName = "Lorent", FirstName = "Steve", Email = "sl@form.be", Password = "Test1234=" });
        }

        public List<Stagiaire> GetAll()
        {
            return Stagiaires;
        }

        public Stagiaire GetById(int Id)
        {
            return Stagiaires.FirstOrDefault(s => s.Id == Id);
        }

        public Stagiaire Login(string email, string password)
        {

            try
            {
                return Stagiaires.First(s => s.Email == email && s.Password == password);
            }
            catch
            {
                throw new ArgumentNullException("Erreut d'authentification");
            }
        }

        public void Create(Stagiaire s)
        {
            s.Id = Stagiaires.Max(s => s.Id) + 1;

            Stagiaires.Add(s);
        }

        public void Delete(int Id)
        {
            Stagiaires.RemoveAt(Stagiaires.FindIndex(s => s.Id == Id));
        }
    }
}
