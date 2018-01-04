using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prediction
{
    public interface IRestService
    {
        Task<List<Profile>> RefreshDataAsync();

        Task<Profile> GetProfileAsync(String id);

        Task<bool> SaveProfileAsync(Profile item, bool isNewItem);

        Task<bool> DeleteProfileAsync(string id);
    }
}
