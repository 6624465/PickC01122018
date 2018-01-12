using Master.Contract;
using Master.DataFactory;
using PickC.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.BusinessFactory
{
    public class ManufacturerBO
    {
        ManufacturerDAL manufacturerDAL;
        public ManufacturerBO()
        {
            manufacturerDAL = new ManufacturerDAL();
        }

        public bool Save(DriverManufacturerDTO manufacturer) {
            return manufacturerDAL.Save(manufacturer);
        }
    }
}
