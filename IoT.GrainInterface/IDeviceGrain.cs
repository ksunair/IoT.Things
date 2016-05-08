using System.Threading.Tasks;
using Orleans;

namespace IoT.GrainInterface
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
	public interface IDeviceGrain : IGrainWithIntegerKey
    {
        Task SetTemperature(double value);
    }
}
