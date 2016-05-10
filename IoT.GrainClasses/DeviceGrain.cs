using System.Threading.Tasks;
using Orleans;
using IoT.GrainInterface;
using System;
using Orleans.Providers;

namespace IoT.GrainClasses
{
    public class DeviceGrainState
    {
        public double LastValue { get; set; }
    }

    [StorageProvider(ProviderName="MemoryStore")]
    public class DeviceGrain : Grain<DeviceGrainState>, IDeviceGrain
    {
        public Task<double> GetTemperature()
        {
            return Task.FromResult(State.LastValue);
        }

        public override Task OnActivateAsync()
        {
            var id = this.GetPrimaryKeyLong();
            Console.WriteLine("Activated {0}", id);
            
            //Console.WriteLine("Last state value {0}", State.LastValue);
            return base.OnActivateAsync();
        }

        public async Task SetTemperature(double value)
        {
            if (this.State.LastValue < 100 && value >= 100)
            {
                Console.WriteLine("High Temperature recorded {0}", value);
            }
            if (State.LastValue != value)
            {
                this.State.LastValue = value;
                await base.WriteStateAsync();
            }
        }
    }
}
