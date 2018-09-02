using System.Collections.Generic;
using System.Threading.Tasks;

namespace EOS.Client
{
    public interface ISignatureProvider
    {
        IEnumerable<string> AvailableKeys { get; }
        Task<IEnumerable<string>> SignAsync(byte[] data, IEnumerable<string> requiredKeys);
    }
}