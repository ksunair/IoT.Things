using IoT.GrainInterface;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class DeviceController : ApiController
    {
        public Task<double> Get()
        {
            var grain = GrainClient.GrainFactory.GetGrain<IDeviceGrain>(0);
            return grain.GetTemperature();
        }
    }
}
