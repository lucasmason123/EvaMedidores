﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaMedidoresModel 
{
    public interface IMedidorDAL
    {
        void AgregarMedidor(Medidor medidor);
        List<Medidor> ObtenerMedidor();
    }
}
