using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;


namespace PayXpert.BusinessLayer.Services
{
    public class TaxService : ITaxService
    {
        readonly ITaxRepository taxRepository;
        public TaxService() 
        { 
            taxRepository = new TaxRepository();
        }

        //for testing purpose
        public TaxService(ITaxRepository taxRepository)
        {
            this.taxRepository = taxRepository;
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
