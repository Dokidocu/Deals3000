using System;
namespace Deals3000.Services
{
    public interface IDataStoreDelegate
    {

        void didFetchData(bool hasError);

    }
}
