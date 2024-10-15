﻿using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    public interface ITaxService
    {
        decimal CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        IEnumerable<Tax> GetTaxesForEmployee(int employeeId);
        IEnumerable<Tax> GetTaxesForYear(int taxYear);
    }
}
