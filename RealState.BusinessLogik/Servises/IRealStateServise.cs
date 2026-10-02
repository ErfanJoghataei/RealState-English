using RealState.Dal.Entities;
using RealState.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealState.BusinessLogik.Servises
{
    public interface IRealStateServise
    {

        public List<Properties> GetAllProperties();
        public Properties GetPropertyById(int id);



    }
}
