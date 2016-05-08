using System.Threading.Tasks;
using Orleans;
using IoT.GrainInterface;
using System;
using Orleans.Providers;

namespace IoT.GrainClasses
{
    public interface IDeviceGrainState : IGrainState
    {
        double LastValue { get; set; }
    }

    [StorageProvider(ProviderName = "MemStore")]
    public class DeviceGrain : Grain<IDeviceGrainState>, IDeviceGrain
    {

        public override Task OnActivateAsync()
        {
            var id = this.GetPrimaryKeyLong();
            Console.WriteLine("Activated {0}", id);
            Console.WriteLine("Last state value {0}", this.State.LastValue);
            return base.OnActivateAsync();
        }

        public async Task SetTemperature(double value)
        {
            if (this.State.LastValue < 100 && value >= 100)
            {
                Console.WriteLine("High Temperature recorded {0}", value);
            }
            if (this.State.LastValue != value)
            {
                this.State.LastValue = value;
                await base.WriteStateAsync();
            }
        }
    }
}
