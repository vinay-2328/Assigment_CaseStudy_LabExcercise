using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;


namespace PayXpert.BusinessLayer.Services
{
    public class TaxService : ITaxService
    {
        TaxRepository taxRepository;
        public TaxService() 
        { 
            taxRepository = new TaxRepository();
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            return taxRepository.CalculateTax(employeeId,taxYear);
        }
        public Tax GetTaxById(int taxId)
        {
            return taxRepository.GetTaxById(taxId);
        }
        public IEnumerable<Tax> GetTaxesForEmployee(int employeeId)
        {
            return taxRepository.GetTaxesForEmployee(employeeId);
        }
        public IEnumerable<Tax> GetTaxesForYear(int taxYear)
        {
            return taxRepository.GetTaxesForYear((int) taxYear);
        }
    }
}
