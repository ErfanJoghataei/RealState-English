using Microsoft.Extensions.Logging;
using RealState.Dal.Contexs;
using RealState.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealState.BusinessLogik.Servises
{
    public class RealStateServise : IRealStateServise
    {
        private readonly RealStateDbContext _realStateDbContext;
        private readonly ILogger<RealStateServise> _logger;

        public RealStateServise(RealStateDbContext realStateDbContext, ILogger<RealStateServise> logger)
        {
            _realStateDbContext = realStateDbContext;
            _logger = logger;
        }

        public List<Properties> GetAllProperties()
        {
            try
            {
                _logger.LogInformation("Fetching all properties from database.");
                var result = _realStateDbContext.properties.ToList();
                _logger.LogInformation("Fetched {Count} properties.", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all properties.");
                throw;
            }
        }

        public Properties GetPropertyById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching property with Id={Id}", id);
                var property = _realStateDbContext.properties.Find(id);
                if (property == null)
                {
                    _logger.LogWarning("Property with Id={Id} not found.", id);
                }
                else
                {
                    _logger.LogInformation("Property with Id={Id} fetched successfully.", id);
                }
                return property;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching property with Id={Id}", id);
                throw;
            }
        }
    }
}
