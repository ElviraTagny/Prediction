using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prediction
{
    public interface IRestService
    {
        Task<List<Profile>> RefreshDataAsync();

        Task<Profile> GetProfileAsync(String id);

        Task SaveProfileAsync(Profile item, bool isNewItem);

        Task DeleteProfileAsync(string id);
    }
}
