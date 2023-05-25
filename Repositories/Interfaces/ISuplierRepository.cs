﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface ISuplierRepository
{
    IEnumerable<Supplier> GetAll();
    Supplier GetSupplierById(int id);
}
