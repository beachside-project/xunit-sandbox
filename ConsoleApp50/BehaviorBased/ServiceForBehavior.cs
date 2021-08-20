using ConsoleApp50.Models;
using System.Threading.Tasks;

namespace ConsoleApp50.BehaviorBased
{
    public class ServiceForBehavior
    {
        private readonly IPersonRepository _repository;

        public ServiceForBehavior(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Person> UpdatePersonAsync(Person personToUpdate, string state)
        {
            personToUpdate.State = state;
            var s1 = _repository.SampleProperty;
            _repository.SampleProperty = state;

            await Task.Delay(5000);
            var result = await _repository.UpdateAsync(personToUpdate);
            return result;
        }


        public async Task<Person> GetPersonAsync(string id)
        {
            // サンプルなのでそもそも GetAsync だけどうにかすれよってのはさておき。
            var isExists = await _repository.ExistsAsync(id);

            if (isExists)
            {
                return await _repository.GetAsync(id);
            }

            return null;
        }

        public Task<bool> ExistsPersonAsync(string id)
        {
            return _repository.ExistsAsync(id);
        }
    }
}