using ConsoleApp50.Models;
using System.Threading.Tasks;

namespace ConsoleApp50.BehaviorBased
{
    public interface IPersonRepository
    {
        public string SampleProperty { get; set; }
        Task<Person> GetAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task<Person> UpdateAsync(Person personToUpdate);
    }
}